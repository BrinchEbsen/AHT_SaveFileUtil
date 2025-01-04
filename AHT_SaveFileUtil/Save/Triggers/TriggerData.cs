using AHT_SaveFileUtil.Save.MiniMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

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
    }
}
