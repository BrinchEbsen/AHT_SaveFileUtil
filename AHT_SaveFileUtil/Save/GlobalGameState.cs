using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace AHT_SaveFileUtil.Save
{
    public class MiniGameBestTime
    {
        public static string[] MiniGameNames { get; } =
        {
            /*  0 */ "Completely Swamped",
            /*  1 */ "Island Speedway",
            /*  2 */ "Cavern Chaos",
            /*  3 */ "Critter Calamity",
            /*  4 */ "All Washed Up",
            /*  5 */ "Cloudy Speedway",
            /*  6 */ "Outlandish Inlet",
            /*  7 */ "Turtle Turmoil",
            /*  8 */ "Snowed Under",
            /*  9 */ "Iceberg Aerobatics",
            /* 10 */ "Frosty Flight",
            /* 11 */ "Iced TNT",
            /* 12 */ "Mined Out",
            /* 13 */ "Lava Palaver",
            /* 14 */ "Sparx Will Fly",
            /* 15 */ "Storming the Beach"
        };

        public uint FileHash { get; set; }
        public float EasyTime { get; set; }
        public float HardTime { get; set; }

        public float EasyTimeSeconds
        {
            get => EasyTime / 60;
            set => EasyTime = value * 60;
        }

        public float EasyTimeMinutes
        {
            get => EasyTime / (60*60);
            set => EasyTime = value * (60*60);
        }

        public float HardTimeSeconds
        {
            get => HardTime / 60;
            set => HardTime = value * 60;
        }

        public float HardTimeMinutes
        {
            get => HardTime / (60 * 60);
            set => HardTime = value * (60 * 60);
        }

        public string EasyTimeString
        {
            get => string.Format("{0}:{1:00}",
                Math.Floor(EasyTimeMinutes),
                Math.Floor(EasyTimeSeconds % 60));
        }

        public string HardTimeString
        {
            get => string.Format("{0}:{1:00}",
                Math.Floor(HardTimeMinutes),
                Math.Floor(HardTimeSeconds % 60));
        }
    }

    public class GlobalGameState : ISaveFileIO<GlobalGameState>
    {
        //For each bit in EggSets
        public const ushort EGG_SETS_CONCEPT_ART        =  0x1;
        public const ushort EGG_SETS_CHARACTER_VIEWER   =  0x2;
        public const ushort EGG_SETS_EMBER_MODEL        =  0x4;
        public const ushort EGG_SETS_FLAME_MODEL        =  0x8;
        public const ushort EGG_SETS_SGT_BYRD_MINIGAMES = 0x10;
        public const ushort EGG_SETS_SPYRO_MINIGAMES    = 0x20;
        public const ushort EGG_SETS_SPARX_MINIGAMES    = 0x40;
        public const ushort EGG_SETS_BLINK_MINIGAMES    = 0x80;

        public static Dictionary<ushort, string> EggSetNames { get; } = new Dictionary<ushort, string>
        {
            { EGG_SETS_CONCEPT_ART,        "Concept Art" },
            { EGG_SETS_CHARACTER_VIEWER,   "Model Viewer" },
            { EGG_SETS_EMBER_MODEL,        "Ember" },
            { EGG_SETS_FLAME_MODEL,        "Flame" },
            { EGG_SETS_SGT_BYRD_MINIGAMES, "Sgt. Byrd Minigames" },
            { EGG_SETS_SPYRO_MINIGAMES,    "Spyro Minigames" },
            { EGG_SETS_SPARX_MINIGAMES,    "Sparx Minigames" },
            { EGG_SETS_BLINK_MINIGAMES,    "Blink Minigames" }
        };

        public ushort EggSets { get; set; }

        public bool GotEggsConceptArt
        {
            get => (EggSets & EGG_SETS_CONCEPT_ART) != 0;
            set => EggSets = (ushort) (value ?
                EggSets | EGG_SETS_CONCEPT_ART :
                EggSets & ~EGG_SETS_CONCEPT_ART);
        }

        public bool GotEggsCharacterViewer
        {
            get => (EggSets & EGG_SETS_CHARACTER_VIEWER) != 0;
            set => EggSets = (ushort) (value?
                EggSets | EGG_SETS_CHARACTER_VIEWER :
                EggSets & ~EGG_SETS_CHARACTER_VIEWER);
        }

        public bool GotEggsEmberModel
        {
            get => (EggSets & EGG_SETS_EMBER_MODEL) != 0;
            set => EggSets = (ushort) (value?
                EggSets | EGG_SETS_EMBER_MODEL :
                EggSets & ~EGG_SETS_EMBER_MODEL);
        }

        public bool GotEggsFlameModel
        {
            get => (EggSets & EGG_SETS_FLAME_MODEL) != 0;
            set => EggSets = (ushort) (value?
                EggSets | EGG_SETS_FLAME_MODEL :
                EggSets & ~EGG_SETS_FLAME_MODEL);
        }

        public bool GotEggsSgtByrdMinigames
        {
            get => (EggSets & EGG_SETS_SGT_BYRD_MINIGAMES) != 0;
            set => EggSets = (ushort) (value?
                EggSets | EGG_SETS_SGT_BYRD_MINIGAMES :
                EggSets & ~EGG_SETS_SGT_BYRD_MINIGAMES);
        }

        public bool GotEggsSpyroMinigames
        {
            get => (EggSets & EGG_SETS_SPYRO_MINIGAMES) != 0;
            set => EggSets = (ushort) (value?
                EggSets | EGG_SETS_SPYRO_MINIGAMES :
                EggSets & ~EGG_SETS_SPYRO_MINIGAMES);
        }

        public bool GotEggsSparxMinigames
        {
            get => (EggSets & EGG_SETS_SPARX_MINIGAMES) != 0;
            set => EggSets = (ushort) (value?
                EggSets | EGG_SETS_SPARX_MINIGAMES :
                EggSets & ~EGG_SETS_SPARX_MINIGAMES);
        }

        public bool GotEggsBlinkMinigames
        {
            get => (EggSets & EGG_SETS_BLINK_MINIGAMES) != 0;
            set => EggSets = (ushort) (value?
                EggSets | EGG_SETS_BLINK_MINIGAMES :
                EggSets & ~EGG_SETS_BLINK_MINIGAMES);
        }

        public MiniGameBestTime[] MiniGameBestTimes { get; private set; } = new MiniGameBestTime[16];



        private GlobalGameState() { }



        public static GlobalGameState FromReader(BinaryReader reader, GamePlatform platform)
        {
            var state = new GlobalGameState();

            bool bigEndian = platform == GamePlatform.GameCube;

            for (int i = 0; i < 16; i++)
            {
                var time = new MiniGameBestTime
                {
                    FileHash = reader.ReadUInt32(bigEndian),
                    EasyTime = reader.ReadSingle(bigEndian),
                    HardTime = reader.ReadSingle(bigEndian)
                };

                state.MiniGameBestTimes[i] = time;
            }

            state.EggSets = reader.ReadUInt16();

            //2 bytes of padding
            reader.BaseStream.Seek(2, SeekOrigin.Current);

            return state;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            foreach(var time in MiniGameBestTimes)
            {
                writer.Write(time.FileHash, bigEndian);
                writer.Write(time.EasyTime, bigEndian);
                writer.Write(time.HardTime, bigEndian);
            }

            writer.Write(EggSets, bigEndian);

            writer.BaseStream.Seek(2, SeekOrigin.Current);
        }
    }
}
