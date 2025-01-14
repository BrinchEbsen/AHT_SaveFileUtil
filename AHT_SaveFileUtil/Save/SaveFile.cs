using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using AHT_SaveFileUtil.Save.Slot;
using System;
using System.IO;
using System.IO.Hashing;
using System.Text;

namespace AHT_SaveFileUtil.Save
{
    public class SaveFile : ISaveFileIO<SaveFile>
    {
        public GamePlatform Platform { get; private set; }

        public bool UsesCheckSum { get; private set; }

        public uint CheckSum { get; private set; } = 0;

        public bool CheckSumValid { get; private set; } = true;

        public SaveInfo SaveInfo { get; private set; }

        public SaveSlot[] Slots { get; private set; } = new SaveSlot[3];

        private SaveFile() { }

        #region InputOutput
        public static SaveFile FromFileStream(FileStream stream, GamePlatform platform)
        {
            if (!stream.CanRead)
                throw new ArgumentException("Provided stream needs to be readable.");

            if (platform == GamePlatform.Xbox)
                throw new NotImplementedException("Xbox not supported yet!");

            bool bigEndian = platform == GamePlatform.GameCube;

            using (BinaryReader reader = new(stream, Encoding.UTF8, true))
            {
                long dataStart = FindDataStart(stream, platform);

                if (dataStart < 0)
                    throw new IOException("Could not find start of data.");

                stream.Seek(dataStart, SeekOrigin.Begin);

                SaveFile file = FromReader(reader, platform);

                file.Platform = platform;

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

                return file;
            }
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

                ToWriter(writer, Platform);

                if (Platform == GamePlatform.GameCube)
                {
                    //Now that we're done, write a new checksum
                    uint newChecksum = GetGCCheckSum(stream, Platform);
                    stream.Seek(0x40, SeekOrigin.Begin);
                    writer.Write(newChecksum, bigEndian);
                }
            }
        }

        public static SaveFile FromReader(BinaryReader reader, GamePlatform platform)
        {
            SaveFile file = new SaveFile();

            file.SaveInfo = SaveInfo.FromReader(reader, platform);

            long addr = reader.BaseStream.Position;
            for (int i = 0; i < 3; i++)
            {
                file.Slots[i] = SaveSlot.FromReader(reader, platform);

                if (i != 2)
                {
                    //Seek forward the GameStateSize + the slot header data
                    reader.BaseStream.Seek(addr + file.SaveInfo.GameStateSize + 0x8, SeekOrigin.Begin);

                    if (platform == GamePlatform.PlayStation2)
                        reader.BaseStream.Seek(8, SeekOrigin.Current);

                    addr = reader.BaseStream.Position;
                }
            }

            return file;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            SaveInfo.ToWriter(writer, Platform);

            long addr = writer.BaseStream.Position;
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].ToWriter(writer, Platform);

                if (i != 2)
                {
                    //Seek forward the GameStateSize + the slot header data
                    writer.BaseStream.Seek(addr + SaveInfo.GameStateSize + 0x8, SeekOrigin.Begin);

                    if (Platform == GamePlatform.PlayStation2)
                        writer.BaseStream.Seek(8, SeekOrigin.Current);

                    addr = writer.BaseStream.Position;
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
        #endregion

        /// <summary>
        /// Update the usage flag on all save slots.
        /// </summary>
        public void ValidateSlotUsageFlags()
        {
            for (int i = 0; i < 3; i++)
            {
                //If slot isn't used, write 0 to the flag
                if (!Slots[i].IsUsed)
                {
                    Slots[i].UsageFlag = 0;
                    SaveInfo.UsageFlags[i] = 0;
                }
                //If the slot is used and the flag is 0 (or the flags don't match), regenerate.
                else if (Slots[i].UsageFlag == 0)
                    RegenerateSlotUsageFlag(i);
                else if (Slots[i].UsageFlag != SaveInfo.UsageFlags[i])
                    RegenerateSlotUsageFlag(i);
            }
        }

        /// <summary>
        /// Generate a new usage flag for a slot.
        /// </summary>
        /// <param name="slot">Slot index to generate the usage flag for. </param>
        public void RegenerateSlotUsageFlag(int slot)
        {
            uint newFlag = EXRand.Rand32();

            Slots[slot].UsageFlag = newFlag;
            SaveInfo.UsageFlags[slot] = newFlag;
        }
    }
}
