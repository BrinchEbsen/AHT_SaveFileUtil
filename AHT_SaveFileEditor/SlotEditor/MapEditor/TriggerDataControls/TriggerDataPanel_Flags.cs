using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using AHT_SaveFileUtil.Save.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor.TriggerDataControls
{
    internal class TriggerDataPanel_Flags : TriggerDataPanel
    {
        private CheckedListBox Flags;

        public TriggerDataPanel_Flags(int bitHeapAddress, TriggerDataUnit definition, GameState gameState)
            : base(bitHeapAddress, definition, gameState)
        {
            if (definition.Type != TriggerDataType.Flags)
                throw new ArgumentException(STR_WRONG_TYPE_EXCEPTION);

            Width = 100;
            BackColor = Color.AliceBlue;

            Controls.Add(new Label()
            {
                Text = definition.Name,
                Width = this.Width,
                Height = 20,
                Location = new Point(0, 0)
            });

            Flags = new CheckedListBox()
            {
                Location = new Point(0, 20),
                Width = this.Width,
            };
            Flags.ItemCheck += Flags_ItemCheck;
            PopulateList();

            Controls.Add(Flags);

            //Set height based on number of flags
            Height = 20 + Flags.Items.Count * 20;
            if (Height > 200) Height = 200;
            if (Height < 70) Height = 70;

            Flags.Height = Height - 20;
        }

        private void PopulateList()
        {
            var data = new BitSpanReader(ReadData(), _definition!.NumBits, 0);

            Flags.Items.Clear();

            for (int i = 0; i < data.Length; i++)
                Flags.Items.Add(i.ToString(), data.NextBit != 0);
        }

        private void Flags_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            WriteDataIndex(e.Index, e.NewValue == CheckState.Checked);
        }

        public override void WriteData()
        {
            for (int i = 0; i < Flags.Items.Count; i++)
            {
                byte val = Flags.CheckedIndices.Contains(i) ? (byte)1 : (byte)0;
                _gameState.BitHeap.WriteBits(1, [val], _bitHeapAddress + i);
            }
        }

        private void WriteDataIndex(int index, bool set)
        {
            _gameState.BitHeap.WriteBits(1, [set ? (byte)1 : (byte)0], _bitHeapAddress + index);
        }
    }
}
