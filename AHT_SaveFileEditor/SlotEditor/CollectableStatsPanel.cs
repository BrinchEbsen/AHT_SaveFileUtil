using AHT_SaveFileUtil.Save.Slot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private PlayerState playerState;
        private CollectableStatsPanelType type;

        private NumericUpDown? Num_1 = null;
        private NumericUpDown? Num_2 = null;
        private NumericUpDown? Num_3 = null;
        private NumericUpDown? Num_4 = null;

        private static readonly int[] yPositions =
        [
            25,
            50,
            75,
            100
        ];

        private static readonly int xOffset = 65;

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

            if (type == CollectableStatsPanelType.FireArrows)
                AddFireArrowStats();
            else
                AddGenericStats();

            if (Num_1 != null)
                Num_1.ValueChanged += Num_1_ValueChanged;
            if (Num_2 != null)
                Num_2.ValueChanged += Num_2_ValueChanged;
            if (Num_3 != null)
                Num_3.ValueChanged += Num_3_ValueChanged;
            if (Num_4 != null)
                Num_4.ValueChanged += Num_4_ValueChanged;
        }

        public void AddGenericStats()
        {
            var tally = TypeTally;

            Num_1 = new NumericUpDown()
            {
                Maximum = sbyte.MaxValue,
                Minimum = sbyte.MinValue,
                Value = tally.Amount,
                Location = new Point(xOffset, yPositions[0]),
                Width = numWidth
            };

            Num_2 = new NumericUpDown()
            {
                Maximum = sbyte.MaxValue,
                Minimum = sbyte.MinValue,
                Value = tally.Max,
                Location = new Point(xOffset, yPositions[1]),
                Width = numWidth
            };

            Num_3 = new NumericUpDown()
            {
                Maximum = sbyte.MaxValue,
                Minimum = sbyte.MinValue,
                Value = tally.Total,
                Location = new Point(xOffset, yPositions[2]),
                Width = numWidth
            };

            Num_4 = new NumericUpDown()
            {
                Maximum = sbyte.MaxValue,
                Minimum = sbyte.MinValue,
                Value = tally.Magazines,
                Location = new Point(xOffset, yPositions[3]),
                Width = numWidth
            };

            Controls.AddRange(Num_1, Num_2, Num_3, Num_4);

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

        public void AddFireArrowStats()
        {
            Num_1 = new NumericUpDown()
            {
                Maximum = sbyte.MaxValue,
                Minimum = sbyte.MinValue,
                Value = playerState.FireArrows,
                Location = new Point(xOffset, yPositions[0]),
                Width = numWidth
            };

            Num_2 = new NumericUpDown()
            {
                Maximum = sbyte.MaxValue,
                Minimum = sbyte.MinValue,
                Value = playerState.FireArrowsMax,
                Location = new Point(xOffset, yPositions[1]),
                Width = numWidth
            };

            Controls.AddRange(Num_1, Num_2);

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

        private void Num_1_ValueChanged(object? sender, EventArgs e)
        {
            if (type == CollectableStatsPanelType.FireArrows)
                playerState.FireArrows = (short)Num_1!.Value;
            else
            {
                var tally = TypeTally;
                tally.Amount = (sbyte)Num_1!.Value;
            }
        }

        private void Num_2_ValueChanged(object? sender, EventArgs e)
        {
            if (type == CollectableStatsPanelType.FireArrows)
                playerState.FireArrowsMax = (short)Num_2!.Value;
            else
            {
                var tally = TypeTally;
                tally.Max = (sbyte)Num_2!.Value;
            }
        }

        private void Num_3_ValueChanged(object? sender, EventArgs e)
        {
            if (type == CollectableStatsPanelType.FireArrows)
                return;
            else
            {
                var tally = TypeTally;
                tally.Total = (sbyte)Num_3!.Value;
            }
        }

        private void Num_4_ValueChanged(object? sender, EventArgs e)
        {
            if (type == CollectableStatsPanelType.FireArrows)
                return;
            else
            {
                var tally = TypeTally;
                tally.Magazines = (sbyte)Num_4!.Value;
            }
        }
    }
}
