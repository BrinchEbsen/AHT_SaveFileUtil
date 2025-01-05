using AHT_SaveFileUtil.Save.Slot;

namespace AHT_SaveFileEditor
{
    internal class SaveSlotPanel : FlowLayoutPanel
    {
        private SaveSlot slot;

        public SaveSlotPanel(SaveSlot slot) : base()
        {
            this.slot = slot;
            FlowDirection = FlowDirection.TopDown;
            WrapContents = false;
            BackColor = Color.AliceBlue;
        }
    }
}
