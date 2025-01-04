using AHT_SaveFileUtil.Save.MiniMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace AHT_SaveFileUtil.Save.Triggers
{
    public class TriggerTable
    {
        public int NumEntries { get; set; }

        public int NumPreservingEntries { get; set; }

        public TriggerTableEntry[] Entries { get; set; }

        public TriggerTable() { }

        public static TriggerTable FromYAML(string yaml)
        {
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            return deserializer.Deserialize<TriggerTable>(yaml);
        }

        public string ToYaml()
        {
            var serializer = new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            return serializer.Serialize(this);
        }
    }
}
