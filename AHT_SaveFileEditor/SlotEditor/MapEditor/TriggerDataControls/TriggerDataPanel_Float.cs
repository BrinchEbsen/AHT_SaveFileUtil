using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using AHT_SaveFileUtil.Save.Triggers;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor.TriggerDataControls
{
    internal class TriggerDataPanel_Float : TriggerDataPanel
    {
        private const decimal MINIMUM = -999999;
        private const decimal MAXIMUM = 999999;

        private const int NumDecimalPlaces = 3;

        private NumericUpDown num;

        public TriggerDataPanel_Float(int bitHeapAddress, TriggerDataUnit definition, GameState gameState)
            : base(bitHeapAddress, definition, gameState)
        {
            if (_definition!.NumBits != 32)
                throw new ArgumentException("A float must contain 32 bits.");

            Width = 130;
            Height = 40 + 2;
            BackColor = Color.AliceBlue;

            float value = ReadFloatData();
            decimal decValue = 0;

            try { decValue = (decimal)value; }
            catch (Exception) { }

            if (decValue < MINIMUM) { decValue = MINIMUM; }
            if (decValue > MAXIMUM) { decValue = MAXIMUM; }

            Controls.Add(new Label()
            {
                Text = _definition!.Name,
                Location = new Point(0, 0),
                Size = new Size(Width, 20)
            });

            num = new NumericUpDown()
            {
                DecimalPlaces = NumDecimalPlaces,
                Minimum = MINIMUM,
                Maximum = MAXIMUM,
                Location = new Point(0, 20),
                Size = new Size(Width, Height - 20),
                Value = decValue
            };
            num.ValueChanged += Num_ValueChanged;

            Controls.Add(num);
        }

        private void Num_ValueChanged(object? sender, EventArgs e)
        {
            WriteData();
        }

        private float ReadFloatData()
        {
            bool bigEndian = SaveFileHandler.Instance.IsBigEndian;

            return _gameState.BitHeap.ReadSingle(_bitHeapAddress, bigEndian);
        }

        public override void WriteData()
        {
            bool bigEndian = SaveFileHandler.Instance.IsBigEndian;

            float val = (float)num.Value;

            _gameState.BitHeap.Write(_bitHeapAddress, val, bigEndian);
        }
    }
}
