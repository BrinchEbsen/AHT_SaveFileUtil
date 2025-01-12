using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.Slot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AHT_SaveFileEditor.SlotEditor
{
    internal class TaskPanel : Panel
    {
        private const int COMMON_HEIGHT = 17;

        private GameState gameState;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EXHashCode TaskHash { get; private set; }
        private CheckBox checkFound;
        private CheckBox checkDone;

        public TaskPanel(FlowLayoutPanel parent, GameState gameState, EXHashCode taskHash)
        {
            this.gameState = gameState;
            this.TaskHash = taskHash;

            Width = parent.Width - 26;
            Height = COMMON_HEIGHT;

            TaskStates state = gameState.GetTaskState(taskHash);

            checkFound = new()
            {
                Text = "Found",
                Height = COMMON_HEIGHT,
                Width = 60,
                Location = new Point(0, 0)
            };

            checkFound.Checked = ((uint)state & (uint)TaskStates.Found) != 0;
            checkFound.CheckedChanged += CheckFound_CheckedChanged;

            checkDone = new()
            {
                Text = "Done",
                Height = COMMON_HEIGHT,
                Width = 60,
                Location = new Point(60, 0)
            };

            checkDone.Checked = ((uint)state & (uint)TaskStates.Done) != 0;
            checkDone.CheckedChanged += CheckDone_CheckedChanged;

            Controls.Add(checkFound);
            Controls.Add(checkDone);
            Controls.Add(new Label()
            {
                Text = taskHash.ToString().Replace("HT_Tasks_", ""),
                Height = COMMON_HEIGHT,
                Width = 150,
                Location = new Point(120, 0)
            });
        }

        private void CheckFound_CheckedChanged(object? sender, EventArgs e)
        {
            SetFound(checkFound.Checked);
        }

        private void CheckDone_CheckedChanged(object? sender, EventArgs e)
        {
            SetDone(checkDone.Checked);
        }

        internal void SetFound(bool found)
        {
            uint state = (uint)gameState.GetTaskState(TaskHash);

            if (found)
                state |= (uint)TaskStates.Found;
            else
                state &= ~(uint)TaskStates.Found;

            gameState.SetTaskState(TaskHash, (TaskStates)state);

            checkFound.Checked = found;
        }

        internal void SetDone(bool done)
        {
            uint state = (uint)gameState.GetTaskState(TaskHash);

            if (done)
                state |= (uint)TaskStates.Done;
            else
                state &= ~(uint)TaskStates.Done;

            gameState.SetTaskState(TaskHash, (TaskStates)state);

            checkDone.Checked = done;
        }

        internal void SetState(TaskStates newState)
        {
            gameState.SetTaskState(TaskHash, newState);

            checkFound.Checked = ((uint)newState & (uint)TaskStates.Found) != 0;
            checkDone.Checked  = ((uint)newState & (uint)TaskStates.Done)  != 0;
        }
    }
}
