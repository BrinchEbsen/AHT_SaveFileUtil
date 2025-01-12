using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using System.Xml.Schema;

namespace AHT_SaveFileEditor
{
    internal class SaveSlotPanel : FlowLayoutPanel
    {
        private MainWnd mainWnd;
        private SaveSlot slot;

        public SaveSlotPanel(MainWnd mainWnd, SaveSlot slot) : base()
        {
            this.mainWnd = mainWnd;
            this.slot = slot;
            FlowDirection = FlowDirection.TopDown;
            WrapContents = false;

            if (slot.IsUsed)
            {
                Size = new Size(Width, 180);
                BackColor = Color.LightBlue;

                AddLabel($"Started: {slot.GameState.StartTime.ToString()}");
                AddLabel($"Played: {slot.GameState.PlayTimerString}");
                AddLabel($"Dark Gems: {slot.GameState.PlayerState.TotalDarkGems}");
                AddLabel($"Light Gems: {slot.GameState.PlayerState.TotalLightGems}");
                AddLabel($"Dragon Eggs: {slot.GameState.PlayerState.TotalDragonEggs}");
                AddLabel($"Completed: {slot.GameState.CompletionPercentage:00.0}%");
                AddLabel($"Character: {slot.GameState.PlayerState.Setup.Player}");

                bool hasDataEntry = MapData.MapDataList.TryGetValue(slot.GameState.StartingMap, out var entry);
                if (hasDataEntry && entry != null)
                    AddLabel($"Level: {entry.Name}");
                else
                    AddLabel($"Level: {slot.GameState.StartingMap}");
            } else
            {
                Size = new Size(Width, 54);
                BackColor = Color.AliceBlue;
                AddLabel("Unused");
            }

            var btn = new Button()
            {
                Text = "Open",
                Width = 100,
                BackColor = Color.White
            };

            btn.Click += OpenSlotEditorWnd;

            Controls.Add(btn);
        }

        private void OpenSlotEditorWnd(object? sender, EventArgs e)
        {
            new SaveSlotEditor(mainWnd, slot).ShowDialog();
        }

        private void AddLabel(string text)
        {
            Controls.Add(new Label() {
                Text = text,
                Size = new Size(Size.Width, 18)
            });
        }
    }
}
