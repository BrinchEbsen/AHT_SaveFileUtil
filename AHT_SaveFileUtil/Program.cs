using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save;
using System;
using System.IO;

namespace AHT_SaveFileUtil
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stream = File.OpenRead("C:\\Users\\Ebbers\\Downloads\\7D-G5SE-G5SE.gci"))
            {
                var file = SaveFile.FromFileStream(stream, GamePlatform.GameCube);
                Console.WriteLine(SaveFile.GetCheckSum(stream, GamePlatform.GameCube).ToString("X"));

                foreach (var slot in file.Slots)
                {
                    Console.WriteLine(slot);
                }
            }
        }
    }
}
