using AHT_SaveFileUtil.Common;
using Common;
using Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public struct PowerupTally
    {
        public byte Amount;
        public byte Max;
        public byte Total;
        public byte Magazines;

        public static PowerupTally FromReader(BinaryReader reader)
        {
            return new PowerupTally
            {
                Amount = reader.ReadByte(),
                Max = reader.ReadByte(),
                Total = reader.ReadByte(),
                Magazines = reader.ReadByte()
            };
        }
    }

    public struct PlayerSetupInfo
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
    }

    public class PlayerState
    {
        public BreathType CurrentBreath { get; private set; }

        public int Health { get; private set; }

        public int Gems { get; private set; }

        public int TotalGems { get; private set; }

        public PowerupTally LockPickers { get; private set; }
        
        public PowerupTally FlameBombs { get; private set; }

        public PowerupTally IceBombs { get; private set; }

        public PowerupTally WaterBombs { get; private set; }

        public PowerupTally ElectricBombs { get; private set; }

        public short FireArrows { get; private set; }

        public short FireArrowsMax { get; private set; }

        public uint AbilityFlags { get; private set; }

        public bool AF_DoubleJump
        {
            get => (AbilityFlags & 0x1) != 0;
            set => AbilityFlags = 
                value ? (AbilityFlags & 0x1) : (AbilityFlags & ~(uint)0x1);
        }
        public bool AF_HitPointUpgrade
        {
            get => (AbilityFlags & 0x4) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x4) : (AbilityFlags & ~(uint)0x4);
        }
        public bool AF_PoleSpin
        {
            get => (AbilityFlags & 0x10) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x10) : (AbilityFlags & ~(uint)0x10);
        }
        public bool AF_IceBreath
        {
            get => (AbilityFlags & 0x20) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x20) : (AbilityFlags & ~(uint)0x20);
        }
        public bool AF_ElectricBreath
        {
            get => (AbilityFlags & 0x40) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x40) : (AbilityFlags & ~(uint)0x40);
        }
        public bool AF_WaterBreath
        {
            get => (AbilityFlags & 0x80) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x80) : (AbilityFlags & ~(uint)0x80);
        }
        public bool AF_DoubleGem
        {
            get => (AbilityFlags & 0x200) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x200) : (AbilityFlags & ~(uint)0x200);
        }
        public bool AF_Aqualung // Unused
        {
            get => (AbilityFlags & 0x800) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x800) : (AbilityFlags & ~(uint)0x800);
        }
        public bool AF_Supercharge
        {
            get => (AbilityFlags & 0x1000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x1000) : (AbilityFlags & ~(uint)0x1000);
        }
        public bool AF_Invincibility
        {
            get => (AbilityFlags & 0x2000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x2000) : (AbilityFlags & ~(uint)0x2000);
        }
        public bool AF_BoughtLockPick
        {
            get => (AbilityFlags & 0x4000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x4000) : (AbilityFlags & ~(uint)0x4000);
        }
        public bool AF_WingShield
        {
            get => (AbilityFlags & 0x8000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x8000) : (AbilityFlags & ~(uint)0x8000);
        }
        public bool AF_WallKick
        {
            get => (AbilityFlags & 0x10000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x10000) : (AbilityFlags & ~(uint)0x10000);
        }
        public bool AF_HornDiveUpgrade
        {
            get => (AbilityFlags & 0x20000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x20000) : (AbilityFlags & ~(uint)0x20000);
        }
        public bool AF_ButterflyJar
        {
            get => (AbilityFlags & 0x40000) != 0;
            set => AbilityFlags =
                value ? (AbilityFlags & 0x40000) : (AbilityFlags & ~(uint)0x40000);
        }

        public float WaterDiveTimer { get; private set; }

        public float SuperchargeTimer { get; private set; }

        public float SuperchargeTimerMax { get; private set; }

        public float InvincibleTimer { get; private set; }

        public float InvincibleTimerMax { get; private set; }

        public float DoubleGemTimer { get; private set; }

        public float DoubleGemTimerMax { get; private set; }

        public float SgtByrdFuel { get; private set; }

        public short SgtByrdBombs { get; private set; }

        public short SgtByrdMissiles { get; private set; }

        public short SparxSmartBombs { get; private set; }

        public short SparxSeekers { get; private set; }

        public short BlinkBombs { get; private set; }

        public byte TotalLightGems { get; private set; }

        public byte TotalDarkGems { get; private set; }

        public byte TotalDragonEggs { get; private set; }

        public byte UNK_0 { get; private set; }

        public PlayerSetupInfo Setup { get; private set; }

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

            state.LockPickers = PowerupTally.FromReader(reader);

            state.FlameBombs = PowerupTally.FromReader(reader);

            state.IceBombs = PowerupTally.FromReader(reader);

            state.WaterBombs = PowerupTally.FromReader(reader);

            state.ElectricBombs = PowerupTally.FromReader(reader);

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
    }
}
