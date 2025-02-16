using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AHT_SaveFileUtil.Save.Triggers
{
    /// <summary>
    /// A table of information for each type of trigger.
    /// </summary>
    public class TriggerTable
    {
        /// <summary>
        /// Amount of entries in <see cref="Entries"/>.
        /// </summary>
        public int NumEntries { get; set; } = 0;

        /// <summary>
        /// Amount of entries in <see cref="Entries"/> that preserve a non-zero amount of data.
        /// </summary>
        public int NumPreservingEntries { get; set; } = 0;

        /// <summary>
        /// Entries into the trigger table.
        /// </summary>
        public TriggerTableEntry[] Entries { get; set; } = [];

        public TriggerTable() { }

        #region YAML

        /// <summary>
        /// Generate a <see cref="TriggerTable"/> object from a yaml string.
        /// </summary>
        /// <param name="yaml"></param>
        /// <returns>The yaml string, converted to a <see cref="TriggerTable"/> object.</returns>
        public static TriggerTable FromYAML(string yaml)
        {
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            return deserializer.Deserialize<TriggerTable>(yaml);
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
