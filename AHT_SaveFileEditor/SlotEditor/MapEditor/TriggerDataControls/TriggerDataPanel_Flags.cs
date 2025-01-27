using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using AHT_SaveFileUtil.Save.Triggers;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor.TriggerDataControls
{
    internal class TriggerDataPanel_Flags : TriggerDataPanel
    {
        private CheckedListBox Flags;
        private uint FlagsValue;

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
            if (Height > 250) Height = 250;
            if (Height < 70) Height = 70;

            Flags.Height = Height - 20;
        }

        private void PopulateList()
        {
            ReadFlagsData();

            Flags.Items.Clear();
            
            uint mask = GetMask();

            int index = 0;
            for (int i = 0; i < 32; i++)
            {
                if ((mask & (1 << i)) != 0)
                {
                    int checkIndex = Flags.Items.Add(
                        "0x" + (1 << i).ToString("X"),
                        (FlagsValue & (1 << i)) != 0);

                    index++;
                }
            }
        }

        private void Flags_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            WriteDataIndex(e.Index, e.NewValue == CheckState.Checked);
        }

        private void ReadFlagsData()
        {
            uint mask = GetMask();

            bool bigEndian = SaveFileHandler.Instance.IsBigEndian;

            FlagsValue = _gameState.BitHeap.ReadBitMask(_bitHeapAddress, mask, bigEndian);
        }

        public override void WriteData()
        {
            uint mask = GetMask();

            bool bigEndian = SaveFileHandler.Instance.IsBigEndian;

            _gameState.BitHeap.WriteBitMask(_bitHeapAddress, mask, FlagsValue, bigEndian);
        }

        private void WriteDataIndex(int index, bool set)
        {
            int bitIndex = CheckIndexToBitIndex(index);
            if (bitIndex < 0) return;

            if (set)
                FlagsValue |= (uint)(1 << bitIndex);
            else
                FlagsValue &= ~(uint)(1 << bitIndex);

            WriteData();
        }

        private uint GetMask()
        {
            if (_definition!.Mask == 0)
                return _definition!.DefaultMask;

            return _definition!.Mask;
        }

        private int CheckIndexToBitIndex(int index)
        {
            uint mask = GetMask();

            int maskIndex = 0;
            for (int i = 0; i < 32; i++)
            {
                if ((mask & (1 << i)) != 0)
                {
                    if (maskIndex == index) return i;
                    maskIndex++;
                }
            }

            return -1;
        }
    }
}
