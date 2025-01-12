using AHT_SaveFileUtil.Extensions;
using AHT_SaveFileUtil.Save;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Transactions;

namespace AHT_SaveFileUtil.Common
{
    public class XAppTime : ISaveFileIO<XAppTime>
    {
        public short Year { get; set; }
        public byte Month { get; set; }
        public byte Day { get; set; }
        public byte Hours { get; set; }
        public byte Minutes { get; set; }
        public byte Seconds { get; set; }

        /*
         * Must match format:
         * D-M-YYYY | H:MM:SS
         */

        /// <summary>
        /// Set the date from a string following the format:
        /// D-M-YYYY | H:MM:SS
        /// </summary>
        /// <param name="str">String to parse into a date.</param>
        /// <returns>true if the parsing succeeded and the date was set, false if not.</returns>
        public bool SetFromString(string str)
        {
            string[] parts = str.Split(" | ");
            if (parts.Length != 2) return false;

            //Parse date
            string[] dateParts = parts[0].Split('-');
            if (dateParts.Length != 3) return false;

            string[] timeParts = parts[1].Split(":");
            if (timeParts.Length != 3) return false;

            byte day;
            byte month;
            short year;
            byte hours;
            byte minutes;
            byte seconds;

            try
            {
                day     = byte.Parse(dateParts[0]);
                month   = byte.Parse(dateParts[1]);
                year    = short.Parse(dateParts[2]);
                hours   = byte.Parse(timeParts[0]);
                minutes = byte.Parse(timeParts[1]);
                seconds = byte.Parse(timeParts[2]);
            } catch (Exception)
            {
                return false;
            }

            Day = day;
            Month = month;
            Year = year;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;

            return true;
        }

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
            bool bigEndian = platform == GamePlatform.GameCube;

            writer.Write(Year, bigEndian);
            writer.Write(Month);
            writer.Write(Day);
            writer.Write(Hours);
            writer.Write(Minutes);
            writer.Write(Seconds);

            writer.BaseStream.Seek(1, SeekOrigin.Current);
        }
    }
}
