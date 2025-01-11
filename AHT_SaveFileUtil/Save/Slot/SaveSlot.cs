using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil.Save.Slot
{
    public class SaveSlot : ISaveFileIO<SaveSlot>
    {
        public uint CheckSum { get; private set; }

        private byte UsedFlag;
        private byte DisplayedSaveMessageFlag;
        private byte SpareFlag2;
        private byte SpareFlag3;

        public bool IsUsed
        {
            get => UsedFlag != 0;
            set => UsedFlag = value ? (byte)1 : (byte)0;
        }
        public bool DisplayedSaveMessage
        {
            get => DisplayedSaveMessageFlag != 0;
            set => DisplayedSaveMessageFlag = value ? (byte)1 : (byte)0;
        }

        public GameState GameState { get; private set; }

        private SaveSlot() { }

        public static SaveSlot FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            var slot = new SaveSlot();

            slot.CheckSum = reader.ReadUInt32(bigEndian);

            slot.UsedFlag = reader.ReadByte();
            slot.DisplayedSaveMessageFlag = reader.ReadByte();
            slot.SpareFlag2 = reader.ReadByte();
            slot.SpareFlag3 = reader.ReadByte();

            if (platform == GamePlatform.PlayStation2)
                reader.BaseStream.Seek(8, SeekOrigin.Current);

            slot.GameState = GameState.FromReader(reader, platform);

            return slot;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            writer.Write(CheckSum, bigEndian);
            writer.Write(UsedFlag);
            writer.Write(DisplayedSaveMessageFlag);
            writer.Write(SpareFlag2);
            writer.Write(SpareFlag3);

            if (platform == GamePlatform.PlayStation2)
                writer.BaseStream.Seek(8, SeekOrigin.Current);

            GameState.ToWriter(writer, platform);
        }

        public override string ToString()
        {
            if (!IsUsed) return "Not used";

            return GameState.ToString();
        }
    }
}
