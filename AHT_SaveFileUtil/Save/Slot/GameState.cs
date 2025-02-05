using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using AHT_SaveFileUtil.Save.MiniMap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AHT_SaveFileUtil.Save.Slot
{
    public enum TaskStates
    {
        Undiscovered = 0b00,
        Found        = 0b01,
        Done         = 0b10,
        FoundAndDone = 0b11
    }

    public class GameState : ISaveFileIO<GameState>
    {
        public GamePlatform Platform { get; private set; }

        #region Variables
        /// <summary>
        /// Maximum number of objectives supported.
        /// </summary>
        public const int MAX_NUM_OBJECTIVES = 0x200;

        /// <summary>
        /// Maximum number of tasks supported.
        /// </summary>
        public const int MAX_NUM_TASKS = 0x50;

        /// <summary>
        /// Maximum number of <see cref="GameStateTrigInfo"/> supported.
        /// </summary>
        public const int MAX_NUM_TRIG_INFO = 256;

        /// <summary>
        /// A checksum-like value calculated from various static
        /// variables related to the game's state.
        /// </summary>
        public uint Version { get; private set; }

        private int VersionValidFlag;

        /// <summary>
        /// A flag denoting if the <see cref="Version"/> value is valid.
        /// </summary>
        public bool VersionValid => VersionValidFlag != 0;

        /// <summary>
        /// The map the game will start on when the slot is loaded.
        /// Set to <see cref="Map.Dragon_Village"/> on a new game.
        /// </summary>
        public Map StartingMap { get; set; }

        /// <summary>
        /// A set of flags.
        /// Only used flag is 0x1, which determines if the <see cref="TimeoutTimer"/> should be set
        /// when the pause menu is closed. This is unused functionality.
        /// </summary>
        public uint Flags { get; private set; }

        /// <summary>
        /// Amount of <see cref="GameStateTrigInfo"/> in use in <see cref="TrigInfo"/>.
        /// </summary>
        public int NumTrigInfo { get; private set; }

        /// <summary>
        /// A fixed-size table of <see cref="GameStateTrigInfo"/>, used
        /// to persistently store the properties of specific trigger types.
        /// </summary>
        public GameStateTrigInfo[] TrigInfo { get; private set; }
            = new GameStateTrigInfo[MAX_NUM_TRIG_INFO];

        /// <summary>
        /// Unused in the code, purpose unknown.
        /// </summary>
        public Players CheatsPlayerType { get; private set; }

        /// <summary>
        /// The timestamp of the moment the save file was started.
        /// </summary>
        public XAppTime StartTime { get; private set; }

        /// <summary>
        /// A timer that counts up by 1 every second the game is running.
        /// </summary>
        public float PlayTimer { get; set; }

        /// <summary>
        /// The <see cref="PlayTimer"/>  converted to minutes.
        /// </summary>
        public int PlayTimerMinutes => (int)PlayTimer / 60;

        /// <summary>
        /// The <see cref="PlayTimer"/>  converted to hours.
        /// </summary>
        public int PlayTimerHours => (int)PlayTimer / (60*60);

        /// <summary>
        /// A string of the <see cref="PlayTimer"/> with the format H:MM:SS.
        /// </summary>
        public string PlayTimerString
            => $"{PlayTimerHours}:{PlayTimerMinutes % 60:00}:{(int)PlayTimer % 60:00}";

        /// <summary>
        /// If <see cref="Flags"/> has 0x1 set, ticks down every frame until it hits 0,
        /// which then boots the player back to the title screen. This is unused functionality.
        /// </summary>
        public float TimeoutTimer { get; private set; }

        /// <summary>
        /// The current state of the player and their collectables.
        /// </summary>
        public PlayerState PlayerState { get; private set; }

        /// <summary>
        /// <para>
        /// A table of sets of flags containing the state of objectives.
        /// </para>
        /// <para>
        /// An <b>objective</b> is a persistent flag pertaining to player progression.
        /// An example might be whether a door has been opened, or if a boss has
        /// been defeated.
        /// </para>
        /// </summary>
        public uint[] Objectives { get; private set; } = new uint[16];

        /// <summary>
        /// <para>
        /// A table of sets of flags containing the state of tasks.
        /// </para>
        /// <para>
        /// A <b>task</b> refers to an entry in the in-game task list. Every task has two flags
        /// associated with it, denoting whether it's found and whether the player has
        /// completed the task respectively.
        /// </para>
        /// </summary>
        public uint[] Tasks { get; private set; } = new uint[5];

        /// <summary>
        /// A set of flags that denote whether items are available in the shop.
        /// </summary>
        public uint ShopAvailableFlags { get; private set; }

        /// <summary>
        /// The <see cref="BitHeap"/> containing various information about the game's state.
        /// <para>
        /// In the final game code, the heap is used for (in allocated order):
        /// <list type="bullet">
        /// <item>The filled-out parts of every minimap.</item>
        /// <item>Which minimaps are viewable and selectable.</item>
        /// <item>The preserved state of triggers in maps.</item>
        /// </list>
        /// </para>
        /// </summary>
        public BitHeap BitHeap { get; private set; }

        /// <summary>
        /// A table of <see cref="MapGameState"/>, storing the general state of every map.
        /// The index correlates with the enum <see cref="Map"/>.
        /// </summary>
        public MapGameState[] MapStates { get; private set; } = new MapGameState[200];

        /// <summary>
        /// The percentage of the game completed, according to the in-game status screen.
        /// <para>
        /// Calculated as such:
        /// <code>
        /// (
        ///     Total Dragon Eggs +
        ///     Total Light Gems +
        ///     Total Dark Gems +
        ///     1 (if objective HT_Objective_Boss4_Beaten is set)
        /// )
        /// / 221 * 100
        /// </code>
        /// </para>
        /// </summary>
        public float CompletionPercentage
        {
            get
            {
                if (PlayerState == null) return 0f;

                int tally =
                    PlayerState.TotalDragonEggs +
                    PlayerState.TotalDarkGems +
                    PlayerState.TotalLightGems;

                if (GetObjective(EXHashCode.HT_Objective_Boss4_Beaten))
                    tally += 1;

                // tally = 221 if the game is 100% complete

                return (tally / 221f) * 100f;
            }
        }
        #endregion

        private GameState() { }

        public static GameState FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;
            
            var state = new GameState() { Platform = platform };

            state.Version = reader.ReadUInt32(bigEndian);
            state.VersionValidFlag = reader.ReadInt32(bigEndian);

            int startingMap = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(Map), startingMap))
                throw new IOException($"Invalid starting map value: {startingMap}");

            state.StartingMap = (Map)startingMap;

            state.Flags = reader.ReadUInt32(bigEndian);

            state.NumTrigInfo = reader.ReadInt32(bigEndian);
            if (state.NumTrigInfo < 0 || state.NumTrigInfo > MAX_NUM_TRIG_INFO)
                throw new IOException($"Invalid number of TrigInfo: {state.NumTrigInfo}");

            for (int i = 0; i < state.NumTrigInfo; i++)
                state.TrigInfo[i] = GameStateTrigInfo.FromReader(reader, platform);

            if (state.NumTrigInfo < MAX_NUM_TRIG_INFO)
                reader.BaseStream.Seek(
                    (MAX_NUM_TRIG_INFO - state.NumTrigInfo) * 0x20,
                    SeekOrigin.Current);

            int cheatsPlayerType = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(Players), cheatsPlayerType))
                throw new IOException($"Invalid CheatsPlayerType value: {cheatsPlayerType}");

            state.CheatsPlayerType = (Players)cheatsPlayerType;

            state.StartTime = XAppTime.FromReader(reader, platform);

            state.PlayTimer = reader.ReadSingle(bigEndian);

            state.TimeoutTimer = reader.ReadSingle(bigEndian);

            if (platform == GamePlatform.PlayStation2)
                reader.BaseStream.Seek(8, SeekOrigin.Current);

            state.PlayerState = PlayerState.FromReader(reader, platform);

            if (platform == GamePlatform.PlayStation2)
                reader.BaseStream.Seek(8, SeekOrigin.Current);

            for(int i = 0; i < state.Objectives.Length; i++)
                state.Objectives[i] = reader.ReadUInt32(bigEndian);

            for (int i = 0; i < state.Tasks.Length; i++)
                state.Tasks[i] = reader.ReadUInt32(bigEndian);

            reader.BaseStream.Seek(4, SeekOrigin.Current);

            state.ShopAvailableFlags = reader.ReadUInt32(bigEndian);

            state.BitHeap = BitHeap.FromReader(reader, platform);

            for (int i = 0; i < state.MapStates.Length; i++)
                state.MapStates[i] = MapGameState.FromReader(reader, platform);

            reader.BaseStream.Seek(4, SeekOrigin.Current);

            return state;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            writer.Write(Version, bigEndian);
            writer.Write(VersionValidFlag, bigEndian);
            writer.Write((int)StartingMap, bigEndian);
            writer.Write(Flags, bigEndian);
            writer.Write(NumTrigInfo, bigEndian);

            for (int i = 0; i < NumTrigInfo; i++)
                TrigInfo[i].ToWriter(writer, platform);

            if (NumTrigInfo < MAX_NUM_TRIG_INFO)
                writer.BaseStream.Seek(
                    (MAX_NUM_TRIG_INFO - NumTrigInfo) * 0x20,
                    SeekOrigin.Current);

            writer.Write((int)CheatsPlayerType, bigEndian);
            StartTime.ToWriter(writer, platform);
            writer.Write(PlayTimer, bigEndian);
            writer.Write(TimeoutTimer, bigEndian);

            if (platform == GamePlatform.PlayStation2)
                writer.BaseStream.Seek(8, SeekOrigin.Current);

            PlayerState.ToWriter(writer, platform);

            if (platform == GamePlatform.PlayStation2)
                writer.BaseStream.Seek(8, SeekOrigin.Current);

            foreach (var obj in Objectives)
                writer.Write(obj, bigEndian);

            foreach (var tsk in Tasks)
                writer.Write(tsk, bigEndian);

            writer.BaseStream.Seek(4, SeekOrigin.Current);

            writer.Write(ShopAvailableFlags, bigEndian);
            BitHeap.ToWriter(writer, platform);

            foreach(var state in MapStates)
                state.ToWriter(writer, platform);

            writer.BaseStream.Seek(4, SeekOrigin.Current);
        }

        /// <summary>
        /// Set the <see cref="PlayTimer"/> to a certain number of hours, minutes and seconds.
        /// </summary>
        /// <param name="hours">The amount of hours.</param>
        /// <param name="minutes">The amount of minutes (between 0 and 59).</param>
        /// <param name="seconds">The amount of seconds (between 0 and 59).</param>
        /// <exception cref="ArgumentException"></exception>
        public void SetPlayTimer(int hours, int minutes, int seconds)
        {
            if (hours < 0)
                throw new ArgumentException(nameof(hours) + " cannot be negative.");
            if (minutes < 0)
                throw new ArgumentException(nameof(minutes) + " cannot be negative.");
            if (seconds < 0)
                throw new ArgumentException(nameof(seconds) + " cannot be negative.");
            if (minutes > 59)
                throw new ArgumentException(nameof(minutes) + " cannot exceed 59.");
            if (seconds > 59)
                throw new ArgumentException(nameof(seconds) + " cannot exceed 59.");

            PlayTimer = (hours * 60*60) + (minutes * 60) + seconds;
        }

        public MapGameState GetMapGameState(Map mapIndex, GamePlatform platform)
        {
            if (platform == GamePlatform.PlayStation2)
            {
                mapIndex -= 2;
            }

            return MapStates[(int)mapIndex];
        }

        internal void SetMapStatesFromSheet()
        {
            uint[][] sheet = PlayerState.DataSheet_GameInfo_0;
            
            foreach (uint[] row in sheet)
            {
                foreach(var pair in MapData.MapDataList)
                {
                    if (row[0] == pair.Value.FileHash)
                    {
                        var state = GetMapGameState(pair.Key, Platform);

                        state.MaxDarkGems   = (int)row[1];
                        state.MaxDragonEggs = (int)row[2];
                        state.MaxLightGems  = (int)row[3];

                        break;
                    }
                }
            }
        }

        public void Clear()
        {
            //General
            CheatsPlayerType = Players.None;
            Flags = 0;
            TimeoutTimer = 0;
            PlayTimer = 0;

            //Trig info
            NumTrigInfo = 0;
            for (int i = 0; i < TrigInfo.Length; i++)
                TrigInfo[i] = null;

            //BitHeap
            BitHeap.ClearAll();
            BitHeap.EmptyStack();

            //Map states
            foreach(var mapState in MapStates)
                mapState.Reset();

            SetMapStatesFromSheet();

            //Player state
            PlayerState.Reset();

            //Objectives/Tasks
            for (int i = 0; i < Objectives.Length; i++)
                Objectives[i] = 0;

            for (int i = 0; i < Tasks.Length; i++)
                Tasks[i] = 0;

            //TODO: probably figure out the shop flags here...
            ShopAvailableFlags = 0;
        }

        public void AllocateMinimaps(MiniMaps miniMaps)
        {
            BitHeap.Allocate(miniMaps.MiniMaps_TotalBitheapSize);
            BitHeap.Allocate(miniMaps.MiniMapStatus_TotalBitHeapSize);
        }

        #region Objectives
        /// <summary>
        /// Get the state of an objective.
        /// </summary>
        /// <param name="objectiveHash">The hashcode of the objective.</param>
        /// <returns>true if the objective is set, false if not.</returns>
        public bool GetObjective(EXHashCode objectiveHash)
        {
            if (!ObjectiveToIndexAndBit(objectiveHash, out int index, out int bit))
                return false;

            return (Objectives[index] & (1 << bit)) != 0;
        }

        /// <summary>
        /// Set the state of an objective.
        /// </summary>
        /// <param name="objectiveHash">The hashcode of the objective.</param>
        /// <param name="value">The value to set the objective's state to.</param>
        /// <returns>true if the hashcode points to a valid objective, false if not.</returns>
        public bool SetObjective(EXHashCode objectiveHash, bool value)
        {
            if (!ObjectiveToIndexAndBit(objectiveHash, out int index, out int bit))
                return false;

            if (value)
                Objectives[index] |= (uint)(1 << bit);
            else
                Objectives[index] &= ~(uint)(1 << bit);

            return true;
        }

        /// <summary>
        /// Set the state of every objective in the game.
        /// </summary>
        /// <param name="set">The value to set every objective's state to.</param>
        public void SetAllObjectives(bool set)
        {
            for (uint hash = (uint)EXHashCode.HT_Objective_HASHCODE_BASE + 1;
                hash < (uint)EXHashCode.HT_Objective_HASHCODE_END;
                hash++)
            {
                if (Enum.IsDefined(typeof(EXHashCode), hash))
                    SetObjective((EXHashCode)hash, set);
            }
        }

        /// <summary>
        /// Get the table index and bit of an objective's state.
        /// </summary>
        /// <param name="objectiveHash">The hashcode of the objective.</param>
        /// <param name="index">The index into the <see cref="Objectives"/> array.</param>
        /// <param name="bit">The bit of the value indexed by <paramref name="index"/>.</param>
        /// <returns>true if the hashcode corresponds to a valid objective, false if not.</returns>
        private static bool ObjectiveToIndexAndBit(EXHashCode objectiveHash, out int index, out int bit)
        {
            index = 0;
            bit = 0;

            uint hashValue = (uint)objectiveHash;

            //Check if valid objective hashcode
            if ((hashValue & 0xFF000000) != (uint)EXHashCode.HT_Objective_HASHCODE_BASE)
                return false;

            uint objective = (hashValue & 0xFFFFFF) - 1;

            //Check if in proper range
            if (objective >= 0x200)
                return false;

            index = (int)objective / 32;
            bit = (int)objective & 0x1f;

            return true;
        }
        #endregion

        #region Tasks
        /// <summary>
        /// Get the state of a task.
        /// </summary>
        /// <param name="taskHash">The hashcode of the task.</param>
        /// <returns>The state of the task.</returns>
        public TaskStates GetTaskState(EXHashCode taskHash)
        {
            if (!TaskToIndexAndBit(taskHash, out int index, out int bit))
                return TaskStates.Undiscovered;

            uint valueMask = ((uint)1 << bit) | ((uint)1 << (bit + 1));

            return (TaskStates)((Tasks[index] & valueMask) >> bit);
        }

        /// <summary>
        /// Set the state of a task.
        /// </summary>
        /// <param name="taskHash">The hashcode of the task.</param>
        /// <param name="value">The value to set the task to.</param>
        /// <returns>true if the hashcode corresponds to a valid task, false if not.</returns>
        public bool SetTaskState(EXHashCode taskHash, TaskStates value)
        {
            if (!TaskToIndexAndBit(taskHash, out int index, out int bit))
                return false;

            uint mask = (uint)3 << bit;
            uint t = Tasks[index] & ~mask;

            Tasks[index] = t | ((uint)value << bit);

            return true;
        }

        /// <summary>
        /// Get the table index and bit of an task's state.
        /// </summary>
        /// <param name="taskHash">The hashcode of the task.</param>
        /// <param name="index">The index into the <see cref="Tasks"/> array.</param>
        /// <param name="bit">The bit of the value indexed by <paramref name="index"/>.</param>
        /// <returns>true if the hashcode corresponds to a valid task, false if not.</returns>
        private static bool TaskToIndexAndBit(EXHashCode taskHash, out int index, out int bit)
        {
            index = 0;
            bit = 0;

            uint hashValue = (uint)taskHash;

            //Check if valid task hashcode
            if ((hashValue & 0xFF000000) != (uint)EXHashCode.HT_Tasks_NONE)
                return false;

            uint task = ((hashValue & 0xFFFFFF) - 1) * 2;

            //Check if in proper range
            if (task >= 0xA0)
                return false;

            index = (int)task / 32;
            bit = (int)task & 0x1F;

            return true;
        }
        #endregion

        #region Trigger Info
        /// <summary>
        /// Get an array of <see cref="GameStateTrigInfo"/> that belong to a map.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <returns>An array of <see cref="GameStateTrigInfo"/> that belong to <paramref name="map"/></returns>
        public GameStateTrigInfo[] GetMapTrigInfo(Map map)
        {
            List<GameStateTrigInfo> list = [];

            //Scoop up the triginfo that have the same map index
            for (int i = 0; i < NumTrigInfo; i++)
                if (TrigInfo[i].MapIndex == (short)map)
                    list.Add(TrigInfo[i]);

            return list.ToArray();
        }

        /// <summary>
        /// Add a new <see cref="TrigInfo_RestartPoint"/> to <see cref="TrigInfo"/>
        /// if there is space for it in the array.
        /// </summary>
        /// <param name="map">Value of <see cref="GameStateTrigInfo.MapIndex"/></param>
        /// <param name="trigIndex">Value of <see cref="GameStateTrigInfo.TrigIndex"/></param>
        /// <param name="XYZ">Value of <see cref="GameStateTrigInfo.XYZ"/></param>
        /// <param name="hashCode">Value of <see cref="TrigInfo_RestartPoint.HashCode"/></param>
        /// <param name="textHashCode">Value of <see cref="TrigInfo_RestartPoint.NameTextHashCode"/></param>
        /// <param name="visited">Value of <see cref="TrigInfo_RestartPoint.HasVisited"/></param>
        /// <returns>true if the info could be added, false if not.</returns>
        public bool AddStartPointTrigInfo(
            Map map, int trigIndex, EXVector3 XYZ, uint hashCode, uint textHashCode, bool visited)
        {
            if (NumTrigInfo >= MAX_NUM_TRIG_INFO)
                return false;

            GameStateTrigInfo info = new()
            {
                MapIndex = (short)map,
                TrigIndex = (short)trigIndex,
                XYZ = new EXVector3(XYZ),
                Type = TrigInfoType.RestartPoint,
                Data = new TrigInfo_RestartPoint()
                {
                    HashCode = hashCode,
                    NameTextHashCode = textHashCode,
                    HasVisited = visited
                }
            };

            return AddPointTrigInfo(info);
        }

        /// <summary>
        /// Add a new <see cref="GameStateTrigInfo"/> to <see cref="TrigInfo"/>
        /// if there is space for it in the array.
        /// </summary>
        /// <param name="info">The info to add.</param>
        /// <returns>true if the info could be added, false if not.</returns>
        public bool AddPointTrigInfo(GameStateTrigInfo info)
        {
            if (NumTrigInfo >= MAX_NUM_TRIG_INFO)
                return false;

            TrigInfo[NumTrigInfo] = info;
            NumTrigInfo++;

            return true;
        }

        public bool RemoveTrigInfo(Map map, int trigIndex)
        {
            int index = FindTrigInfo(map, trigIndex);
            if (index == -1) return false;

            return RemoveTrigInfo(index);
        }

        /// <summary>
        /// Remove a <see cref="GameStateTrigInfo"/> from <see cref="TrigInfo"/>
        /// at the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Index of the info to remove.</param>
        /// <returns>true if the index corresponds to a valid entry, false if not.</returns>
        public bool RemoveTrigInfo(int index)
        {
            if (!TrigInfoIndexValid(index))
                return false;

            if (index >= NumTrigInfo)
                return false;

            //Shift everything from this index onward back one index
            for (int i = index; i < MAX_NUM_TRIG_INFO - 1; i++)
            {
                TrigInfo[i] = TrigInfo[i + 1];

                //Check if we've reached the end of the used triginfo.
                if (TrigInfo[i + 1] == null)
                    break;
            }

            NumTrigInfo--;

            return true;
        }

        /// <summary>
        /// Find the index of the <see cref="GameStateTrigInfo"/> with the given
        /// map and trigger index.
        /// </summary>
        /// <param name="map">Value to match with <see cref="GameStateTrigInfo.MapIndex"/>.</param>
        /// <param name="trigIndex">Value to match with <see cref="GameStateTrigInfo.TrigIndex"/>.</param>
        /// <returns>The index into <see cref="TrigInfo"/>
        /// of the found <see cref="GameStateTrigInfo"/>, or -1 if it could not be found.</returns>
        public int FindTrigInfo(Map map, int trigIndex)
        {
            for (int i = 0; i < NumTrigInfo; i++)
                if ((TrigInfo[i].MapIndex  == (short)map) &&
                    (TrigInfo[i].TrigIndex == (short)trigIndex))
                    return i;

            return -1;
        }

        /// <summary>
        /// Check if an index into <see cref="TrigInfo"/> is within the bounds of the array.
        /// </summary>
        /// <param name="index">Index to check against.</param>
        /// <returns>true if the index is within the bounds of the <see cref="TrigInfo"/> array, false if not.</returns>
        public static bool TrigInfoIndexValid(int index)
        {
            return (index >= 0) && (index < MAX_NUM_TRIG_INFO);
        }
        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(string.Format("Started: {0}-{1}-{2} {3}:{4:00}:{5:00}",
                StartTime.Year, StartTime.Month, StartTime.Day,
                StartTime.Hours, StartTime.Minutes, StartTime.Seconds));

            sb.AppendLine(string.Format("Played: {0}:{1:00}:{2:00}",
                (int)PlayTimer / (60*60),
                ((int)PlayTimer / 60) % 60,
                (int)PlayTimer % 60
                ));

            sb.AppendLine($"Dark Gems: {PlayerState.TotalDarkGems}");
            sb.AppendLine($"Light Gems: {PlayerState.TotalLightGems}");
            sb.AppendLine($"Dragon Eggs: {PlayerState.TotalDragonEggs}");

            sb.AppendLine($"Completed: {CompletionPercentage:0.00}%");

            sb.AppendLine($"Character: {PlayerState.Setup.Player}");
            sb.AppendLine($"Level: {StartingMap}");

            return sb.ToString();
        }
    }
}
