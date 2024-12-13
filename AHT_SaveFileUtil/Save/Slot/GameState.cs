using AHT_SaveFileUtil.Common;
using Common;
using Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil.Save.Slot
{
    public enum TaskStates
    {
        Undiscovered = 0x0,
        Found = 0x1,
        ClearedButHidden = 0x2,
        FoundAndCleared = 0x3
    }

    public class GameState
    {
        public uint Version { get; private set; }

        private int VersionValidFlag;

        public bool VersionValid => VersionValidFlag != 0;

        public Map StartingMap { get; private set; }

        public uint Flags { get; private set; }

        public int NumTrigInfo { get; private set; }

        public GameStateTrigInfo[] TrigInfo { get; private set; }

        public Players CheatsPlayerType { get; private set; }

        public XAppTime StartTime { get; private set; }

        public float PlayTimer { get; private set; }

        public float TimeoutTimer { get; private set; }

        public PlayerState PlayerState { get; private set; }

        public uint[] Objectives { get; private set; } = new uint[16];

        public uint[] Tasks { get; private set; } = new uint[5];



        private GameState() { }

        public static GameState FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;
            
            var state = new GameState();

            state.Version = reader.ReadUInt32(bigEndian);
            state.VersionValidFlag = reader.ReadInt32(bigEndian);

            int startingMap = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(Map), startingMap))
                throw new IOException($"Invalid starting map value: {startingMap}");

            state.StartingMap = (Map)startingMap;

            state.Flags = reader.ReadUInt32(bigEndian);

            state.NumTrigInfo = reader.ReadInt32(bigEndian);
            if (state.NumTrigInfo < 0 || state.NumTrigInfo > 256)
            {
                throw new IOException($"Invalid number of TrigInfo: {state.NumTrigInfo}");
            }

            state.TrigInfo = new GameStateTrigInfo[state.NumTrigInfo];
            for (int i = 0; i < state.TrigInfo.Length; i++)
            {
                state.TrigInfo[i] = GameStateTrigInfo.FromReader(reader, platform);
            }

            if (state.NumTrigInfo < 256)
            {
                reader.BaseStream.Seek(
                    (256 - state.NumTrigInfo) * 0x20,
                    SeekOrigin.Current);
            }

            int cheatsPlayerType = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(Players), cheatsPlayerType))
                throw new IOException($"Invalid CheatsPlayerType value: {cheatsPlayerType}");

            state.CheatsPlayerType = (Players)cheatsPlayerType;

            state.StartTime = XAppTime.FromReader(reader, platform);

            state.PlayTimer = reader.ReadSingle(bigEndian);

            state.TimeoutTimer = reader.ReadSingle(bigEndian);

            state.PlayerState = PlayerState.FromReader(reader, platform);

            for(int i = 0; i < state.Objectives.Length; i++)
                state.Objectives[i] = reader.ReadUInt32(bigEndian);

            for (int i = 0; i < state.Tasks.Length; i++)
                state.Tasks[i] = reader.ReadUInt32(bigEndian);

            return state;
        }

        //OBJECTIVES

        public bool GetObjective(EXHashCode objectiveHash)
        {
            if (!ObjectiveIntoIndexAndBit(objectiveHash, out int index, out int bit))
                return false;

            return (Objectives[index] & (1 << bit)) != 0;
        }

        public bool SetObjective(EXHashCode objectiveHash, bool value)
        {
            if (!ObjectiveIntoIndexAndBit(objectiveHash, out int index, out int bit))
                return false;

            Objectives[index] &= ~((uint)(value ? 0 : 1) << bit);
            return true;
        }

        private static bool ObjectiveIntoIndexAndBit(EXHashCode objectiveHash, out int index, out int bit)
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

        //TASKS

        public TaskStates GetTaskState(EXHashCode taskHash)
        {
            if (!TaskIntoIndexAndBit(taskHash, out int index, out int bit))
                return TaskStates.Undiscovered;

            uint valueMask = ((uint)1 << bit) | ((uint)1 << (bit + 1));

            return (TaskStates)((Tasks[index] & valueMask) >> bit);
        }

        public bool SetTaskState(EXHashCode taskHash, TaskStates value)
        {
            if (!TaskIntoIndexAndBit(taskHash, out int index, out int bit))
                return false;

            uint mask = (uint)3 << bit;
            uint t = Tasks[index] & ~mask;

            Tasks[index] = t | ((uint)value << bit);

            return true;
        }

        private static bool TaskIntoIndexAndBit(EXHashCode taskHash, out int index, out int bit)
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
    }
}
