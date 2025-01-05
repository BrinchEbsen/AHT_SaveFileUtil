using AHT_SaveFileUtil.Common;

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

        private void flowLayoutPanel_SaveSlots_Resize(object sender, EventArgs e)
        {
            foreach(Control ctrl in flowLayoutPanel_SaveSlots.Controls)
                ctrl.Width = flowLayoutPanel_SaveSlots.Width;
        }
    }
}
