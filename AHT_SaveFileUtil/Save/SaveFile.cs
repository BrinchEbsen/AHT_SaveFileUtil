using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using AHT_SaveFileUtil.Save.Slot;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Text;

namespace AHT_SaveFileUtil.Save
{
    public class SaveFile
    {
        public static readonly Dictionary<GamePlatform, long> SAVE_OFFSET_CHECKSUM = new()
        {
            [GamePlatform.GameCube] = 0x40,
            [GamePlatform.PlayStation2] = 0, /* ??? */
            [GamePlatform.Xbox] = 0, /* ??? */
        };

        public static readonly Dictionary<GamePlatform, long> SAVE_OFFSET_CHECKSUM_START = new()
        {
            [GamePlatform.GameCube] = 0x1c0,
            [GamePlatform.PlayStation2] = 0, /* ??? */
            [GamePlatform.Xbox] = 0 /* ??? */
        };

        public static readonly Dictionary<GamePlatform, long> SAVE_OFFSET_DATA = new()
        {
            [GamePlatform.GameCube] = 0x4040,
            [GamePlatform.PlayStation2] = 0x800,
            [GamePlatform.Xbox] = 0 /* ??? */
        };

        public GamePlatform Platform { get; private set; }

        public bool UsesCheckSum { get; private set; }

        public uint CheckSum { get; private set; } = 0;

        public bool CheckSumValid { get; private set; } = true;

        public SaveInfo SaveInfo { get; private set; }

        public SaveSlot[] Slots { get; private set; } = new SaveSlot[3];

        private SaveFile() { }

        public static SaveFile FromFileStream(FileStream stream, GamePlatform platform)
        {
            if (!stream.CanRead)
                throw new ArgumentException("Provided stream needs to be readable.");

            if (platform == GamePlatform.Xbox)
                throw new NotImplementedException("Xbox not supported yet!");

            var file = new SaveFile();

            file.Platform = platform;

            bool bigEndian = platform == GamePlatform.GameCube;

            using (BinaryReader reader = new(stream, Encoding.UTF8, true))
            {
                if (platform == GamePlatform.GameCube)
                {
                    file.UsesCheckSum = true;

                    stream.Seek(0x40, SeekOrigin.Begin);
                    file.CheckSum = reader.ReadUInt32(bigEndian);
                    file.CheckSumValid = file.CheckSum == GetGCCheckSum(stream, platform);
                }
                else
                {
                    file.UsesCheckSum = false;
                }

                long dataStart = FindDataStart(stream, platform);

                if (dataStart < 0)
                    throw new IOException("Could not find start of data.");

                stream.Seek(dataStart, SeekOrigin.Begin);

                //Start reading actual save data
                file.SaveInfo = SaveInfo.FromReader(reader, platform);

                long addr = stream.Position;
                for (int i = 0; i < 3; i++)
                {
                    file.Slots[i] = SaveSlot.FromReader(reader, platform);

                    if (i != 2)
                    {
                        //Seek forward the GameStateSize + the slot header data
                        stream.Seek(addr + file.SaveInfo.GameStateSize + 0x8, SeekOrigin.Begin);

                        if (platform == GamePlatform.PlayStation2)
                            stream.Seek(8, SeekOrigin.Current);

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
                long dataStart = FindDataStart(stream, Platform);

                if (dataStart < 0)
                    throw new IOException("Could not find start of data.");

                stream.Seek(dataStart, SeekOrigin.Begin);
                SaveInfo.ToWriter(writer, Platform);

                long addr = stream.Position;
                for (int i = 0; i < Slots.Length; i++)
                {
                    Slots[i].ToWriter(writer, Platform);

                    if (i != 2)
                    {
                        //Seek forward the GameStateSize + the slot header data
                        stream.Seek(addr + SaveInfo.GameStateSize + 0x8, SeekOrigin.Begin);

                        if (Platform == GamePlatform.PlayStation2)
                            stream.Seek(8, SeekOrigin.Current);

                        addr = stream.Position;
                    }
                }

                if (Platform == GamePlatform.GameCube)
                {
                    //Now that we're done, write a new checksum
                    uint newChecksum = GetGCCheckSum(stream, Platform);
                    stream.Seek(0x40, SeekOrigin.Begin);
                    writer.Write(newChecksum, bigEndian);
                }
            }
        }

        public static uint GetGCCheckSum(FileStream stream, GamePlatform platform)
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

        public static long FindDataStart(FileStream stream, GamePlatform platform)
        {
            return platform switch
            {
                GamePlatform.GameCube     => FindGCDataStart  (stream, platform),
                GamePlatform.PlayStation2 => FindPS2DataStart (stream, platform),
                GamePlatform.Xbox         => FindXboxDataStart(stream, platform),
                _ => -1,
            };
        }

        public static long FindGCDataStart(FileStream stream, GamePlatform platform)
        {
            if (platform != GamePlatform.GameCube)
                throw new ArgumentException("Method for GameCube only.");

            return 0x4040;
        }

        public static long FindPS2DataStart(FileStream stream, GamePlatform platform)
        {
            if (platform != GamePlatform.PlayStation2)
                throw new ArgumentException("Method for PS2 only.");

            using (BinaryReader reader = new(stream, Encoding.UTF8, true))
            {
                stream.Seek(4, SeekOrigin.Begin);

                int numEntries = reader.ReadInt32();

                if (numEntries < 3)
                    throw new IOException("Number of entries in .psu file is too low.");

                long currentAddress = stream.Seek(0x200, SeekOrigin.Begin);

                while ((stream.Position + 0x200) <= stream.Length)
                {
                    int entryID = reader.ReadInt32();

                    //Check for save data ID
                    if (entryID == 0x8497)
                    {
                        stream.Seek(0x3c, SeekOrigin.Current);

                        //Read "DATA0" string in entry name
                        var sb = new StringBuilder();
                        for (int i = 0; i < 5; i++)
                            sb.Append((char)reader.ReadByte());

                        if (sb.ToString() == "DATA0")
                            return currentAddress + 0x200;
                    }

                    currentAddress = stream.Seek(currentAddress + 0x200, SeekOrigin.Begin);
                }
            }

            return -1;
        }

        public static long FindXboxDataStart(FileStream stream, GamePlatform platform)
        {
            if (platform != GamePlatform.Xbox)
                throw new ArgumentException("Method for Xbox only.");

            throw new NotImplementedException();
        }
    }
}
