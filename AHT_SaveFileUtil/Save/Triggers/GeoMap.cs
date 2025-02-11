using AHT_SaveFileUtil.Common;
using System.Collections.Generic;
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

        public int GetTriggerIndex(Trigger trigger)
        {
            for (int i = 0; i < TriggerList.Length; i++)
                if (TriggerList[i] == trigger) return i;

            return -1;
        }

        public List<Trigger> GetStartPointTriggerList(bool includeShopStartPoints = false)
        {
            List<Trigger> list = [];

            foreach(Trigger trigger in TriggerList)
            {
                if (trigger.Type == (uint)EXHashCode.HT_TriggerType_StartPoint)
                {
                    //If trigger is shop startpoint then skip (if requested)
                    if (!includeShopStartPoints &&
                        (trigger.Data[0] == (uint)EXHashCode.HT_StartPoint_SHOP ||
                         trigger.Data[0] == (uint)EXHashCode.HT_StartPoint_MAINSHOP))
                        continue;

                    list.Add(trigger);
                }
            }

            return list;
        }

        /// <summary>
        /// Get the index of the startpoint trigger with the given startpoint hash.
        /// </summary>
        /// <param name="startPointHash">Startpoint hashcode to check against (T_StartPoint, 0x4a000000)</param>
        /// <returns>The index of the startpoint trigger, or -1 if no trigger was found.</returns>
        public int GetStartPointTriggerIndex(uint startPointHash)
        {
            for (int i = 0; i < TriggerList.Length; i++)
            {
                Trigger trigger = TriggerList[i];
                if (trigger.Data == null) continue;
                if (trigger.Data.Length == 0) continue;

                if (trigger.Data[0] == startPointHash) return i;
            }

            return -1;
        }

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
