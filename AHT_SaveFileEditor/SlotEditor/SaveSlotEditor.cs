using AHT_SaveFileEditor.SlotEditor;
using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;

namespace AHT_SaveFileEditor
{
    public partial class SaveSlotEditor : Form
    {
        private MainWnd mainWnd;

        private readonly SaveSlot Slot;

        private bool monitorPlayTimerFields = true;

        private Dictionary<int, EXHashCode> CheckList_Objectives_Mapping = [];

        private Dictionary<int, uint> CheckList_AbilityFlags_Mapping = [];

        public SaveSlotEditor(MainWnd mainWnd, SaveSlot slot, int slotIndex)
        {
            InitializeComponent();

            this.mainWnd = mainWnd;
            Slot = slot;

            Lbl_SlotIndex.Text = "Slot " + (slotIndex + 1);
        }

        private void SaveSlotEditor_Load(object sender, EventArgs e)
        {
            InitializeControls();
        }

        private void InitializeControls()
        {
            PopulateLevelsPanel();
            UpdateStartingLevelLabel();

            monitorPlayTimerFields = false;
            Num_PlayTimeHours.Value = Slot.GameState.PlayTimerHours;
            Num_PlayTimeMinutes.Value = Slot.GameState.PlayTimerMinutes % 60;
            Num_PlayTimeSeconds.Value = (int)Slot.GameState.PlayTimer % 60;
            monitorPlayTimerFields = true;

            TextBox_StartTime.Text = Slot.GameState.StartTime.ToString();

            UpdateCompletionPercentage();

            UpdateHealthControls();

            InitializeObjectivesMapping();
            InitializeAbilityFlagsMapping();

            PopulateObjectivesCheckList();
            PopulateTasksFlowPanel();
            PopulateAbilityFlagsCheckList();

            Check_FileUsed.Checked = Slot.IsUsed;

            var playerState = Slot.GameState.PlayerState;

            Num_Gems.Value = playerState.Gems;
            Num_TotalGems.Value = playerState.TotalGems;

            FlowPanel_PowerUps.Controls.AddRange([
                new CollectableStatsPanel(playerState, CollectableStatsPanelType.LockPickers),
                new CollectableStatsPanel(playerState, CollectableStatsPanelType.FireBombs),
                new CollectableStatsPanel(playerState, CollectableStatsPanelType.IceBombs),
                new CollectableStatsPanel(playerState, CollectableStatsPanelType.WaterBombs),
                new CollectableStatsPanel(playerState, CollectableStatsPanelType.ElectricBombs),
                new CollectableStatsPanel(playerState, CollectableStatsPanelType.FireArrows),
            ]);

            Num_LightGems.Value = playerState.TotalLightGems;
            Num_DarkGems.Value = playerState.TotalDarkGems;
            Num_DragonEggs.Value = playerState.TotalDragonEggs;

            Num_SgtByrdBombs.Value = playerState.SgtByrdBombs;
            Num_SgtByrdMissiles.Value = playerState.SgtByrdMissiles;
            Num_SparxBombs.Value = playerState.SparxSmartBombs;
            Num_SparxMissiles.Value = playerState.SparxSeekers;
            Num_BlinkBombs.Value = playerState.BlinkBombs;

            Num_Supercharge.Value = (decimal)playerState.SuperchargeTimer;
            Num_SuperchargeMax.Value = (decimal)playerState.SuperchargeTimerMax;
            Num_Invincibility.Value = (decimal)playerState.InvincibleTimer;
            Num_InvincibilityMax.Value = (decimal)playerState.InvincibleTimerMax;
            Num_DoubleGem.Value = (decimal)playerState.DoubleGemTimer;
            Num_DoubleGemMax.Value = (decimal)playerState.DoubleGemTimerMax;
        }

        private void Check_FileUsed_CheckedChanged(object sender, EventArgs e)
        {
            Slot.IsUsed = Check_FileUsed.Checked;
        }

