using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using System;
using System.IO;
using System.Reflection.PortableExecutable;

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
        #region Constants

        /// <summary>
        /// Size of the bitheap in bytes.
        /// </summary>
        public const int BYTEHEAP_LENGTH = 0x4000;

        /// <summary>
        /// Size of the bitheap in bits.
        /// </summary>
        public const int BITHEAP_LENGTH = BYTEHEAP_LENGTH * 8;

        #endregion

        #region Properties

        /// <summary>
        /// The array of bytes containing the data of the bitheap.
        /// </summary>
        public byte[] ByteHeap { get; private set; } = new byte[BYTEHEAP_LENGTH];

        /// <summary>
        /// The amount of bits currently used in the heap.
        /// </summary>
        public int NumBitsUsed { get; private set; }

        /// <summary>
        /// The stack pointer. Only used during runtime.
        /// </summary>
        public int StackPtr { get; private set; }

        /// <summary>
        /// The heap stack. Only used during runtime.
        /// </summary>
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

        #endregion

        private BitHeap() { }

        #region Savefile Read/Write

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

        #endregion

        #region BitHeap Operations

        #region General

        /// <summary>
        /// Allocate a range of bits on the bitheap.
        /// </summary>
        /// <param name="bitCount">Number of bits to allocate.</param>
        /// <returns>The address at the start of the allocated range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="OverflowException"></exception>
        public int Allocate(int bitCount)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(bitCount, 0);

            if ((NumBitsUsed + bitCount) > BITHEAP_LENGTH)
                throw new OverflowException(
                    $"Cannot allocate {bitCount} bits, as there are only " +
                    $"{BITHEAP_LENGTH - NumBitsUsed} bits free.");

            //Get the address to allocate from
            int writeAddress = NumBitsUsed;

            //Clear the allocated space
            ClearBits(writeAddress, bitCount);

            //Increase the number of bits used
            NumBitsUsed += bitCount;

            return writeAddress;
        }

        /// <summary>
        /// Set a range of bits to zero.
        /// </summary>
        /// <param name="address">Address to start clearing from.</param>
        /// <param name="bitCount">Number of bits to clear.</param>
        public void ClearBits(int address, int bitCount)
        {
            byte[] zeros = new byte[GetNumRequiredBytes(bitCount)];
            WriteBits(bitCount, zeros, address);
        }

        /// <summary>
        /// Set a range of bits to one.
        /// </summary>
        /// <param name="address">Address to start writing from.</param>
        /// <param name="bitCount">Number of bits to set.</param>
        public void SetBits(int address, int bitCount)
        {
            byte[] ones = new byte[GetNumRequiredBytes(bitCount)];

            for(int i = 0; i < ones.Length; i++)
                ones[i] = 0xFF;

            WriteBits(bitCount, ones, address);
        }

        /// <summary>
        /// Clear every bit in the bitheap.
        /// </summary>
        public void ClearAll()
        {
            for (int i = 0; i < ByteHeap.Length; i++)
                ByteHeap[i] = 0;

            NumBitsUsed = 0;
        }

        #endregion

        #region Reading

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

            if (!ValidBitHeapAddress(readAddress, bitCount))
                throw new ArgumentException(
                    $"{nameof(readAddress)} and {nameof(bitCount)} map to out-of-bounds addresses.");

            ArgumentOutOfRangeException.ThrowIfLessThan(writeBit, 0);

            ArgumentOutOfRangeException.ThrowIfGreaterThan(writeBit, 7);

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
        /// Read an <see cref="int"/> from the bitheap.
        /// </summary>
        /// <param name="readAddress">Starting bit to read from.</param>
        /// <param name="bigEndian">Whether the value should be read as big endian.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int ReadInt32(int readAddress, bool bigEndian = false)
        {
            byte[] buff = ReadBits(32, readAddress);

            if (bigEndian) Array.Reverse(buff);

            return BitConverter.ToInt32(buff, 0);
        }

        /// <summary>
        /// Read a <see cref="uint"/> from the bitheap.
        /// </summary>
        /// <param name="readAddress">Starting bit to read from.</param>
        /// <param name="bigEndian">Whether the value should be read as big endian.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint ReadUInt32(int readAddress, bool bigEndian = false)
        {
            byte[] buff = ReadBits(32, readAddress);

            if (bigEndian) Array.Reverse(buff);

            return BitConverter.ToUInt32(buff, 0);
        }

        /// <summary>
        /// Read a <see cref="float"/> from the bitheap.
        /// </summary>
        /// <param name="readAddress">Starting bit to read from.</param>
        /// <param name="bigEndian">Whether the value should be read as big endian.</param>
        /// <returns>The <see cref="float"/>.</returns>
        public float ReadSingle(int readAddress, bool bigEndian = false)
        {
            byte[] buff = ReadBits(32, readAddress);

            if (bigEndian) Array.Reverse(buff);

            return BitConverter.ToSingle(buff, 0);
        }

        /// <summary>
        /// Read an <see cref="EXVector"/> from the bitheap.
        /// </summary>
        /// <param name="readAddress">Starting bit to read from.</param>
        /// <param name="bigEndian">Whether the value should be read as big endian.</param>
        /// <returns>The <see cref="EXVector"/>.</returns>
        public EXVector ReadEXVector(int readAddress, bool bigEndian = false)
        {
            return new EXVector()
            {
                X = ReadSingle(readAddress + 32 * 0, bigEndian),
                Y = ReadSingle(readAddress + 32 * 1, bigEndian),
                Z = ReadSingle(readAddress + 32 * 2, bigEndian),
                W = ReadSingle(readAddress + 32 * 3, bigEndian)
            };
        }

        /// <summary>
        /// Read a <see cref="short"/> from the bitheap.
        /// </summary>
        /// <param name="readAddress">Starting bit to read from.</param>
        /// <param name="bigEndian">Whether the value should be read as big endian.</param>
        /// <returns>The <see cref="short"/>.</returns>
        public short ReadInt16(int readAddress, bool bigEndian = false)
        {
            byte[] buff = ReadBits(16, readAddress);

            if (bigEndian) Array.Reverse(buff);

            return BitConverter.ToInt16(buff, 0);
        }

        /// <summary>
        /// Read a <see cref="ushort"/> from the bitheap.
        /// </summary>
        /// <param name="readAddress">Starting bit to read from.</param>
        /// <param name="bigEndian">Whether the value should be read as big endian.</param>
        /// <returns>The <see cref="ushort"/>.</returns>
        public ushort ReadUInt16(int readAddress, bool bigEndian = false)
        {
            byte[] buff = ReadBits(16, readAddress);

            if (bigEndian) Array.Reverse(buff);

            return BitConverter.ToUInt16(buff, 0);
        }

        /// <summary>
        /// Read a <see cref="byte"/> from the bitheap.
        /// </summary>
        /// <param name="readAddress">Starting bit to read from.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        public byte ReadByte(int readAddress)
        {
            return ReadBits(8, readAddress)[0];
        }

        /// <summary>
        /// Read a <see cref="uint"/> value from the bitheap,
        /// with the desired bits selected with a <paramref name="mask"/>.
        /// </summary>
        /// <param name="readAddress">Address to read bits from.</param>
        /// <param name="mask">Mask dictating which bits in the value to read to.</param>
        /// <param name="bigEndian">Whether the value should be read as big endian.</param>
        /// <returns>The value.</returns>
        public uint ReadBitMask(int readAddress, uint mask, bool bigEndian = false)
        {
            if (mask == 0) return 0;
            if (mask == 0xFFFFFFFF) return ReadUInt32(readAddress, bigEndian);

            if (bigEndian)
                mask = ReverseUIntBytes(mask);

            int numBits = CountBits(mask);

            var reader = new BitSpanReader(ReadBits(numBits, readAddress), numBits, 0);

            uint value = 0;
            
            for (int i = 0; i < 32; i++)
            {
                if ((mask & (1 << i)) != 0)
                {
                    byte b = reader.NextBit;
                    value |= (uint)(b << i);
                }
            }

            //Swap back
            if (bigEndian)
                value = ReverseUIntBytes(value);

            return value;
        }

        #endregion

        #region Writing

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

            if (!ValidBitHeapAddress(writeAddress, bitCount))
                throw new ArgumentException(
                    $"{nameof(writeAddress)} and {nameof(bitCount)} map to out-of-bounds addresses.");

            ArgumentOutOfRangeException.ThrowIfLessThan(readBit, 0);

            ArgumentOutOfRangeException.ThrowIfGreaterThan(readBit, 7);

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
                    ByteHeap[writeByte] &= (byte)~(1 << writeBit);

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
        /// Write an <see cref="int"/> to the bitheap.
        /// </summary>
        /// <param name="writeAddress">Starting bit to write to.</param>
        /// <param name="value">The <see cref="int"/> to write.</param>
        /// <param name="bigEndian">Whether the value should be written as big endian.</param>
        public void Write(int writeAddress, int value, bool bigEndian = false)
        {
            byte[] buff = BitConverter.GetBytes(value);

            if (bigEndian) Array.Reverse(buff);

            WriteBits(32, buff, writeAddress);
        }

        /// <summary>
        /// Write a <see cref="uint"/> to the bitheap.
        /// </summary>
        /// <param name="writeAddress">Starting bit to write to.</param>
        /// <param name="value">The <see cref="uint"/> to write.</param>
        /// <param name="bigEndian">Whether the value should be written as big endian.</param>
        public void Write(int writeAddress, uint value, bool bigEndian = false)
        {
            byte[] buff = BitConverter.GetBytes(value);

            if (bigEndian) Array.Reverse(buff);

            WriteBits(32, buff, writeAddress);
        }

        /// <summary>
        /// Write a <see cref="float"/> to the bitheap.
        /// </summary>
        /// <param name="writeAddress">Starting bit to write to.</param>
        /// <param name="value">The <see cref="float"/> to write.</param>
        /// <param name="bigEndian">Whether the value should be written as big endian.</param>
        public void Write(int writeAddress, float value, bool bigEndian = false)
        {
            byte[] buff = BitConverter.GetBytes(value);

            if (bigEndian) Array.Reverse(buff);

            WriteBits(32, buff, writeAddress);
        }

        /// <summary>
        /// Write an <see cref="EXVector"/> to the bitheap.
        /// </summary>
        /// <param name="writeAddress">Starting bit to write to.</param>
        /// <param name="value">The <see cref="EXVector"/> to write.</param>
        /// <param name="bigEndian">Whether the value should be written as big endian.</param>
        public void Write(int writeAddress, EXVector value, bool bigEndian = false)
        {
            Write(writeAddress + 32 * 0, value.X, bigEndian);
            Write(writeAddress + 32 * 1, value.Y, bigEndian);
            Write(writeAddress + 32 * 2, value.Z, bigEndian);
            Write(writeAddress + 32 * 3, value.W, bigEndian);
        }

        /// <summary>
        /// Write a <see cref="short"/> to the bitheap.
        /// </summary>
        /// <param name="writeAddress">Starting bit to write to.</param>
        /// <param name="value">The <see cref="short"/> to write.</param>
        /// <param name="bigEndian">Whether the value should be written as big endian.</param>
        public void Write(int writeAddress, short value, bool bigEndian = false)
        {
            byte[] buff = BitConverter.GetBytes(value);

            if (bigEndian) Array.Reverse(buff);

            WriteBits(16, buff, writeAddress);
        }

        /// <summary>
        /// Write a <see cref="ushort"/> to the bitheap.
        /// </summary>
        /// <param name="writeAddress">Starting bit to write to.</param>
        /// <param name="value">The <see cref="ushort"/> to write.</param>
        /// <param name="bigEndian">Whether the value should be written as big endian.</param>
        public void Write(int writeAddress, ushort value, bool bigEndian = false)
        {
            byte[] buff = BitConverter.GetBytes(value);

            if (bigEndian) Array.Reverse(buff);

            WriteBits(16, buff, writeAddress);
        }

        /// <summary>
        /// Write a <see cref="byte"/> to the bitheap.
        /// </summary>
        /// <param name="writeAddress">Starting bit to write to.</param>
        /// <param name="value">The <see cref="byte"/> to write.</param>
        public void Write(int writeAddress, byte value)
        {
            WriteBits(8, [value], writeAddress);
        }

        /// <summary>
        /// Write a <see cref="uint"/> value to the bitheap,
        /// with the desired bits selected with a <paramref name="mask"/>.
        /// </summary>
        /// <param name="writeAddress">Address to start writing to.</param>
        /// <param name="mask">Mask dictating which bits in the value to write from.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="bigEndian">Whether the value should be read as big endian.</param>
        public void WriteBitMask(int writeAddress, uint mask, uint value, bool bigEndian = false)
        {
            if (mask == 0) return;
            if (mask == 0xFFFFFFFF) Write(writeAddress, value, bigEndian);

            if (bigEndian)
            {
                mask = ReverseUIntBytes(mask);
                value = ReverseUIntBytes(value);
            }

            uint bits = 0;
            int bitIndex = 0;

            for (int i = 0; i < 32; i++)
            {
                if ((mask & (1 << i)) != 0)
                {
                    //Extract the bit from value
                    uint v = value & (uint)(1 << i);
                    v >>= i;

                    //Merge with bits
                    bits |= (v << bitIndex);
                    bitIndex++;
                }
            }

            int numBits = CountBits(mask);

            byte[] bytes = BitConverter.GetBytes(bits);
            WriteBits(numBits, bytes, writeAddress);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Get the amount of bytes required to fit a number of bits.
        /// </summary>
        /// <param name="bitCount">The number of bits.</param>
        /// <returns>The amount of bytes needed to contain the given number of bits.</returns>
        public static int GetNumRequiredBytes(int bitCount)
        {
            if (bitCount <= 0) return 0;

            return (bitCount / 8) //Number of sets of 8 bits
                 + (bitCount % 8 == 0 ? 0 : 1); //An extra byte if there are leftover bits
        }

        /// <summary>
        /// Check if a bitheap address or range of bits is valid.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="range">The number of bits to check ahead of <paramref name="address"/>.</param>
        /// <returns>Whether the given address/range is valid.</returns>
        public static bool ValidBitHeapAddress(int address, int range = 0)
        {
            if (address < 0) return false;
            if (range < 0) return false;

            if (address + range >= BITHEAP_LENGTH) return false;

            return true;
        }

        /// <summary>
        /// Check if a bitheap address or range of bits is valid and within the allocated space.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="range">The number of bits to check ahead of <paramref name="address"/>.</param>
        /// <returns>Whether the given address/range is valid and within the allocated space.</returns>
        public bool ValidAllocatedBitHeapAddress(int address, int range = 0)
        {
            if (!ValidBitHeapAddress(address, range)) return false;

            if (address + range >= NumBitsUsed) return false;

            return true;
        }

        /// <summary>
        /// Count how many bits are set in a value.
        /// </summary>
        /// <param name="value">The value to count bits of.</param>
        /// <returns>The number of bits in <paramref name="value"/> that are set.</returns>
        public static int CountBits(uint value)
        {
            int numBits = 0;

            while (value > 0)
            {
                if ((value & 1) != 0)
                   numBits++;

                value >>= 1;
            }

            return numBits;
        }

        /// <summary>
        /// Reverse the byte order of a <see cref="uint"/> value.
        /// </summary>
        /// <param name="value">The value to swap.</param>
        /// <returns><paramref name="value"/>, with its bytes in reverse order.</returns>
        public static uint ReverseUIntBytes(uint value)
        {
            return ((value & 0x000000ff) << 24) +
                   ((value & 0x0000ff00) << 8) +
                   ((value & 0x00ff0000) >> 8) +
                   ((value & 0xff000000) >> 24);
        }

        #endregion

        #endregion
    }
}
