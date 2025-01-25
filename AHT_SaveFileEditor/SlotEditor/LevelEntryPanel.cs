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
            bool ps2Hack = false;

            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile != null)
                ps2Hack = saveFile.Platform == GamePlatform.PlayStation2;

            this.parentWnd = parentWnd;
            this.gameState = gameState;
            this.entryNr = entryNr;

            var levelState =
                ps2Hack ?
                gameState.MapStates[(int)entryNr - 2] :
                gameState.MapStates[(int)entryNr];

            var dataEntry = MapData.MapDataList[entryNr];

            FlowDirection = FlowDirection.TopDown;
            WrapContents = false;
            BackColor = Color.Azure;

            Height = 100;

            Controls.Add(new Label()
            {
                Text = dataEntry.Name,
                Width = 300
            });

            Controls.Add(new Label()
            {
                Text = string.Format("DG: {0}/{1}, LG: {2}/{3}, DE: {4}/{5}",
                    levelState.NumDarkGems,
                    levelState.MaxDarkGems == -1 ? 0 : levelState.MaxDarkGems,
                    levelState.NumLightGems,
                    levelState.MaxLightGems == -1 ? 0 : levelState.MaxLightGems,
                    levelState.SumOfEggs,
                    levelState.MaxDragonEggs == -1 ? 0 : levelState.MaxDragonEggs
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
