using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.MiniMap;
using AHT_SaveFileUtil.Save.Triggers;
using System.Text;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor
{
    internal class ResourceHandler
    {
        private ResourceHandler() { }

        private static ResourceHandler? _instance = null;

        public static ResourceHandler Instance => _instance ??= new ResourceHandler();

        /// <summary>
        /// TEMPORARY
        /// </summary>
        public static readonly string ResourcePath = "../../../../resources";

        public MiniMaps? MiniMaps { get; private set; } = null;

        public TriggerTable? TriggerTable { get; private set; } = null;

        public TriggerDataDefinitions? TriggerDataDefinitions { get; private set; } = null;

        public Dictionary<Map, GeoMap>? Maps { get; private set; } = null;

        public Dictionary<EXHashCode, Image>? MapTextures { get; private set; } = null;

        public bool AllYamlLoaded =>
            (MiniMaps != null) &
            (TriggerTable != null) &
            (TriggerDataDefinitions != null) &
            (Maps != null);

        public bool LoadYamlFiles(out string message)
        {
            if (!Path.Exists(ResourcePath))
            {
                message = "Resource path not found";
                return false;
            }

            var sb = new StringBuilder();
            message = "Success";

            string yamlPath = Path.Join(ResourcePath, "yaml");
            if (!Path.Exists(yamlPath))
                sb.AppendLine("Resource yaml path not found.");
            else
            {
                //load minimaps.yaml
                try
                {
                    string yaml = File.ReadAllText(Path.Join(yamlPath, "minimaps.yaml"));
                    MiniMaps = MiniMaps.FromYAML(yaml);
                }
                catch (Exception e)
                {
                    sb.AppendLine("Failed to load \"minimaps.yaml\": " + e.Message);
                }

                //load triggertable.yaml
                try
                {
                    string yaml = File.ReadAllText(Path.Join(yamlPath, "triggertable.yaml"));
                    TriggerTable = TriggerTable.FromYAML(yaml);
                }
                catch (Exception e)
                {
                    sb.AppendLine("Failed to load \"triggertable.yaml\": " + e.Message);
                }

                //load triggerdata.yaml
                try
                {
                    string yaml = File.ReadAllText(Path.Join(yamlPath, "triggerdata.yaml"));
                    TriggerDataDefinitions = TriggerDataDefinitions.FromYAML(yaml);
                }
                catch (Exception e)
                {
                    sb.AppendLine("Failed to load \"triggerdata.yaml\": " + e.Message);
                }
            }

            string yamlTriggersPath = Path.Join(yamlPath, "triggers");
            if (!Path.Exists(yamlPath))
                sb.AppendLine("Resource yaml/triggers path not found.");
            else
            {
                string[] files = Directory.GetFiles(yamlTriggersPath);

                foreach (string file in files)
                {
                    try
                    {
                        string[] parts = Path.GetFileName(file).Replace(".yaml", "").Split('_');
                        if (parts.Length != 2) continue;
                        if (parts[0] != "MAP") continue;

                        Map index = (Map)int.Parse(parts[1]);

                        var map = GeoMap.FromYAML(File.ReadAllText(file));

                        Maps ??= []; //make non-null if at least one map is found
                        if (Maps.ContainsKey(index)) continue;

                        Maps.Add(index, map);
                    } catch (Exception e)
                    {
                        sb.AppendLine($"Failed to load file {file}: " + e.Message);
                    }
                }
            }

            if (sb.Length != 0)
            {
                message = sb.ToString();
                return false;
            }
            else return true;
        }

        public bool LoadMapTextures(out string message)
        {
            string texturePath = Path.Join(ResourcePath, "textures");
            if (!Path.Exists(texturePath))
            {
                message = "Resource path \"textures\" not found";
                return false;
            }

            var sb = new StringBuilder();
            message = "Success";

            string[] files = Directory.GetFiles(texturePath);
            foreach(string file in files)
            {
                try
                {
                    string name = Path.GetFileName(file).Replace(".png", "");
                    EXHashCode textureHash = (EXHashCode)Convert.ToUInt32(name, 16);

                    Image img = Image.FromFile(file);

                    MapTextures ??= [];
                    if (MapTextures.ContainsKey(textureHash)) continue;

                    MapTextures.Add(textureHash, img);
                } catch (Exception e)
                {
                    sb.AppendLine($"Failed to load texture {file}: " + e.Message);
                }
            }

            if (sb.Length != 0)
            {
                message = sb.ToString();
                return false;
            }
            else return true;
        }
    }
}
