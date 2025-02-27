﻿using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using AHT_SaveFileUtil.Save.Triggers;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor.TriggerDataControls
{
    internal class TriggerDataPanel_SingleFlag : TriggerDataPanel
    {
        private CheckBox Check_Flag;

        public TriggerDataPanel_SingleFlag(int bitHeapAddress, TriggerDataUnit definition, GameState gameState)
            : base(bitHeapAddress, definition, gameState)
        {
            if (definition.Type != TriggerDataType.SingleFlag)
                throw new ArgumentException(STR_WRONG_TYPE_EXCEPTION);

            Size = new(150, 25);
            BackColor = Color.AliceBlue;

            Check_Flag = new CheckBox()
            {
                Text = _definition!.Name,
                Checked = (ReadData()[0] & 1) != 0,
                Location = new Point(5, 0),
                Size = Size
            };
            Check_Flag.CheckedChanged += Check_Flag_CheckedChanged;
            Controls.Add(Check_Flag);
        }

        private void Check_Flag_CheckedChanged(object? sender, EventArgs e) => WriteData();

        public override void WriteData()
        {
            if (Check_Flag == null) return;

            byte value = Check_Flag.Checked ? (byte)1 : (byte)0;
            _gameState.BitHeap.WriteBits(1, [value], _bitHeapAddress);
        }
    }
}