        private void Btn_SetStartTime_Click(object sender, EventArgs e)
        {
            if (!Slot.GameState.StartTime.SetFromString(TextBox_StartTime.Text))
            {
                TextBox_StartTime.Text = Slot.GameState.StartTime.ToString();

                MessageBox.Show("Time must be formatted as \"D-M-YYYY | H:MM:SS\".", "Invalid format",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveSlotEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWnd.PopulateSaveSlotPanel();
        }

        private void UpdateCompletionPercentage()
        {
            Label_Completion.Text = $"{Slot.GameState.CompletionPercentage:0.00}%";
            Lbl_CompletionCalc.Text = string.Format(
                "= (Light Gems ({0}) + Dark Gems ({1}) + Dragon Eggs ({2}) + Final Boss Beaten ({3})) / 221 * 100",
                Slot.GameState.PlayerState.TotalLightGems,
                Slot.GameState.PlayerState.TotalDarkGems,
                Slot.GameState.PlayerState.TotalDragonEggs,
                Slot.GameState.GetObjective(EXHashCode.HT_Objective_Boss4_Beaten) ? 1 : 0
                );
        }

        #region PlayerState Vars
        private void UpdateHealthControls()
        {
            Num_Health.Value = Slot.GameState.PlayerState.Health;
            UpdateHealthDescriptor();
        }

        private void Num_Health_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.Health = (int)Num_Health.Value;
            UpdateHealthDescriptor();
        }

        private void UpdateHealthDescriptor()
        {
            int health = Slot.GameState.PlayerState.Health;
            string descr;

            //If health isn't a valid value
            if (PlayerState.RoundHealthToValid(health) != health)
            {
                descr = "???";
            }
            else
            {
                Dictionary<int, string> names;

                if (Slot.GameState.PlayerState.AF_HitPointUpgrade)
                    names = PlayerState.HealthStrings_Upgrade;
                else
                    names = PlayerState.HealthStrings_NoUpgrade;

                descr = names[health];
            }

            Lbl_HealthDescriptor.Text = descr;
        }

        private void Btn_Health_Damage_Click(object sender, EventArgs e)
        {
            int health = (int)Num_Health.Value;

            health = PlayerState.RoundHealthToValid(health - 0x20);

            Num_Health.Value = health;
        }

        private void Btn_Health_Heal_Click(object sender, EventArgs e)
        {
            int health = (int)Num_Health.Value;

            health = PlayerState.RoundHealthToValid(health + 0x20);

            Num_Health.Value = health;
        }

        private void Num_Gems_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.Gems = (int)Num_Gems.Value;
        }

        private void Num_TotalGems_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.TotalGems = (int)Num_TotalGems.Value;
        }
        #endregion

        #region Objectives
        private void InitializeObjectivesMapping()
        {
            int index = 0;
            uint mask = (uint)EXHashCode.HT_Objective_HASHCODE_BASE;

            for (int i = 0; i < GameState.MAX_NUM_OBJECTIVES; i++)
            {
                uint hash = ((uint)i + 1) | mask;

                if (hash == (uint)EXHashCode.HT_Objective_HASHCODE_END)
                    continue;

                if (Enum.IsDefined(typeof(EXHashCode), hash))
                {
                    CheckList_Objectives_Mapping.Add(index, (EXHashCode)hash);
                    index++;
                }
            }
        }

        private void PopulateObjectivesCheckList()
        {
            CheckList_Objectives.Items.Clear();

            foreach (var entry in CheckList_Objectives_Mapping)
            {
                CheckList_Objectives.Items.Add(
                    entry.Value.ToString().Replace("HT_Objective_", ""),
                    Slot.GameState.GetObjective(entry.Value)
                    );
            }
        }

