using Common;
using Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil.Save.Slot
{
    public class SaveSlot
    {
        public uint CheckSum { get; private set; }

        private byte UsedFlag;
        private byte DisplayedSaveMessageFlag;
        private byte SpareFlag2;
        private byte SpareFlag3;

        public bool IsUsed => UsedFlag != 0;
        public bool DisplayedSaveMessage => DisplayedSaveMessageFlag != 0;

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

            slot.GameState = GameState.FromReader(reader, platform);

            return slot;
        }

        public override string ToString()
        {
            if (!IsUsed) return "Not used";

            return GameState.ToString();
        }
    }
}
