using AHT_SaveFileUtil.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileEditor
{
    internal class EggSetCheckBox : CheckBox
    {
        private GlobalGameState state;
        private ushort eggBit;

        public EggSetCheckBox(GlobalGameState state, ushort eggBit)
        {
            this.state = state;
            this.eggBit = eggBit;
            Checked = (state.EggSets & this.eggBit) != 0;

            CheckedChanged += EggSetCheckBox_CheckedChanged;

            Margin = new Padding(0);
        }

        private void EggSetCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (Checked)
            {
                state.EggSets |= eggBit;
            } else
            {
                state.EggSets &= (ushort)~eggBit;
            }
        }
    }
}
