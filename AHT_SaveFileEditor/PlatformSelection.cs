using AHT_SaveFileUtil.Common;

namespace AHT_SaveFileEditor
{
    public partial class PlatformSelection : Form
    {
        public static GamePlatform SelectedPlatform;

        public PlatformSelection()
        {
            InitializeComponent();
        }

        private void Btn_GameCube_Click(object sender, EventArgs e)
        {
            SelectedPlatform = GamePlatform.GameCube;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Btn_PS2_Click(object sender, EventArgs e)
        {
            SelectedPlatform = GamePlatform.PlayStation2;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Btn_Xbox_Click(object sender, EventArgs e)
        {
            SelectedPlatform = GamePlatform.Xbox;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
