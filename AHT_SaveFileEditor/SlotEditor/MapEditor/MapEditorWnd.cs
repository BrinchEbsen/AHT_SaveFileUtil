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
    }
}