        private void Btn_SetAllObjectives_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CheckList_Objectives.Items.Count; i++)
                CheckList_Objectives.SetItemChecked(i, true);
        }

        private void Btn_ClearAllObjectives_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CheckList_Objectives.Items.Count; i++)
                CheckList_Objectives.SetItemChecked(i, false);
        }

        private void CheckList_Objectives_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool newValue = e.NewValue == CheckState.Checked;

            EXHashCode obj = CheckList_Objectives_Mapping[e.Index];
            Slot.GameState.SetObjective(obj, newValue);

            if (obj == EXHashCode.HT_Objective_Boss4_Beaten)
                UpdateCompletionPercentage();
        }
        #endregion

        #region Tasks
        private void PopulateTasksFlowPanel()
        {
            foreach (TaskPanel panel in FlowPanel_Tasks.Controls)
                panel.Dispose();

            FlowPanel_Tasks.Controls.Clear();

            for (uint hash = (uint)EXHashCode.HT_Tasks_NONE + 1;
                 hash < (uint)EXHashCode.HT_Tasks_HASHCODE_END;
                 hash++)
            {
                if (!Enum.IsDefined(typeof(EXHashCode), hash))
                    continue;

                FlowPanel_Tasks.Controls.Add(
                    new TaskPanel(
                        FlowPanel_Tasks,
                        Slot.GameState,
                        (EXHashCode)hash
                    )
                );
            }
        }

        private void Btn_FindAllTasks_Click(object sender, EventArgs e)
        {
            foreach (TaskPanel panel in FlowPanel_Tasks.Controls)
                panel.SetFound(true);
        }

        private void Btn_DoAllTasks_Click(object sender, EventArgs e)
        {
            foreach (TaskPanel panel in FlowPanel_Tasks.Controls)
                panel.SetDone(true);
        }

        private void Btn_ClearAllTasks_Click(object sender, EventArgs e)
        {
            foreach (TaskPanel panel in FlowPanel_Tasks.Controls)
                panel.SetState(TaskStates.Undiscovered);
        }
        #endregion

        #region Ability Flags
        private void InitializeAbilityFlagsMapping()
        {
            int i = 0;
            foreach (var pair in PlayerState.AbilityFlagNames)
            {
                //Aqualung is unused, so skip it
                if (pair.Key != PlayerState.AF_MASK_AQUALUNG)
                {
                    CheckList_AbilityFlags_Mapping.Add(i, pair.Key);
                    i++;
                }
            }
        }

        private void PopulateAbilityFlagsCheckList()
        {
            CheckList_AbilityFlags.Items.Clear();

            foreach (var pair in CheckList_AbilityFlags_Mapping)
            {
                CheckList_AbilityFlags.Items.Add(
                    PlayerState.AbilityFlagNames[pair.Value],
                    Slot.GameState.PlayerState.GetAbilityFlag(pair.Value)
                    );
            }
        }

        private void CheckList_AbilityFlags_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool newValue = e.NewValue == CheckState.Checked;

            uint mask = CheckList_AbilityFlags_Mapping[e.Index];
            Slot.GameState.PlayerState.SetAbilityFlag(mask, newValue);

            if (mask == PlayerState.AF_MASK_HIT_POINT_UPGRADE)
                UpdateHealthDescriptor();
        }

        private void Btn_SetAllAbilityFlags_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CheckList_AbilityFlags.Items.Count; i++)
                CheckList_AbilityFlags.SetItemChecked(i, true);
        }

        private void Btn_ClearAllAbilityFlags_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CheckList_AbilityFlags.Items.Count; i++)
                CheckList_AbilityFlags.SetItemChecked(i, false);
        }
        #endregion

        #region LevelPanel
        private void UpdateStartingLevelLabel()
        {
            bool entryExists =
                MapData.MapDataList.TryGetValue(Slot.GameState.StartingMap, out var entry);

            if (entryExists)
                Label_StartingLevel.Text = entry!.Name;
            else
                Label_StartingLevel.Text = Slot.GameState.StartingMap.ToString();
        }

        private void PopulateLevelsPanel()
        {
            foreach (LevelEntryPanel ctrl in FlowPanel_Levels.Controls)
                ctrl.Dispose();

            FlowPanel_Levels.Controls.Clear();

            foreach (var entry in MapData.MapDataList)
            {
                if (!Check_ShowNonPreserving.Checked && !entry.Value.DoesPreserve)
                    continue;

                if (!Check_ShowUnused.Checked && entry.Value.Unused)
                    continue;

                FlowPanel_Levels.Controls.Add(new LevelEntryPanel(this, Slot.GameState, entry.Key)
                {
                    Width = FlowPanel_Levels.Width - 24
                });
            }

            ColourStartingMap();
        }

        private void Check_ShowNonPreserving_CheckedChanged(object sender, EventArgs e)
        {
            PopulateLevelsPanel();
        }

        private void Check_ShowUnused_CheckedChanged(object sender, EventArgs e)
        {
            PopulateLevelsPanel();
        }

        internal void SetStartingMap(Map map)
        {
            Slot.GameState.StartingMap = map;
            UpdateStartingLevelLabel();
            ColourStartingMap();
        }

        private void ColourStartingMap()
        {
            foreach (LevelEntryPanel entry in FlowPanel_Levels.Controls)
            {
                if (entry.EntryNr == Slot.GameState.StartingMap)
                    entry.BackColor = Color.LightBlue;
                else
                    entry.BackColor = Color.Azure;
            }
        }
        #endregion

        #region PlayTimer
        private void Num_PlayTimeHours_ValueChanged(object sender, EventArgs e)
        {
            if (!monitorPlayTimerFields) return;

            UpdatePlayTimer();
        }

        private void Num_PlayTimeMinutes_ValueChanged(object sender, EventArgs e)
        {
            bool updateTimer = monitorPlayTimerFields;

            monitorPlayTimerFields = false;

            if (Num_PlayTimeMinutes.Value < 0)
            {
                if (Num_PlayTimeHours.Value > 0)
                {
                    Num_PlayTimeHours.Value--;
                    Num_PlayTimeMinutes.Value = 59;
                }
                else
                {
                    Num_PlayTimeMinutes.Value = 0;
                }
            }
            else if (Num_PlayTimeMinutes.Value > 59)
            {
                Num_PlayTimeHours.Value++;
                Num_PlayTimeMinutes.Value = 0;
            }

            monitorPlayTimerFields = true;

            if (updateTimer)
                UpdatePlayTimer();
        }

        private void Num_PlayTimeSeconds_ValueChanged(object sender, EventArgs e)
        {
            bool updateTimer = monitorPlayTimerFields;

            monitorPlayTimerFields = false;

            if (Num_PlayTimeSeconds.Value < 0)
            {
                if (Num_PlayTimeMinutes.Value > 0 || Num_PlayTimeHours.Value > 0)
                {
                    Num_PlayTimeMinutes.Value--;
                    Num_PlayTimeSeconds.Value = 59;
                }
                else
                {
                    Num_PlayTimeSeconds.Value = 0;
                }
            }
            else if (Num_PlayTimeSeconds.Value > 59)
            {
                Num_PlayTimeMinutes.Value++;
                Num_PlayTimeSeconds.Value = 0;
            }

            monitorPlayTimerFields = true;

            if (updateTimer)
                UpdatePlayTimer();
        }

        private void UpdatePlayTimer()
        {
            Slot.GameState.SetPlayTimer(
                (int)Num_PlayTimeHours.Value,
                (int)Num_PlayTimeMinutes.Value,
                (int)Num_PlayTimeSeconds.Value
                );
        }
        #endregion

        private void Num_LightGems_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.TotalLightGems = (sbyte)Num_LightGems.Value;
            UpdateCompletionPercentage();
        }

        private void Num_DarkGems_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.TotalDarkGems = (sbyte)Num_DarkGems.Value;
            UpdateCompletionPercentage();
        }

        private void Num_DragonEggs_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.TotalDragonEggs = (sbyte)Num_DragonEggs.Value;
            UpdateCompletionPercentage();
        }

        private void Num_SgtByrdBombs_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.SgtByrdBombs = (short)Num_SgtByrdBombs.Value;
        }

        private void Num_SgtByrdMissiles_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.SgtByrdMissiles = (short)Num_SgtByrdMissiles.Value;
        }

        private void Num_SparxBombs_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.SparxSmartBombs = (short)Num_SparxBombs.Value;
        }

        private void Num_SparxMissiles_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.SparxSeekers = (short)Num_SparxMissiles.Value;
        }

        private void Num_BlinkBombs_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.BlinkBombs = (short)Num_BlinkBombs.Value;
        }

        private void Num_Supercharge_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.SuperchargeTimer = (float)Num_Supercharge.Value;
        }

        private void Num_SuperchargeMax_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.SuperchargeTimerMax = (float)Num_SuperchargeMax.Value;
        }

        private void Num_Invincibility_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.InvincibleTimer = (float)Num_Invincibility.Value;
        }

        private void Num_InvincibilityMax_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.InvincibleTimerMax = (float)Num_InvincibilityMax.Value;
        }

        private void Num_DoubleGem_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.DoubleGemTimer = (float)Num_DoubleGem.Value;
        }

        private void Num_DoubleGemMax_ValueChanged(object sender, EventArgs e)
        {
            Slot.GameState.PlayerState.DoubleGemTimerMax = (float)Num_DoubleGemMax.Value;
        }
    }
}
