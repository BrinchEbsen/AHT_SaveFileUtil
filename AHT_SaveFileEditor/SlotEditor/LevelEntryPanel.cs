using AHT_SaveFileEditor.SlotEditor.MapEditor;
using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;

namespace AHT_SaveFileEditor.SlotEditor
{
    public class LevelEntryPanel : FlowLayoutPanel
    {
        private SaveSlotEditor parentWnd;

        private GameState gameState;

        private Map entryNr;

        public Map EntryNr => entryNr;

        public LevelEntryPanel(SaveSlotEditor parentWnd, GameState gameState, Map entryNr)
        {
            this.parentWnd = parentWnd;
            this.gameState = gameState;
            this.entryNr = entryNr;

            var levelState = gameState.GetMapGameState(entryNr, SaveFileHandler.Instance.Platform);

            var dataEntry = MapData.MapDataList[entryNr];

            MapGameState derivedState = null;
            if (dataEntry.DerivedCollectableTallies != Map.None)
                derivedState = gameState.GetMapGameState(
                    dataEntry.DerivedCollectableTallies, SaveFileHandler.Instance.Platform);

            FlowDirection = FlowDirection.TopDown;
            WrapContents = false;
            BackColor = Color.Azure;

            Height = 100;

            Controls.Add(new Label()
            {
                Text = dataEntry.Name,
                Width = 300
            });

            MapGameState stateForCollectables;
            if (derivedState != null)
                stateForCollectables = derivedState;
            else
                stateForCollectables = levelState;

            Controls.Add(new Label()
            {
                Text = string.Format("DG: {0}/{1}, LG: {2}/{3}, DE: {4}/{5}",
                    stateForCollectables.NumDarkGems,
                    stateForCollectables.MaxDarkGems == -1 ? 0 : stateForCollectables.MaxDarkGems,
                    stateForCollectables.NumLightGems,
                    stateForCollectables.MaxLightGems == -1 ? 0 : stateForCollectables.MaxLightGems,
                    stateForCollectables.SumOfEggs,
                    stateForCollectables.MaxDragonEggs == -1 ? 0 : stateForCollectables.MaxDragonEggs
                ),
                Width = 300
            });

            Button setStartMapBtn = new()
            {
                Text = "Set As Current Map",
                Width = 150
            };

            setStartMapBtn.Click += SetStartMapBtn_Click;

            Controls.Add(setStartMapBtn);

            Button openMapEditorBtn = new()
            {
                Text = "Open Editor",
                Width = 100
            };

            openMapEditorBtn.Click += OpenMapEditorBtn_Click;

            Controls.Add(openMapEditorBtn);
        }

        private void OpenMapEditorBtn_Click(object? sender, EventArgs e)
        {
            new MapEditorWnd(gameState, entryNr).ShowDialog();
        }

        private void SetStartMapBtn_Click(object? sender, EventArgs e)
        {
            parentWnd.SetStartingMap(entryNr);
        }
    }
}
