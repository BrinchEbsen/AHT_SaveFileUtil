﻿using AHT_SaveFileEditor.SlotEditor.MapEditor.TriggerDataControls;
using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using AHT_SaveFileUtil.Save.Triggers;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor
{
    public enum SortMode
    {
        Index,
        Name,
        DataSize
    }

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

            bool ps2Hack = false;

            var saveFile = SaveFileHandler.Instance.SaveFile;
            if (saveFile != null)
                ps2Hack = saveFile.Platform == GamePlatform.PlayStation2;

            this.gameState = gameState;
            this.mapIndex = mapIndex;

            mapGameState =
                ps2Hack ?
                gameState.MapStates[(int)mapIndex - 2] :
                gameState.MapStates[(int)mapIndex];

            ComboBox_SortMode.SelectedIndex = (int)SortMode.Index;
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

            UpdateCollectableAmounts();
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
                Lbl_IsAllocated.Text = $"Allocated: Yes ({mapGameState.TriggerListBitHeapAddress})";
                Lbl_MapAllocatedSize.Text = "Size: " + mapGameState.TriggerListBitHeapSize;
            }
            else
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

            if (map.TriggerList == null) return false;

            int bitHeapOffset = 0;

            List<TriggerPanel> list = new(rsc.TriggerTable.NumPreservingEntries);

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
                list.Add(triggerPanel);

                //1+ for written flag
                bitHeapOffset += tTableEntry.StoredDataSize + 1;
            }

            SortTriggerPanelList(list);

            //Clear existing items
            foreach (TriggerPanel panel in FlowPanel_Triggers.Controls)
                panel.Dispose();

            FlowPanel_Triggers.Controls.Clear();

            //add new items
            foreach (var panel in list)
                FlowPanel_Triggers.Controls.Add(panel);

            if (mapGameState.TriggerListBitHeapSize != bitHeapOffset)
                MessageBox.Show(
                    $"This map has allocated {mapGameState.TriggerListBitHeapSize} bits, " +
                    $"but the trigger list was found to use {bitHeapOffset} bits.",
                    "Inconsistent bitheap allocation size.",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            return true;
        }

        private void ComboBox_SortMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FlowPanel_Triggers.SuspendLayout();

            ReSortTriggerPanelList();

            FlowPanel_Triggers.ResumeLayout();
        }

        private void ReSortTriggerPanelList()
        {
            List<TriggerPanel> list = new(FlowPanel_Triggers.Controls.Count);

            foreach (TriggerPanel panel in FlowPanel_Triggers.Controls)
                list.Add(panel);



            FlowPanel_Triggers.Controls.Clear();

            SortTriggerPanelList(list);

            foreach (var panel in list)
                FlowPanel_Triggers.Controls.Add(panel);
        }

        private void SortTriggerPanelList(List<TriggerPanel> list)
        {
            SortMode sortMode;
            try
            {
                sortMode = (SortMode)ComboBox_SortMode.SelectedIndex;
            }
            catch { return; }

            switch (sortMode)
            {
                case SortMode.Index:
                    list.Sort((p1, p2)
                        => p1.TrigIndex.CompareTo(p2.TrigIndex));
                    break;
                case SortMode.Name:
                    list.Sort((p1, p2)
                        => p1.TriggerData.ObjectName.CompareTo(p2.TriggerData.ObjectName));
                    break;
                case SortMode.DataSize:
                    list.Sort((p1, p2)
                        => p1.TriggerData.Size.CompareTo(p2.TriggerData.Size));
                    break;
            }
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
                    case TriggerDataType.Unused:
                        break;
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
                    case TriggerDataType.Float:
                        FlowPanel_TriggerData.Controls.Add(
                            new TriggerDataPanel_Float(
                                bitHeapOffset,
                                definition,
                                gameState
                            ));
                        break;
                    case TriggerDataType.Int32:
                        FlowPanel_TriggerData.Controls.Add(
                            new TriggerDataPanel_Int32(
                                bitHeapOffset,
                                definition,
                                gameState
                            ));
                        break;
                    case TriggerDataType.EXVector:
                        FlowPanel_TriggerData.Controls.Add(
                            new TriggerDataPanel_EXVector(
                                bitHeapOffset,
                                definition,
                                gameState
                            ));
                        break;
                    default:
                        throw new NotImplementedException(
                            $"Trigger data type {definition.Type} not supported yet!");
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

        #region Map Data Controls

        private void Btn_MapAllocate_Click(object sender, EventArgs e)
        {
            if (Allocated) return;

            if (MapData.MapDataList.TryGetValue(mapIndex, out var mapData))
            {
                if (!mapData.DoesPreserve)
                {
                    MessageBox.Show(
                        "This map does not preserve its state to the savefile, so data cannot be edited.",
                        "Cannot allocate.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            var maps = ResourceHandler.Instance.Maps;
            if (maps == null) return;

            var tTable = ResourceHandler.Instance.TriggerTable;
            if (tTable == null) return;

            //this should always succeed but whatevs
            if (!maps.TryGetValue(mapIndex, out var map))
                return;

            if (map.TriggerList == null)
            {
                MessageBox.Show(
                    "Cannot allocate data for triggers, as the map has none.",
                    "Cannot allocate.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int size = map.GetBitHeapSize(tTable);

            int address;
            try
            {
                address = gameState.BitHeap.Allocate(size);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Cannot allocate.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

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

                //+1 for the writtenflag
                address += tTableEntry.StoredDataSize + 1;
            }
        }

        #endregion

        #region CollectableAmounts

        private void UpdateCollectableAmounts()
        {
            Num_LightGemsAmount.Value = mapGameState.NumLightGems;
            Num_LightGemsMax.Value = mapGameState.MaxLightGems;

            Num_DarkGemsAmount.Value = mapGameState.NumDarkGems;
            Num_DarkGemsMax.Value = mapGameState.MaxDarkGems;

            Num_DragonEggs_ConceptArt.Value = mapGameState.NumEggs_ConceptArt;
            Num_DragonEggs_ModelViewer.Value = mapGameState.NumEggs_ModelViewer;
            Num_DragonEggs_Ember.Value = mapGameState.NumEggs_Ember;
            Num_DragonEggs_Flame.Value = mapGameState.NumEggs_Flame;
            Num_DragonEggs_SgtByrd.Value = mapGameState.NumEggs_SgtByrd;
            Num_DragonEggs_Turret.Value = mapGameState.NumEggs_Turret;
            Num_DragonEggs_Sparx.Value = mapGameState.NumEggs_Sparx;
            Num_DragonEggs_Blink.Value = mapGameState.NumEggs_Blink;

            Num_DragonEggsMax.Value = mapGameState.MaxDragonEggs;

            UpdateDragonEggSum();
        }

        private void UpdateDragonEggSum()
        {
            Lbl_EggsSum.Text = $"Sum: {mapGameState.SumOfEggs}";
        }

        private void Num_DragonEggsAmount_ValueChanged(object? sender, EventArgs e)
        {
            if (sender is null) return;

            NumericUpDown num = (NumericUpDown)sender;

            string senderName = num.Name;

            switch (senderName)
            {
                case "Num_DragonEggs_ConceptArt":
                    mapGameState.NumEggs_ConceptArt = (int)num.Value;
                    break;
                case "Num_DragonEggs_ModelViewer":
                    mapGameState.NumEggs_ModelViewer = (int)num.Value;
                    break;
                case "Num_DragonEggs_Ember":
                    mapGameState.NumEggs_Ember = (int)num.Value;
                    break;
                case "Num_DragonEggs_Flame":
                    mapGameState.NumEggs_Flame = (int)num.Value;
                    break;
                case "Num_DragonEggs_SgtByrd":
                    mapGameState.NumEggs_SgtByrd = (int)num.Value;
                    break;
                case "Num_DragonEggs_Turret":
                    mapGameState.NumEggs_Turret = (int)num.Value;
                    break;
                case "Num_DragonEggs_Sparx":
                    mapGameState.NumEggs_Sparx = (int)num.Value;
                    break;
                case "Num_DragonEggs_Blink":
                    mapGameState.NumEggs_Blink = (int)num.Value;
                    break;
            }

            UpdateDragonEggSum();
        }

        private void Num_DragonEggsMax_ValueChanged(object sender, EventArgs e)
        {
            mapGameState.MaxDragonEggs = (int)Num_DragonEggsMax.Value;
        }

        private void Num_LightGemsAmount_ValueChanged(object sender, EventArgs e)
        {
            mapGameState.NumLightGems = (int)Num_LightGemsAmount.Value;
        }

        private void Num_LightGemsMax_ValueChanged(object sender, EventArgs e)
        {
            mapGameState.MaxLightGems = (int)Num_LightGemsMax.Value;
        }

        private void Num_DarkGemsAmount_ValueChanged(object sender, EventArgs e)
        {
            mapGameState.NumDarkGems = (int)Num_DarkGemsAmount.Value;
        }

        private void Num_DarkGemsMax_ValueChanged(object sender, EventArgs e)
        {
            mapGameState.MaxDarkGems = (int)Num_DarkGemsMax.Value;
        }

        #endregion
    }
}
