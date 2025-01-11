using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using System;
using System.IO;

namespace AHT_SaveFileUtil.Save.Slot
{
    /// <summary>
    /// The current operation of a stack entry.
    /// </summary>
    public enum PreserveMode
    {
        Stop = 0,
        Read = 1,
        Write = 2,
        GetSize = 3
    }

    /// <summary>
    /// Entry into the BitHeap stack.
    /// </summary>
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

    /// <summary>
    /// Stores data in a stream of bits 0x4000 bytes long.
    /// Data can be allocated, written to and read from.
    /// </summary>
    public class BitHeap : ISaveFileIO<BitHeap>
    {
        /// <summary>
        /// Size of the bitheap in bytes.
        /// </summary>
        public const int BYTEHEAP_LENGTH = 0x4000;

        /// <summary>
        /// Size of the bitheap in bits.
        /// </summary>
        public const int BITHEAP_LENGTH = BYTEHEAP_LENGTH * 8;

        public byte[] ByteHeap { get; private set; } = new byte[BYTEHEAP_LENGTH];

        public int NumBitsUsed { get; private set; }

        public int StackPtr { get; private set; }

        public StackEntry[] Stack { get; private set; } = new StackEntry[32];

        /// <summary>
        /// Whether the object's data is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (ByteHeap == null)
                    return false;

                if (ByteHeap.Length != BYTEHEAP_LENGTH)
                    return false;

                if (NumBitsUsed < 0)
                    return false;

                return true;
            }
        }

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


        //BITHEAP OPERATIONS:


        /// <summary>
        /// Read a string of bits from the bitheap.
        /// </summary>
        /// <param name="bitCount">Number of bits to read.</param>
        /// <param name="readAddress">Bit address to start reading from.</param>
        /// <param name="writeBit">The bit in the first byte of the returned buffer to start writing to.</param>
        /// <returns>A buffer of bytes with the data read from the heap.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public byte[] ReadBits(int bitCount, int readAddress, int writeBit = 0)
        {
            if (bitCount <= 0) return [];

            if (readAddress < 0)
                throw new ArgumentException($"Parameter {nameof(readAddress)} cannot be negative.");

            if (writeBit < 0)
                throw new ArgumentException($"Parameter {nameof(writeBit)} cannot be negative.");

            if (writeBit > 7)
                throw new ArgumentOutOfRangeException(nameof(writeBit));

            //Check if resulting read parameters would exceed number of bits in the heap.
            if (readAddress + bitCount > (ByteHeap.Length * 8))
                throw new ArgumentOutOfRangeException(nameof(readAddress));
            
            /*
             * PRE:
             * - bitCount is a positive non-zero integer.
             * - readAddress is a positive non-zero integer.
             * - writeBit is an integer between 0 and 7.
             * - The last bit to be read would not be outside the bounds of the bitheap.
             */

            //Current byte/bit index of the heap
            int readByte = readAddress / 8;
            int readBit = readAddress & 7;

            //Create buffer
            byte[] buffer = new byte[GetNumRequiredBytes(bitCount + writeBit)];

            //Current index into buffer
            int writeByte = 0;

            /*
             * readBit/Byte:  Index into bitheap.
             * writeBit/Byte: Index into buffer.
             */

            //Run through all bits
            for (; bitCount > 0; bitCount--)
            {
                //If current bit is 1, write it to the buffer
                if (((ByteHeap[readByte] >> readBit) & 1) != 0)
                    buffer[writeByte] |= (byte)(1 << writeBit);

                //Go to next bit and check for rollover:

                //Read address:
                readBit++;
                if (readBit > 7)
                {
                    //Start from the first bit of the next byte
                    readBit = 0;
                    readByte++;
                }

                //Write address:
                writeBit++;
                if (writeBit > 7)
                {
                    writeBit = 0;
                    writeByte++;
                }
            }

            return buffer;
        }

        /// <summary>
        /// Write a string of bits to the bitheap.
        /// </summary>
        /// <param name="bitCount">Number of bits to write.</param>
        /// <param name="buffer">Buffer of bytes with the data to write.</param>
        /// <param name="writeAddress">Bit address to start writing to.</param>
        /// <param name="readBit">The bit in the first byte of the given buffer to start reading from.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void WriteBits(int bitCount, byte[] buffer, int writeAddress, int readBit = 0)
        {
            if (bitCount <= 0) return;

            if (writeAddress < 0)
                throw new ArgumentException($"Parameter {nameof(writeAddress)} cannot be negative.");

            if (readBit < 0)
                throw new ArgumentException($"Parameter {nameof(readBit)} cannot be negative.");

            if (readBit > 7)
                throw new ArgumentOutOfRangeException(nameof(readBit));

            //Check if resulting write parameters would exceed number of bits in the heap.
            if (writeAddress + bitCount > (ByteHeap.Length * 8))
                throw new ArgumentOutOfRangeException(nameof(writeAddress));

            //Check that there are enough bytes in the buffer to read from.
            if (GetNumRequiredBytes(bitCount + readBit) > buffer.Length)
                throw new ArgumentException("Not enough bytes in given buffer.");

            /*
             * PRE:
             * - bitCount is a positive non-zero integer.
             * - writeAddress is a positive non-zero integer.
             * - readBit is an integer between 0 and 7.
             * - The last bit to be written would not be outside the bounds of the bitheap.
             * - There are enough bytes in the buffer to contain the number of bits to write from.
             */

            //Current byte/bit index of the heap
            int writeByte = writeAddress / 8;
            int writeBit = writeAddress & 7;

            //Current index into buffer
            int readByte = 0;

            /*
             * readBit/Byte:  Index into buffer.
             * writeBit/Byte: Index into bitheap.
             */

            for (; bitCount > 0; bitCount--)
            {
                //If current bit is 1, write it to the heap, else clear it
                if (((buffer[readByte] >> readBit) & 1) != 0)
                    ByteHeap[writeByte] |= (byte)(1 << writeBit);
                else
                    //Write inverse
                    ByteHeap[writeByte] |= (byte)~(1 << writeBit);

                //Go to next bit and check for rollover:

                //Read address:
                readBit++;
                if (readBit > 7)
                {
                    //Start from the first bit of the next byte
                    readBit = 0;
                    readByte++;
                }

                //Write address:
                writeBit++;
                if (writeBit > 7)
                {
                    writeBit = 0;
                    writeByte++;
                }
            }
        }

        /// <summary>
        /// Get the amount of bytes required to fit a number of bits.
        /// </summary>
        /// <param name="bitCount">The number of bits.</param>
        /// <returns>The amount of bytes needed to contain the given number of bits.</returns>
        private static int GetNumRequiredBytes(int bitCount)
        {
            if (bitCount <= 0) return 0;

            return (bitCount / 8) //Number of sets of 8 bits
                 + (bitCount % 8 == 0 ? 0 : 1); //An extra byte if there are leftover bits
        }
    }
}
