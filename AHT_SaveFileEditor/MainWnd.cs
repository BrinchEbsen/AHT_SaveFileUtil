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

        private void MainWnd_Load(object sender, EventArgs e)
        {
            OpenSaveFile("../../../../7D-G5SE-G5SE.gci", GamePlatform.GameCube);
        }

        private void OpenSaveFileDialog()
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

                    OpenSaveFile(file, platform);
                }
            }
        }

        private void OpenSaveFile(string file, GamePlatform platform)
        {
            try
            {
                SaveFileHandler.Instance.CloseStream();
                SaveFileHandler.Instance.OpenFile(file, platform);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error reading file",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DisableSaveFileControls();
                return;
            }

            EnableSaveFileControls();
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

        private void DisableSaveFileControls()
        {
            SC_SaveFile.Visible = false;
        }

        internal void PopulateSaveSlotPanel()
        {
            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile == null)
                return;

            foreach (var ctrl in flowLayoutPanel_SaveSlots.Controls)
                if (ctrl is SaveSlotPanel)
                    ((SaveSlotPanel)ctrl).Dispose();

            flowLayoutPanel_SaveSlots.Controls.Clear();
            for (int i = 0; i < saveFile.Slots.Length; i++)
            {
                flowLayoutPanel_SaveSlots.Controls.Add(new Label()
                {
                    Text = "Slot " + (i + 1),
                    Height = 18,
                    Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold)
                });
                var panel = new SaveSlotPanel(this, saveFile.Slots[i]);
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

            FlowPanel_EggSets.Controls.Clear();
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

            TrackBar_SFXVolume.Value = (int)saveFile.SaveInfo.SfxVolume;
            TrackBar_MusicVolume.Value = saveFile.SaveInfo.MusicVolume;

            Check_FPAxisInverted.Checked = saveFile.SaveInfo.FirstPersonYAxisInverted;
            Check_SgtAxisInverted.Checked = saveFile.SaveInfo.SgtByrdYAxisInverted;
            Check_SparxAxisInverted.Checked = saveFile.SaveInfo.SparxFlyingYAxisInverted;
            Check_CamActive.Checked = saveFile.SaveInfo.CameraModeActive;
            Check_Rumble.Checked = saveFile.SaveInfo.RumbleEnabled;

            ComboBox_BonusCharacter.Items.Clear();
            var playerNames = Enum.GetValues<Players>();
            foreach (var name in playerNames)
                ComboBox_BonusCharacter.Items.Add(name);

            ComboBox_BonusCharacter.SelectedIndex = (int)saveFile.SaveInfo.SelectedSpyroSkin;
        }

        #region Events
        private void toolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            OpenSaveFileDialog();
        }

        private void toolStripMenuItem_Export_Click(object sender, EventArgs e)
        {
            SaveFileHandler.Instance.SaveFile?.ValidateSlotUsageFlags();

            try
            {
                SaveFileHandler.Instance.CreateBackup();
                SaveFileHandler.Instance.WriteFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error writing file",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void toolStripMenuItem_ExportAs_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel_SaveSlots_Resize(object sender, EventArgs e)
        {
            foreach (Control ctrl in flowLayoutPanel_SaveSlots.Controls)
                ctrl.Width = flowLayoutPanel_SaveSlots.Width - 25;
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

        private void TrackBar_SFXVolume_Scroll(object sender, EventArgs e)
        {
            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile == null)
                return;

            saveFile.SaveInfo.SfxVolume = TrackBar_SFXVolume.Value;
        }

        private void TrackBar_MusicVolume_Scroll(object sender, EventArgs e)
        {
            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile == null)
                return;

            saveFile.SaveInfo.MusicVolume = TrackBar_MusicVolume.Value;
        }

        private void Check_FPAxisInverted_CheckedChanged(object sender, EventArgs e)
        {
            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile == null)
                return;

            saveFile.SaveInfo.FirstPersonYAxisInverted = Check_FPAxisInverted.Checked;
        }

        private void Check_SgtAxisInverted_CheckedChanged(object sender, EventArgs e)
        {
            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile == null)
                return;

            saveFile.SaveInfo.SgtByrdYAxisInverted = Check_SgtAxisInverted.Checked;
        }

        private void Check_SparxAxisInverted_CheckedChanged(object sender, EventArgs e)
        {
            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile == null)
                return;

            saveFile.SaveInfo.SparxFlyingYAxisInverted = Check_SparxAxisInverted.Checked;
        }

        private void Check_CamActive_CheckedChanged(object sender, EventArgs e)
        {
            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile == null)
                return;

            saveFile.SaveInfo.CameraModeActive = Check_CamActive.Checked;
        }

        private void Check_Rumble_CheckedChanged(object sender, EventArgs e)
        {
            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile == null)
                return;

            saveFile.SaveInfo.RumbleEnabled = Check_Rumble.Checked;
        }

        private void ComboBox_BonusCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile == null)
                return;

            saveFile.SaveInfo.SelectedSpyroSkin = (Players)ComboBox_BonusCharacter.SelectedIndex;
        }
        #endregion
    }
}
