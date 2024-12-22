using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil.Save
{
    public struct MiniGameBestTime
    {
        public uint FileHash;
        public float EasyTime;
        public float HardTime;

        public float EasyTimeSeconds
        {
            get
            {
                return EasyTime / 60;
            }
        }

        public float HardTimeSeconds
        {
            get
            {
                return HardTime / 60;
            }
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

        public ushort EggSets { get; private set; }

        public bool GotEggsConceptArt       => (EggSets & EGG_SETS_CONCEPT_ART) != 0;

        public bool GotEggsCharacterViewer  => (EggSets & EGG_SETS_CHARACTER_VIEWER) != 0;

        public bool GotEggsEmberModel       => (EggSets & EGG_SETS_EMBER_MODEL) != 0;

        public bool GotEggsFlameModel       => (EggSets & EGG_SETS_FLAME_MODEL) != 0;

        public bool GotEggsSgtByrdMinigames => (EggSets & EGG_SETS_SGT_BYRD_MINIGAMES) != 0;

        public bool GotEggsSpyroMinigames   => (EggSets & EGG_SETS_SPYRO_MINIGAMES) != 0;

        public bool GotEggsSparxMinigames   => (EggSets & EGG_SETS_SPARX_MINIGAMES) != 0;

        public bool GotEggsBlinkMinigames   => (EggSets & EGG_SETS_BLINK_MINIGAMES) != 0;

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
