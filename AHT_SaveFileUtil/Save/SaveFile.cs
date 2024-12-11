using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil.Save
{
    public class SaveFile
    {
        public GamePlatform Platform;

        public uint CheckSum { get; }

        private SaveFile() { }

        public static SaveFile FromFileStream(FileStream stream, GamePlatform platform)
        {
            if (platform != GamePlatform.GameCube)
            {
                throw new NotImplementedException();
            }

            var file = new SaveFile();

            // ...

            return file;
        }
    }
}
