using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AHT_SaveFileUtil.Save.Triggers
{
    public class GeoMap
    {
        public uint FileHash { get; set; }

        public uint MapHash { get; set; }

        public Trigger[] TriggerList { get; set; }

        public GeoMap() { }

        public int GetTriggerBitHeapOffset(int trigIndex, TriggerTable tTable)
        {
            if (tTable == null) return -1;

            if (trigIndex < 0 || trigIndex >= TriggerList.Length)
                return -1;

            int offset = 0;

            for (int i = 0; i < TriggerList.Length; i++)
            {
                //Increase offset every time we don't encounter the target trigger
                if (trigIndex != i)
                {
                    int tTableIndex = TriggerList[i].GetTriggerTableIndex(tTable);
                    if (tTableIndex < 0) continue;

                    int dataSize = tTable.Entries[tTableIndex].StoredDataSize;
                    if (dataSize > 0)
                        offset += dataSize + 1;
                } else
                {
                    return offset;
                }
            }

            //Trigger was somehow not found
            return -1;
        }

        public int GetBitHeapSize(TriggerTable tTable)
        {
            if (tTable == null) return -1;

            int size = 0;

            foreach(var trigger in TriggerList)
            {
                int tTableIndex = trigger.GetTriggerTableIndex(tTable);
                if (tTableIndex < 0) continue;

                int dataSize = tTable.Entries[tTableIndex].StoredDataSize;
                if (dataSize > 0)
                    size += dataSize + 1;
            }

            return size;
        }

        #region YAML

        public static GeoMap FromYAML(string yaml)
        {
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            return deserializer.Deserialize<GeoMap>(yaml);
        }

        public string ToYaml()
        {
            var serializer = new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            return serializer.Serialize(this);
        }

        #endregion
    }
}
