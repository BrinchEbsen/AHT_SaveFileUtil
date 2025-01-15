using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using System.IO;
using System.Text;

namespace AHT_SaveFileUtil.Save
{
    /// <summary>
    /// Contains any global information in a <see cref="SaveFile"/> not specific to any <see cref="Slot.SaveSlot"/>
    /// </summary>
    public class SaveInfo : ISaveFileIO<SaveInfo>
    {
        /// <summary>
        /// 32-byte capacity UTF-8 string describing the timestamp when the game build was created.
        /// </summary>
        private byte[] BuildTime = new byte[32];

        /// <summary>
        /// 32-byte capacity UTF-8 string describing the date when the game build was created.
        /// </summary>
        private byte[] BuildDate = new byte[32];

        /// <summary>
        /// A version number. Not used and should remain unchanged.
        /// </summary>
        public int SaveVersion { get; private set; }

        /// <summary>
        /// The usage flags for each of the three <see cref="Slot.SaveSlot"/> in the file.
        /// <para>
        /// Should match the usage flags in each slot. The save data will be determined
        /// to be invalid if not.
        /// </para>
        /// </summary>
        public uint[] UsageFlags { get; private set; } = new uint[3];

        /// <summary>
        /// The size in bytes of each slot's <see cref="Slot.GameState"/> struct.
        /// </summary>
        public int GameStateSize { get; private set; }

        /// <summary>
        /// The global state of in-game stats.
        /// </summary>
        public GlobalGameState GlobalGameState { get; private set; }

        /// <summary>
        /// The current SFX volume in the settings.
        /// </summary>
        public float SfxVolume { get; set; }

        /// <summary>
        /// The current music volume in the settings.
        /// </summary>
        public int MusicVolume { get; set; }

        /// <summary>
        /// Whether the first person y-axis invertion option is enabled in the settings.
        /// </summary>
        public bool FirstPersonYAxisInverted { get; set; } //4 BYTES

        /// <summary>
        /// Whether the Sgt. Byrd flying y-axis invertion option is enabled in the settings.
        /// </summary>
        public bool SgtByrdYAxisInverted { get; set; } //4 BYTES

        /// <summary>
        /// Whether the Sparx flying y-axis invertion option is enabled in the settings.
        /// </summary>
        public bool SparxFlyingYAxisInverted { get; set; } //4 BYTES

        /// <summary>
        /// Whether the camera mode is set to 'Active' in the settings.
        /// </summary>
        public bool CameraModeActive { get; set; } //4 BYTES

        /// <summary>
        /// Whether the rumble feature is enabled in the settings.
        /// </summary>
        public bool RumbleEnabled { get; set; } //4 BYTES

        /// <summary>
        /// Which unlockable Spyro skin is selected from the 'Unlockables' menu.
        /// Intended values are Spyro, Ember and Flame.
        /// </summary>
        public Players SelectedSpyroSkin { get; set; }

        /// <summary>
        /// String representation of the timestamp when the game build was created.
        /// </summary>
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

        /// <summary>
        /// String representation of the date when the game build was created.
        /// </summary>
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

            info.UsageFlags[0] = reader.ReadUInt32(bigEndian);
            info.UsageFlags[1] = reader.ReadUInt32(bigEndian);
            info.UsageFlags[2] = reader.ReadUInt32(bigEndian);

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
            writer.Write(UsageFlags[0], bigEndian);
            writer.Write(UsageFlags[1], bigEndian);
            writer.Write(UsageFlags[2], bigEndian);
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
