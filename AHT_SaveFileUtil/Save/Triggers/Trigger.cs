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

        public int GetTriggerTableIndex(TriggerTable tTable)
        {
            if (tTable == null) return -1;

            //Iteration 1: check both type and sybtype
            for(int i = 0; i < tTable.Entries.Length; i++)
            {
                if (tTable.Entries[i].PrimaryHash == Type &&
                    tTable.Entries[i].SubHash == SubType)
                    return i;
            }

            //Iteration 2: check just type
            if (SubType == (uint)EXHashCode.HT_TriggerSubType_Undefined ||
                SubType == (uint)EXHashCode.HT_TriggerType_Undefined)
            {
                for (int i = 0; i < tTable.Entries.Length; i++)
                {
                    if (tTable.Entries[i].PrimaryHash == Type)
                        return i;
                }
            }

            return -1;
        }
    }
}
