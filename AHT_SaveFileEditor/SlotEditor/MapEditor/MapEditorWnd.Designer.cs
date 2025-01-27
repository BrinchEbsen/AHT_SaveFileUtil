namespace AHT_SaveFileEditor.SlotEditor.MapEditor
{
    partial class MapEditorWnd
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
            splitContainer4 = new SplitContainer();
            label1 = new Label();
            ComboBox_SortMode = new ComboBox();
            Lbl_MapAllocatedSize = new Label();
            Lbl_IsAllocated = new Label();
            Btn_MapAllocate = new Button();
            Btn_ClearAllWrittenFlag = new Button();
            Btn_WriteAllWrittenFlag = new Button();
            Btn_ClearAllTriggerData = new Button();
            FlowPanel_Triggers = new FlowLayoutPanel();
            splitContainer3 = new SplitContainer();
            splitContainer5 = new SplitContainer();
            Panel_MiniMap = new Panel();
            FlowPanel_TriggerData = new FlowLayoutPanel();
            GroupBox_DrawControls = new GroupBox();
            Btn_PaintReveal = new Button();
            Check_ShowSquares = new CheckBox();
            Btn_PaintUnreveal = new Button();
            Check_ShowMiniMap = new CheckBox();
            Btn_PaintFill = new Button();
            Btn_PaintClear = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer5).BeginInit();
            splitContainer5.Panel1.SuspendLayout();
            splitContainer5.Panel2.SuspendLayout();
            splitContainer5.SuspendLayout();
            GroupBox_DrawControls.SuspendLayout();
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
            splitContainer1.Panel1.Controls.Add(splitContainer4);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer3);
            splitContainer1.Size = new Size(1124, 775);
            splitContainer1.SplitterDistance = 300;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer4
            // 
            splitContainer4.Dock = DockStyle.Fill;
            splitContainer4.FixedPanel = FixedPanel.Panel1;
            splitContainer4.IsSplitterFixed = true;
            splitContainer4.Location = new Point(0, 0);
            splitContainer4.Name = "splitContainer4";
            splitContainer4.Orientation = Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.Controls.Add(label1);
            splitContainer4.Panel1.Controls.Add(ComboBox_SortMode);
            splitContainer4.Panel1.Controls.Add(Lbl_MapAllocatedSize);
            splitContainer4.Panel1.Controls.Add(Lbl_IsAllocated);
            splitContainer4.Panel1.Controls.Add(Btn_MapAllocate);
            splitContainer4.Panel1.Controls.Add(Btn_ClearAllWrittenFlag);
            splitContainer4.Panel1.Controls.Add(Btn_WriteAllWrittenFlag);
            splitContainer4.Panel1.Controls.Add(Btn_ClearAllTriggerData);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(FlowPanel_Triggers);
            splitContainer4.Size = new Size(300, 775);
            splitContainer4.SplitterDistance = 136;
            splitContainer4.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 106);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 7;
            label1.Text = "Sort By:";
            // 
            // ComboBox_SortMode
            // 
            ComboBox_SortMode.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox_SortMode.FormattingEnabled = true;
            ComboBox_SortMode.Items.AddRange(new object[] { "Index", "Name", "Data Size" });
            ComboBox_SortMode.Location = new Point(65, 103);
            ComboBox_SortMode.Name = "ComboBox_SortMode";
            ComboBox_SortMode.Size = new Size(121, 23);
            ComboBox_SortMode.TabIndex = 6;
            ComboBox_SortMode.SelectedIndexChanged += ComboBox_SortMode_SelectedIndexChanged;
            // 
            // Lbl_MapAllocatedSize
            // 
            Lbl_MapAllocatedSize.AutoSize = true;
            Lbl_MapAllocatedSize.Location = new Point(12, 27);
            Lbl_MapAllocatedSize.Name = "Lbl_MapAllocatedSize";
            Lbl_MapAllocatedSize.Size = new Size(114, 15);
            Lbl_MapAllocatedSize.TabIndex = 5;
            Lbl_MapAllocatedSize.Text = "Allocated Size: temp";
            // 
            // Lbl_IsAllocated
            // 
            Lbl_IsAllocated.AutoSize = true;
            Lbl_IsAllocated.Location = new Point(12, 12);
            Lbl_IsAllocated.Name = "Lbl_IsAllocated";
            Lbl_IsAllocated.Size = new Size(91, 15);
            Lbl_IsAllocated.TabIndex = 4;
            Lbl_IsAllocated.Text = "Allocated: temp";
            // 
            // Btn_MapAllocate
            // 
            Btn_MapAllocate.Enabled = false;
            Btn_MapAllocate.Location = new Point(12, 45);
            Btn_MapAllocate.Name = "Btn_MapAllocate";
            Btn_MapAllocate.Size = new Size(135, 23);
            Btn_MapAllocate.TabIndex = 0;
            Btn_MapAllocate.Text = "Allocate Trigger Data";
            Btn_MapAllocate.UseVisualStyleBackColor = true;
            Btn_MapAllocate.Click += Btn_MapAllocate_Click;
            // 
            // Btn_ClearAllWrittenFlag
            // 
            Btn_ClearAllWrittenFlag.BackColor = Color.FromArgb(255, 192, 192);
            Btn_ClearAllWrittenFlag.Enabled = false;
            Btn_ClearAllWrittenFlag.Location = new Point(153, 74);
            Btn_ClearAllWrittenFlag.Name = "Btn_ClearAllWrittenFlag";
            Btn_ClearAllWrittenFlag.Size = new Size(135, 23);
            Btn_ClearAllWrittenFlag.TabIndex = 3;
            Btn_ClearAllWrittenFlag.Text = "Clear All Written Flags";
            Btn_ClearAllWrittenFlag.UseVisualStyleBackColor = false;
            Btn_ClearAllWrittenFlag.Click += Btn_ClearAllWrittenFlag_Click;
            // 
            // Btn_WriteAllWrittenFlag
            // 
            Btn_WriteAllWrittenFlag.BackColor = Color.FromArgb(192, 255, 192);
            Btn_WriteAllWrittenFlag.Enabled = false;
            Btn_WriteAllWrittenFlag.Location = new Point(12, 74);
            Btn_WriteAllWrittenFlag.Name = "Btn_WriteAllWrittenFlag";
            Btn_WriteAllWrittenFlag.Size = new Size(135, 23);
            Btn_WriteAllWrittenFlag.TabIndex = 2;
            Btn_WriteAllWrittenFlag.Text = "Set All Written Flags";
            Btn_WriteAllWrittenFlag.UseVisualStyleBackColor = false;
            Btn_WriteAllWrittenFlag.Click += Btn_WriteAllWrittenFlag_Click;
            // 
            // Btn_ClearAllTriggerData
            // 
            Btn_ClearAllTriggerData.Enabled = false;
            Btn_ClearAllTriggerData.Location = new Point(153, 45);
            Btn_ClearAllTriggerData.Name = "Btn_ClearAllTriggerData";
            Btn_ClearAllTriggerData.Size = new Size(135, 23);
            Btn_ClearAllTriggerData.TabIndex = 1;
            Btn_ClearAllTriggerData.Text = "Clear All Trigger Data";
            Btn_ClearAllTriggerData.UseVisualStyleBackColor = true;
            Btn_ClearAllTriggerData.Click += Btn_ClearAllTriggerData_Click;
            // 
            // FlowPanel_Triggers
            // 
            FlowPanel_Triggers.AutoScroll = true;
            FlowPanel_Triggers.BackColor = Color.FromArgb(224, 224, 224);
            FlowPanel_Triggers.Dock = DockStyle.Fill;
            FlowPanel_Triggers.FlowDirection = FlowDirection.TopDown;
            FlowPanel_Triggers.Location = new Point(0, 0);
            FlowPanel_Triggers.Name = "FlowPanel_Triggers";
            FlowPanel_Triggers.Size = new Size(300, 635);
            FlowPanel_Triggers.TabIndex = 0;
            FlowPanel_Triggers.WrapContents = false;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.FixedPanel = FixedPanel.Panel1;
            splitContainer3.IsSplitterFixed = true;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(splitContainer5);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(GroupBox_DrawControls);
            splitContainer3.Size = new Size(820, 775);
            splitContainer3.SplitterDistance = 512;
            splitContainer3.TabIndex = 1;
            // 
            // splitContainer5
            // 
            splitContainer5.Dock = DockStyle.Fill;
            splitContainer5.FixedPanel = FixedPanel.Panel1;
            splitContainer5.IsSplitterFixed = true;
            splitContainer5.Location = new Point(0, 0);
            splitContainer5.Name = "splitContainer5";
            splitContainer5.Orientation = Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            splitContainer5.Panel1.Controls.Add(Panel_MiniMap);
            // 
            // splitContainer5.Panel2
            // 
            splitContainer5.Panel2.Controls.Add(FlowPanel_TriggerData);
            splitContainer5.Size = new Size(512, 775);
            splitContainer5.SplitterDistance = 512;
            splitContainer5.TabIndex = 0;
            // 
            // Panel_MiniMap
            // 
            Panel_MiniMap.BackColor = Color.FromArgb(224, 224, 224);
            Panel_MiniMap.Dock = DockStyle.Fill;
            Panel_MiniMap.Location = new Point(0, 0);
            Panel_MiniMap.Name = "Panel_MiniMap";
            Panel_MiniMap.Size = new Size(512, 512);
            Panel_MiniMap.TabIndex = 1;
            // 
            // FlowPanel_TriggerData
            // 
            FlowPanel_TriggerData.AutoScroll = true;
            FlowPanel_TriggerData.BackColor = Color.FromArgb(224, 224, 224);
            FlowPanel_TriggerData.Dock = DockStyle.Fill;
            FlowPanel_TriggerData.FlowDirection = FlowDirection.TopDown;
            FlowPanel_TriggerData.Location = new Point(0, 0);
            FlowPanel_TriggerData.Name = "FlowPanel_TriggerData";
            FlowPanel_TriggerData.Size = new Size(512, 259);
            FlowPanel_TriggerData.TabIndex = 0;
            // 
            // GroupBox_DrawControls
            // 
            GroupBox_DrawControls.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox_DrawControls.Controls.Add(Btn_PaintReveal);
            GroupBox_DrawControls.Controls.Add(Check_ShowSquares);
            GroupBox_DrawControls.Controls.Add(Btn_PaintUnreveal);
            GroupBox_DrawControls.Controls.Add(Check_ShowMiniMap);
            GroupBox_DrawControls.Controls.Add(Btn_PaintFill);
            GroupBox_DrawControls.Controls.Add(Btn_PaintClear);
            GroupBox_DrawControls.Location = new Point(3, 12);
            GroupBox_DrawControls.Name = "GroupBox_DrawControls";
            GroupBox_DrawControls.Size = new Size(298, 111);
            GroupBox_DrawControls.TabIndex = 6;
            GroupBox_DrawControls.TabStop = false;
            GroupBox_DrawControls.Text = "MiniMap Controls";
            // 
            // Btn_PaintReveal
            // 
            Btn_PaintReveal.Location = new Point(6, 47);
            Btn_PaintReveal.Name = "Btn_PaintReveal";
            Btn_PaintReveal.Size = new Size(107, 23);
            Btn_PaintReveal.TabIndex = 1;
            Btn_PaintReveal.Text = "Paint Reveal";
            Btn_PaintReveal.UseVisualStyleBackColor = true;
            Btn_PaintReveal.Click += Btn_PaintReveal_Click;
            // 
            // Check_ShowSquares
            // 
            Check_ShowSquares.AutoSize = true;
            Check_ShowSquares.Location = new Point(161, 22);
            Check_ShowSquares.Name = "Check_ShowSquares";
            Check_ShowSquares.Size = new Size(105, 19);
            Check_ShowSquares.TabIndex = 5;
            Check_ShowSquares.Text = "Debug Squares";
            Check_ShowSquares.UseVisualStyleBackColor = true;
            Check_ShowSquares.CheckedChanged += Check_ShowSquares_CheckedChanged;
            // 
            // Btn_PaintUnreveal
            // 
            Btn_PaintUnreveal.Location = new Point(119, 47);
            Btn_PaintUnreveal.Name = "Btn_PaintUnreveal";
            Btn_PaintUnreveal.Size = new Size(107, 23);
            Btn_PaintUnreveal.TabIndex = 2;
            Btn_PaintUnreveal.Text = "Paint Unreveal";
            Btn_PaintUnreveal.UseVisualStyleBackColor = true;
            Btn_PaintUnreveal.Click += Btn_PaintUnreveal_Click;
            // 
            // Check_ShowMiniMap
            // 
            Check_ShowMiniMap.AutoSize = true;
            Check_ShowMiniMap.Checked = true;
            Check_ShowMiniMap.CheckState = CheckState.Checked;
            Check_ShowMiniMap.Location = new Point(6, 22);
            Check_ShowMiniMap.Name = "Check_ShowMiniMap";
            Check_ShowMiniMap.Size = new Size(149, 19);
            Check_ShowMiniMap.TabIndex = 0;
            Check_ShowMiniMap.Text = "Show Minimap Overlay";
            Check_ShowMiniMap.UseVisualStyleBackColor = true;
            Check_ShowMiniMap.CheckedChanged += Check_ShowMiniMap_CheckedChanged;
            // 
            // Btn_PaintFill
            // 
            Btn_PaintFill.BackColor = Color.FromArgb(192, 255, 192);
            Btn_PaintFill.Location = new Point(119, 76);
            Btn_PaintFill.Name = "Btn_PaintFill";
            Btn_PaintFill.Size = new Size(107, 23);
            Btn_PaintFill.TabIndex = 4;
            Btn_PaintFill.Text = "Fill";
            Btn_PaintFill.UseVisualStyleBackColor = false;
            Btn_PaintFill.Click += Btn_PaintFill_Click;
            // 
            // Btn_PaintClear
            // 
            Btn_PaintClear.BackColor = Color.FromArgb(255, 192, 192);
            Btn_PaintClear.Location = new Point(6, 76);
            Btn_PaintClear.Name = "Btn_PaintClear";
            Btn_PaintClear.Size = new Size(107, 23);
            Btn_PaintClear.TabIndex = 3;
            Btn_PaintClear.Text = "Clear";
            Btn_PaintClear.UseVisualStyleBackColor = false;
            Btn_PaintClear.Click += Btn_PaintClear_Click;
            // 
            // MapEditorWnd
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1124, 775);
            Controls.Add(splitContainer1);
            Name = "MapEditorWnd";
            ShowIcon = false;
            Text = "Map Editor";
            Load += MapEditor_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel1.PerformLayout();
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            splitContainer5.Panel1.ResumeLayout(false);
            splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer5).EndInit();
            splitContainer5.ResumeLayout(false);
            GroupBox_DrawControls.ResumeLayout(false);
            GroupBox_DrawControls.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private FlowLayoutPanel FlowPanel_Triggers;
        private SplitContainer splitContainer4;
        private Button Btn_MapAllocate;
        private Button Btn_ClearAllWrittenFlag;
        private Button Btn_WriteAllWrittenFlag;
        private Button Btn_ClearAllTriggerData;
        private Label Lbl_IsAllocated;
        private Label Lbl_MapAllocatedSize;
        private SplitContainer splitContainer3;
        private SplitContainer splitContainer5;
        private Panel Panel_MiniMap;
        private FlowLayoutPanel FlowPanel_TriggerData;
        private GroupBox GroupBox_DrawControls;
        private Button Btn_PaintReveal;
        private CheckBox Check_ShowSquares;
        private Button Btn_PaintUnreveal;
        private CheckBox Check_ShowMiniMap;
        private Button Btn_PaintFill;
        private Button Btn_PaintClear;
        private Label label1;
        private ComboBox ComboBox_SortMode;
    }
}