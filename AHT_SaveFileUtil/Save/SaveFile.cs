using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using AHT_SaveFileUtil.Save.Slot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil.Save
{
    public class SaveFile
    {
        public GamePlatform Platform { get; private set; }

        public uint CheckSum { get; private set; }

        public bool CheckSumValid { get; private set; }

        public SaveInfo SaveInfo { get; private set; }

        public SaveSlot[] Slots { get; private set; } = new SaveSlot[3];

        private SaveFile() { }

        public static SaveFile FromFileStream(FileStream stream, GamePlatform platform)
        {
            if (!stream.CanRead)
                throw new ArgumentException("Provided stream needs to be readable.");

            if (platform != GamePlatform.GameCube)
                throw new NotImplementedException("Only GameCube supported.");

            var file = new SaveFile();

            file.Platform = platform;

            bool bigEndian = platform == GamePlatform.GameCube;

            //Get the checksum (SPECIFIC TO GAMECUBE FOR NOW)
            uint actualCheck = GetCheckSum(stream, platform);

            using (BinaryReader reader = new(stream, Encoding.UTF8, true))
            {
                stream.Seek(0x40, SeekOrigin.Begin);
                file.CheckSum = reader.ReadUInt32(bigEndian);
                file.CheckSumValid = file.CheckSum == actualCheck;

                stream.Seek(0x4040, SeekOrigin.Begin);
                file.SaveInfo = SaveInfo.FromReader(reader, platform);

                long addr = stream.Position;
                for (int i = 0; i < 3; i++)
                {
                    file.Slots[i] = SaveSlot.FromReader(reader, platform);

                    if (i != 2)
                    {
                        //Seek forward the GameStateSize + the slot header data
                        stream.Seek(addr + file.SaveInfo.GameStateSize + 0x8, SeekOrigin.Begin);
                        addr = stream.Position;
                    }
                }
            }

            return file;
        }

        public void ToFileStream(FileStream stream)
        {
            if (!stream.CanWrite || !stream.CanRead)
                throw new ArgumentException("Provided stream needs to be both readable and writeable.");

            bool bigEndian = Platform == GamePlatform.GameCube;

            using (BinaryWriter writer = new(stream, Encoding.UTF8, true))
            {
                //Write new checksum later
                stream.Seek(0x4040, SeekOrigin.Begin);
                SaveInfo.ToWriter(writer, Platform);

                long addr = stream.Position;
                for (int i = 0; i < Slots.Length; i++)
                {
                    Slots[i].ToWriter(writer, Platform);

                    if (i != 2)
                    {
                        //Seek forward the GameStateSize + the slot header data
                        stream.Seek(addr + SaveInfo.GameStateSize + 0x8, SeekOrigin.Begin);
                        addr = stream.Position;
                    }
                }

                //Now that we're done, write a new checksum
                uint newChecksum = GetCheckSum(stream, Platform);
                stream.Seek(0x40, SeekOrigin.Begin);
                writer.Write(newChecksum, bigEndian);
            }
        }

        public static uint GetCheckSum(FileStream stream, GamePlatform platform)
        {
            //this only works for GameCube so far, use the platform parameter to get the correct byte range
            if (platform != GamePlatform.GameCube)
                throw new NotImplementedException("Only GameCube supported.");

            using (BinaryReader reader = new(stream, Encoding.UTF8, true))
            {
                stream.Seek(0x1c0, SeekOrigin.Begin);

                byte[] bytes = new byte[stream.Length - stream.Position];

                int i = 0;
                while (stream.Position < stream.Length)
                {
                    bytes[i] = reader.ReadByte();
                    i++;
                }

                byte[] hashBytes = Crc32.Hash(bytes);

                uint hash = BitConverter.ToUInt32(hashBytes);

                return hash;
            }
        }
    }
}
