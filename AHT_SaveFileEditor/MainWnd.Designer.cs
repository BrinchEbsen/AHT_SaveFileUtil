
namespace AHT_SaveFileEditor
{
    partial class MainWnd
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWnd));
            menuStrip = new MenuStrip();
            fileToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem_Open = new ToolStripMenuItem();
            toolStripMenuItem_Export = new ToolStripMenuItem();
            toolStripMenuItem_ExportAs = new ToolStripMenuItem();
            helpToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem_About = new ToolStripMenuItem();
            SC_SaveFile = new SplitContainer();
            flowLayoutPanel_SaveSlots = new FlowLayoutPanel();
            groupBox1 = new GroupBox();
            DataGrid_MiniGameTimes = new DataGridView();
            MiniGameName = new DataGridViewTextBoxColumn();
            EasyTime = new DataGridViewTextBoxColumn();
            HardTime = new DataGridViewTextBoxColumn();
            FlowPanel_EggSets = new FlowLayoutPanel();
            Lbl_SaveVersion = new Label();
            label2 = new Label();
            Lbl_BuildTime = new Label();
            label1 = new Label();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SC_SaveFile).BeginInit();
            SC_SaveFile.Panel1.SuspendLayout();
            SC_SaveFile.Panel2.SuspendLayout();
            SC_SaveFile.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGrid_MiniGameTimes).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem1, helpToolStripMenuItem1 });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(926, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            fileToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem_Open, toolStripMenuItem_Export, toolStripMenuItem_ExportAs });
            fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            fileToolStripMenuItem1.Size = new Size(37, 20);
            fileToolStripMenuItem1.Text = "&File";
            // 
            // toolStripMenuItem_Open
            // 
            toolStripMenuItem_Open.Image = (Image)resources.GetObject("toolStripMenuItem_Open.Image");
            toolStripMenuItem_Open.ImageTransparentColor = Color.Magenta;
            toolStripMenuItem_Open.Name = "toolStripMenuItem_Open";
            toolStripMenuItem_Open.ShortcutKeys = Keys.Control | Keys.O;
            toolStripMenuItem_Open.Size = new Size(195, 22);
            toolStripMenuItem_Open.Text = "&Open";
            toolStripMenuItem_Open.Click += toolStripMenuItem_Open_Click;
            // 
            // toolStripMenuItem_Export
            // 
            toolStripMenuItem_Export.Image = (Image)resources.GetObject("toolStripMenuItem_Export.Image");
            toolStripMenuItem_Export.ImageTransparentColor = Color.Magenta;
            toolStripMenuItem_Export.Name = "toolStripMenuItem_Export";
            toolStripMenuItem_Export.ShortcutKeys = Keys.Control | Keys.S;
            toolStripMenuItem_Export.Size = new Size(195, 22);
            toolStripMenuItem_Export.Text = "&Export";
            toolStripMenuItem_Export.Click += toolStripMenuItem_Export_Click;
            // 
            // toolStripMenuItem_ExportAs
            // 
            toolStripMenuItem_ExportAs.Image = (Image)resources.GetObject("toolStripMenuItem_ExportAs.Image");
            toolStripMenuItem_ExportAs.ImageTransparentColor = Color.Magenta;
            toolStripMenuItem_ExportAs.Name = "toolStripMenuItem_ExportAs";
            toolStripMenuItem_ExportAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            toolStripMenuItem_ExportAs.Size = new Size(195, 22);
            toolStripMenuItem_ExportAs.Text = "Export &As";
            toolStripMenuItem_ExportAs.Click += toolStripMenuItem_ExportAs_Click;
            // 
            // helpToolStripMenuItem1
            // 
            helpToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem_About });
            helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            helpToolStripMenuItem1.Size = new Size(44, 20);
            helpToolStripMenuItem1.Text = "&Help";
            // 
            // toolStripMenuItem_About
            // 
            toolStripMenuItem_About.Name = "toolStripMenuItem_About";
            toolStripMenuItem_About.Size = new Size(116, 22);
            toolStripMenuItem_About.Text = "&About...";
            // 
            // SC_SaveFile
            // 
            SC_SaveFile.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SC_SaveFile.FixedPanel = FixedPanel.Panel1;
            SC_SaveFile.Location = new Point(0, 24);
            SC_SaveFile.Name = "SC_SaveFile";
            // 
            // SC_SaveFile.Panel1
            // 
            SC_SaveFile.Panel1.Controls.Add(flowLayoutPanel_SaveSlots);
            // 
            // SC_SaveFile.Panel2
            // 
            SC_SaveFile.Panel2.Controls.Add(groupBox1);
            SC_SaveFile.Panel2.Controls.Add(Lbl_SaveVersion);
            SC_SaveFile.Panel2.Controls.Add(label2);
            SC_SaveFile.Panel2.Controls.Add(Lbl_BuildTime);
            SC_SaveFile.Panel2.Controls.Add(label1);
            SC_SaveFile.Size = new Size(926, 739);
            SC_SaveFile.SplitterDistance = 286;
            SC_SaveFile.TabIndex = 1;
            SC_SaveFile.Visible = false;
            // 
            // flowLayoutPanel_SaveSlots
            // 
            flowLayoutPanel_SaveSlots.BackColor = SystemColors.ControlLight;
            flowLayoutPanel_SaveSlots.Dock = DockStyle.Fill;
            flowLayoutPanel_SaveSlots.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel_SaveSlots.Location = new Point(0, 0);
            flowLayoutPanel_SaveSlots.Name = "flowLayoutPanel_SaveSlots";
            flowLayoutPanel_SaveSlots.Size = new Size(286, 739);
            flowLayoutPanel_SaveSlots.TabIndex = 0;
            flowLayoutPanel_SaveSlots.WrapContents = false;
            flowLayoutPanel_SaveSlots.Resize += flowLayoutPanel_SaveSlots_Resize;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(DataGrid_MiniGameTimes);
            groupBox1.Controls.Add(FlowPanel_EggSets);
            groupBox1.Location = new Point(3, 43);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(630, 287);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Global Game State";
            // 
            // DataGrid_MiniGameTimes
            // 
            DataGrid_MiniGameTimes.AllowUserToAddRows = false;
            DataGrid_MiniGameTimes.AllowUserToDeleteRows = false;
            DataGrid_MiniGameTimes.AllowUserToResizeRows = false;
            DataGrid_MiniGameTimes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DataGrid_MiniGameTimes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGrid_MiniGameTimes.Columns.AddRange(new DataGridViewColumn[] { MiniGameName, EasyTime, HardTime });
            DataGrid_MiniGameTimes.Location = new Point(209, 22);
            DataGrid_MiniGameTimes.Name = "DataGrid_MiniGameTimes";
            DataGrid_MiniGameTimes.RowHeadersVisible = false;
            DataGrid_MiniGameTimes.ShowEditingIcon = false;
            DataGrid_MiniGameTimes.Size = new Size(415, 259);
            DataGrid_MiniGameTimes.TabIndex = 1;
            DataGrid_MiniGameTimes.CellEndEdit += DataGrid_MiniGameTimes_CellEndEdit;
            // 
            // MiniGameName
            // 
            MiniGameName.HeaderText = "Name";
            MiniGameName.Name = "MiniGameName";
            MiniGameName.ReadOnly = true;
            MiniGameName.Width = 150;
            // 
            // EasyTime
            // 
            EasyTime.HeaderText = "Easy Time";
            EasyTime.Name = "EasyTime";
            // 
            // HardTime
            // 
            HardTime.HeaderText = "Hard Time";
            HardTime.Name = "HardTime";
            // 
            // FlowPanel_EggSets
            // 
            FlowPanel_EggSets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            FlowPanel_EggSets.FlowDirection = FlowDirection.TopDown;
            FlowPanel_EggSets.Location = new Point(6, 22);
            FlowPanel_EggSets.Margin = new Padding(0);
            FlowPanel_EggSets.Name = "FlowPanel_EggSets";
            FlowPanel_EggSets.Size = new Size(200, 259);
            FlowPanel_EggSets.TabIndex = 0;
            FlowPanel_EggSets.WrapContents = false;
            // 
            // Lbl_SaveVersion
            // 
            Lbl_SaveVersion.AutoSize = true;
            Lbl_SaveVersion.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_SaveVersion.Location = new Point(69, 20);
            Lbl_SaveVersion.Name = "Lbl_SaveVersion";
            Lbl_SaveVersion.Size = new Size(44, 20);
            Lbl_SaveVersion.TabIndex = 3;
            Lbl_SaveVersion.Text = "temp";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(3, 20);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 2;
            label2.Text = "Version:";
            // 
            // Lbl_BuildTime
            // 
            Lbl_BuildTime.AutoSize = true;
            Lbl_BuildTime.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_BuildTime.Location = new Point(88, 0);
            Lbl_BuildTime.Name = "Lbl_BuildTime";
            Lbl_BuildTime.Size = new Size(44, 20);
            Lbl_BuildTime.TabIndex = 1;
            Lbl_BuildTime.Text = "temp";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(2, 0);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 0;
            label1.Text = "Build time:";
            // 
            // MainWnd
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(926, 763);
            Controls.Add(SC_SaveFile);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "MainWnd";
            ShowIcon = false;
            Text = "Save File Editor";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            SC_SaveFile.Panel1.ResumeLayout(false);
            SC_SaveFile.Panel2.ResumeLayout(false);
            SC_SaveFile.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SC_SaveFile).EndInit();
            SC_SaveFile.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGrid_MiniGameTimes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem_Open;
        private ToolStripMenuItem toolStripMenuItem_ExportAs;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem_About;
        private ToolStripMenuItem toolStripMenuItem_Export;
        private SplitContainer SC_SaveFile;
        private FlowLayoutPanel flowLayoutPanel_SaveSlots;
        private Label label1;
        private Label Lbl_BuildTime;
        private Label Lbl_SaveVersion;
        private Label label2;
        private GroupBox groupBox1;
        private FlowLayoutPanel FlowPanel_EggSets;
        private DataGridView DataGrid_MiniGameTimes;
        private DataGridViewTextBoxColumn MiniGameName;
        private DataGridViewTextBoxColumn EasyTime;
        private DataGridViewTextBoxColumn HardTime;
    }
}
