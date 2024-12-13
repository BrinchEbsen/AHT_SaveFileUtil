using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stream = File.OpenRead("C:\\Users\\Ebbers\\Downloads\\7D-G5SE-G5SE.gci"))
            {
                var file = SaveFile.FromFileStream(stream, GamePlatform.GameCube);
            }
        }
    }
}
