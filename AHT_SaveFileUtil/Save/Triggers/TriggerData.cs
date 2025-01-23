using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AHT_SaveFileUtil.Save.Triggers
{
    public class TriggerDataUnit
    {
        public string Name { get; set; }

        public TriggerDataType Type { get; set; }

        public int NumBits { get; set; }

        public TriggerDataUnit() { }
    }

    public class TriggerData
    {
        public int TriggerTableEntry { get; set; }

        public string ObjectName { get; set; }

        public TriggerDataUnit[] Data { get; set; }

        public TriggerData() { }
    }

    public class TriggerDataDefinitions
    {
        public TriggerData[] TriggerData { get; set; }

        public TriggerDataDefinitions() { }

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
