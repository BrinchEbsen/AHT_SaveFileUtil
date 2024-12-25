using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AHT_SaveFileUtil.Save.MiniMap
{
    public class MiniMaps
    {
        public int NumMapInfo { get; set; }

        public MiniMapInfo[] MiniMapInfo { get; set; } = [];

        public MiniMaps() { }

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
