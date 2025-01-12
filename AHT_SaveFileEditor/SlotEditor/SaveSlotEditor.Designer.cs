
namespace AHT_SaveFileEditor
{
    partial class SaveSlotEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            Check_ShowUnused = new CheckBox();
            Check_ShowNonPreserving = new CheckBox();
            FlowPanel_Levels = new FlowLayoutPanel();
            Label_Completion = new Label();
            label7 = new Label();
            Btn_SetStartTime = new Button();
            TextBox_StartTime = new TextBox();
            label6 = new Label();
            label5 = new Label();
            Num_PlayTimeSeconds = new NumericUpDown();
            label4 = new Label();
            Num_PlayTimeMinutes = new NumericUpDown();
            label3 = new Label();
            Num_PlayTimeHours = new NumericUpDown();
            label2 = new Label();
            label1 = new Label();
            Label_StartingLevel = new Label();
            Check_FileUsed = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Num_PlayTimeSeconds).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_PlayTimeMinutes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_PlayTimeHours).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(Label_Completion);
            splitContainer1.Panel2.Controls.Add(label7);
            splitContainer1.Panel2.Controls.Add(Btn_SetStartTime);
            splitContainer1.Panel2.Controls.Add(TextBox_StartTime);
            splitContainer1.Panel2.Controls.Add(label6);
            splitContainer1.Panel2.Controls.Add(label5);
            splitContainer1.Panel2.Controls.Add(Num_PlayTimeSeconds);
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Controls.Add(Num_PlayTimeMinutes);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(Num_PlayTimeHours);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Panel2.Controls.Add(Label_StartingLevel);
            splitContainer1.Panel2.Controls.Add(Check_FileUsed);
            splitContainer1.Size = new Size(1176, 886);
            splitContainer1.SplitterDistance = 316;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(Check_ShowUnused);
            splitContainer2.Panel1.Controls.Add(Check_ShowNonPreserving);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(FlowPanel_Levels);
            splitContainer2.Size = new Size(316, 886);
            splitContainer2.SplitterDistance = 66;
            splitContainer2.TabIndex = 0;
            // 
            // Check_ShowUnused
            // 
            Check_ShowUnused.AutoSize = true;
            Check_ShowUnused.Location = new Point(12, 37);
            Check_ShowUnused.Name = "Check_ShowUnused";
            Check_ShowUnused.Size = new Size(130, 19);
            Check_ShowUnused.TabIndex = 1;
            Check_ShowUnused.Text = "Show Unused Maps";
            Check_ShowUnused.UseVisualStyleBackColor = true;
            Check_ShowUnused.CheckedChanged += Check_ShowUnused_CheckedChanged;
            // 
            // Check_ShowNonPreserving
            // 
            Check_ShowNonPreserving.AutoSize = true;
            Check_ShowNonPreserving.Location = new Point(12, 12);
            Check_ShowNonPreserving.Name = "Check_ShowNonPreserving";
            Check_ShowNonPreserving.Size = new Size(173, 19);
            Check_ShowNonPreserving.TabIndex = 0;
            Check_ShowNonPreserving.Text = "Show Non-Preserving Maps";
            Check_ShowNonPreserving.UseVisualStyleBackColor = true;
            Check_ShowNonPreserving.CheckedChanged += Check_ShowNonPreserving_CheckedChanged;
            // 
            // FlowPanel_Levels
            // 
            FlowPanel_Levels.AutoScroll = true;
            FlowPanel_Levels.Dock = DockStyle.Fill;
            FlowPanel_Levels.FlowDirection = FlowDirection.TopDown;
            FlowPanel_Levels.Location = new Point(0, 0);
            FlowPanel_Levels.Name = "FlowPanel_Levels";
            FlowPanel_Levels.Size = new Size(316, 816);
            FlowPanel_Levels.TabIndex = 0;
            FlowPanel_Levels.WrapContents = false;
            // 
            // Label_Completion
            // 
            Label_Completion.AutoSize = true;
            Label_Completion.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label_Completion.Location = new Point(89, 116);
            Label_Completion.Name = "Label_Completion";
            Label_Completion.Size = new Size(44, 20);
            Label_Completion.TabIndex = 14;
            Label_Completion.Text = "temp";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(3, 116);
            label7.Name = "label7";
            label7.Size = new Size(86, 20);
            label7.TabIndex = 13;
            label7.Text = "Completed:";
            // 
            // Btn_SetStartTime
            // 
            Btn_SetStartTime.Location = new Point(217, 90);
            Btn_SetStartTime.Name = "Btn_SetStartTime";
            Btn_SetStartTime.Size = new Size(45, 23);
            Btn_SetStartTime.TabIndex = 12;
            Btn_SetStartTime.Text = "Set";
            Btn_SetStartTime.UseVisualStyleBackColor = true;
            Btn_SetStartTime.Click += Btn_SetStartTime_Click;
            // 
            // TextBox_StartTime
            // 
            TextBox_StartTime.Location = new Point(89, 90);
            TextBox_StartTime.Name = "TextBox_StartTime";
            TextBox_StartTime.Size = new Size(122, 23);
            TextBox_StartTime.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(3, 93);
            label6.Name = "label6";
            label6.Size = new Size(80, 20);
            label6.TabIndex = 10;
            label6.Text = "Start Time:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(268, 68);
            label5.Name = "label5";
            label5.Size = new Size(16, 15);
            label5.TabIndex = 9;
            label5.Text = "S:";
            // 
            // Num_PlayTimeSeconds
            // 
            Num_PlayTimeSeconds.Location = new Point(290, 60);
            Num_PlayTimeSeconds.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            Num_PlayTimeSeconds.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            Num_PlayTimeSeconds.Name = "Num_PlayTimeSeconds";
            Num_PlayTimeSeconds.Size = new Size(47, 23);
            Num_PlayTimeSeconds.TabIndex = 8;
            Num_PlayTimeSeconds.ValueChanged += Num_PlayTimeSeconds_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(190, 68);
            label4.Name = "label4";
            label4.Size = new Size(21, 15);
            label4.TabIndex = 7;
            label4.Text = "M:";
            // 
            // Num_PlayTimeMinutes
            // 
            Num_PlayTimeMinutes.Location = new Point(215, 60);
            Num_PlayTimeMinutes.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            Num_PlayTimeMinutes.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            Num_PlayTimeMinutes.Name = "Num_PlayTimeMinutes";
            Num_PlayTimeMinutes.Size = new Size(47, 23);
            Num_PlayTimeMinutes.TabIndex = 6;
            Num_PlayTimeMinutes.ValueChanged += Num_PlayTimeMinutes_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(85, 68);
            label3.Name = "label3";
            label3.Size = new Size(19, 15);
            label3.TabIndex = 5;
            label3.Text = "H:";
            // 
            // Num_PlayTimeHours
            // 
            Num_PlayTimeHours.Location = new Point(110, 60);
            Num_PlayTimeHours.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            Num_PlayTimeHours.Name = "Num_PlayTimeHours";
            Num_PlayTimeHours.Size = new Size(74, 23);
            Num_PlayTimeHours.TabIndex = 4;
            Num_PlayTimeHours.ValueChanged += Num_PlayTimeHours_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(3, 63);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 3;
            label2.Text = "Play Time:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 37);
            label1.Name = "label1";
            label1.Size = new Size(94, 20);
            label1.TabIndex = 1;
            label1.Text = "Current Map:";
            // 
            // Label_StartingLevel
            // 
            Label_StartingLevel.AutoSize = true;
            Label_StartingLevel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label_StartingLevel.Location = new Point(93, 37);
            Label_StartingLevel.Name = "Label_StartingLevel";
            Label_StartingLevel.Size = new Size(44, 20);
            Label_StartingLevel.TabIndex = 2;
            Label_StartingLevel.Text = "temp";
            // 
            // Check_FileUsed
            // 
            Check_FileUsed.AutoSize = true;
            Check_FileUsed.Location = new Point(3, 12);
            Check_FileUsed.Name = "Check_FileUsed";
            Check_FileUsed.Size = new Size(90, 19);
            Check_FileUsed.TabIndex = 0;
            Check_FileUsed.Text = "File Is In Use";
            Check_FileUsed.UseVisualStyleBackColor = true;
            Check_FileUsed.CheckedChanged += Check_FileUsed_CheckedChanged;
            // 
            // SaveSlotEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1176, 886);
            Controls.Add(splitContainer1);
            Name = "SaveSlotEditor";
            ShowIcon = false;
            Text = "Save Slot Editor";
            Load += SaveSlotEditor_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Num_PlayTimeSeconds).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_PlayTimeMinutes).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_PlayTimeHours).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private FlowLayoutPanel FlowPanel_Levels;
        private CheckBox Check_ShowUnused;
        private CheckBox Check_ShowNonPreserving;
        private CheckBox Check_FileUsed;
        private Label label1;
        private Label Label_StartingLevel;
        private Label label2;
        private Label label5;
        private NumericUpDown Num_PlayTimeSeconds;
        private Label label4;
        private NumericUpDown Num_PlayTimeMinutes;
        private Label label3;
        private NumericUpDown Num_PlayTimeHours;
        private TextBox TextBox_StartTime;
        private Label label6;
        private Button Btn_SetStartTime;
        private Label Label_Completion;
        private Label label7;
    }
}