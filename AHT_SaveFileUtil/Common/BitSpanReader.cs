using System;
using System.IO;

namespace AHT_SaveFileUtil.Common
{
    /// <summary>
    /// Wrapper for a buffer of data to be read from incrementally bit-by-bit.
    /// </summary>
    public class BitSpanReader
    {
        private readonly byte[] _buffer;
        private long _bytePosition;
        private int _bitPosition;

        /// <summary>
        /// Length of the buffer in bits.
        /// </summary>
        public long Length { get; private set; }

        public long MaxLength => _buffer.Length * 8;

        /// <summary>
        /// Current bit position of the span.
        /// </summary>
        public long Position => ByteAndBitToBitPosition(_bytePosition, _bitPosition);

        /// <summary>
        /// Get whether the current position is within the bounds of the buffer.
        /// </summary>
        public bool WithinBufferLength
        {
            get
            {
                long pos = Position;

                return pos < Length && pos >= 0;
            }
        }

        /// <summary>
        /// Get the current bit in the sequence and then increment the position.
        /// </summary>
        /// <exception cref="OverflowException"></exception>
        public byte NextBit
        {
            get
            {
                byte b = CurrentBit;
                IncPosition();
                return b;
            }
        }

        /// <summary>
        /// Get the bit at the current position.
        /// </summary>
        /// <exception cref="OverflowException"></exception>
        public byte CurrentBit
        {
            get
            {
                if (!WithinBufferLength)
                    throw new OverflowException("Bit position outside the bounds of buffer.");

                if (_bytePosition >= _buffer.Length)
                    throw new OverflowException("Byte position outside the bounds of buffer.");

                if (_bitPosition < 0 || _bitPosition > 7)
                    throw new OverflowException("Invalid bit position.");

                int val = _buffer[_bytePosition] & (1 << _bitPosition);

                return val != 0 ? (byte)1 : (byte)0;
            }
        }

        /// <summary>
        /// Instantiate a new <see cref="BitSpanReader"/> with a buffer,
        /// and set the bit length to however many bits are in the buffer.
        /// </summary>
        /// <param name="buffer">Provided buffer for the span.</param>
        public BitSpanReader(byte[] buffer)
            : this(buffer, buffer.Length * 8, 0)
        { }

        /// <summary>
        /// Instantiate a new <see cref="BitSpanReader"/> with a buffer, a length in bits,
        /// and a starting bit position.
        /// </summary>
        /// <param name="buffer">Provided buffer for the span.</param>
        /// <param name="length">Length of the span in bits.</param>
        /// <param name="start">Position to start reading from.</param>
        /// <exception cref="ArgumentException"></exception>
        public BitSpanReader(byte[] buffer, long length, long start)
        {
            _buffer = buffer;
            Length = length;

            if (length < 0 || length > (buffer.Length * 8))
                throw new ArgumentException("Length is out of bounds.");

            if (start < 0)
                throw new ArgumentException("Cannot start from negative bit position.");

            if (start >= length)
                throw new ArgumentException("Starting position overlaps with buffer length.");

            var (bytePos, bitPos) = BitPositionToByteAndBit(start);

            _bytePosition = bytePos;
            _bitPosition = bitPos;
        }

        /// <summary>
        /// Set the reader's bit position to <paramref name="pos"/>.
        /// </summary>
        /// <param name="pos">New position for the reader.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Seek(long pos, SeekOrigin origin)
        {
            long newPos = origin switch
            {
                SeekOrigin.Begin => pos,
                SeekOrigin.Current => Position + pos,
                SeekOrigin.End => (Length - 1) - pos,
                _ => throw new ArgumentException($"Invalid value for {nameof(origin)}.")
            };

            if (newPos >= Length || newPos < 0)
                throw new ArgumentException("New position is out of bounds.");

            var (bytePos, bitPos) = BitPositionToByteAndBit(newPos);

            _bytePosition = bytePos;
            _bitPosition = bitPos;
        }

        /// <summary>
        /// Increment the current bit position by 1.
        /// </summary>
        /// <returns>true if the position is within bounds.
        /// If false is returned, attempting to read the current bit will throw an <see cref="OverflowException"/></returns>
        public bool IncPosition()
        {
            _bitPosition++;

            if (_bitPosition > 7)
            {
                _bytePosition++;
                _bitPosition = 0;
            }

            return WithinBufferLength;
        }

        private static (long bytePos, int bitPos) BitPositionToByteAndBit(long bitPos)
        {
            return (bitPos / 8, (int)(bitPos % 8));
        }

        private static long ByteAndBitToBitPosition(long bytePos, int bitPos)
        {
            return (bytePos * 8) + bitPos;
        }
    }
}
