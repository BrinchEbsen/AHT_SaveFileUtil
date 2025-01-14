
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
            groupBox2 = new GroupBox();
            label8 = new Label();
            label7 = new Label();
            ComboBox_BonusCharacter = new ComboBox();
            Check_Rumble = new CheckBox();
            Check_CamActive = new CheckBox();
            Check_SparxAxisInverted = new CheckBox();
            Check_SgtAxisInverted = new CheckBox();
            Check_FPAxisInverted = new CheckBox();
            label6 = new Label();
            TrackBar_MusicVolume = new TrackBar();
            label5 = new Label();
            TrackBar_SFXVolume = new TrackBar();
            groupBox1 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
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
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TrackBar_MusicVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrackBar_SFXVolume).BeginInit();
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
            toolStripMenuItem_Open.Size = new Size(186, 22);
            toolStripMenuItem_Open.Text = "&Open";
            toolStripMenuItem_Open.Click += toolStripMenuItem_Open_Click;
            // 
            // toolStripMenuItem_Export
            // 
            toolStripMenuItem_Export.Image = (Image)resources.GetObject("toolStripMenuItem_Export.Image");
            toolStripMenuItem_Export.ImageTransparentColor = Color.Magenta;
            toolStripMenuItem_Export.Name = "toolStripMenuItem_Export";
            toolStripMenuItem_Export.ShortcutKeys = Keys.Control | Keys.S;
            toolStripMenuItem_Export.Size = new Size(186, 22);
            toolStripMenuItem_Export.Text = "&Save";
            toolStripMenuItem_Export.Click += toolStripMenuItem_Export_Click;
            // 
            // toolStripMenuItem_ExportAs
            // 
            toolStripMenuItem_ExportAs.Image = (Image)resources.GetObject("toolStripMenuItem_ExportAs.Image");
            toolStripMenuItem_ExportAs.ImageTransparentColor = Color.Magenta;
            toolStripMenuItem_ExportAs.Name = "toolStripMenuItem_ExportAs";
            toolStripMenuItem_ExportAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            toolStripMenuItem_ExportAs.Size = new Size(186, 22);
            toolStripMenuItem_ExportAs.Text = "Save &As";
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
            SC_SaveFile.Panel2.Controls.Add(groupBox2);
            SC_SaveFile.Panel2.Controls.Add(groupBox1);
            SC_SaveFile.Panel2.Controls.Add(Lbl_SaveVersion);
            SC_SaveFile.Panel2.Controls.Add(label2);
            SC_SaveFile.Panel2.Controls.Add(Lbl_BuildTime);
            SC_SaveFile.Panel2.Controls.Add(label1);
            SC_SaveFile.Size = new Size(926, 655);
            SC_SaveFile.SplitterDistance = 286;
            SC_SaveFile.TabIndex = 1;
            SC_SaveFile.Visible = false;
            // 
            // flowLayoutPanel_SaveSlots
            // 
            flowLayoutPanel_SaveSlots.AutoScroll = true;
            flowLayoutPanel_SaveSlots.BackColor = SystemColors.ControlLight;
            flowLayoutPanel_SaveSlots.Dock = DockStyle.Fill;
            flowLayoutPanel_SaveSlots.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel_SaveSlots.Location = new Point(0, 0);
            flowLayoutPanel_SaveSlots.Name = "flowLayoutPanel_SaveSlots";
            flowLayoutPanel_SaveSlots.Size = new Size(286, 655);
            flowLayoutPanel_SaveSlots.TabIndex = 0;
            flowLayoutPanel_SaveSlots.WrapContents = false;
            flowLayoutPanel_SaveSlots.Resize += flowLayoutPanel_SaveSlots_Resize;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(ComboBox_BonusCharacter);
            groupBox2.Controls.Add(Check_Rumble);
            groupBox2.Controls.Add(Check_CamActive);
            groupBox2.Controls.Add(Check_SparxAxisInverted);
            groupBox2.Controls.Add(Check_SgtAxisInverted);
            groupBox2.Controls.Add(Check_FPAxisInverted);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(TrackBar_MusicVolume);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(TrackBar_SFXVolume);
            groupBox2.Location = new Point(3, 336);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(630, 316);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Settings";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(6, 201);
            label8.Name = "label8";
            label8.Size = new Size(209, 13);
            label8.TabIndex = 14;
            label8.Text = "Intended values: Spyro, Ember or Flame";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(6, 155);
            label7.Name = "label7";
            label7.Size = new Size(188, 17);
            label7.TabIndex = 13;
            label7.Text = "Selected Unlockable Character:";
            // 
            // ComboBox_BonusCharacter
            // 
            ComboBox_BonusCharacter.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox_BonusCharacter.FormattingEnabled = true;
            ComboBox_BonusCharacter.Location = new Point(6, 175);
            ComboBox_BonusCharacter.Name = "ComboBox_BonusCharacter";
            ComboBox_BonusCharacter.Size = new Size(121, 23);
            ComboBox_BonusCharacter.TabIndex = 12;
            ComboBox_BonusCharacter.SelectedIndexChanged += ComboBox_BonusCharacter_SelectedIndexChanged;
            // 
            // Check_Rumble
            // 
            Check_Rumble.AutoSize = true;
            Check_Rumble.Location = new Point(316, 122);
            Check_Rumble.Name = "Check_Rumble";
            Check_Rumble.Size = new Size(112, 19);
            Check_Rumble.TabIndex = 11;
            Check_Rumble.Text = "Rumble Enabled";
            Check_Rumble.UseVisualStyleBackColor = true;
            Check_Rumble.CheckedChanged += Check_Rumble_CheckedChanged;
            // 
            // Check_CamActive
            // 
            Check_CamActive.AutoSize = true;
            Check_CamActive.Location = new Point(316, 97);
            Check_CamActive.Name = "Check_CamActive";
            Check_CamActive.Size = new Size(137, 19);
            Check_CamActive.TabIndex = 10;
            Check_CamActive.Text = "Camera Active Mode";
            Check_CamActive.UseVisualStyleBackColor = true;
            Check_CamActive.CheckedChanged += Check_CamActive_CheckedChanged;
            // 
            // Check_SparxAxisInverted
            // 
            Check_SparxAxisInverted.AutoSize = true;
            Check_SparxAxisInverted.Location = new Point(316, 72);
            Check_SparxAxisInverted.Name = "Check_SparxAxisInverted";
            Check_SparxAxisInverted.Size = new Size(134, 19);
            Check_SparxAxisInverted.TabIndex = 9;
            Check_SparxAxisInverted.Text = "Sparx Y Axis Inverted";
            Check_SparxAxisInverted.UseVisualStyleBackColor = true;
            Check_SparxAxisInverted.CheckedChanged += Check_SparxAxisInverted_CheckedChanged;
            // 
            // Check_SgtAxisInverted
            // 
            Check_SgtAxisInverted.AutoSize = true;
            Check_SgtAxisInverted.Location = new Point(316, 47);
            Check_SgtAxisInverted.Name = "Check_SgtAxisInverted";
            Check_SgtAxisInverted.Size = new Size(153, 19);
            Check_SgtAxisInverted.TabIndex = 8;
            Check_SgtAxisInverted.Text = "Sgt. Byrd Y Axis Inverted";
            Check_SgtAxisInverted.UseVisualStyleBackColor = true;
            Check_SgtAxisInverted.CheckedChanged += Check_SgtAxisInverted_CheckedChanged;
            // 
            // Check_FPAxisInverted
            // 
            Check_FPAxisInverted.AutoSize = true;
            Check_FPAxisInverted.Location = new Point(316, 22);
            Check_FPAxisInverted.Name = "Check_FPAxisInverted";
            Check_FPAxisInverted.Size = new Size(169, 19);
            Check_FPAxisInverted.TabIndex = 7;
            Check_FPAxisInverted.Text = "First-Person Y Axis Inverted";
            Check_FPAxisInverted.UseVisualStyleBackColor = true;
            Check_FPAxisInverted.CheckedChanged += Check_FPAxisInverted_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(6, 87);
            label6.Name = "label6";
            label6.Size = new Size(92, 17);
            label6.TabIndex = 6;
            label6.Text = "Music Volume:";
            // 
            // TrackBar_MusicVolume
            // 
            TrackBar_MusicVolume.Location = new Point(6, 107);
            TrackBar_MusicVolume.Maximum = 100;
            TrackBar_MusicVolume.Name = "TrackBar_MusicVolume";
            TrackBar_MusicVolume.Size = new Size(304, 45);
            TrackBar_MusicVolume.TabIndex = 5;
            TrackBar_MusicVolume.Scroll += TrackBar_MusicVolume_Scroll;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(6, 19);
            label5.Name = "label5";
            label5.Size = new Size(79, 17);
            label5.TabIndex = 4;
            label5.Text = "SFX Volume:";
            // 
            // TrackBar_SFXVolume
            // 
            TrackBar_SFXVolume.Location = new Point(6, 39);
            TrackBar_SFXVolume.Maximum = 100;
            TrackBar_SFXVolume.Name = "TrackBar_SFXVolume";
            TrackBar_SFXVolume.Size = new Size(304, 45);
            TrackBar_SFXVolume.TabIndex = 0;
            TrackBar_SFXVolume.Scroll += TrackBar_SFXVolume_Scroll;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(DataGrid_MiniGameTimes);
            groupBox1.Controls.Add(FlowPanel_EggSets);
            groupBox1.Location = new Point(3, 43);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(630, 287);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Global Game State";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(209, 19);
            label4.Name = "label4";
            label4.Size = new Size(133, 17);
            label4.TabIndex = 3;
            label4.Text = "MiniGame Best Times";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 19);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 2;
            label3.Text = "Egg Sets";
            // 
            // DataGrid_MiniGameTimes
            // 
            DataGrid_MiniGameTimes.AllowUserToAddRows = false;
            DataGrid_MiniGameTimes.AllowUserToDeleteRows = false;
            DataGrid_MiniGameTimes.AllowUserToResizeRows = false;
            DataGrid_MiniGameTimes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            DataGrid_MiniGameTimes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGrid_MiniGameTimes.Columns.AddRange(new DataGridViewColumn[] { MiniGameName, EasyTime, HardTime });
            DataGrid_MiniGameTimes.Location = new Point(209, 39);
            DataGrid_MiniGameTimes.Name = "DataGrid_MiniGameTimes";
            DataGrid_MiniGameTimes.RowHeadersVisible = false;
            DataGrid_MiniGameTimes.ShowEditingIcon = false;
            DataGrid_MiniGameTimes.Size = new Size(377, 242);
            DataGrid_MiniGameTimes.TabIndex = 1;
            DataGrid_MiniGameTimes.CellEndEdit += DataGrid_MiniGameTimes_CellEndEdit;
            // 
            // MiniGameName
            // 
            MiniGameName.HeaderText = "Name";
            MiniGameName.Name = "MiniGameName";
            MiniGameName.ReadOnly = true;
            MiniGameName.SortMode = DataGridViewColumnSortMode.NotSortable;
            MiniGameName.Width = 150;
            // 
            // EasyTime
            // 
            EasyTime.HeaderText = "Easy Time";
            EasyTime.Name = "EasyTime";
            EasyTime.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // HardTime
            // 
            HardTime.HeaderText = "Hard Time";
            HardTime.Name = "HardTime";
            HardTime.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // FlowPanel_EggSets
            // 
            FlowPanel_EggSets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            FlowPanel_EggSets.FlowDirection = FlowDirection.TopDown;
            FlowPanel_EggSets.Location = new Point(6, 39);
            FlowPanel_EggSets.Margin = new Padding(0);
            FlowPanel_EggSets.Name = "FlowPanel_EggSets";
            FlowPanel_EggSets.Size = new Size(200, 242);
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
            ClientSize = new Size(926, 679);
            Controls.Add(SC_SaveFile);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "MainWnd";
            ShowIcon = false;
            Text = "Save File Editor";
            Load += MainWnd_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            SC_SaveFile.Panel1.ResumeLayout(false);
            SC_SaveFile.Panel2.ResumeLayout(false);
            SC_SaveFile.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SC_SaveFile).EndInit();
            SC_SaveFile.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TrackBar_MusicVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrackBar_SFXVolume).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
        private GroupBox groupBox2;
        private Label label4;
        private Label label3;
        private Label label6;
        private TrackBar TrackBar_MusicVolume;
        private Label label5;
        private TrackBar TrackBar_SFXVolume;
        private CheckBox Check_FPAxisInverted;
        private CheckBox Check_SgtAxisInverted;
        private CheckBox Check_SparxAxisInverted;
        private CheckBox Check_CamActive;
        private CheckBox Check_Rumble;
        private ComboBox ComboBox_BonusCharacter;
        private Label label7;
        private Label label8;
        private DataGridViewTextBoxColumn MiniGameName;
        private DataGridViewTextBoxColumn EasyTime;
        private DataGridViewTextBoxColumn HardTime;
    }
}
