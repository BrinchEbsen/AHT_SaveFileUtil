using AHT_SaveFileEditor.SlotEditor.MapEditor.TriggerDataControls;
using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using AHT_SaveFileUtil.Save.Triggers;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor
{
    public partial class MapEditorWnd : Form
    {
        private readonly GameState gameState;

        private readonly Map mapIndex;

        private readonly MapGameState mapGameState;

        private MiniMapPanel? miniMapPanel;

        private bool Allocated => mapGameState.TriggerListBitHeapAddress != 0x7FFFFFFF;

        private static readonly TriggerDataUnit WrittenFlagUnit = new()
        {
            Name = "Written Flag",
            Type = TriggerDataType.SingleFlag,
            NumBits = 1
        };

        public MapEditorWnd(GameState gameState, Map mapIndex)
        {
            InitializeComponent();

            this.gameState = gameState;
            this.mapIndex = mapIndex;
            mapGameState = gameState.MapStates[(int)mapIndex];
        }

        private void MapEditor_Load(object sender, EventArgs e)
        {
            //Minimap
            miniMapPanel = new MiniMapPanel(gameState, mapIndex)
            {
                PaintMode = PaintMode.Reveal,
                ShowSquares = Check_ShowSquares.Checked
            };

            UpdatePaintButtons();

            Panel_MiniMap.Controls.Add(miniMapPanel);
            miniMapPanel.Invalidate();

            if (miniMapPanel.UsingDefault)
                GroupBox_DrawControls.Visible = false;

            UpdateTriggerControls();

            if (Allocated)
                PopulateTriggerList();
        }

        #region Paint Controls

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

        #endregion

        #region Triggers

        private void UpdateTriggerControls()
        {
            Btn_MapAllocate.Enabled = !Allocated;
            Btn_ClearAllTriggerData.Enabled = Allocated;
            Btn_ClearAllWrittenFlag.Enabled = Allocated;
            Btn_WriteAllWrittenFlag.Enabled = Allocated;

            if (Allocated)
            {
                Lbl_IsAllocated.Text = "Allocated: Yes";
                Lbl_MapAllocatedSize.Text = "Size: " + mapGameState.TriggerListBitHeapSize;
            } else
            {
                Lbl_IsAllocated.Text = "Allocated: No";
                Lbl_MapAllocatedSize.Text = "Size: 0";
            }
        }

        private void DepopulateTriggerData()
        {
            foreach (TriggerDataPanel ctrl in FlowPanel_TriggerData.Controls)
                ctrl.Dispose();

            FlowPanel_TriggerData.Controls.Clear();
        }

        private bool PopulateTriggerList()
        {
            var rsc = ResourceHandler.Instance;

            //Check if any needed resources are null
            if (rsc.TriggerDataDefinitions == null) return false;
            if (rsc.TriggerTable == null) return false;
            if (rsc.Maps == null) return false;

            if (!rsc.Maps.TryGetValue(
                mapIndex, out GeoMap? map))
                return false;

            int bitHeapOffset = 0;

            for (int i = 0; i < map.TriggerList.Length; i++)
            {
                var trigger = map.TriggerList[i];

                //TriggerTable index
                int tTableIndex = trigger.GetTriggerTableIndex(rsc.TriggerTable);
                if (tTableIndex < 0) continue;

                //TriggerTable entry
                var tTableEntry = rsc.TriggerTable.Entries[tTableIndex];
                if (tTableEntry.StoredDataSize == 0) continue;

                //Trigger data definition
                var triggerData = rsc.TriggerDataDefinitions.GetTriggerData(tTableIndex);
                if (triggerData == null) continue;

                //Add a new trigger panel
                var triggerPanel
                    = new TriggerPanel(i, bitHeapOffset, trigger, tTableEntry, triggerData, this);
                FlowPanel_Triggers.Controls.Add(triggerPanel);

                //1+ for written flag
                bitHeapOffset += tTableEntry.StoredDataSize + 1;
            }

            return true;
        }

        internal void PopulateTriggerData(TriggerPanel triggerPanel)
        {
            DepopulateTriggerData();

            //Only populate if the map has data allocated
            if (mapGameState.TriggerListBitHeapAddress == 0x7FFFFFFF)
                return;

            int bitHeapOffset = mapGameState.TriggerListBitHeapAddress + triggerPanel.BitHeapOffset;

            FlowPanel_TriggerData.Controls.Add(
                    new TriggerDataPanel_SingleFlag(
                        bitHeapOffset,
                        WrittenFlagUnit,
                        gameState)
                    {
                        BackColor = Color.LightGray
                    }
                    );

            bitHeapOffset++;

            foreach (var definition in triggerPanel.TriggerData.Data)
            {
                switch (definition.Type)
                {
                    case TriggerDataType.SingleFlag:
                        FlowPanel_TriggerData.Controls.Add(
                            new TriggerDataPanel_SingleFlag(
                                bitHeapOffset,
                                definition,
                                gameState
                            ));
                        break;
                    case TriggerDataType.Flags:
                        FlowPanel_TriggerData.Controls.Add(
                            new TriggerDataPanel_Flags(
                                bitHeapOffset,
                                definition,
                                gameState
                            ));
                        break;
                    default:
                        throw new NotImplementedException();
                }

                bitHeapOffset += definition.NumBits;
            }

            if (miniMapPanel != null)
            {
                miniMapPanel.SetHighLight(triggerPanel.Trigger.Position);
                miniMapPanel.Invalidate();
            }
        }

        #endregion

        private void Btn_MapAllocate_Click(object sender, EventArgs e)
        {
            if (Allocated) return;

            var maps = ResourceHandler.Instance.Maps;
            if (maps == null) return;

            var tTable = ResourceHandler.Instance.TriggerTable;
            if (tTable == null) return;

            //this should always succeed but whatevs
            if (!maps.TryGetValue(mapIndex, out var map))
                return;

            int size = map.GetBitHeapSize(tTable);

            int address = gameState.BitHeap.Allocate(size);

            mapGameState.TriggerListBitHeapAddress = address;
            mapGameState.TriggerListBitHeapSize = size;

            UpdateTriggerControls();
            PopulateTriggerList();
        }

        private void Btn_ClearAllTriggerData_Click(object sender, EventArgs e)
        {
            if (!Allocated) return;

            gameState.BitHeap.ClearBits(
                mapGameState.TriggerListBitHeapAddress, mapGameState.TriggerListBitHeapSize);

            DepopulateTriggerData();
        }

        private void Btn_WriteAllWrittenFlag_Click(object sender, EventArgs e)
        {
            if (!Allocated) return;

            SetAllWrittenFlag(true);

            DepopulateTriggerData();
        }

        private void Btn_ClearAllWrittenFlag_Click(object sender, EventArgs e)
        {
            if (!Allocated) return;

            SetAllWrittenFlag(false);

            DepopulateTriggerData();
        }

        private void SetAllWrittenFlag(bool set)
        {
            if (!Allocated) return;

            var rsc = ResourceHandler.Instance;

            //Check if any needed resources are null
            if (rsc.TriggerTable == null) return;
            if (rsc.Maps == null) return;

            if (!rsc.Maps.TryGetValue(
                mapIndex, out GeoMap? map))
                return;

            int address = mapGameState.TriggerListBitHeapAddress;

            for (int i = 0; i < map.TriggerList.Length; i++)
            {
                var trigger = map.TriggerList[i];

                //TriggerTable index
                int tTableIndex = trigger.GetTriggerTableIndex(rsc.TriggerTable);
                if (tTableIndex < 0) continue;

                //TriggerTable entry
                var tTableEntry = rsc.TriggerTable.Entries[tTableIndex];
                if (tTableEntry.StoredDataSize == 0) continue;

                gameState.BitHeap.WriteBits(1, [set ? (byte)1 : (byte)0], address);

                address += tTableEntry.StoredDataSize + 1;
            }
        }
    }
}
