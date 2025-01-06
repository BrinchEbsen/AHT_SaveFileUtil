using AHT_SaveFileUtil.Save;

namespace AHT_SaveFileEditor
{
    internal class MiniGameBestTimeCell : DataGridViewTextBoxCell
    {
        private MiniGameBestTime bestTime;
        private bool isHard;

        public MiniGameBestTimeCell(MiniGameBestTime bestTime, bool isHard)
        {
            this.bestTime = bestTime;
            this.isHard = isHard;
            SetValueFromTime();
        }

        public bool CheckChangeValue()
        {
            if (Value == null)
            {
                SetValueFromTime(); return false;
            }

            //split into left and right of ":"
            string newVal = (string)Value!;
            string[] parts = newVal.Split(':');
            if (parts.Length != 2)
            {
                SetValueFromTime(); return false;
            }

            int minutes;
            int seconds;

            //Attempt to parse numbers to check if they're correct
            try
            {
                minutes = int.Parse(parts[0]);
                seconds = int.Parse(parts[1]);
            } catch
            {
                SetValueFromTime(); return false;
            }

            if (minutes < 0
                || seconds < 0
                || seconds >= 60) //seconds over 59 should spill into minutes
            {
                SetValueFromTime(); return false;
            }

            if (isHard)
            {
                bestTime.HardTimeMinutes = minutes;
                bestTime.HardTimeSeconds += seconds;
            } else
            {
                bestTime.EasyTimeMinutes = minutes;
                bestTime.EasyTimeSeconds += seconds;
            }

            //Call this anyway to make sure it's formatted correctly
            SetValueFromTime();
            return true;
        }

        private void SetValueFromTime()
        {
            Value = isHard ? bestTime.HardTimeString : bestTime.EasyTimeString;
        }
    }
}
