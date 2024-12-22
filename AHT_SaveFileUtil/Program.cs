using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save;
using System;
using System.IO;

namespace AHT_SaveFileUtil
{
    public class Program
    {
        private static readonly string input  = "C:\\Users\\Ebbers\\source\\repos\\AHT_SaveFileUtil\\7D-G5SE-G5SE.gci";
        private static readonly string output = "C:\\Users\\Ebbers\\source\\repos\\AHT_SaveFileUtil\\7D-G5SE-G5SE_2.gci";

        public static void Main(string[] args)
        {
            File.Copy(input, output, true);

            using (var stream = File.OpenRead(input))
            {
                var file = SaveFile.FromFileStream(stream, GamePlatform.GameCube);
                Console.WriteLine(SaveFile.GetCheckSum(stream, GamePlatform.GameCube).ToString("X"));

                foreach (var slot in file.Slots)
                {
                    Console.WriteLine(slot);
                }

                using (var stream2 = File.Open(output, FileMode.Open, FileAccess.ReadWrite))
                {
                    file.ToFileStream(stream2);
                }
            }
        }
    }
}
