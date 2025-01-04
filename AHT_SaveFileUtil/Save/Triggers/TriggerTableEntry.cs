using AHT_SaveFileUtil.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil.Save.Triggers
{
    public class TriggerTableEntry
    {
        public uint PrimaryHash { get; set; }

        public uint SubHash { get; set; }

        public int StoredDataSize { get; set; }

        public TriggerTableEntry() { }

        public override string ToString()
        {
            if (StoredDataSize == 0)
            {
                return "No data";
            } else
            {
                return $"{StoredDataSize} bits | " + ((EXHashCode)PrimaryHash).ToString() + " | " + ((EXHashCode)SubHash).ToString();
            }
        }
    }
}
