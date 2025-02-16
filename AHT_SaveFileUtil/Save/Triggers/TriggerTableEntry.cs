using AHT_SaveFileUtil.Common;

namespace AHT_SaveFileUtil.Save.Triggers
{
    /// <summary>
    /// Defines information regarding a type of trigger.
    /// </summary>
    public class TriggerTableEntry
    {
        /// <summary>
        /// The primary trigger type.
        /// </summary>
        public uint PrimaryHash { get; set; }

        /// <summary>
        /// The secondary trigger type.
        /// </summary>
        public uint SubHash { get; set; }

        /// <summary>
        /// The number of bits preserved by this trigger type.
        /// </summary>
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
