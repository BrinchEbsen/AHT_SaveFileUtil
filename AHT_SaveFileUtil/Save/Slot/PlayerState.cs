using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace AHT_SaveFileUtil.Save.Slot
{
    #region Enums
    public enum BreathType
    {
        None     = 0x0,
        Fire     = 0x1,
        Water    = 0x2,
        Electric = 0x4,
        Ice      = 0x8
    }

    public enum MiniGameID
    {
        Undefined = 0,
        NoMiniGame = 1,
        Cannon = 2,
        Turret = 3,
        Sparx1 = 4
    }

    public enum CamTypes
    {
        Base = 0,
        Follow = 1,
        Scan = 2,
        FirstPerson = 3,
        PoleGrab = 4,
        Fixed = 5,
        PathFollow = 6,
        PathDrag = 7,
        Cutscene = 8,
        Fall = 9,
        CannonCam = 10,
        BallGadgetFollow = 11,
        NpcDialog = 12,
        Boss = 13,
        PreviousCam = 14,
        Tunnel = 15,
        Rocket = 16
    }

    public enum CamCreateMode
    {
        SetPos = 0,
        ForcePos = 1,
        ClonePos = 2
    }
    #endregion

    /// <summary>
    /// A 4-byte structure with info for a collectable.
    /// </summary>
    public class PowerupTally : ISaveFileIO<PowerupTally>
    {
        /// <summary>
        /// The number of items.
        /// </summary>
        public sbyte Amount { get; set; }
        /// <summary>
        /// The maximum number of the item the user can hold.
        /// </summary>
        public sbyte Max { get; set; }
        /// <summary>
        /// The maximum number of the item the user can hold with all magazines purchased.
        /// </summary>
        public sbyte Total { get; set; }
        /// <summary>
        /// The number of magazines purchased.
        /// </summary>
        public sbyte Magazines { get; set; }

        public void Clear()
        {
            Amount = 0;
            Max = 0;
            Total = 0;
            Magazines = 0;
        }

        public static PowerupTally FromReader(BinaryReader reader, GamePlatform platform)
        {
            return new PowerupTally
            {
                Amount = reader.ReadSByte(),
                Max = reader.ReadSByte(),
                Total = reader.ReadSByte(),
                Magazines = reader.ReadSByte()
            };
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            writer.Write(Amount);
            writer.Write(Max);
            writer.Write(Total);
            writer.Write(Magazines);
        }
    }

    /// <summary>
    /// Defines how the player will be set up on a map loading in.
    /// There isn't usually a need to edit this structure, as it's overwritten
    /// by the map when it loads.
    /// </summary>
    public struct PlayerSetupInfo : ISaveFileIO<PlayerSetupInfo>
    {
        public EXVector Position;
        
        public EXVector Rotation;

        public Players Player;

        public MiniGameID MiniGameID;

        public Map MapListIndex;

        public CamTypes CamType;

        public CamCreateMode CamCreateMode;

        public uint Flags;

        public uint[] DataHashcodes;

        public float[] DataR32;

        public int[] DataS32;

        public uint[] DataU32;

        public static PlayerSetupInfo FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            var info = new PlayerSetupInfo();

            info.Position = new EXVector
            {
                X = reader.ReadSingle(bigEndian),
                Y = reader.ReadSingle(bigEndian),
                Z = reader.ReadSingle(bigEndian),
                W = reader.ReadSingle(bigEndian)
            };

            info.Rotation = new EXVector
            {
                X = reader.ReadSingle(bigEndian),
                Y = reader.ReadSingle(bigEndian),
                Z = reader.ReadSingle(bigEndian),
                W = reader.ReadSingle(bigEndian)
            };

            int player = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(Players), player))
                throw new IOException($"Invalid value for Player: {player}");

            info.Player = (Players)player;

            int minigameID = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(MiniGameID), minigameID))
                throw new IOException($"Invalid value for MiniGameID: {minigameID}");

            info.MiniGameID = (MiniGameID)minigameID;

            int mapIndex = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(Map), mapIndex))
                throw new IOException($"Invalid value for MapListIndex: {mapIndex}");

            info.MapListIndex = (Map)mapIndex;

            int camType = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(CamTypes), camType))
                throw new IOException($"Invalid value for CamType: {camType}");

            info.CamType = (CamTypes)camType;

            int camCreateMode = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(CamCreateMode), camCreateMode))
                throw new IOException($"Invalid value for CamCreateMode: {camCreateMode}");

            info.CamCreateMode = (CamCreateMode)camCreateMode;

            info.Flags = reader.ReadUInt32(bigEndian);

            info.DataHashcodes = new uint[8];
            for (int i = 0; i < 8; i++)
                info.DataHashcodes[i] = reader.ReadUInt32(bigEndian);

            info.DataR32 = new float[8];
            for (int i = 0; i < 8; i++)
                info.DataR32[i] = reader.ReadSingle(bigEndian);

            info.DataS32 = new int[8];
            for (int i = 0; i < 8; i++)
                info.DataS32[i] = reader.ReadInt32(bigEndian);

            info.DataU32 = new uint[8];
            for (int i = 0; i < 8; i++)
                info.DataU32[i] = reader.ReadUInt32(bigEndian);

            reader.BaseStream.Seek(8, SeekOrigin.Current);

            return info;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            writer.Write(Position.X, bigEndian);
            writer.Write(Position.Y, bigEndian);
            writer.Write(Position.Z, bigEndian);
            writer.Write(Position.W, bigEndian);

            writer.Write(Rotation.X, bigEndian);
            writer.Write(Rotation.Y, bigEndian);
            writer.Write(Rotation.Z, bigEndian);
            writer.Write(Rotation.W, bigEndian);

            writer.Write((int)Player, bigEndian);
            writer.Write((int)MiniGameID, bigEndian);
            writer.Write((int)MapListIndex, bigEndian);
            writer.Write((int)CamType, bigEndian);
            writer.Write((int)CamCreateMode, bigEndian);
            writer.Write(Flags, bigEndian);

            foreach (var hash in DataHashcodes)
                writer.Write(hash, bigEndian);

            foreach (var val in DataR32)
                writer.Write(val, bigEndian);

            foreach (var val in DataS32)
                writer.Write(val, bigEndian);

            foreach (var val in DataU32)
                writer.Write(val, bigEndian);

            writer.BaseStream.Seek(8, SeekOrigin.Current);
        }
    }

    /// <summary>
    /// The current state of the player and their collectables, among other things.
    /// </summary>
    public class PlayerState : ISaveFileIO<PlayerState>
    {
        #region Data Sheets

        /// <summary>
        /// <para>
        /// Hardcoded array with values from the first data sheet in
        /// HT_SpreadSheet_GameInfo from the file HT_File_GameInfo.
        /// </para>
        /// <para>
        /// Contains the maximum collectable tallies for each level file.
        /// </para>
        /// </summary>
        internal static readonly uint[][] DataSheet_GameInfo_0 = [
        /*                      HashCode,   Max Dark Gems, Max Dragon Eggs, Max Light Gems */
        /* HT_File_Realm1A */ [ 0x01000037, 3,             6,               6              ],
        /* HT_File_Realm1B */ [ 0x01000039, 3,             10,              8              ],
        /* HT_File_Realm1C */ [ 0x0100003A, 4,             9,               6              ],
        /* HT_File_Realm2A */ [ 0x0100003B, 4,             9,               7              ],
        /* HT_File_Realm2B */ [ 0x0100005A, 3,             8,               6              ],
        /* HT_File_Realm2C */ [ 0x0100005B, 3,             8,               7              ],
        /* HT_File_Realm3A */ [ 0x01000038, 5,             9,               7              ],
        /* HT_File_Realm3B */ [ 0x0100005C, 0,             8,               6              ],
        /* HT_File_Realm3C */ [ 0x01000059, 5,             8,               7              ],
        /* HT_File_Realm4A */ [ 0x0100005D, 1,             2,               2              ],
        /* HT_File_Realm4B */ [ 0x0100005E, 3,             6,               5              ],
        /* HT_File_Realm4C */ [ 0x0100005F, 1,             6,               5              ],
        /* HT_File_Realm4D */ [ 0x010000A1, 2,             6,               5              ],
        /* HT_File_Realm4E */ [ 0x010000A2, 3,             5,               3              ]
        ];

        /// <summary>
        /// <para>
        /// Hardcoded array with values from the second data sheet in
        /// HT_SpreadSheet_GameInfo from the file HT_File_GameInfo.
        /// </para>
        /// <para>
        /// Contains the savefile startup values for several player state stats.
        /// </para>
        /// </summary>
        internal static readonly sbyte[][] DataSheet_GameInfo_1 = [
        /*                       Element Flags,  Data 1,  Data 2,  Data 3,  Data 4,  Data 5 */
        /* Flame Bombs      */ [ 0x1,            0,       0,       10,      50,      0      ],
        /* Ice Bombs        */ [ 0x2,            0,       0,       4,       20,      0      ],
        /* Water Bombs      */ [ 0x4,            0,       0,       20,      100,     0      ],
        /* Electric Bombs   */ [ 0x8,            0,       0,       2,       10,      0      ],
        /* Fire Arrows      */ [ 0x10,           0,       0,       20,      20,      0      ],
        /* Lock Picks       */ [ 0x20,           0,       0,       1,       3,       0      ],
        /* Double Gem Timer */ [ 0x40,           0,       120,     0,       0,       0      ]
        ];

        #endregion

        #region Variables

        /// <summary>
        /// The currently selected breath.
        /// </summary>
        public BreathType CurrentBreath { get; private set; }

        #region Health

        /// <summary>
        /// The current health.
        /// <para>
        /// Full health is 0xA0 and decrements by 0x20 with every hit point lost.
        /// Without the health upgrade, the player dies at 0x20 health. With the upgrade, they die at 0x0.
        /// </para>
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Get whether <see cref="Health"/> is an intended value.
        /// </summary>
        public bool HealthIsValid =>
            (Health % 0x20 == 0) && (Health >= 0) && (Health <= 0xA0);

        /// <summary>
        /// Health values mapped to descriptive strings, for values without the health point upgrade.
        /// </summary>
        public static readonly Dictionary<int, string> HealthStrings_NoUpgrade = new()
        {
            { 0x0,  "No Health" },
            { 0x20, "No Health" },
            { 0x40, "No Sparx" },
            { 0x60, "Green Sparx" },
            { 0x80, "Blue Sparx" },
            { 0xA0, "Gold Sparx" }
        };

        /// <summary>
        /// Health values mapped to descriptive strings, for values with the health point upgrade.
        /// </summary>
        public static readonly Dictionary<int, string> HealthStrings_Upgrade = new()
        {
            { 0x0,  "No Health" },
            { 0x20, "No Sparx" },
            { 0x40, "Red Sparx" },
            { 0x60, "Green Sparx" },
            { 0x80, "Blue Sparx" },
            { 0xA0, "Gold Sparx" }
        };

        #endregion

        /// <summary>
        /// The current amount of gems.
        /// </summary>
        public int Gems { get; set; }

        /// <summary>
        /// The total amount of gems collected on this save.
        /// Never shown to the player.
        /// </summary>
        public int TotalGems { get; set; }

        /// <summary>
        /// Stats for lock-picks.
        /// </summary>
        public PowerupTally LockPickers { get; private set; }
        
        /// <summary>
        /// Stats for flame bombs.
        /// </summary>
        public PowerupTally FlameBombs { get; private set; }

        /// <summary>
        /// Stats for ice missiles.
        /// </summary>
        public PowerupTally IceBombs { get; private set; }

        /// <summary>
        /// Stats for water bombs.
        /// </summary>
        public PowerupTally WaterBombs { get; private set; }

        /// <summary>
        /// Stats for electric missiles.
        /// </summary>
        public PowerupTally ElectricBombs { get; private set; }

        /// <summary>
        /// The current amount of fire arrows.
        /// </summary>
        public short FireArrows { get; set; }

        /// <summary>
        /// The max amount of fire arrows.
        /// </summary>
        public short FireArrowsMax { get; set; }

        /// <summary>
        /// A set of flags for certain abilities/collectables.
        /// </summary>
        public uint AbilityFlags { get; private set; }

        #region Ability Flags Constants

        public const uint AF_MASK_DOUBLE_JUMP = 0x1;
        public const uint AF_MASK_HIT_POINT_UPGRADE = 0x4;
        public const uint AF_MASK_POLE_SPIN = 0x10;
        public const uint AF_MASK_ICE_BREATH = 0x20;
        public const uint AF_MASK_ELECTRIC_BREATH = 0x40;
        public const uint AF_MASK_WATER_BREATH = 0x80;
        public const uint AF_MASK_DOUBLE_GEM = 0x200;
        public const uint AF_MASK_AQUALUNG = 0x800;
        public const uint AF_MASK_SUPERCHARGE = 0x1000;
        public const uint AF_MASK_INVINCIBILITY = 0x2000;
        public const uint AF_MASK_BOUGHT_LOCK_PICK = 0x4000;
        public const uint AF_MASK_WING_SHIELD = 0x8000;
        public const uint AF_MASK_WALL_KICK = 0x10000;
        public const uint AF_MASK_HORN_DIVE_UPGRADE = 0x20000;
        public const uint AF_MASK_BUTTERFLY_JAR = 0x40000;

        public const uint ABILITY_FLAGS_MASK = 
            AF_MASK_DOUBLE_JUMP |
            AF_MASK_HIT_POINT_UPGRADE |
            AF_MASK_POLE_SPIN |
            AF_MASK_ICE_BREATH |
            AF_MASK_ELECTRIC_BREATH |
            AF_MASK_WATER_BREATH |
            AF_MASK_DOUBLE_GEM |
            AF_MASK_AQUALUNG |
            AF_MASK_SUPERCHARGE |
            AF_MASK_INVINCIBILITY |
            AF_MASK_BOUGHT_LOCK_PICK |
            AF_MASK_WING_SHIELD |
            AF_MASK_WALL_KICK |
            AF_MASK_HORN_DIVE_UPGRADE |
            AF_MASK_BUTTERFLY_JAR;

        public static readonly Dictionary<uint, string> AbilityFlagNames = new()
        {
            { AF_MASK_DOUBLE_JUMP, "Double Jump" },
            { AF_MASK_HIT_POINT_UPGRADE, "Hit Point Upgrade" },
            { AF_MASK_POLE_SPIN, "Pole Spin" },
            { AF_MASK_ICE_BREATH, "Ice Breath" },
            { AF_MASK_ELECTRIC_BREATH, "Electric Breath" },
            { AF_MASK_WATER_BREATH, "Water Breath" },
            { AF_MASK_DOUBLE_GEM, "Double Gem" },
            { AF_MASK_AQUALUNG, "Aqualung" },
            { AF_MASK_SUPERCHARGE, "Supercharge" },
            { AF_MASK_INVINCIBILITY, "Invincibility" },
            { AF_MASK_BOUGHT_LOCK_PICK, "Bought Lock Pick" },
            { AF_MASK_WING_SHIELD, "Wing Shield" },
            { AF_MASK_WALL_KICK, "Wall Kick" },
            { AF_MASK_HORN_DIVE_UPGRADE, "Horn Dive Upgrade" },
            { AF_MASK_BUTTERFLY_JAR, "Butterfly Jar" }
        };

        #endregion
        
        #region Ability Flags Properties

        /// <summary>
        /// Gets or sets whether Spyro can double jump/horn dive.
        /// </summary>
        public bool AF_DoubleJump
        {
            get => (AbilityFlags & AF_MASK_DOUBLE_JUMP) != 0;
            set => AbilityFlags = 
                value ? (AbilityFlags & AF_MASK_DOUBLE_JUMP) : (AbilityFlags & ~AF_MASK_DOUBLE_JUMP);
        }
        /// <summary>
        /// Gets or sets whether the player has the hit point upgrade.
        /// </summary>
        public bool AF_HitPointUpgrade
        {
            get => (AbilityFlags & AF_MASK_HIT_POINT_UPGRADE) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_HIT_POINT_UPGRADE) : (AbilityFlags & ~AF_MASK_HIT_POINT_UPGRADE);
        }
        /// <summary>
        /// Gets or sets whether Spyro can pole spin.
        /// </summary>
        public bool AF_PoleSpin
        {
            get => (AbilityFlags & AF_MASK_POLE_SPIN) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_POLE_SPIN) : (AbilityFlags & ~AF_MASK_POLE_SPIN);
        }
        /// <summary>
        /// Gets or sets whether Spyro can breathe ice.
        /// </summary>
        public bool AF_IceBreath
        {
            get => (AbilityFlags & AF_MASK_ICE_BREATH) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_ICE_BREATH) : (AbilityFlags & ~AF_MASK_ICE_BREATH);
        }
        /// <summary>
        /// Gets or sets whether Spyro can breathe electricity.
        /// </summary>
        public bool AF_ElectricBreath
        {
            get => (AbilityFlags & AF_MASK_ELECTRIC_BREATH) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_ELECTRIC_BREATH) : (AbilityFlags & ~AF_MASK_ELECTRIC_BREATH);
        }
        /// <summary>
        /// Gets or sets whether Spyro can breathe water.
        /// </summary>
        public bool AF_WaterBreath
        {
            get => (AbilityFlags & AF_MASK_WATER_BREATH) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_WATER_BREATH) : (AbilityFlags & ~AF_MASK_WATER_BREATH);
        }
        /// <summary>
        /// Gets or sets whether the 2x gem multiplier is active.
        /// </summary>
        public bool AF_DoubleGem
        {
            get => (AbilityFlags & AF_MASK_DOUBLE_GEM) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_DOUBLE_GEM) : (AbilityFlags & ~AF_MASK_DOUBLE_GEM);
        }
        /// <summary>
        /// Gets or sets whether aqualung is active (unused in the final game code).
        /// </summary>
        public bool AF_Aqualung // Unused
        {
            get => (AbilityFlags & AF_MASK_AQUALUNG) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_AQUALUNG) : (AbilityFlags & ~AF_MASK_AQUALUNG);
        }
        /// <summary>
        /// Gets or sets whether supercharge is active.
        /// </summary>
        public bool AF_Supercharge
        {
            get => (AbilityFlags & AF_MASK_SUPERCHARGE) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_SUPERCHARGE) : (AbilityFlags & ~AF_MASK_SUPERCHARGE);
        }
        /// <summary>
        /// Gets or sets whether invincibility is active.
        /// </summary>
        public bool AF_Invincibility
        {
            get => (AbilityFlags & AF_MASK_INVINCIBILITY) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_INVINCIBILITY) : (AbilityFlags & ~AF_MASK_INVINCIBILITY);
        }
        /// <summary>
        /// Gets or sets whether the player has bought a lock pick on this save.
        /// This is used to unlock the other shop items for purchase.
        /// </summary>
        public bool AF_BoughtLockPick
        {
            get => (AbilityFlags & AF_MASK_BOUGHT_LOCK_PICK) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_BOUGHT_LOCK_PICK) : (AbilityFlags & ~AF_MASK_BOUGHT_LOCK_PICK);
        }
        /// <summary>
        /// Gets or sets whether Spyro can wing shield.
        /// </summary>
        public bool AF_WingShield
        {
            get => (AbilityFlags & AF_MASK_WING_SHIELD) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_WING_SHIELD) : (AbilityFlags & ~AF_MASK_WING_SHIELD);
        }
        /// <summary>
        /// Gets or sets whether Spyro can wall kick.
        /// </summary>
        public bool AF_WallKick
        {
            get => (AbilityFlags & AF_MASK_WALL_KICK) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_WALL_KICK) : (AbilityFlags & ~AF_MASK_WALL_KICK);
        }
        /// <summary>
        /// Gets or sets whether Spyro has the horn dive upgrade.
        /// </summary>
        public bool AF_HornDiveUpgrade
        {
            get => (AbilityFlags & AF_MASK_HORN_DIVE_UPGRADE) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_HORN_DIVE_UPGRADE) : (AbilityFlags & ~AF_MASK_HORN_DIVE_UPGRADE);
        }
        /// <summary>
        /// Gets or sets whether the player has the butterfly jar.
        /// </summary>
        public bool AF_ButterflyJar
        {
            get => (AbilityFlags & AF_MASK_BUTTERFLY_JAR) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & AF_MASK_BUTTERFLY_JAR) : (AbilityFlags & ~AF_MASK_BUTTERFLY_JAR);
        }

        #endregion

        #region Timers

        /// <summary>
        /// The amount of time underwater before Spyro runs out of oxygen (unused in the final game code).
        /// </summary>
        public float WaterDiveTimer { get; set; }

        /// <summary>
        /// Amount of supercharge time left.
        /// </summary>
        public float SuperchargeTimer { get; set; }

        /// <summary>
        /// Current maximum for the supercharge gauge.
        /// </summary>
        public float SuperchargeTimerMax { get; set; }

        /// <summary>
        /// Amount of invincibility left.
        /// </summary>
        public float InvincibleTimer { get; set; }

        /// <summary>
        /// Current maximum for the invincibility gauge.
        /// </summary>
        public float InvincibleTimerMax { get; set; }

        /// <summary>
        /// The amount of time left of the double gem powerup.
        /// </summary>
        public float DoubleGemTimer { get; set; }

        /// <summary>
        /// The upper limit of the double gem powerup duration (usually 2 minutes/120 seconds).
        /// </summary>
        public float DoubleGemTimerMax { get; set; }

        #endregion

        #region MiniGame Characters

        /// <summary>
        /// The amount of Sgt. Byrd boost fuel left.
        /// </summary>
        public float SgtByrdFuel { get; set; }

        /// <summary>
        /// The amount of Sgt. Byrd's bombs left.
        /// Not shown to the player and set to 9999 when starting his minigame.
        /// </summary>
        public short SgtByrdBombs { get; set; }

        /// <summary>
        /// The amount of Sgt. Byrd's missiles left.
        /// Not shown to the player and set to 9999 when starting his minigame.
        /// </summary>
        public short SgtByrdMissiles { get; set; }

        /// <summary>
        /// The amount of Sparx smart bombs left.
        /// Set to a predetermined value when starting his minigame.
        /// </summary>
        public short SparxSmartBombs { get; set; }

        /// <summary>
        /// The amount of Sparx seeker missiles left.
        /// Set to a predetermined value when starting his minigame.
        /// </summary>
        public short SparxSeekers { get; set; }

        /// <summary>
        /// The amount of Blink bombs left.
        /// Set to a predetermined value when starting his minigame.
        /// </summary>
        public short BlinkBombs { get; set; }

        #endregion

        /// <summary>
        /// Total amount of light gems collected.
        /// </summary>
        public sbyte TotalLightGems { get; set; }

        /// <summary>
        /// Total amount of Dark Gems broken.
        /// </summary>
        public sbyte TotalDarkGems { get; set; }

        /// <summary>
        /// Total amount of Dragon Eggs collected.
        /// </summary>
        public sbyte TotalDragonEggs { get; set; }

        /// <summary>
        /// Unknown field.
        /// </summary>
        public sbyte UNK_0 { get; private set; }

        /// <summary>
        /// The setup information for the player.
        /// Editing this struct is not needed as it's overwritten on a map loading.
        /// </summary>
        public PlayerSetupInfo Setup { get; private set; }

        /// <summary>
        /// The last player character to be set up.
        /// </summary>
        public Players LastPlayerSetup { get; private set; }

        #endregion

        private PlayerState() { }

        public static PlayerState FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            var state = new PlayerState();

            int breath = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(BreathType), breath))
                throw new IOException($"Invalid value for BreathType: {breath}");

            state.CurrentBreath = (BreathType)breath;

            state.Health = reader.ReadInt32(bigEndian);

            state.Gems = reader.ReadInt32(bigEndian);

            state.TotalGems = reader.ReadInt32(bigEndian);

            state.LockPickers = PowerupTally.FromReader(reader, platform);

            state.FlameBombs = PowerupTally.FromReader(reader, platform);

            state.IceBombs = PowerupTally.FromReader(reader, platform);

            state.WaterBombs = PowerupTally.FromReader(reader, platform);

            state.ElectricBombs = PowerupTally.FromReader(reader, platform);

            state.FireArrows = reader.ReadInt16(bigEndian);

            state.FireArrowsMax = reader.ReadInt16(bigEndian);

            state.AbilityFlags = reader.ReadUInt32(bigEndian);

            state.WaterDiveTimer = reader.ReadSingle(bigEndian);

            state.SuperchargeTimer = reader.ReadSingle(bigEndian);

            state.SuperchargeTimerMax = reader.ReadSingle(bigEndian);

            state.InvincibleTimer = reader.ReadSingle(bigEndian);

            state.InvincibleTimerMax = reader.ReadSingle(bigEndian);

            state.DoubleGemTimer = reader.ReadSingle(bigEndian);

            state.DoubleGemTimerMax = reader.ReadSingle(bigEndian);

            state.SgtByrdFuel = reader.ReadSingle(bigEndian);

            state.SgtByrdBombs = reader.ReadInt16(bigEndian);

            state.SgtByrdMissiles = reader.ReadInt16(bigEndian);

            state.SparxSmartBombs = reader.ReadInt16(bigEndian);

            state.SparxSeekers = reader.ReadInt16(bigEndian);

            state.BlinkBombs = reader.ReadInt16(bigEndian);

            state.TotalLightGems = reader.ReadSByte();

            state.TotalDarkGems = reader.ReadSByte();

            state.TotalDragonEggs = reader.ReadSByte();

            state.UNK_0 = reader.ReadSByte();

            reader.BaseStream.Seek(6, SeekOrigin.Current);

            state.Setup = PlayerSetupInfo.FromReader(reader, platform);

            int lastPlayer = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(Players), lastPlayer))
                throw new IOException($"Invalid value for LastPLayerSetup: {lastPlayer}");

            state.LastPlayerSetup = (Players)lastPlayer;

            reader.BaseStream.Seek(4, SeekOrigin.Current);

            return state;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            writer.Write((int)CurrentBreath, bigEndian);
            writer.Write(Health, bigEndian);
            writer.Write(Gems, bigEndian);
            writer.Write(TotalGems, bigEndian);
            LockPickers.ToWriter(writer, platform);
            FlameBombs.ToWriter(writer, platform);
            IceBombs.ToWriter(writer, platform);
            WaterBombs.ToWriter(writer, platform);
            ElectricBombs.ToWriter(writer, platform);
            writer.Write(FireArrows, bigEndian);
            writer.Write(FireArrowsMax, bigEndian);
            writer.Write(AbilityFlags, bigEndian);
            writer.Write(WaterDiveTimer, bigEndian);
            writer.Write(SuperchargeTimer, bigEndian);
            writer.Write(SuperchargeTimerMax, bigEndian);
            writer.Write(InvincibleTimer, bigEndian);
            writer.Write(InvincibleTimerMax, bigEndian);
            writer.Write(DoubleGemTimer, bigEndian);
            writer.Write(DoubleGemTimerMax, bigEndian);
            writer.Write(SgtByrdFuel, bigEndian);
            writer.Write(SgtByrdBombs, bigEndian);
            writer.Write(SgtByrdMissiles, bigEndian);
            writer.Write(SparxSmartBombs, bigEndian);
            writer.Write(SparxSeekers, bigEndian);
            writer.Write(BlinkBombs, bigEndian);
            writer.Write(TotalLightGems);
            writer.Write(TotalDarkGems);
            writer.Write(TotalDragonEggs);
            writer.Write(UNK_0);
            writer.BaseStream.Seek(6, SeekOrigin.Current);
            Setup.ToWriter(writer, platform);
            writer.Write((int)LastPlayerSetup, bigEndian);
            writer.BaseStream.Seek(4, SeekOrigin.Current);
        }

        private void SetFromDataSheet()
        {
            foreach (sbyte[] row in DataSheet_GameInfo_1)
            {
                switch(row[0])
                {
                    case 0x1:
                        FlameBombs.Amount = row[2];
                        FlameBombs.Max = row[3];
                        FlameBombs.Total = row[5];
                        break;
                    case 0x2:
                        IceBombs.Amount = row[2];
                        IceBombs.Max = row[3];
                        IceBombs.Total = row[5];
                        break;
                    case 0x4:
                        WaterBombs.Amount = row[2];
                        WaterBombs.Max = row[3];
                        WaterBombs.Total = row[5];
                        break;
                    case 0x8:
                        ElectricBombs.Amount = row[2];
                        ElectricBombs.Max = row[3];
                        ElectricBombs.Total = row[5];
                        break;
                    case 0x10:
                        FireArrows = row[2];
                        FireArrowsMax = row[3];
                        break;
                    case 0x20:
                        LockPickers.Amount = row[2];
                        LockPickers.Max = row[3];
                        LockPickers.Total = row[5];
                        break;
                    case 0x40:
                        DoubleGemTimerMax = row[2] * 60f;
                        break;
                }
            }
        }

        /// <summary>
        /// Set the player state to that of a new game.
        /// </summary>
        public void StartNewGame()
        {
            FlameBombs.Clear();
            IceBombs.Clear();
            WaterBombs.Clear();
            ElectricBombs.Clear();
            FireArrows = 0;
            FireArrowsMax = 0;
            LockPickers.Clear();
            DoubleGemTimerMax = 0f;
            Health = 0xA0;
            CurrentBreath = BreathType.Fire;
            Gems = 0;
            TotalGems = 0;
            AbilityFlags = 0;
            SgtByrdFuel = 0f;
            BlinkBombs = 0;
            InvincibleTimerMax = 0f;
            LastPlayerSetup = Players.Spyro;
            SuperchargeTimerMax = 0f;
            DoubleGemTimerMax = 10f * 60f;
            DoubleGemTimer = 0f;
            TotalLightGems = 0;
            TotalDragonEggs = 0;
            TotalDarkGems = 0;
            UNK_0 = 0;

            SetFromDataSheet();
        }

        #region Health Methods

        /// <summary>
        /// Ensure that <see cref="Health"/> is rounded to a valid health value.
        /// </summary>
        public void RoundHealthToValid()
        {
            Health = RoundHealthToValid(Health);
        }

        /// <summary>
        /// Take a health value and ensure it's rounded to a valid health value.
        /// </summary>
        /// <param name="health">Health value to round.</param>
        /// <returns>The rounded health value.</returns>
        public static int RoundHealthToValid(int health)
        {
            int fraction = health % 0x20;

            //Round up
            if (fraction != 0)
                health += 0x20 - fraction;

            //Put within range
            if (health > 0xA0)
                health = 0xA0;
            else if (health < 0)
                health = 0;

            return health;
        }

        #endregion

        #region Ability Flag Methods

        /// <summary>
        /// Get the state of an ability flag, where <paramref name="mask"/> is
        /// one of the constants AF_MASK_*.
        /// </summary>
        /// <param name="mask">Mask to check against.</param>
        /// <returns>true if the ability flag is set, false if not.</returns>
        public bool GetAbilityFlag(uint mask)
        {
            return (AbilityFlags & mask) != 0;
        }

        /// <summary>
        /// Set the state of an ability flag, where <paramref name="mask"/> is
        /// one of the constants AF_MASK_*.
        /// </summary>
        /// <param name="mask">Mask for the flag to set.</param>
        /// <param name="value">Value to set the flag's state to.</param>
        public void SetAbilityFlag(uint mask, bool value)
        {
            if ((mask & ABILITY_FLAGS_MASK) == 0) return;

            if (value)
                AbilityFlags |= mask;
            else
                AbilityFlags &= ~mask;
        }

        #endregion
    }
}
