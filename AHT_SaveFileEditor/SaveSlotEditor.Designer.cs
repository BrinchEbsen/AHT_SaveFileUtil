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
            FlowPanel_Levels = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1176, 886);
            splitContainer1.SplitterDistance = 315;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(FlowPanel_Levels);
            splitContainer2.Size = new Size(315, 886);
            splitContainer2.SplitterDistance = 79;
            splitContainer2.TabIndex = 0;
            // 
            // FlowPanel_Levels
            // 
            FlowPanel_Levels.AutoScroll = true;
            FlowPanel_Levels.Dock = DockStyle.Fill;
            FlowPanel_Levels.FlowDirection = FlowDirection.TopDown;
            FlowPanel_Levels.Location = new Point(0, 0);
            FlowPanel_Levels.Name = "FlowPanel_Levels";
            FlowPanel_Levels.Size = new Size(315, 803);
            FlowPanel_Levels.TabIndex = 0;
            FlowPanel_Levels.WrapContents = false;
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
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private FlowLayoutPanel FlowPanel_Levels;
    }
}