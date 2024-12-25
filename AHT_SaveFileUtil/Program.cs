using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using AHT_SaveFileUtil.Save;
using AHT_SaveFileUtil.Save.MiniMap;
using System;
using System.IO;
using System.IO.Hashing;

namespace AHT_SaveFileUtil
{
    /// <summary>
    /// The main program in this codebase is merely for testing the library in a sandbox environment.
    /// Eventually it will be removed.
    /// </summary>
    public class Program
    {
        private static readonly string gc_input   = "../../../../7D-G5SE-G5SE.gci";
        private static readonly string gc_output  = "../../../../7D-G5SE-G5SE_2.gci";
        private static readonly string ps2_input  = "../../../../SLUS-20884 Spyro A Hero's Tail (1118BEA6).psu";
        private static readonly string ps2_output = "../../../../SLUS-20884 Spyro A Hero's Tail (1118BEA6)_2.psu";

        private static readonly string MainDol_Path = "../../../../main.dol";
        private static readonly int MainDol_NumMiniMapInfo = 30;
        private static readonly long MainDol_MiniMapInfo_Offset = 0x3DF5BC;

        public static void Main(string[] args)
        {
            //GenerateYamlFromGCExe();
            //return;

            File.Copy(ps2_input, ps2_output, true);

            using (var stream = File.OpenRead(ps2_input))
            {
                var file = SaveFile.FromFileStream(stream, GamePlatform.PlayStation2);

                try
                {
                    Console.WriteLine(SaveFile.GetGCCheckSum(stream, GamePlatform.PlayStation2).ToString("X"));
                }
                catch (Exception e) { }

                foreach (var slot in file.Slots)
                {
                    Console.WriteLine(slot);
                }

                using (var stream2 = File.Open(ps2_output, FileMode.Open, FileAccess.ReadWrite))
                {
                    file.ToFileStream(stream2);
                }
            }
        }

        public static void GenerateYamlFromGCExe()
        {
            using (BinaryReader reader = new(File.OpenRead(MainDol_Path)))
            {
                MiniMaps miniMaps = new();

                miniMaps.NumMapInfo = MainDol_NumMiniMapInfo;

                reader.BaseStream.Seek(MainDol_MiniMapInfo_Offset, SeekOrigin.Begin);
                miniMaps.MiniMapInfo = new MiniMapInfo[miniMaps.NumMapInfo];

                for (int i = 0; i < miniMaps.NumMapInfo; i++)
                {
                    miniMaps.MiniMapInfo[i] = new MiniMapInfo()
                    {
                        MapFile = reader.ReadUInt32(true),
                        Map = reader.ReadUInt32(true),
                        Type = (InfoType)reader.ReadInt32(true),
                        TextureFile = reader.ReadUInt32(true),
                        Texture = reader.ReadUInt32(true),
                        WorldEdge =
                        [
                            reader.ReadSingle(true),
                            reader.ReadSingle(true),
                            reader.ReadSingle(true),
                            reader.ReadSingle(true)
                        ],
                        PixelEdge =
                        [
                            reader.ReadSingle(true),
                            reader.ReadSingle(true),
                            reader.ReadSingle(true),
                            reader.ReadSingle(true)
                        ],
                    };
                }

                string yaml = miniMaps.ToYaml();
                File.WriteAllText("../../../../minimaps.yaml", yaml);
            }
        }
    }
}
