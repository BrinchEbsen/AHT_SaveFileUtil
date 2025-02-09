using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

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

    public class MapOrderInfo
    {
        public uint FileHash { get; set; }
        public uint MapHash { get; set; }
    }

    public class MiniMaps
    {
        public int NumMapInfo { get; set; }

        public int NumMapStatus { get; set; }

        public MiniMapInfo[] MiniMapInfo { get; set; } = [];

        public MapOrderInfo[] MiniMapOrder { get; set; } = [];

        public int MiniMaps_TotalBitheapSize
        {
            get
            {
                int size = 0;

                foreach (var map in MiniMapInfo)
                    size += map.BitHeapSize;

                return size;
            }
        }

        public int MiniMapStatus_TotalBitHeapSize
        {
            get
            {
                int size = 0;

                foreach (var _ in MiniMapOrder)
                    size += 2;

                return size;
            }
        }

        public int MiniMapStatus_BitHeapAddress => MiniMaps_TotalBitheapSize;

        public MiniMaps() { }

        public bool[][] GetMiniMapArrayFromBitHeap(BitHeap bitHeap, int index)
        {
            ArgumentNullException.ThrowIfNull(bitHeap);

            if (index < 0 || index > MiniMapInfo.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (MiniMapInfo[index].Type != InfoType.Mapped)
                throw new ArgumentException("Index does not correspond to a \"Mapped\" MiniMapInfo type.");

            //Get the bitheap offset for the info
            int offset = GetMiniMapInfoOffset(MiniMapInfo[index]);

            return MiniMapInfo[index].GetArrayFromBitHeap(bitHeap, offset);
        }

        public List<bool[][]> GetAllMiniMapArrayFromBitHeap(BitHeap bitHeap)
        {
            ArgumentNullException.ThrowIfNull(bitHeap);

            //We can assume half of the minimap info is for "Mapped" type.
            List<bool[][]> list = new(MiniMapInfo.Length / 2);

            int offset = 0;

            foreach(var info in MiniMapInfo)
                if (info.Type == InfoType.Mapped)
                {
                    list.Add(info.GetArrayFromBitHeap(bitHeap, offset));
                    offset += info.BitHeapSize;
                }

            return list;
        }

        public int GetMiniMapInfoIndex(MiniMapInfo info)
        {
            for(int i = 0; i < MiniMapInfo.Length; i++)
                if (MiniMapInfo[i] == info) return i;

            return -1;
        }

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

        public MiniMapInfo? GetInfo(uint fileHash, InfoType type)
        {
            foreach(var info in MiniMapInfo)
            {
                if (fileHash == info.MapFile && info.Type == type)
                    return info;
            }

            return null;
        }

        public int GetMiniMapInfoOffset(MiniMapInfo info)
        {
            int index = GetMiniMapInfoIndex(info);
            if (index < 0) return -1;

            return GetMiniMapInfoOffset(index);
        }

        public int GetMiniMapInfoOffset(int index)
        {
            int offset = 0;
            for (int i = 0; i < index; i++)
                if (MiniMapInfo[i].Type == InfoType.Mapped)
                    offset += MiniMapInfo[i].BitHeapSize;

            return offset;
        }

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

        public static MiniMaps FromYAML(string yaml)
        {
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            return deserializer.Deserialize<MiniMaps>(yaml);
        }

        public string ToYaml()
        {
            var serializer = new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            return serializer.Serialize(this);
        }
    }
}
