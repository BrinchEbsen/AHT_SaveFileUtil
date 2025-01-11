using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHT_SaveFileEditor
{
    public partial class SaveSlotEditor : Form
    {
        private readonly SaveSlot Slot;

        public SaveSlotEditor(SaveSlot slot)
        {
            InitializeComponent();

            Slot = slot;
        }

        private void SaveSlotEditor_Load(object sender, EventArgs e)
        {
            foreach(var entry in MapData.MapDataList)
            {
                FlowPanel_Levels.Controls.Add(new Label()
                {
                    Text = entry.Value.Name,
                    Width = this.Width - 25
                });
            }
        }
    }
}
