using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using AHT_SaveFileUtil.Save.Triggers;
using System.ComponentModel;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor.TriggerDataControls
{
    internal abstract class TriggerDataPanel : Panel
    {
        protected const string STR_WRONG_TYPE_EXCEPTION = "Wrong type of data definition.";

        protected TriggerDataUnit? _definition;

        protected int _bitHeapAddress;
        protected GameState _gameState;

        public TriggerDataPanel(int bitHeapAddress, TriggerDataUnit definition, GameState gameState)
        {
            ArgumentNullException.ThrowIfNull(definition, nameof(definition));
            ArgumentNullException.ThrowIfNull(gameState, nameof(gameState));

            _definition = definition;
            _bitHeapAddress = bitHeapAddress;
            _gameState = gameState;
        }

        /// <summary>
        /// Write the data represented by the panel's controls
        /// to the corresponding data unit in the bitheap.
        /// </summary>
        public abstract void WriteData();

        /// <summary>
        /// Read a generic stream of bytes with the unit's data from the bitheap.
        /// </summary>
        /// <returns>A generic stream of bytes with the unit's data from the bitheap.</returns>
        public byte[] ReadData()
        {
            return _gameState.BitHeap.ReadBits(_definition!.NumBits, _bitHeapAddress);
        }
    }
}
