using Common;
using Extensions;
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

    public struct StackEntry
    {
        public int Start;
        public int Address;
        public int End;
        public PreserveMode Mode;
        public int BeenWrittenFlag;

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
    }

    public class BitHeap
    {
        public byte[] ByteHeap { get; private set; } = new byte[0x4000];

        public int NumBitsUsed { get; private set; }

        public int StackPtr { get; private set; }

        public StackEntry[] Stack { get; private set; } = new StackEntry[32];

        private BitHeap() { }

        public static BitHeap FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            BitHeap heap = new BitHeap();

            for (int i = 0; i < heap.ByteHeap.Length; i++)
            {
                heap.ByteHeap[i] = reader.ReadByte();
            }

            heap.NumBitsUsed = reader.ReadInt32(bigEndian);

            heap.StackPtr = reader.ReadInt32(bigEndian);

            for (int i = 0; i < heap.Stack.Length; i++)
            {
                heap.Stack[i] = StackEntry.FromReader(reader, platform);
            }

            return heap;
        }
    }
}
