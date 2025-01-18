using AHT_SaveFileUtil.Save.Slot;

namespace AHT_SaveFileEditor.SlotEditor
{
    internal enum CollectableStatsPanelType
    {
        LockPickers,
        FireBombs,
        IceBombs,
        WaterBombs,
        ElectricBombs,
        FireArrows
    }

    internal class CollectableStatsPanel : Panel
    {
        private readonly PlayerState playerState;
        private readonly CollectableStatsPanelType type;

        //Numeric controls
        private NumericUpDown? num_1 = null;
        private NumericUpDown? num_2 = null;
        private NumericUpDown? num_3 = null;
        private NumericUpDown? num_4 = null;

        //Positions for the controls+labels
        private static readonly int[] yPositions =
        [
            25,
            50,
            75,
            100
        ];

        //x-Offset for the numeric controls
        private static readonly int xOffset = 65;

        //Width of the numeric controls
        private static readonly int numWidth = 65;

        private string TypeName => type switch
        {
            CollectableStatsPanelType.LockPickers => "Lock Picks",
            CollectableStatsPanelType.FireBombs => "Fire Bombs",
            CollectableStatsPanelType.IceBombs => "Ice Missiles",
            CollectableStatsPanelType.WaterBombs => "Water Bombs",
            CollectableStatsPanelType.ElectricBombs => "Electric Missiles",
            CollectableStatsPanelType.FireArrows => "Fire Arrows",
            _ => "N/A"
        };

        private PowerupTally TypeTally => type switch
        {
            CollectableStatsPanelType.LockPickers => playerState.LockPickers,
            CollectableStatsPanelType.FireBombs => playerState.FlameBombs,
            CollectableStatsPanelType.IceBombs => playerState.IceBombs,
            CollectableStatsPanelType.WaterBombs => playerState.WaterBombs,
            CollectableStatsPanelType.ElectricBombs => playerState.ElectricBombs,
            _ => throw new ArgumentException($"Attempted to access {nameof(PowerupTally)} object for type {type}")
        };

        public CollectableStatsPanel(PlayerState playerState, CollectableStatsPanelType type)
        {
            this.playerState = playerState;
            this.type = type;

            Size = new Size(130, 130);
            BackColor = Color.LightGray;

            Controls.Add(new Label() {
                Text = TypeName,
                Location = new Point(0, 0),
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
            });

            //Fire arrows is the special case
            if (type == CollectableStatsPanelType.FireArrows)
                AddFireArrowStats();
            else
                AddGenericStats();

            //Add event methods
            if (num_1 != null) num_1.ValueChanged += Num_1_ValueChanged;
            if (num_2 != null) num_2.ValueChanged += Num_2_ValueChanged;
            if (num_3 != null) num_3.ValueChanged += Num_3_ValueChanged;
            if (num_4 != null) num_4.ValueChanged += Num_4_ValueChanged;
        }

        //Add controls for anything other than fire arrows
        private void AddGenericStats()
        {
            var tally = TypeTally;

            num_1 = new NumericUpDown()
            {
                Maximum = sbyte.MaxValue,
                Minimum = sbyte.MinValue,
                Value = tally.Amount,
                Location = new Point(xOffset, yPositions[0]),
                Width = numWidth
            };

            num_2 = new NumericUpDown()
            {
                Maximum = sbyte.MaxValue,
                Minimum = sbyte.MinValue,
                Value = tally.Max,
                Location = new Point(xOffset, yPositions[1]),
                Width = numWidth
            };

            num_3 = new NumericUpDown()
            {
                Maximum = sbyte.MaxValue,
                Minimum = sbyte.MinValue,
                Value = tally.Total,
                Location = new Point(xOffset, yPositions[2]),
                Width = numWidth
            };

            num_4 = new NumericUpDown()
            {
                Maximum = sbyte.MaxValue,
                Minimum = sbyte.MinValue,
                Value = tally.Magazines,
                Location = new Point(xOffset, yPositions[3]),
                Width = numWidth
            };

            Controls.AddRange(num_1, num_2, num_3, num_4);

            Controls.AddRange(
            [
                new Label()
                {
                    Text = "Amount",
                    Location = new Point(0, yPositions[0])
                },
                new Label()
                {
                    Text = "Max",
                    Location = new Point(0, yPositions[1])
                },
                new Label()
                {
                    Text = "Total",
                    Location = new Point(0, yPositions[2])
                },
                new Label()
                {
                    Text = "Magazines",
                    Location = new Point(0, yPositions[3])
                }
            ]);
        }

        //Add controls for fire arrows
        private void AddFireArrowStats()
        {
            num_1 = new NumericUpDown()
            {
                Maximum = short.MaxValue,
                Minimum = short.MinValue,
                Value = playerState.FireArrows,
                Location = new Point(xOffset, yPositions[0]),
                Width = numWidth
            };

            num_2 = new NumericUpDown()
            {
                Maximum = short.MaxValue,
                Minimum = short.MinValue,
                Value = playerState.FireArrowsMax,
                Location = new Point(xOffset, yPositions[1]),
                Width = numWidth
            };

            Controls.AddRange(num_1, num_2);

            Controls.AddRange(
            [
                new Label()
                {
                    Text = "Amount",
                    Location = new Point(0, yPositions[0])
                },
                new Label()
                {
                    Text = "Max",
                    Location = new Point(0, yPositions[1])
                }
            ]);
        }

        #region Numeric Controls Events

        private void Num_1_ValueChanged(object? sender, EventArgs e)
        {
            if (type == CollectableStatsPanelType.FireArrows)
                playerState.FireArrows = (short)num_1!.Value;
            else
            {
                var tally = TypeTally;
                tally.Amount = (sbyte)num_1!.Value;
            }
        }

        private void Num_2_ValueChanged(object? sender, EventArgs e)
        {
            if (type == CollectableStatsPanelType.FireArrows)
                playerState.FireArrowsMax = (short)num_2!.Value;
            else
            {
                var tally = TypeTally;
                tally.Max = (sbyte)num_2!.Value;
            }
        }

        private void Num_3_ValueChanged(object? sender, EventArgs e)
        {
            if (type == CollectableStatsPanelType.FireArrows)
                return;
            else
            {
                var tally = TypeTally;
                tally.Total = (sbyte)num_3!.Value;
            }
        }

        private void Num_4_ValueChanged(object? sender, EventArgs e)
        {
            if (type == CollectableStatsPanelType.FireArrows)
                return;
            else
            {
                var tally = TypeTally;
                tally.Magazines = (sbyte)num_4!.Value;
            }
        }

        #endregion
    }
}
