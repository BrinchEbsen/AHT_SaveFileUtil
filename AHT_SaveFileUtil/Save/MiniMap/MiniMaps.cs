using AHT_SaveFileUtil.Save.Slot;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AHT_SaveFileUtil.Save.MiniMap
{
    public class MiniMaps
    {
        public int NumMapInfo { get; set; }

        public MiniMapInfo[] MiniMapInfo { get; set; } = [];

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
