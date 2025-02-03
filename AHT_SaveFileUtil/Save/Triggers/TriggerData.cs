using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AHT_SaveFileUtil.Save.Triggers
{
    /// <summary>
    /// Represents a defintion for a unit of data preserved by a trigger.
    /// </summary>
    public class TriggerDataUnit
    {
        /// <summary>
        /// The name of this unit.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of this unit.
        /// </summary>
        public TriggerDataType Type { get; set; }

        /// <summary>
        /// Number of bits defined by this unit.
        /// </summary>
        public int NumBits { get; set; }

        /// <summary>
        /// Mask for a bitmask, if such is preserved.
        /// </summary>
        public uint Mask { get; set; } = 0;

        public TriggerDataUnit() { }

        /// <summary>
        /// A mask with bits filled from the bottom to match <see cref="NumBits"/>.
        /// </summary>
        public uint DefaultMask
        {
            get
            {
                uint mask = 0;

                for (int i = 0; i < NumBits; i++)
                {
                    mask <<= 1;
                    mask |= 1;
                }

                return mask;
            }
        }
    }

    /// <summary>
    /// Define the data preserved by a type of trigger.
    /// </summary>
    public class TriggerData
    {
        /// <summary>
        /// The entry index into the trigger table.
        /// </summary>
        public int TriggerTableEntry { get; set; }

        /// <summary>
        /// A name for this type of trigger.
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Collection of the defintions of the preserved data.
        /// </summary>
        public TriggerDataUnit[] Data { get; set; }

        /// <summary>
        /// Total size of the trigger's preserved data.
        /// </summary>
        public int Size
        {
            get
            {
                int size = 0;

                foreach(var def in Data)
                {
                    size += def.NumBits;
                }

                return size;
            }
        }

        public TriggerData() { }
    }

    /// <summary>
    /// Yaml-serializable collection of trigger data definitions.
    /// </summary>
    public class TriggerDataDefinitions
    {
        public TriggerData[] TriggerData { get; set; }

        public TriggerDataDefinitions() { }

        /// <summary>
        /// Get the data definition for the trigger with the given index into the trigger table.
        /// </summary>
        /// <param name="triggerTableIndex">Index into the trigger table.</param>
        /// <returns>The data defintion for the trigger,
        /// or null if none exist that match <paramref name="triggerTableIndex"/></returns>
        public TriggerData? GetTriggerData(int triggerTableIndex)
        {
            foreach(var triggerData in TriggerData)
                if (triggerData.TriggerTableEntry == triggerTableIndex) 
                    return triggerData;

            return null;
        }

        #region YAML

        public static TriggerDataDefinitions FromYAML(string yaml)
        {
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            return deserializer.Deserialize<TriggerDataDefinitions>(yaml);
        }

        public string ToYaml()
        {
            var serializer = new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            return serializer.Serialize(this);
        }

        #endregion
    }
}
