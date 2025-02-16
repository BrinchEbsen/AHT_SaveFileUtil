using AHT_SaveFileUtil.Common;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AHT_SaveFileUtil.Save.Triggers
{
    /// <summary>
    /// A YAML-Serializable object representing a map with a list of triggers.
    /// </summary>
    [YamlSerializable]
    public class GeoMap
    {
        /// <summary>
        /// File hash of the map.
        /// </summary>
        public uint FileHash { get; set; }

        /// <summary>
        /// Hashcode of the map.
        /// </summary>
        public uint MapHash { get; set; }

        /// <summary>
        /// list of triggers lol
        /// </summary>
        public Trigger[] TriggerList { get; set; }

        public GeoMap() { }

        /// <summary>
        /// Get the index of a trigger.
        /// </summary>
        /// <param name="trigger">Trigger to search for.</param>
        /// <returns>Index of <paramref name="trigger"/>, or -1 if it could not be found.</returns>
        public int GetTriggerIndex(Trigger trigger)
        {
            for (int i = 0; i < TriggerList.Length; i++)
                if (TriggerList[i] == trigger) return i;

            return -1;
        }

        /// <summary>
        /// Get a list of startpoint triggers in the map.
        /// </summary>
        /// <param name="includeShopStartPoints">Include startpoint triggers of type
        /// HT_StartPoint_SHOP and HT_StartPoint_MAINSHOP.</param>
        /// <returns>The list of startpoint triggers.</returns>
        public Trigger[] GetStartPointTriggerList(bool includeShopStartPoints = false)
        {
            if (includeShopStartPoints)
            {
                return Array.FindAll(TriggerList, t =>
                {
                    return t.Type == (uint)EXHashCode.HT_TriggerType_StartPoint;
                });
            } else
            {
                return Array.FindAll(TriggerList, t =>
                {
                    if (t.Type != (uint)EXHashCode.HT_TriggerType_StartPoint)
                        return false;

                    if (t.Data == null)
                        return false;

                    if (t.Data.Length == 0)
                        return false;

                    if (
                        (t.Data[0] == (uint)EXHashCode.HT_StartPoint_SHOP) ||
                        (t.Data[0] == (uint)EXHashCode.HT_StartPoint_MAINSHOP))
                        return false;

                    return true;
                });
            }
        }

        /// <summary>
        /// Get the index of the startpoint trigger with the given startpoint hash.
        /// </summary>
        /// <param name="startPointHash">Startpoint hashcode to check against (HT_StartPoint, 0x4a000000)</param>
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

        /// <summary>
        /// Get the bitheap offset of a trigger's data relative to the map's bitheap address.
        /// </summary>
        /// <param name="trigIndex">Index of trigger.</param>
        /// <param name="tTable">Trigger table.</param>
        /// <returns>The bitheap offset of the trigger at index <paramref name="trigIndex"/>,
        /// or -1 if the operation failed.</returns>
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

        /// <summary>
        /// Get the combined bitheap size of all triggers in the map.
        /// </summary>
        /// <param name="tTable">Trigger table.</param>
        /// <returns>The total bitheap size of the map's triggers.</returns>
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

        /// <summary>
        /// Generate a <see cref="GeoMap"/> object from a yaml string.
        /// </summary>
        /// <param name="yaml"></param>
        /// <returns>The yaml string, converted to a <see cref="GeoMap"/> object.</returns>
        public static GeoMap FromYAML(string yaml)
        {
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            return deserializer.Deserialize<GeoMap>(yaml);
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

        #endregion
    }
}
