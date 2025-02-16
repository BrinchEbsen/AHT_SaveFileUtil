using AHT_SaveFileUtil.Common;
using System;

namespace AHT_SaveFileUtil.Save.Triggers
{
    /// <summary>
    /// A static object in a map responsible for
    /// spawning items or other dynamic events.
    /// </summary>
    public class Trigger
    {
        /// <summary>
        /// The trigger's primary type.
        /// </summary>
        public uint Type { get; set; }
        
        /// <summary>
        /// The trigger's secondary type.
        /// </summary>
        public uint SubType { get; set; }

        /// <summary>
        /// The flags applied to an item on being spawned.
        /// </summary>
        public uint GameFlags { get; set; }

        /// <summary>
        /// The flags denoting which data it contains.
        /// </summary>
        public uint TrigFlags { get; set; }
        
        /// <summary>
        /// The trigger's world position.
        /// </summary>
        public EXVector3 Position { get; set; }

        /// <summary>
        /// The trigger's world rotation.
        /// </summary>
        public EXVector3 Rotation { get; set; }

        /// <summary>
        /// A table of various data related to the trigger.
        /// </summary>
        public uint[] Data { get; set; } = [];

        /// <summary>
        /// A table of links to other triggers.
        /// </summary>
        public ushort[] Links { get; set; } = [];

        /// <summary>
        /// The hashcode for the graphical object tied to the trigger.
        /// </summary>
        public uint GfxHashRef { get; set; }

        /// <summary>
        /// The hashcode for the file associated with the trigger.
        /// </summary>
        public uint GeoFileHashRef { get; set; }

        /// <summary>
        /// The index of the gamescript tied to the trigger.
        /// </summary>
        public uint ScriptIndex { get; set; }

        public Trigger() { }

        /// <summary>
        /// Get the trigger's index into the trigger table.
        /// </summary>
        /// <param name="tTable">Trigger table.</param>
        /// <returns>The trigger's index into <paramref name="tTable"/>,
        /// or -1 if the entry could not be found.</returns>
        public int GetTriggerTableIndex(TriggerTable tTable)
        {
            ArgumentNullException.ThrowIfNull(tTable);

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
