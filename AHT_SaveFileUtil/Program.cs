using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save;
using System;
using System.IO;
using System.IO.Hashing;

namespace AHT_SaveFileUtil
{
    public class Program
    {
        private static readonly string gc_input   = "C:\\Users\\Ebbers\\source\\repos\\AHT_SaveFileUtil\\7D-G5SE-G5SE.gci";
        private static readonly string gc_output  = "C:\\Users\\Ebbers\\source\\repos\\AHT_SaveFileUtil\\7D-G5SE-G5SE_2.gci";
        private static readonly string ps2_input  = "C:\\Users\\Ebbers\\source\\repos\\AHT_SaveFileUtil\\SLUS-20884 Spyro A Hero's Tail (1118BEA6).psu";
        private static readonly string ps2_output = "C:\\Users\\Ebbers\\source\\repos\\AHT_SaveFileUtil\\SLUS-20884 Spyro A Hero's Tail (1118BEA6)_2.psu";

        public static void Main(string[] args)
        {
            File.Copy(ps2_input, ps2_output, true);

            using (var stream = File.OpenRead(ps2_input))
            {
                var file = SaveFile.FromFileStream(stream, GamePlatform.PlayStation2);
                Console.WriteLine(SaveFile.GetGCCheckSum(stream, GamePlatform.GameCube).ToString("X"));

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
    }
}
