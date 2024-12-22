using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil.Save
{
    public class SaveInfo : ISaveFileIO<SaveInfo>
    {
        private byte[] BuildTime = new byte[32];

        private byte[] BuildDate = new byte[32];

        public int SaveVersion { get; private set; }

        public uint[] SaveChecksums { get; private set; } = new uint[3];

        public int GameStateSize { get; private set; }

        public GlobalGameState GlobalGameState { get; private set; }

        public float SfxVolume { get; private set; }

        public int MusicVolume { get; private set; }

        public bool FirstPersonYAxisInverted { get; private set; } //4 BYTES

        public bool SgtByrdYAxisInverted { get; private set; } //4 BYTES

        public bool SparxFlyingYAxisInverted { get; private set; } //4 BYTES

        public bool CameraModeActive { get; private set; } //4 BYTES

        public bool RumbleEnabled { get; private set; } //4 BYTES

        public Players SelectedSpyroSkin { get; private set; }



        public string BuildTimeString
        {
            get
            {
                var sb = new StringBuilder();

                foreach(byte b in BuildTime)
                {
                    if (b == 0) break;
                    sb.Append((char)b);
                }

                return sb.ToString();
            }
        }

        public string BuildDateString
        {
            get
            {
                var sb = new StringBuilder();

                foreach (byte b in BuildDate)
                {
                    if (b == 0) break;
                    sb.Append((char)b);
                }

                return sb.ToString();
            }
        }



        private SaveInfo() { }



        public static SaveInfo FromReader(BinaryReader reader, GamePlatform platform)
        {
            var info = new SaveInfo();

            bool bigEndian = platform == GamePlatform.GameCube;

            //Read build time
            for (int i = 0; i < 32; i++)
            {
                info.BuildTime[i] = reader.ReadByte();
            }

            //Read build date
            for (int i = 0; i < 32; i++)
            {
                info.BuildDate[i] = reader.ReadByte();
            }

            info.SaveVersion = reader.ReadInt32(bigEndian);

            info.SaveChecksums[0] = reader.ReadUInt32(bigEndian);
            info.SaveChecksums[1] = reader.ReadUInt32(bigEndian);
            info.SaveChecksums[2] = reader.ReadUInt32(bigEndian);

            info.GameStateSize = reader.ReadInt32(bigEndian);

            info.GlobalGameState = GlobalGameState.FromReader(reader, platform);

            //4 bytes of padding
            reader.BaseStream.Seek(4, SeekOrigin.Current);

            info.SfxVolume = reader.ReadSingle(bigEndian);

            info.MusicVolume = reader.ReadInt32(bigEndian);

            info.FirstPersonYAxisInverted = reader.ReadInt32(bigEndian) != 0;

            info.SgtByrdYAxisInverted = reader.ReadInt32(bigEndian) != 0;

            info.SparxFlyingYAxisInverted = reader.ReadInt32(bigEndian) != 0;

            info.CameraModeActive = reader.ReadInt32(bigEndian) != 0;

            info.RumbleEnabled = reader.ReadInt32(bigEndian) != 0;

            info.SelectedSpyroSkin = (Players)reader.ReadInt32(bigEndian);

            //4 bytes of padding
            reader.BaseStream.Seek(4, SeekOrigin.Current);

            return info;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            foreach (var chr in BuildTime)
                writer.Write(chr);

            foreach (var chr in BuildDate)
                writer.Write(chr);

            writer.Write(SaveVersion, bigEndian);
            writer.Write(SaveChecksums[0], bigEndian);
            writer.Write(SaveChecksums[1], bigEndian);
            writer.Write(SaveChecksums[2], bigEndian);
            writer.Write(GameStateSize, bigEndian);
            GlobalGameState.ToWriter(writer, platform);
            writer.BaseStream.Seek(4, SeekOrigin.Current);
            writer.Write(SfxVolume, bigEndian);
            writer.Write(MusicVolume, bigEndian);
            writer.Write(FirstPersonYAxisInverted ? 1 : 0, bigEndian);
            writer.Write(SgtByrdYAxisInverted ? 1 : 0, bigEndian);
            writer.Write(SparxFlyingYAxisInverted ? 1 : 0, bigEndian);
            writer.Write(CameraModeActive ? 1 : 0, bigEndian);
            writer.Write(RumbleEnabled ? 1 : 0, bigEndian);
            writer.Write((int)SelectedSpyroSkin, bigEndian);
            writer.BaseStream.Seek(4, SeekOrigin.Current);
        }
    }
}
