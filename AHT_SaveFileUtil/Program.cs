using AHT_SaveFileUtil.Save;
using Common;
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

                foreach(var slot in file.Slots)
                {
                    Console.WriteLine(slot);
                }
            }
        }
    }
}
