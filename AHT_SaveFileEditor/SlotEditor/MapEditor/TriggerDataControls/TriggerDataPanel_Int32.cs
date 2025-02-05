using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using AHT_SaveFileUtil.Save.Triggers;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor.TriggerDataControls
{
    internal class TriggerDataPanel_Int32 : TriggerDataPanel
    {
        private NumericUpDown num;

        public TriggerDataPanel_Int32(int bitHeapAddress, TriggerDataUnit definition, GameState gameState)
            : base(bitHeapAddress, definition, gameState)
        {
            if (_definition!.NumBits != 32)
                throw new ArgumentException("A float must contain 32 bits.");

            Width = 130;
            Height = 40 + 2;
            BackColor = Color.AliceBlue;

            int value = ReadInt32Data();

            Controls.Add(new Label()
            {
                Text = _definition!.Name,
                Location = new Point(0, 0),
                Size = new Size(Width, 20)
            });

            num = new NumericUpDown()
            {
                Location = new Point(0, 20),
                Size = new Size(Width, Height - 20),
                Minimum = int.MinValue,
                Maximum = int.MaxValue,
                Value = value
            };
            num.ValueChanged += Num_ValueChanged;

            Controls.Add(num);
        }

        private void Num_ValueChanged(object? sender, EventArgs e)
        {
            WriteData();
        }

        private int ReadInt32Data()
        {
            bool bigEndian = SaveFileHandler.Instance.IsBigEndian;

            return _gameState.BitHeap.ReadInt32(_bitHeapAddress, bigEndian);
        }

        public override void WriteData()
        {
            bool bigEndian = SaveFileHandler.Instance.IsBigEndian;

            int val = (int)num.Value;

            _gameState.BitHeap.Write(_bitHeapAddress, val, bigEndian);
        }
    }
}
