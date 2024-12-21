using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil.Save.Slot
{
    public enum PreserveMode
    {
        Stop = 0,
        Read = 1,
        Write = 2,
        GetSize = 3
    }

    public class StackEntry : ISaveFileIO<StackEntry>
    {
        public int Start;
        public int Address;
        public int End;
        public PreserveMode Mode;
        public int BeenWrittenFlag;

        private StackEntry() { }

        public static StackEntry FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            var entry = new StackEntry();

            entry.Start = reader.ReadInt32(bigEndian);
            entry.Address = reader.ReadInt32(bigEndian);
            entry.End = reader.ReadInt32(bigEndian);

            int mode = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(PreserveMode), mode))
                throw new IOException($"Invalid value for PreserveMode: {mode}");

            entry.Mode = (PreserveMode)mode;
            entry.BeenWrittenFlag = reader.ReadInt32(bigEndian);

            return entry;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            writer.Write(Start, bigEndian);
            writer.Write(Address, bigEndian);
            writer.Write(End, bigEndian);
            writer.Write((int)Mode, bigEndian);
            writer.Write(BeenWrittenFlag, bigEndian);
        }
    }

    public class BitHeap : ISaveFileIO<BitHeap>
    {
        public byte[] ByteHeap { get; private set; } = new byte[0x4000];

        public int NumBitsUsed { get; private set; }

        public int StackPtr { get; private set; }

        public StackEntry[] Stack { get; private set; } = new StackEntry[32];

        private BitHeap() { }

        public static BitHeap FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            var heap = new BitHeap();

            //0x4000 bytes
            for (int i = 0; i < heap.ByteHeap.Length; i++)
                heap.ByteHeap[i] = reader.ReadByte();

            heap.NumBitsUsed = reader.ReadInt32(bigEndian);

            heap.StackPtr = reader.ReadInt32(bigEndian);

            for (int i = 0; i < heap.Stack.Length; i++)
                heap.Stack[i] = StackEntry.FromReader(reader, platform);

            return heap;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            foreach (byte b in ByteHeap)
                writer.Write(b);

            writer.Write(NumBitsUsed, bigEndian);
            writer.Write(StackPtr, bigEndian);

            foreach(var entry in Stack)
                entry.ToWriter(writer, platform);
        }
    }
}
