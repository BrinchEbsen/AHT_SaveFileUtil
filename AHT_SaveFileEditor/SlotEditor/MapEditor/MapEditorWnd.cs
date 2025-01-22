using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor
{
    public partial class MapEditorWnd : Form
    {
        private GameState gameState;

        private Map mapIndex;

        private MapGameState mapGameState;

        private MiniMapPanel miniMapPanel;

        public MapEditorWnd(GameState gameState, Map mapIndex)
        {
            InitializeComponent();

            this.gameState = gameState;
            this.mapIndex = mapIndex;
            mapGameState = gameState.MapStates[(int)mapIndex];
        }

        private void MapEditor_Load(object sender, EventArgs e)
        {
            miniMapPanel = new MiniMapPanel(gameState, mapIndex);
            miniMapPanel.PaintMode = PaintMode.Reveal;
            miniMapPanel.ShowSquares = Check_ShowSquares.Checked;

            Panel_MiniMap.Controls.Add(miniMapPanel);
            miniMapPanel.Invalidate();

            if (miniMapPanel.UsingDefault)
                Check_ShowMiniMap.Enabled = false;
        }

        private void Check_ShowMiniMap_CheckedChanged(object sender, EventArgs e)
        {
            miniMapPanel.ShowMiniMap = Check_ShowMiniMap.Checked;
            miniMapPanel.Invalidate();
        }

        private void Btn_PaintReveal_Click(object sender, EventArgs e)
        {
            miniMapPanel.PaintMode = PaintMode.Reveal;
            Btn_PaintReveal.Enabled = false;
            Btn_PaintUnreveal.Enabled = true;
        }

        private void Btn_PaintUnreveal_Click(object sender, EventArgs e)
        {
            miniMapPanel.PaintMode = PaintMode.Unreveal;
            Btn_PaintReveal.Enabled = true;
            Btn_PaintUnreveal.Enabled = false;
        }

        private void Btn_PaintClear_Click(object sender, EventArgs e)
        {
            miniMapPanel.ClearMiniMap();
            miniMapPanel.Invalidate();
        }

        private void Btn_PaintFill_Click(object sender, EventArgs e)
        {
            miniMapPanel.FillMiniMap();
            miniMapPanel.Invalidate();
        }

        private void Check_ShowSquares_CheckedChanged(object sender, EventArgs e)
        {
            miniMapPanel.ShowSquares = Check_ShowSquares.Checked;
            miniMapPanel.Invalidate();
        }
    }
}
