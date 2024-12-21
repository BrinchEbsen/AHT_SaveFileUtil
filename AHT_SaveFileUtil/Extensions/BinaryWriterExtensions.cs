using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace AHT_SaveFileUtil.Extensions
{
    public static class BinaryWriterExtensions
    {
        //Big endian compatible methods (aka swapping bytes using a shit load of bitwise stuff and casts)


        //UInt16
        public static void Write(this BinaryWriter writer, ushort value, bool bigEndian)
        {
            if (bigEndian)
                value = (ushort)((ushort)((value & 0xff) << 8) | value >> 8 & 0xff);

            writer.Write(value);
        }

        //Int16
        public static void Write(this BinaryWriter writer, short value, bool bigEndian)
        {
            writer.Write((ushort)value, bigEndian);
        }

        //UInt32
        public static void Write(this BinaryWriter writer, uint value, bool bigEndian)
        {
            if (bigEndian)
                value = ((value & 0x000000ff) << 24) +
                        ((value & 0x0000ff00) << 8) +
                        ((value & 0x00ff0000) >> 8) +
                        ((value & 0xff000000) >> 24);

            writer.Write(value);
        }

        //Int32
        public static void Write(this BinaryWriter writer, int value, bool bigEndian)
        {
            writer.Write((uint)value, bigEndian);
        }

        //Single (float)
        public static void Write(this BinaryWriter writer, float value, bool bigEndian)
        {
            if (bigEndian)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                writer.Write(BitConverter.ToUInt32(bytes), bigEndian);
            } else
            {
                writer.Write(value);
            }
        }
    }
}