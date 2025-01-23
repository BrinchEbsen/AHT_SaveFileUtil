using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
