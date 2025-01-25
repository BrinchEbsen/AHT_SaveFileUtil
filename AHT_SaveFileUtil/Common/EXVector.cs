using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil.Common
{
    public class EXVector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public EXVector() { }

        public EXVector(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public EXVector(EXVector other)
        {
            X = other.X;
            Y = other.Y;
            Z = other.Z;
            W = other.W;
        }

        public EXVector(byte[] buffer, bool bigEndian = false)
        {
            if (buffer.Length < 16)
                throw new ArgumentException("Insufficient data in byte buffer to construct vector.");

            if (bigEndian)
            {
                Array.Reverse(buffer, 4 * 0, 4);
                Array.Reverse(buffer, 4 * 1, 4);
                Array.Reverse(buffer, 4 * 2, 4);
                Array.Reverse(buffer, 4 * 3, 4);
            }

            X = BitConverter.ToSingle(buffer, 4 * 0);
            Y = BitConverter.ToSingle(buffer, 4 * 1);
            Z = BitConverter.ToSingle(buffer, 4 * 2);
            W = BitConverter.ToSingle(buffer, 4 * 3);
        }
    }
}
