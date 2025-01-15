using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using System;
using System.IO;

namespace AHT_SaveFileUtil.Save.Slot
{
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

    /// <summary>
    /// A 4-byte structure with info for a collectable.
    /// </summary>
    public struct PowerupTally : ISaveFileIO<PowerupTally>
    {
        /// <summary>
        /// The number of items.
        /// </summary>
        public byte Amount;
        /// <summary>
        /// The maximum number of the item the user can hold.
        /// </summary>
        public byte Max;
        /// <summary>
        /// The maximum number of the item the user can hold with all magazines purchased.
        /// </summary>
        public byte Total;
        /// <summary>
        /// The number of magazines purchased.
        /// </summary>
        public byte Magazines;

        public static PowerupTally FromReader(BinaryReader reader, GamePlatform platform)
        {
            return new PowerupTally
            {
                Amount = reader.ReadByte(),
                Max = reader.ReadByte(),
                Total = reader.ReadByte(),
                Magazines = reader.ReadByte()
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
        /// <summary>
        /// The currently selected breath.
        /// </summary>
        public BreathType CurrentBreath { get; private set; }

        /// <summary>
        /// The current health.
        /// <para>
        /// Full health is 0xA0 and decrements by 0x20 with every hit point lost.
        /// Without the health upgrade, the player dies at 0x20 health. With the upgrade, they die at 0x0.
        /// </para>
        /// </summary>
        public int Health { get; private set; }

        /// <summary>
        /// The current amount of gems.
        /// </summary>
        public int Gems { get; private set; }

        /// <summary>
        /// The total amount of gems collected on this save.
        /// </summary>
        public int TotalGems { get; private set; }

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
        public short FireArrows { get; private set; }

        /// <summary>
        /// The max amount of fire arrows.
        /// </summary>
        public short FireArrowsMax { get; private set; }

        /// <summary>
        /// A set of flags for certain abilities/collectables.
        /// </summary>
        public uint AbilityFlags { get; private set; }

        #region Ability Flags Properties
        /// <summary>
        /// Gets or sets whether Spyro can double jump/horn dive.
        /// </summary>
        public bool AF_DoubleJump
        {
            get => (AbilityFlags & 0x1) != 0;
            set => AbilityFlags = 
                value ? (AbilityFlags & 0x1) : (AbilityFlags & ~(uint)0x1);
        }
        /// <summary>
        /// Gets or sets whether the player has the hit point upgrade.
        /// </summary>
        public bool AF_HitPointUpgrade
        {
            get => (AbilityFlags & 0x4) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x4) : (AbilityFlags & ~(uint)0x4);
        }
        /// <summary>
        /// Gets or sets whether Spyro can pole spin.
        /// </summary>
        public bool AF_PoleSpin
        {
            get => (AbilityFlags & 0x10) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x10) : (AbilityFlags & ~(uint)0x10);
        }
        /// <summary>
        /// Gets or sets whether Spyro can breathe ice.
        /// </summary>
        public bool AF_IceBreath
        {
            get => (AbilityFlags & 0x20) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x20) : (AbilityFlags & ~(uint)0x20);
        }
        /// <summary>
        /// Gets or sets whether Spyro can breathe electricity.
        /// </summary>
        public bool AF_ElectricBreath
        {
            get => (AbilityFlags & 0x40) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x40) : (AbilityFlags & ~(uint)0x40);
        }
        /// <summary>
        /// Gets or sets whether Spyro can breathe water.
        /// </summary>
        public bool AF_WaterBreath
        {
            get => (AbilityFlags & 0x80) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x80) : (AbilityFlags & ~(uint)0x80);
        }
        /// <summary>
        /// Gets or sets whether the 2x gem multiplier is active.
        /// </summary>
        public bool AF_DoubleGem
        {
            get => (AbilityFlags & 0x200) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x200) : (AbilityFlags & ~(uint)0x200);
        }
        /// <summary>
        /// Gets or sets whether aqualung is active (unused in the final game code).
        /// </summary>
        public bool AF_Aqualung // Unused
        {
            get => (AbilityFlags & 0x800) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x800) : (AbilityFlags & ~(uint)0x800);
        }
        /// <summary>
        /// Gets or sets whether supercharge is active.
        /// </summary>
        public bool AF_Supercharge
        {
            get => (AbilityFlags & 0x1000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x1000) : (AbilityFlags & ~(uint)0x1000);
        }
        /// <summary>
        /// Gets or sets whether invincibility is active.
        /// </summary>
        public bool AF_Invincibility
        {
            get => (AbilityFlags & 0x2000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x2000) : (AbilityFlags & ~(uint)0x2000);
        }
        /// <summary>
        /// Gets or sets whether the player has bought a lock pick on this save.
        /// This is used to unlock the other shop items for purchase.
        /// </summary>
        public bool AF_BoughtLockPick
        {
            get => (AbilityFlags & 0x4000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x4000) : (AbilityFlags & ~(uint)0x4000);
        }
        /// <summary>
        /// Gets or sets whether Spyro can wing shield.
        /// </summary>
        public bool AF_WingShield
        {
            get => (AbilityFlags & 0x8000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x8000) : (AbilityFlags & ~(uint)0x8000);
        }
        /// <summary>
        /// Gets or sets whether Spyro can wall kick.
        /// </summary>
        public bool AF_WallKick
        {
            get => (AbilityFlags & 0x10000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x10000) : (AbilityFlags & ~(uint)0x10000);
        }
        /// <summary>
        /// Gets or sets whether Spyro has the horn dive upgrade.
        /// </summary>
        public bool AF_HornDiveUpgrade
        {
            get => (AbilityFlags & 0x20000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x20000) : (AbilityFlags & ~(uint)0x20000);
        }
        /// <summary>
        /// Gets or sets whether the player has the butterfly jar.
        /// </summary>
        public bool AF_ButterflyJar
        {
            get => (AbilityFlags & 0x40000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x40000) : (AbilityFlags & ~(uint)0x40000);
        }
        #endregion

        #region Timers
        /// <summary>
        /// The amount of time underwater before Spyro runs out of oxygen (unused in the final game code).
        /// </summary>
        public float WaterDiveTimer { get; private set; }

        /// <summary>
        /// Amount of supercharge time left.
        /// </summary>
        public float SuperchargeTimer { get; private set; }

        /// <summary>
        /// Current maximum for the supercharge gauge.
        /// </summary>
        public float SuperchargeTimerMax { get; private set; }

        /// <summary>
        /// Amount of invincibility left.
        /// </summary>
        public float InvincibleTimer { get; private set; }

        /// <summary>
        /// Current maximum for the invincibility gauge.
        /// </summary>
        public float InvincibleTimerMax { get; private set; }

        /// <summary>
        /// The amount of time left of the double gem powerup.
        /// </summary>
        public float DoubleGemTimer { get; private set; }

        /// <summary>
        /// The upper limit of the double gem powerup duration (usually 2 minutes/120 seconds).
        /// </summary>
        public float DoubleGemTimerMax { get; private set; }
        #endregion

        /// <summary>
        /// The amount of Sgt. Byrd boost fuel left.
        /// </summary>
        public float SgtByrdFuel { get; private set; }

        /// <summary>
        /// The amount of Sgt. Byrd's bombs left.
        /// Not shown to the player and set to 9999 when starting his minigame.
        /// </summary>
        public short SgtByrdBombs { get; private set; }

        /// <summary>
        /// The amount of Sgt. Byrd's missiles left.
        /// Not shown to the player and set to 9999 when starting his minigame.
        /// </summary>
        public short SgtByrdMissiles { get; private set; }

        /// <summary>
        /// The amount of Sparx smart bombs left.
        /// Set to a predetermined value when starting his minigame.
        /// </summary>
        public short SparxSmartBombs { get; private set; }

        /// <summary>
        /// The amount of Sparx seeker missiles left.
        /// Set to a predetermined value when starting his minigame.
        /// </summary>
        public short SparxSeekers { get; private set; }

        /// <summary>
        /// The amount of Blink bombs left.
        /// Set to a predetermined value when starting his minigame.
        /// </summary>
        public short BlinkBombs { get; private set; }

        /// <summary>
        /// Total amount of light gems collected.
        /// </summary>
        public byte TotalLightGems { get; private set; }

        /// <summary>
        /// Total amount of Dark Gems broken.
        /// </summary>
        public byte TotalDarkGems { get; private set; }

        /// <summary>
        /// Total amount of Dragon Eggs collected.
        /// </summary>
        public byte TotalDragonEggs { get; private set; }

        /// <summary>
        /// Unknown field.
        /// </summary>
        public byte UNK_0 { get; private set; }

        /// <summary>
        /// The setup information for the player.
        /// Editing this struct is not needed as it's overwritten on a map loading.
        /// </summary>
        public PlayerSetupInfo Setup { get; private set; }

        /// <summary>
        /// The last player character to be set up.
        /// </summary>
        public Players LastPlayerSetup { get; private set; }

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

            state.TotalLightGems = reader.ReadByte();

            state.TotalDarkGems = reader.ReadByte();

            state.TotalDragonEggs = reader.ReadByte();

            state.UNK_0 = reader.ReadByte();

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
    }
}
