using Common;
using Extensions;
using System.IO;

namespace AHT_SaveFileUtil.Common
{
    public class XAppTime
    {
        public short Year { get; set; }
        public byte Month { get; set; }
        public byte Day { get; set; }
        public byte Hours { get; set; }
        public byte Minutes { get; set; }
        public short Seconds { get; set; }

        public override string ToString()
        {
            return $"{Day}-{Month}-{Year} | {Hours}:{Minutes.ToString().PadLeft(2, '0')}:{Seconds.ToString().PadLeft(2, '0')}";
        }

        public static XAppTime FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            var time = new XAppTime();

            time.Year = reader.ReadInt16(bigEndian);
            time.Month = reader.ReadByte();
            time.Day = reader.ReadByte();
            time.Hours = reader.ReadByte();
            time.Minutes = reader.ReadByte();
            time.Seconds = reader.ReadByte();
            reader.BaseStream.Seek(1, SeekOrigin.Current);

            return time;
        }
    }
}
