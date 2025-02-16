using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using System;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

#nullable enable

namespace AHT_SaveFileUtil.Save.MiniMap
{
    /// <summary>
    /// The name of each of the two bits in every entry
    /// in the minimap status array in the bitheap.
    /// </summary>
    public enum BitNames
    {
        Visible = 0,
        Selectable = 1
    }

    /// <summary>
    /// An entry into <see cref="MiniMaps.MiniMapOrder"/>.
    /// </summary>
    public class MapOrderInfo
    {
        public uint FileHash { get; set; }
        public uint MapHash { get; set; }
    }

    /// <summary>
    /// YAML-Serializable data regarding the game's minigams.
    /// </summary>
    [YamlSerializable]
    public class MiniMaps
    {
        /// <summary>
        /// Amount of minimap info.
        /// </summary>
        public int NumMapInfo { get; set; }

        /// <summary>
        /// Amount of minimap status.
        /// </summary>
        public int NumMapStatus { get; set; }

        /// <summary>
        /// List of minimap info.
        /// </summary>
        public MiniMapInfo[] MiniMapInfo { get; set; } = [];

        /// <summary>
        /// List of minimaps visible in the map screen.
        /// </summary>
        public MapOrderInfo[] MiniMapOrder { get; set; } = [];

        /// <summary>
        /// Number of bits used by the minimap reveal data.
        /// </summary>
        public int MiniMapsInfo_TotalBitheapSize
        {
            get
            {
                int size = 0;

                foreach (var map in MiniMapInfo)
                    size += map.BitHeapSize;

                return size;
            }
        }

        /// <summary>
        /// Number of bits used by the minimap statuses.
        /// </summary>
        public int MiniMapStatus_TotalBitHeapSize
        {
            get
            {
                int size = 0;

                foreach (var _ in MiniMapOrder)
                    size += 2; //Each status takes up 2 bits

                return size;
            }
        }

        /// <summary>
        /// BitHeap address of the minimap info.
        /// </summary>
        public static int MiniMapInfo_BitHeapAddress => 0;

        /// <summary>
        /// BitHeap address of the minimap status.
        /// </summary>
        public int MiniMapStatus_BitHeapAddress => MiniMapsInfo_TotalBitheapSize;

        public MiniMaps() { }

        /// <summary>
        /// Get the index of a minimap info.
        /// </summary>
        /// <param name="info">Info to search for.</param>
        /// <returns>Index of <paramref name="info"/>, or -1 if it could not be found.</returns>
        public int GetMiniMapInfoIndex(MiniMapInfo info)
        {
            for(int i = 0; i < MiniMapInfo.Length; i++)
                if (MiniMapInfo[i] == info) return i;

            return -1;
        }

        /// <summary>
        /// Get a minimap info with a given file hash, map hash and type of info.
        /// </summary>
        /// <param name="fileHash">File hash to search for.</param>
        /// <param name="mapHash">Map hash to search for.</param>
        /// <param name="type">Info type to search for.</param>
        /// <returns>Minimap info with the given
        /// <paramref name="fileHash"/>,
        /// <paramref name="mapHash"/> and
        /// <paramref name="type"/> (or null if none was found).</returns>
        public MiniMapInfo? GetInfo(uint fileHash, uint mapHash, InfoType type)
        {
            if (Enum.IsDefined(typeof(EXHashCode), mapHash) && mapHash != 0)
            {
                foreach(var info in MiniMapInfo)
                {
                    if (info.MapFile == fileHash &&
                        info.Map == mapHash &&
                        info.Type == type)
                        return info;
                }

                return null;
            } else
            {
                return GetInfo(fileHash, type);
            }
        }

        /// <summary>
        /// Get a minimap info with a given file hash and type of info.
        /// </summary>
        /// <param name="fileHash">File hash to search for.</param>
        /// <param name="type">Info type to search for.</param>
        /// <returns>Minimap info with the given
        /// <paramref name="fileHash"/> and
        /// <paramref name="type"/> (or null if none was found).</returns>
        public MiniMapInfo? GetInfo(uint fileHash, InfoType type)
        {
            foreach(var info in MiniMapInfo)
            {
                if (fileHash == info.MapFile && info.Type == type)
                    return info;
            }

            return null;
        }

