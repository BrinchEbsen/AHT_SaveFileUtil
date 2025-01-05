
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
            label1 = new Label();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SC_SaveFile).BeginInit();
            SC_SaveFile.Panel1.SuspendLayout();
            SC_SaveFile.Panel2.SuspendLayout();
            SC_SaveFile.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem1, helpToolStripMenuItem1 });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1035, 24);
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
            SC_SaveFile.Panel2.Controls.Add(label1);
            SC_SaveFile.Size = new Size(1035, 739);
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 27);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // MainWnd
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1035, 763);
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
    }
}
