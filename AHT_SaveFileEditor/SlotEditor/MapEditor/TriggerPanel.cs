using AHT_SaveFileUtil.Save.Triggers;
using System.ComponentModel;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor
{
    internal class TriggerPanel : Panel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TrigIndex { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal int BitHeapOffset { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal Trigger Trigger { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal TriggerTableEntry TriggerTableEntry { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal TriggerData TriggerData { get; private set; }

        private MapEditorWnd _parentWnd;

        internal TriggerPanel(
            int trigIndex,
            int bitHeapOffset,
            Trigger trigger,
            TriggerTableEntry triggerTableEntry,
            TriggerData triggerData,
            MapEditorWnd parentWnd)
        {
            TrigIndex = trigIndex;
            BitHeapOffset = bitHeapOffset;
            Trigger = trigger;
            TriggerTableEntry = triggerTableEntry;
            TriggerData = triggerData;
            _parentWnd = parentWnd;

            BackColor = Color.AliceBlue;
            Height = 40;
            Width = 300 - 24;
            
            Controls.Add(new Label() {
                Text = "[" + TrigIndex + "] " + TriggerData.ObjectName,
                Location = new Point(0, 0),
                Width = Width - 70
                });

            Controls.Add(new Label()
            {
                Text = "Offset: " + BitHeapOffset,
                Location = new Point(0, 20),
                Width = 150
            });

            Button btn = new()
            {
                Location = new Point(this.Width-70, 0),
                Size = new Size(70, Height),
                Text = "View"
            };
            btn.Click += Btn_Click;
            Controls.Add(btn);
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            _parentWnd!.PopulateTriggerData(this);
        }
    }
}
