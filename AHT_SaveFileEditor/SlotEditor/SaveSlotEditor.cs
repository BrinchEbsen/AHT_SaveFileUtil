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

        public SaveSlotEditor(MainWnd mainWnd, SaveSlot slot)
        {
            InitializeComponent();

            this.mainWnd = mainWnd;
            Slot = slot;
        }

        private void SaveSlotEditor_Load(object sender, EventArgs e)
        {
            PopulateLevelsPanel();
            UpdateStartingLevelLabel();

            monitorPlayTimerFields = false;
            Num_PlayTimeHours.Value = Slot.GameState.PlayTimerHours;
            Num_PlayTimeMinutes.Value = Slot.GameState.PlayTimerMinutes % 60;
            Num_PlayTimeSeconds.Value = (int)Slot.GameState.PlayTimer % 60;
            monitorPlayTimerFields = true;

            TextBox_StartTime.Text = Slot.GameState.StartTime.ToString();

            Label_Completion.Text = $"{Slot.GameState.CompletionPercentage:0.00}%";

            InitializeObjectivesMapping();
            PopulateObjectivesCheckList();
            PopulateTasksFlowPanel();

            Check_FileUsed.Checked = Slot.IsUsed;
        }

        private void InitializeObjectivesMapping()
        {
            int index = 0;
            uint mask = (uint)EXHashCode.HT_Objective_HASHCODE_BASE;

            for (int i = 0; i < GameState.NUM_OBJECTIVES; i++)
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

        private void CheckList_Objectives_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool newValue = e.NewValue == CheckState.Checked;

            EXHashCode obj = CheckList_Objectives_Mapping[e.Index];
            Slot.GameState.SetObjective(obj, newValue);
        }

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

        private void Check_FileUsed_CheckedChanged(object sender, EventArgs e)
        {
            Slot.IsUsed = Check_FileUsed.Checked;
        }

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

        private void Btn_SetStartTime_Click(object sender, EventArgs e)
        {
            if (!Slot.GameState.StartTime.SetFromString(TextBox_StartTime.Text))
            {
                TextBox_StartTime.Text = Slot.GameState.StartTime.ToString();

                MessageBox.Show("Time must be formatted as \"D-M-YYYY | H:MM:SS\".", "Invalid format",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SaveSlotEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWnd.PopulateSaveSlotPanel();
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
    }
}
