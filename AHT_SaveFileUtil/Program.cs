using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using AHT_SaveFileUtil.Save;
using AHT_SaveFileUtil.Save.MiniMap;
using AHT_SaveFileUtil.Save.Slot;
using AHT_SaveFileUtil.Save.Triggers;
using System;
using System.IO;
using System.IO.Hashing;
using System.Text;

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
        private static readonly long MainDol_TriggerTable_Offset = 0x3DAC40;

        private static readonly string MiniMapsYamlPath = "../../../../minimaps.yaml";
        private static readonly string TriggerTableYamlPath = "../../../../triggertable.yaml";
        private static readonly string TriggerDataYamlPath_Temp = "../../../../triggerdata_temp.yaml";
        private static readonly string TriggerDataYamlPath = "../../../../triggerdata.yaml";

        private static readonly string TestMapYaml = "../../../../temp_5000009.yaml";

        public static void Main(string[] args)
        {
            //var def = TriggerDataDefinitions.FromYAML(File.ReadAllText(TriggerDataYamlPath));
            //return;

            //GenerateMinimapsYamlFromGCExe();
            //return;

            //GenerateTriggerTableYamlFromGCExe();
            //return;

            string yaml = File.ReadAllText(MiniMapsYamlPath);
            var miniMaps = MiniMaps.FromYAML(yaml);

            File.Copy(gc_input, gc_output, true);

            using (var stream = File.OpenRead(gc_input))
            {
                var file = SaveFile.FromFileStream(stream, GamePlatform.GameCube);

                try
                {
                    Console.WriteLine(SaveFile.GetGCCheckSum(stream, GamePlatform.GameCube).ToString("X"));
                }
                catch (Exception) { }

                foreach (var slot in file.Slots)
                {
                    Console.WriteLine(slot);
                }

                PrintSaveFileMiniMaps(file.Slots[0].GameState.BitHeap, miniMaps);

                //using (var stream2 = File.Open(gc_output, FileMode.Open, FileAccess.ReadWrite))
                //{
                //    file.ToFileStream(stream2);
                //}
            }
        }

        public static void PrintSaveFileMiniMaps(BitHeap bitHeap, MiniMaps miniMaps)
        {
            var list = miniMaps.GetAllMiniMapArrayFromBitHeap(bitHeap);

            foreach (var map in list)
            {
                for(int i = map.Length-1; i >= 0 ; i--)
                {
                    for (int j = 0; j < map[i].Length; j++)
                    {
                        Console.Write(map[i][j] ? "XX" : "  ");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine(new string('-', map[0].Length*2));
            }
        }

        public static void GenerateTriggerTableYamlFromGCExe()
        {
            using (BinaryReader reader = new(File.OpenRead(MainDol_Path)))
            {
                TriggerTable triggerTable = new();
                var ttYaml = new StringBuilder();
                var tdYaml = new StringBuilder();

                triggerTable.NumEntries = 426;
                ttYaml.AppendLine("num_entries: " + triggerTable.NumEntries);
                triggerTable.NumPreservingEntries = 168;
                ttYaml.AppendLine("num_preserving_entries: " + triggerTable.NumPreservingEntries);

                tdYaml.AppendLine("trigger_data:");

                reader.BaseStream.Seek(MainDol_TriggerTable_Offset, SeekOrigin.Begin);
                triggerTable.Entries = new TriggerTableEntry[triggerTable.NumEntries];

                int preserveCount = 0;

                ttYaml.AppendLine("entries:");
                for (int i = 0; i < triggerTable.NumEntries; i++)
                {
                    triggerTable.Entries[i] = new TriggerTableEntry();

                    triggerTable.Entries[i].PrimaryHash = reader.ReadUInt32(true);
                    ttYaml.Append("- primary_hash: 0x" + triggerTable.Entries[i].PrimaryHash.ToString("X"));
                    string primaryHashStr = ((EXHashCode)triggerTable.Entries[i].PrimaryHash).ToString();
                    ttYaml.AppendLine(" # " + primaryHashStr);

                    triggerTable.Entries[i].SubHash = reader.ReadUInt32(true);
                    ttYaml.Append("  sub_hash: 0x" + triggerTable.Entries[i].SubHash.ToString("X"));
                    string subHashStr = ((EXHashCode)triggerTable.Entries[i].SubHash).ToString();
                    ttYaml.AppendLine(" # " + subHashStr);

                    reader.BaseStream.Seek(0x10, SeekOrigin.Current);

                    triggerTable.Entries[i].StoredDataSize = reader.ReadInt32(true);
                    ttYaml.AppendLine("  stored_data_size: " + triggerTable.Entries[i].StoredDataSize);

                    reader.BaseStream.Seek(0xc, SeekOrigin.Current);

                    if (triggerTable.Entries[i].StoredDataSize > 0)
                    {
                        preserveCount++;

                        tdYaml.AppendLine("- trigger_table_entry: " + i);
                        tdYaml.Append("  object_name: " + primaryHashStr.Replace("HT_TriggerType_", ""));
                        if (subHashStr != "HT_TriggerSubType_Undefined")
                        {
                            tdYaml.AppendLine("_" + subHashStr.Replace("HT_TriggerSubType_", ""));
                        } else { tdYaml.AppendLine(); }
                        tdYaml.AppendLine("  data:");
                        tdYaml.AppendLine("  - name: Data");
                        tdYaml.AppendLine("    type: Flags");
                        tdYaml.AppendLine("    num_bits: " + triggerTable.Entries[i].StoredDataSize);
                    }
                }

                File.WriteAllText(TriggerTableYamlPath, ttYaml.ToString());
                File.WriteAllText(TriggerDataYamlPath_Temp, tdYaml.ToString());
            }
        }
        
        public static void GenerateMinimapsYamlFromGCExe()
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
                File.WriteAllText(MiniMapsYamlPath, yaml);
            }
        }
    }
}
