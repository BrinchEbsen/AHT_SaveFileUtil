using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor
{
    public partial class MapEditorWnd : Form
    {
        private readonly GameState gameState;

        private readonly Map mapIndex;

        private readonly MapGameState mapGameState;

        private MiniMapPanel? miniMapPanel;

        public MapEditorWnd(GameState gameState, Map mapIndex)
        {
            InitializeComponent();

            this.gameState = gameState;
            this.mapIndex = mapIndex;
            mapGameState = gameState.MapStates[(int)mapIndex];
        }

        private void MapEditor_Load(object sender, EventArgs e)
        {
            miniMapPanel = new MiniMapPanel(gameState, mapIndex)
            {
                PaintMode = PaintMode.Reveal,
                ShowSquares = Check_ShowSquares.Checked
            };

            UpdatePaintButtons();

            Panel_MiniMap.Controls.Add(miniMapPanel);
            miniMapPanel.Invalidate();

            if (miniMapPanel.UsingDefault)
                Check_ShowMiniMap.Enabled = false;
        }

        private void Check_ShowMiniMap_CheckedChanged(object sender, EventArgs e)
        {
            if (miniMapPanel == null) return;

            miniMapPanel.ShowMiniMap = Check_ShowMiniMap.Checked;
            miniMapPanel.Invalidate();
        }

        private void Btn_PaintReveal_Click(object sender, EventArgs e)
        {
            if (miniMapPanel == null) return;

            miniMapPanel.PaintMode = PaintMode.Reveal;
            UpdatePaintButtons();
        }

        private void Btn_PaintUnreveal_Click(object sender, EventArgs e)
        {
            if (miniMapPanel == null) return;

            miniMapPanel.PaintMode = PaintMode.Unreveal;
            UpdatePaintButtons();
        }

        private void Btn_PaintClear_Click(object sender, EventArgs e)
        {
            if (miniMapPanel == null) return;

            miniMapPanel.ClearMiniMap();
            miniMapPanel.Invalidate();
        }

        private void Btn_PaintFill_Click(object sender, EventArgs e)
        {
            if (miniMapPanel == null) return;

            miniMapPanel.FillMiniMap();
            miniMapPanel.Invalidate();
        }

        private void Check_ShowSquares_CheckedChanged(object sender, EventArgs e)
        {
            if (miniMapPanel == null) return;

            miniMapPanel.ShowSquares = Check_ShowSquares.Checked;
            miniMapPanel.Invalidate();
        }

        private void UpdatePaintButtons()
        {
            if (miniMapPanel == null) return;

            Btn_PaintReveal.Enabled = miniMapPanel.PaintMode != PaintMode.Reveal;
            Btn_PaintUnreveal.Enabled = miniMapPanel.PaintMode != PaintMode.Unreveal;
        }
    }
}
