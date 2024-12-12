using Common;
using Extensions;
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
        public uint CheckSum { get; private set; }

        public SaveInfo SaveInfo { get; private set; }

        private SaveFile() { }

        public static SaveFile FromFileStream(FileStream stream, GamePlatform platform)
        {
            if (platform != GamePlatform.GameCube)
            {
                throw new NotImplementedException();
            }

            var file = new SaveFile();

            bool bigEndian = platform == GamePlatform.GameCube;

            using (BinaryReader reader = new BinaryReader(stream))
            {
                stream.Seek(0x40, SeekOrigin.Begin);
                file.CheckSum = reader.ReadUInt32(bigEndian);

                stream.Seek(0x4040, SeekOrigin.Begin);
                file.SaveInfo = SaveInfo.FromReader(reader, platform);
            }

            return file;
        }
    }
}
