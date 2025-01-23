using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil.Save.Slot
{
    public class MapGameState : ISaveFileIO<MapGameState>
    {
        public int MapIndexValue { get; private set; }

        public uint LastStartPoint { get; set; }

        public Players LastStartPointPlayer { get; set; }

        public int MaxDarkGems { get; set; }

        public int MaxDragonEggs { get; set; }

        public int MaxLightGems { get; set; }

        public int NumDarkGems { get; set; }

        public int NumLightGems { get; set; }

        public int NumEggs_ConceptArt { get; set; }

        public int NumEggs_ModelViewer { get; set; }

        public int NumEggs_Ember { get; set; }

        public int NumEggs_Flame { get; set; }

        public int NumEggs_SgtByrd { get; set; }

        public int NumEggs_Turret { get; set; }

        public int NumEggs_Sparx { get; set; }

        public int NumEggs_Blink { get; set; }

        public uint Flags { get; private set; }

        public int TriggerListBitHeapAddress { get; set; }

        public int TriggerListBitHeapSize { get; set; }

        public int SumOfEggs => NumEggs_ConceptArt +
                NumEggs_ModelViewer +
                NumEggs_Ember +
                NumEggs_Flame +
                NumEggs_SgtByrd +
                NumEggs_Turret +
                NumEggs_Sparx +
                NumEggs_Blink;

        private MapGameState() { }

        public static MapGameState FromReader(BinaryReader reader, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            var state = new MapGameState();

            state.LastStartPoint = reader.ReadUInt32(bigEndian);

            int player = reader.ReadInt32(bigEndian);
            if (!Enum.IsDefined(typeof(Players), player))
                throw new IOException($"Invalid value for LastStartPointPlayer: {player}");

            state.LastStartPointPlayer = (Players)player;

            state.MaxDarkGems = reader.ReadInt32(bigEndian);

            state.MaxDragonEggs = reader.ReadInt32(bigEndian);

            state.MaxLightGems = reader.ReadInt32(bigEndian);

            reader.BaseStream.Seek(7*4, SeekOrigin.Current); // 7 unused fields

            state.NumDarkGems = reader.ReadInt32(bigEndian);

            state.NumLightGems = reader.ReadInt32(bigEndian);

            state.NumEggs_ConceptArt = reader.ReadInt32(bigEndian);

            state.NumEggs_ModelViewer = reader.ReadInt32(bigEndian);

            state.NumEggs_Ember = reader.ReadInt32(bigEndian);

            state.NumEggs_Flame = reader.ReadInt32(bigEndian);

            state.NumEggs_SgtByrd = reader.ReadInt32(bigEndian);

            state.NumEggs_Turret = reader.ReadInt32(bigEndian);

            state.NumEggs_Sparx = reader.ReadInt32(bigEndian);

            state.NumEggs_Blink = reader.ReadInt32(bigEndian);

            state.Flags = reader.ReadUInt32(bigEndian);

            state.TriggerListBitHeapAddress = reader.ReadInt32(bigEndian);

            state.TriggerListBitHeapSize = reader.ReadInt32(bigEndian);

            return state;
        }

        public void ToWriter(BinaryWriter writer, GamePlatform platform)
        {
            bool bigEndian = platform == GamePlatform.GameCube;

            writer.Write(LastStartPoint, bigEndian);
            writer.Write((int)LastStartPointPlayer, bigEndian);
            writer.Write(MaxDarkGems, bigEndian);
            writer.Write(MaxDragonEggs, bigEndian);
            writer.Write(MaxLightGems, bigEndian);
            writer.BaseStream.Seek(7*4, SeekOrigin.Current); // 7 unused fields
            writer.Write(NumDarkGems, bigEndian);
            writer.Write(NumLightGems, bigEndian);
            writer.Write(NumEggs_ConceptArt, bigEndian);
            writer.Write(NumEggs_ModelViewer, bigEndian);
            writer.Write(NumEggs_Ember, bigEndian);
            writer.Write(NumEggs_Flame, bigEndian);
            writer.Write(NumEggs_SgtByrd, bigEndian);
            writer.Write(NumEggs_Turret, bigEndian);
            writer.Write(NumEggs_Sparx, bigEndian);
            writer.Write(NumEggs_Blink, bigEndian);
            writer.Write(Flags, bigEndian);
            writer.Write(TriggerListBitHeapAddress, bigEndian);
            writer.Write(TriggerListBitHeapSize, bigEndian);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Startpoint: {LastStartPoint.ToString("X")} | Character: {LastStartPointPlayer}");
            sb.AppendLine($"Dark Gems: {NumDarkGems}/{(MaxDarkGems < 0 ? 0 : MaxDarkGems)}");
            sb.AppendLine($"Dragon Eggs: {SumOfEggs}/{(MaxDragonEggs < 0 ? 0 : MaxDragonEggs)}");
            sb.AppendLine($"Light Gems: {NumLightGems}/{(MaxLightGems < 0 ? 0 : MaxLightGems)}");

            return sb.ToString();
        }
    }
}
