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

        public abstract void WriteData();

        public abstract BitSpanReader ReadData();
    }
}
