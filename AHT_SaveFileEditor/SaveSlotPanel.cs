using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;

namespace AHT_SaveFileEditor
{
    /// <summary>
    /// A panel for a save slot placed in the list on the left side of the main window.
    /// </summary>
    internal class SaveSlotPanel : FlowLayoutPanel
    {
        private MainWnd mainWnd;
        private SaveSlot slot;
        private int slotIndex;

        public SaveSlotPanel(MainWnd mainWnd, SaveSlot slot, int slotIndex)
        {
            this.mainWnd = mainWnd;
            this.slot = slot;
            this.slotIndex = slotIndex;
            FlowDirection = FlowDirection.TopDown;
            WrapContents = false;

            Controls.Add(new Label
            {
                Text = "Slot " + (slotIndex + 1),
                Height = 18,
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold)
            });

            if (slot.IsUsed)
            {
                Size = new Size(Width, 200);
                BackColor = Color.LightBlue;

                AddLabel($"Started: {slot.GameState.StartTime}");
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
                Size = new Size(Width, 74);
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
            new SaveSlotEditor(mainWnd, slot, slotIndex).ShowDialog();
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
