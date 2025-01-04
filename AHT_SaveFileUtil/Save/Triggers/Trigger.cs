using AHT_SaveFileUtil.Common;

namespace AHT_SaveFileUtil.Save.Triggers
{
    public class Trigger
    {
        public uint Type { get; set; }
        
        public uint SubType { get; set; }

        public uint GameFlags { get; set; }

        public uint TrigFlags { get; set; }
        
        public EXVector3 Position { get; set; }

        public EXVector3 Rotation { get; set; }

        public uint[] Data { get; set; }

        public ushort[] Links { get; set; }

        public uint GfxHashRef { get; set; }

        public uint GeoFileHashRef { get; set; }

        public uint ScriptIndex { get; set; }

        public Trigger() { }
    }
}
