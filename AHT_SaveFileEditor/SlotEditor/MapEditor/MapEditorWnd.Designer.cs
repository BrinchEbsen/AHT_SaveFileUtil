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
            splitContainer2 = new SplitContainer();
            splitContainer3 = new SplitContainer();
            Panel_MiniMap = new Panel();
            Check_ShowMiniMap = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1086, 775);
            splitContainer1.SplitterDistance = 253;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(splitContainer3);
            splitContainer2.Size = new Size(829, 775);
            splitContainer2.SplitterDistance = 512;
            splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.FixedPanel = FixedPanel.Panel1;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(Panel_MiniMap);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(Check_ShowMiniMap);
            splitContainer3.Size = new Size(829, 512);
            splitContainer3.SplitterDistance = 512;
            splitContainer3.TabIndex = 0;
            // 
            // Panel_MiniMap
            // 
            Panel_MiniMap.BackColor = Color.FromArgb(224, 224, 224);
            Panel_MiniMap.Dock = DockStyle.Fill;
            Panel_MiniMap.Location = new Point(0, 0);
            Panel_MiniMap.Name = "Panel_MiniMap";
            Panel_MiniMap.Size = new Size(512, 512);
            Panel_MiniMap.TabIndex = 0;
            // 
            // Check_ShowMiniMap
            // 
            Check_ShowMiniMap.AutoSize = true;
            Check_ShowMiniMap.Checked = true;
            Check_ShowMiniMap.CheckState = CheckState.Checked;
            Check_ShowMiniMap.Location = new Point(3, 12);
            Check_ShowMiniMap.Name = "Check_ShowMiniMap";
            Check_ShowMiniMap.Size = new Size(149, 19);
            Check_ShowMiniMap.TabIndex = 0;
            Check_ShowMiniMap.Text = "Show Minimap Overlay";
            Check_ShowMiniMap.UseVisualStyleBackColor = true;
            Check_ShowMiniMap.CheckedChanged += Check_ShowMiniMap_CheckedChanged;
            // 
            // MapEditorWnd
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1086, 775);
            Controls.Add(splitContainer1);
            Name = "MapEditorWnd";
            ShowIcon = false;
            Text = "Map Editor";
            Load += MapEditor_Load;
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
        private Panel Panel_MiniMap;
        private CheckBox Check_ShowMiniMap;
    }
}