        /// <summary>
        /// Get the bitheap offset of a "Mapped" minimap info.
        /// </summary>
        /// <param name="info">Info to get the bitheap offset of.</param>
        /// <returns>Bitheap offset of <paramref name="info"/>,
        /// or -1 if it could not be found.</returns>
        public int GetMiniMapInfoBitHeapOffset(MiniMapInfo info)
        {
            int index = GetMiniMapInfoIndex(info);
            if (index < 0) return -1;

            return GetMiniMapInfoOffset(index);
        }

        /// <summary>
        /// Get the bitheap offset of the "Mapped" minimap info at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetMiniMapInfoOffset(int index)
        {
            int offset = 0;
            for (int i = 0; i < index; i++)
                if (MiniMapInfo[i].Type == InfoType.Mapped)
                    offset += MiniMapInfo[i].BitHeapSize;

            return offset;
        }

        /// <summary>
        /// Get the index of a minimap status with a given file hash and map hash.
        /// </summary>
        /// <param name="fileHash">File hash to search for.</param>
        /// <param name="mapHash">Map hash to search for.</param>
        /// <returns>Index of the minimap status with <paramref name="fileHash"/>
        /// and <paramref name="mapHash"/>, or -1 if it could not be found.</returns>
        public int GetMiniMapStatusIndex(uint fileHash, uint mapHash = 0)
        {
            for (int i = 0; i < MiniMapOrder.Length; i++)
            {
                var state = MiniMapOrder[i];

                if (mapHash == 0)
                {
                    if (state.FileHash == fileHash)
                        return i;
                } else
                {
                    if (state.FileHash == fileHash && state.MapHash == mapHash)
                        return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Get the part of a minimap's status according to <paramref name="name"/>.
        /// </summary>
        /// <param name="bitHeap">Bitheap to read data from.</param>
        /// <param name="index">Index of the minimap status.</param>
        /// <param name="name">The name of the parameter to return.</param>
        /// <returns>The part of a minimap's status according to <paramref name="name"/>.</returns>
        public bool MiniMapStatus_GetBitName(BitHeap bitHeap, int index, BitNames name)
        {
            int address = MiniMapStatus_BitHeapAddress;

            //Find address of the entry
            address += index * 2;

            byte value = bitHeap.ReadBits(2, address)[0];

            return name switch
            {
                BitNames.Visible    => (value & 0b01) != 0,
                BitNames.Selectable => (value & 0b10) != 0,
                _ => false
            };
        }

        /// <summary>
        /// Set the part of a minimap's status according to <paramref name="name"/>.
        /// </summary>
        /// <param name="bitHeap">Bitheap to write data to.</param>
        /// <param name="index">Index of the minimap status.</param>
        /// <param name="name">The name of the parameter to write to.</param>
        /// <param name="set">Value to set the parameter to.</param>
        public void MiniMapStatus_SetBitName(BitHeap bitHeap, int index, BitNames name, bool set)
        {
            int address = MiniMapStatus_BitHeapAddress;

            //Find address of the entry
            address += index * 2;

            //Add 1 if we're targeting the second bit
            if (name == BitNames.Selectable) address++;

            //Write the bit
            bitHeap.WriteBits(1, [set ? (byte)1 : (byte)0], address);
        }

        /// <summary>
        /// Generate a <see cref="MiniMaps"/> object from a yaml string.
        /// </summary>
        /// <param name="yaml"></param>
        /// <returns>The yaml string, converted to a <see cref="MiniMaps"/> object.</returns>
        public static MiniMaps FromYAML(string yaml)
        {
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            return deserializer.Deserialize<MiniMaps>(yaml);
        }

        /// <summary>
        /// Convert the object to a yaml string.
        /// </summary>
        /// <returns>The object, converted to a yaml string.</returns>
        public string ToYaml()
        {
            var serializer = new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            return serializer.Serialize(this);
        }
    }
}
