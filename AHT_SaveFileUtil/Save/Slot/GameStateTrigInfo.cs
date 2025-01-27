using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using System;
using System.IO;
using System.Text;

namespace AHT_SaveFileUtil.Save.Slot
{
    public enum TrigInfoType
    {
        Undefined = 0,
        RestartPoint = 1,
        LightGem = 2,
        MapReveal = 3
    }

    /// <summary>
    /// A saved record of information about a specific type of trigger.
    /// </summary>
    public interface ITrigInfoData { }

    public class TrigInfo_RestartPoint : ISaveFileIO<TrigInfo_RestartPoint>, ITrigInfoData
    {
        public bool HasVisited { get; set; }

        public uint HashCode { get; set; }

        public uint NameTextHashCode { get; set; }

        public static TrigInfo_RestartPoint FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            var data = new TrigInfo_RestartPoint();

            byte b = reader.ReadByte();
            if (bigEndian)
                data.HasVisited = (b & 0x80) != 0;
            else
                data.HasVisited = (b & 0x1) != 0;

            reader.BaseStream.Seek(3, SeekOrigin.Current);

            data.HashCode = reader.ReadUInt32(bigEndian);

            data.NameTextHashCode = reader.ReadUInt32(bigEndian);

            return data;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            if (bigEndian)
                writer.Write(HasVisited ? (byte)0x80 : (byte)0);
            else
                writer.Write(HasVisited ? (byte)0x1 : (byte)0);

            writer.BaseStream.Seek(3, SeekOrigin.Current);

            writer.Write(HashCode, bigEndian);
            writer.Write(NameTextHashCode, bigEndian);
        }
    }

    public class TrigInfo_LightGem : ISaveFileIO<TrigInfo_LightGem>, ITrigInfoData
    {
        public bool IsLight { get; set; }

        public static TrigInfo_LightGem FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            var data = new TrigInfo_LightGem();

            byte b = reader.ReadByte();
            if (bigEndian)
                data.IsLight = (b & 0x80) != 0;
            else
                data.IsLight = (b & 0x1) != 0;

            reader.BaseStream.Seek(11, SeekOrigin.Current);

            return data;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            if (bigEndian)
                writer.Write(IsLight ? (byte)0x80 : (byte)0);
            else
                writer.Write(IsLight ? (byte)0x1 : (byte)0);

            writer.BaseStream.Seek(3, SeekOrigin.Current);

            writer.Write(0);
            writer.Write(0);
        }
    }

    public class TrigInfo_MapReveal : ISaveFileIO<TrigInfo_MapReveal>, ITrigInfoData
    {
        public uint IdentifierHashCode { get; set; }

        public bool IsRevealed { get; set; }

        public static TrigInfo_MapReveal FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            var data = new TrigInfo_MapReveal();

            data.IdentifierHashCode = reader.ReadUInt32(bigEndian);

            byte b = reader.ReadByte();
            if (bigEndian)
                data.IsRevealed = (b & 0x80) != 0;
            else
                data.IsRevealed = (b & 0x1) != 0;

            reader.BaseStream.Seek(7, SeekOrigin.Current);

            return data;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            writer.Write(IdentifierHashCode, bigEndian);

            if (bigEndian)
                writer.Write(IsRevealed ? (byte)0x80 : (byte)0);
            else
                writer.Write(IsRevealed ? (byte)0x1 : (byte)0);

            writer.BaseStream.Seek(3, SeekOrigin.Current);

            writer.Write(0);
        }
    }

    public class GameStateTrigInfo : ISaveFileIO<GameStateTrigInfo>
    {
        public short MapIndex { get; internal set; }

        public short TrigIndex { get; internal set; }

        public EXVector3 XYZ { get; internal set; }

        public TrigInfoType Type { get; internal set; }

        public Type TrigInfoDataType
        {
            get
            {
                return Type switch
                {
                    TrigInfoType.RestartPoint => typeof(TrigInfo_RestartPoint),
                    TrigInfoType.LightGem => typeof(TrigInfo_LightGem),
                    TrigInfoType.MapReveal => typeof(TrigInfo_MapReveal),
                    _ => null,
                };
            }
        }

        public ITrigInfoData Data { get; internal set; }

        public static GameStateTrigInfo FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            var trig = new GameStateTrigInfo();

            trig.MapIndex = reader.ReadInt16(bigEndian);

            trig.TrigIndex = reader.ReadInt16(bigEndian);

            trig.XYZ = new EXVector3
            {
                X = reader.ReadSingle(bigEndian),
                Y = reader.ReadSingle(bigEndian),
                Z = reader.ReadSingle(bigEndian)
            };

            int type = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(TrigInfoType), type))
                throw new IOException("Invalid TrigInfo type: " + type);

            trig.Type = (TrigInfoType)type;

#if DEBUG
            if (trig.Type != TrigInfoType.RestartPoint)
                throw new Exception("Weird gamestate trig info type??!?!? who knows");
#endif

            switch(trig.Type)
            {
                case TrigInfoType.RestartPoint:
                    trig.Data = TrigInfo_RestartPoint.FromReader(reader, platform); break;
                case TrigInfoType.LightGem:
                    trig.Data = TrigInfo_LightGem.FromReader(reader, platform); break;
                case TrigInfoType.MapReveal:
                    trig.Data = TrigInfo_MapReveal.FromReader(reader, platform); break;
                default:
                    //Skip data
                    reader.BaseStream.Seek(12, SeekOrigin.Current); break;
            }

            return trig;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            writer.Write(MapIndex, bigEndian);
            writer.Write(TrigIndex, bigEndian);
            writer.Write(XYZ.X, bigEndian);
            writer.Write(XYZ.Y, bigEndian);
            writer.Write(XYZ.Z, bigEndian);
            writer.Write((int)Type, bigEndian);

            switch (Type)
            {
                case TrigInfoType.RestartPoint:
                    ((TrigInfo_RestartPoint)Data).ToWriter(writer, platform);
                    break;
                case TrigInfoType.LightGem:
                    ((TrigInfo_LightGem)Data).ToWriter(writer, platform);
                    break;
                case TrigInfoType.MapReveal:
                    ((TrigInfo_MapReveal)Data).ToWriter(writer, platform);
                    break;
                default:
                    throw new IOException("Invalid TrigInfo type: " + Type);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Map: {MapIndex}, Trigger: {TrigIndex}");
            sb.AppendLine($"Type: {Type}");
            sb.AppendLine(string.Format("Position: X: {0:0.00}, Y: {1:0.00}, Z: {2:0.00}",
                XYZ.X, XYZ.Y, XYZ.Z));

            switch(Type)
            {
                case TrigInfoType.RestartPoint:
                    {
                        var data = (TrigInfo_RestartPoint)Data;
                        sb.AppendLine($"Has visited: {data.HasVisited}");
                        sb.AppendLine($"StartPoint HashCode: {data.HashCode.ToString("X")}");
                        sb.AppendLine($"Name HashCode: {data.NameTextHashCode.ToString("X")}");

                        break;
                    }
                case TrigInfoType.LightGem:
                    {
                        var data = (TrigInfo_LightGem)Data;
                        sb.AppendLine($"Is light: {data.IsLight}");

                        break;
                    }
                case TrigInfoType.MapReveal:
                    {
                        var data = (TrigInfo_MapReveal)Data;
                        sb.AppendLine($"Identifier HashCode: {data.IdentifierHashCode}");
                        sb.AppendLine($"Is visited: {data.IsRevealed}");

                        break;
                    }
                default:
                    throw new IOException("Invalid TrigInfo type: " + Type);
            }

            return sb.ToString();
        }
    }
}
