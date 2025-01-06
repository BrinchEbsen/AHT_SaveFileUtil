using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save;

namespace AHT_SaveFileEditor
{
    public partial class MainWnd : Form
    {
        public MainWnd()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "GameCube Saves (*.gci)|*.gci|PS2 Save for EMS Adapter (*.psu)|*.psu";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var platSel = new PlatformSelection();

                    if (platSel.ShowDialog() != DialogResult.OK)
                        return;

                    GamePlatform platform = PlatformSelection.SelectedPlatform;

                    string file = openFileDialog.FileName;

                    SaveFileHandler.Instance.CloseStream();
                    SaveFileHandler.Instance.OpenFile(file, platform);
                    EnableSaveFileControls();
                }
            }
        }

        private void toolStripMenuItem_Export_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem_ExportAs_Click(object sender, EventArgs e)
        {

        }

        private void EnableSaveFileControls()
        {
            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile == null)
                return;

            SC_SaveFile.Visible = true;

            PopulateSaveSlotPanel();
            PopulateGlobalSaveInfo();
        }

        private void PopulateSaveSlotPanel()
        {
            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile == null)
                return;

            for (int i = 0; i < saveFile.Slots.Length; i++)
            {
                flowLayoutPanel_SaveSlots.Controls.Add(new Label() { Text = "Slot " + i });
                var panel = new SaveSlotPanel(saveFile.Slots[i]);
                flowLayoutPanel_SaveSlots.Controls.Add(panel);
                panel.Width = flowLayoutPanel_SaveSlots.Width;
            }
        }

        private void PopulateGlobalSaveInfo()
        {
            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile == null)
                return;

            Lbl_BuildTime.Text = saveFile.SaveInfo.BuildDateString + " | " + saveFile.SaveInfo.BuildTimeString;
            Lbl_SaveVersion.Text = saveFile.SaveInfo.SaveVersion.ToString();

            var globalGameState = saveFile.SaveInfo.GlobalGameState;

            for (int i = 0; i < 8; i++)
            {
                ushort eggBit = (ushort)(1 << i);
                var cb = new EggSetCheckBox(globalGameState, eggBit);
                cb.Text = GlobalGameState.EggSetNames[eggBit];
                FlowPanel_EggSets.Controls.Add(cb);
            }

            DataGrid_MiniGameTimes.Rows.Clear();
            DataGrid_MiniGameTimes.Rows.Add(16);

            for (int i = 0; i < 16; i++)
            {
                var row = DataGrid_MiniGameTimes.Rows[i];
                row.Cells[0].Value = MiniGameBestTime.MiniGameNames[i];
                row.Cells[1] = new MiniGameBestTimeCell(globalGameState.MiniGameBestTimes[i], false);
                row.Cells[2] = new MiniGameBestTimeCell(globalGameState.MiniGameBestTimes[i], true);
            }
        }

        private void flowLayoutPanel_SaveSlots_Resize(object sender, EventArgs e)
        {
            foreach (Control ctrl in flowLayoutPanel_SaveSlots.Controls)
                ctrl.Width = flowLayoutPanel_SaveSlots.Width;
        }

        private void DataGrid_MiniGameTimes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cell = (MiniGameBestTimeCell)DataGrid_MiniGameTimes.Rows[e.RowIndex].Cells[e.ColumnIndex];
            bool success = cell.CheckChangeValue();

            if (!success)
            {
                MessageBox.Show("Time must be formatted as \"[minutes]:[seconds]\".", "Invalid format",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
