using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using AHT_SaveFileUtil.Save.Triggers;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor.TriggerDataControls
{
    internal class TriggerDataPanel_EXVector : TriggerDataPanel
    {
        private const decimal MINIMUM = -999999;
        private const decimal MAXIMUM = 999999;

        private const int NumDecimalPlaces = 3;

        private const int XNumOffset = 30;

        private const int TextSpacing = 20;

        private NumericUpDown numX;
        private NumericUpDown numY;
        private NumericUpDown numZ;
        private NumericUpDown numW;

        public TriggerDataPanel_EXVector(int bitHeapAddress, TriggerDataUnit definition, GameState gameState)
            : base(bitHeapAddress, definition, gameState)
        {
            if (definition!.NumBits != 128)
                throw new ArgumentException(
                    $"Definition specifies {definition!.NumBits} bits - an EXVector uses 128 bits.");

            Width = 130;
            Height = TextSpacing * 5 + 2;
            BackColor = Color.AliceBlue;

            EXVector vec = ReadVectorData();

            decimal x, y, z, w;

            //conversion sometimes fails
            try
            {
                x = (decimal)vec.X;
                y = (decimal)vec.Y;
                z = (decimal)vec.Z;
                w = (decimal)vec.W;
            } catch (Exception)
            {
                x = 0; y = 0; z = 0; w = 0;
            }

            if (x > MAXIMUM) x = MAXIMUM;
            if (x < MINIMUM) x = MINIMUM;

            if (y > MAXIMUM) y = MAXIMUM;
            if (y < MINIMUM) y = MINIMUM;

            if (z > MAXIMUM) z = MAXIMUM;
            if (z < MINIMUM) z = MINIMUM;

            if (w > MAXIMUM) w = MAXIMUM;
            if (w < MINIMUM) w = MINIMUM;

            int offset = 0;

            Controls.Add(new Label()
            {
                Text = definition!.Name,
                Location = new Point(0, 0),
                Size = new Size(Width, TextSpacing),
                Height = TextSpacing
            });

            offset += TextSpacing;

            Controls.Add(new Label()
            {
                Text = "X:",
                Location = new Point(0, offset),
                Size = new Size(XNumOffset, TextSpacing)
            });

            numX = new()
            {
                DecimalPlaces = NumDecimalPlaces,
                Minimum = MINIMUM,
                Maximum = MAXIMUM,
                Location = new Point(XNumOffset, offset),
                Size = new Size(Width - XNumOffset, TextSpacing),
                Value = x
            };
            Controls.Add(numX);

            offset += TextSpacing;

            Controls.Add(new Label()
            {
                Text = "Y:",
                Location = new Point(0, offset),
                Size = new Size(XNumOffset, TextSpacing)
            });

            numY = new()
            {
                DecimalPlaces = NumDecimalPlaces,
                Minimum = MINIMUM,
                Maximum = MAXIMUM,
                Location = new Point(XNumOffset, offset),
                Size = new Size(Width - XNumOffset, TextSpacing),
                Value = y
            };
            Controls.Add(numY);

            offset += TextSpacing;

            Controls.Add(new Label()
            {
                Text = "Z:",
                Location = new Point(0, offset),
                Size = new Size(XNumOffset, TextSpacing)
            });

            numZ = new()
            {
                DecimalPlaces = NumDecimalPlaces,
                Minimum = MINIMUM,
                Maximum = MAXIMUM,
                Location = new Point(XNumOffset, offset),
                Size = new Size(Width - XNumOffset, TextSpacing),
                Value = z
            };
            Controls.Add(numZ);

            offset += TextSpacing;

            Controls.Add(new Label()
            {
                Text = "W:",
                Location = new Point(0, offset),
                Size = new Size(XNumOffset, TextSpacing)
            });

            numW = new()
            {
                DecimalPlaces = NumDecimalPlaces,
                Minimum = MINIMUM,
                Maximum = MAXIMUM,
                Location = new Point(XNumOffset, offset),
                Size = new Size(Width - XNumOffset, TextSpacing),
                Value = w
            };
            Controls.Add(numW);

            numX.ValueChanged += Num_ValueChanged;
            numY.ValueChanged += Num_ValueChanged;
            numZ.ValueChanged += Num_ValueChanged;
            numW.ValueChanged += Num_ValueChanged;
        }

        private void Num_ValueChanged(object? sender, EventArgs e)
        {
            WriteData();
        }

        private EXVector ReadVectorData()
        {
            bool bigEndian = false;

            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile != null)
                bigEndian = saveFile.Platform == GamePlatform.GameCube;

            return _gameState.BitHeap.ReadEXVector(_bitHeapAddress, bigEndian);
        }

        public override void WriteData()
        {
            bool bigEndian = false;

            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile != null)
                bigEndian = saveFile.Platform == GamePlatform.GameCube;

            var vec = new EXVector()
            {
                X = (float)numX.Value,
                Y = (float)numY.Value,
                Z = (float)numZ.Value,
                W = (float)numW.Value
            };

            _gameState.BitHeap.WriteEXVector(_bitHeapAddress, vec, bigEndian);
        }
    }
}
