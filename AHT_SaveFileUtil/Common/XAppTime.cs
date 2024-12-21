using AHT_SaveFileUtil.Extensions;
using AHT_SaveFileUtil.Save;
using System.IO;

namespace AHT_SaveFileUtil.Common
{
    public class XAppTime : ISaveFileIO<XAppTime>
    {
        public short Year { get; set; }
        public byte Month { get; set; }
        public byte Day { get; set; }
        public byte Hours { get; set; }
        public byte Minutes { get; set; }
        public short Seconds { get; set; }

        public override string ToString()
        {
            return $"{Day}-{Month}-{Year} | {Hours}:{Minutes:00}:{Seconds:00}";
        }

        public static XAppTime FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            var time = new XAppTime
            {
                Year = reader.ReadInt16(bigEndian),
                Month = reader.ReadByte(),
                Day = reader.ReadByte(),
                Hours = reader.ReadByte(),
                Minutes = reader.ReadByte(),
                Seconds = reader.ReadByte()
            };

            reader.BaseStream.Seek(1, SeekOrigin.Current);

            return time;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            throw new System.NotImplementedException();
        }
    }
}
