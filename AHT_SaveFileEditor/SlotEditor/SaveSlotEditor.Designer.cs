
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
            Lbl_HealthDescriptor = new Label();
            Btn_Health_Heal = new Button();
            Btn_Health_Damage = new Button();
            Num_Health = new NumericUpDown();
            label31 = new Label();
            Lbl_CompletionCalc = new Label();
            label30 = new Label();
            label29 = new Label();
            label28 = new Label();
            Num_DoubleGemMax = new NumericUpDown();
            Num_DoubleGem = new NumericUpDown();
            Num_Supercharge = new NumericUpDown();
            Num_SuperchargeMax = new NumericUpDown();
            Num_Invincibility = new NumericUpDown();
            Num_InvincibilityMax = new NumericUpDown();
            label27 = new Label();
            label26 = new Label();
            label25 = new Label();
            label24 = new Label();
            label23 = new Label();
            label22 = new Label();
            label21 = new Label();
            Num_BlinkBombs = new NumericUpDown();
            Num_SgtByrdBombs = new NumericUpDown();
            Num_SgtByrdMissiles = new NumericUpDown();
            Num_SparxBombs = new NumericUpDown();
            Num_SparxMissiles = new NumericUpDown();
            label20 = new Label();
            label19 = new Label();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            Num_DragonEggs = new NumericUpDown();
            Num_DarkGems = new NumericUpDown();
            label14 = new Label();
            label13 = new Label();
            Num_LightGems = new NumericUpDown();
            FlowPanel_PowerUps = new FlowLayoutPanel();
            label12 = new Label();
            Num_TotalGems = new NumericUpDown();
            Num_Gems = new NumericUpDown();
            label11 = new Label();
            Btn_ClearAllAbilityFlags = new Button();
            Btn_SetAllAbilityFlags = new Button();
            label10 = new Label();
            CheckList_AbilityFlags = new CheckedListBox();
            Lbl_SlotIndex = new Label();
            Btn_DoAllTasks = new Button();
            Btn_FindAllTasks = new Button();
            Btn_ClearAllTasks = new Button();
            label9 = new Label();
            FlowPanel_Tasks = new FlowLayoutPanel();
            Btn_ClearAllObjectives = new Button();
            Btn_SetAllObjectives = new Button();
            label8 = new Label();
            CheckList_Objectives = new CheckedListBox();
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
            menuStrip1 = new MenuStrip();
            quickActionsToolStripMenuItem = new ToolStripMenuItem();
            MenuItem_ResetSlot = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Num_Health).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_DoubleGemMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_DoubleGem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_Supercharge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_SuperchargeMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_Invincibility).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_InvincibilityMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_BlinkBombs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_SgtByrdBombs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_SgtByrdMissiles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_SparxBombs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_SparxMissiles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_DragonEggs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_DarkGems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_LightGems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_TotalGems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_Gems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_PlayTimeSeconds).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_PlayTimeMinutes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Num_PlayTimeHours).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(Lbl_HealthDescriptor);
            splitContainer1.Panel2.Controls.Add(Btn_Health_Heal);
            splitContainer1.Panel2.Controls.Add(Btn_Health_Damage);
            splitContainer1.Panel2.Controls.Add(Num_Health);
            splitContainer1.Panel2.Controls.Add(label31);
            splitContainer1.Panel2.Controls.Add(Lbl_CompletionCalc);
            splitContainer1.Panel2.Controls.Add(label30);
            splitContainer1.Panel2.Controls.Add(label29);
            splitContainer1.Panel2.Controls.Add(label28);
            splitContainer1.Panel2.Controls.Add(Num_DoubleGemMax);
            splitContainer1.Panel2.Controls.Add(Num_DoubleGem);
            splitContainer1.Panel2.Controls.Add(Num_Supercharge);
            splitContainer1.Panel2.Controls.Add(Num_SuperchargeMax);
            splitContainer1.Panel2.Controls.Add(Num_Invincibility);
            splitContainer1.Panel2.Controls.Add(Num_InvincibilityMax);
            splitContainer1.Panel2.Controls.Add(label27);
            splitContainer1.Panel2.Controls.Add(label26);
            splitContainer1.Panel2.Controls.Add(label25);
            splitContainer1.Panel2.Controls.Add(label24);
            splitContainer1.Panel2.Controls.Add(label23);
            splitContainer1.Panel2.Controls.Add(label22);
            splitContainer1.Panel2.Controls.Add(label21);
            splitContainer1.Panel2.Controls.Add(Num_BlinkBombs);
            splitContainer1.Panel2.Controls.Add(Num_SgtByrdBombs);
            splitContainer1.Panel2.Controls.Add(Num_SgtByrdMissiles);
            splitContainer1.Panel2.Controls.Add(Num_SparxBombs);
            splitContainer1.Panel2.Controls.Add(Num_SparxMissiles);
            splitContainer1.Panel2.Controls.Add(label20);
            splitContainer1.Panel2.Controls.Add(label19);
            splitContainer1.Panel2.Controls.Add(label18);
            splitContainer1.Panel2.Controls.Add(label17);
            splitContainer1.Panel2.Controls.Add(label16);
            splitContainer1.Panel2.Controls.Add(label15);
            splitContainer1.Panel2.Controls.Add(Num_DragonEggs);
            splitContainer1.Panel2.Controls.Add(Num_DarkGems);
            splitContainer1.Panel2.Controls.Add(label14);
            splitContainer1.Panel2.Controls.Add(label13);
            splitContainer1.Panel2.Controls.Add(Num_LightGems);
            splitContainer1.Panel2.Controls.Add(FlowPanel_PowerUps);
            splitContainer1.Panel2.Controls.Add(label12);
            splitContainer1.Panel2.Controls.Add(Num_TotalGems);
            splitContainer1.Panel2.Controls.Add(Num_Gems);
            splitContainer1.Panel2.Controls.Add(label11);
            splitContainer1.Panel2.Controls.Add(Btn_ClearAllAbilityFlags);
            splitContainer1.Panel2.Controls.Add(Btn_SetAllAbilityFlags);
            splitContainer1.Panel2.Controls.Add(label10);
            splitContainer1.Panel2.Controls.Add(CheckList_AbilityFlags);
            splitContainer1.Panel2.Controls.Add(Lbl_SlotIndex);
            splitContainer1.Panel2.Controls.Add(Btn_DoAllTasks);
            splitContainer1.Panel2.Controls.Add(Btn_FindAllTasks);
            splitContainer1.Panel2.Controls.Add(Btn_ClearAllTasks);
            splitContainer1.Panel2.Controls.Add(label9);
            splitContainer1.Panel2.Controls.Add(FlowPanel_Tasks);
            splitContainer1.Panel2.Controls.Add(Btn_ClearAllObjectives);
            splitContainer1.Panel2.Controls.Add(Btn_SetAllObjectives);
            splitContainer1.Panel2.Controls.Add(label8);
            splitContainer1.Panel2.Controls.Add(CheckList_Objectives);
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
            splitContainer1.Size = new Size(1243, 929);
            splitContainer1.SplitterDistance = 316;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
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
            splitContainer2.Size = new Size(316, 929);
            splitContainer2.SplitterDistance = 64;
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
            FlowPanel_Levels.Size = new Size(316, 861);
            FlowPanel_Levels.TabIndex = 0;
            FlowPanel_Levels.WrapContents = false;
            // 
            // Lbl_HealthDescriptor
            // 
            Lbl_HealthDescriptor.AutoSize = true;
            Lbl_HealthDescriptor.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            Lbl_HealthDescriptor.ForeColor = Color.FromArgb(64, 64, 64);
            Lbl_HealthDescriptor.Location = new Point(222, 143);
            Lbl_HealthDescriptor.Name = "Lbl_HealthDescriptor";
            Lbl_HealthDescriptor.Size = new Size(34, 15);
            Lbl_HealthDescriptor.TabIndex = 71;
            Lbl_HealthDescriptor.Text = "temp";
            // 
            // Btn_Health_Heal
            // 
            Btn_Health_Heal.BackColor = Color.FromArgb(192, 255, 192);
            Btn_Health_Heal.Location = new Point(193, 139);
            Btn_Health_Heal.Name = "Btn_Health_Heal";
            Btn_Health_Heal.Size = new Size(23, 23);
            Btn_Health_Heal.TabIndex = 70;
            Btn_Health_Heal.Text = "+";
            Btn_Health_Heal.UseVisualStyleBackColor = false;
            Btn_Health_Heal.Click += Btn_Health_Heal_Click;
            // 
            // Btn_Health_Damage
            // 
            Btn_Health_Damage.BackColor = Color.FromArgb(255, 192, 192);
            Btn_Health_Damage.Location = new Point(164, 139);
            Btn_Health_Damage.Name = "Btn_Health_Damage";
            Btn_Health_Damage.Size = new Size(23, 23);
            Btn_Health_Damage.TabIndex = 69;
            Btn_Health_Damage.Text = "-";
            Btn_Health_Damage.UseVisualStyleBackColor = false;
            Btn_Health_Damage.Click += Btn_Health_Damage_Click;
            // 
            // Num_Health
            // 
            Num_Health.Hexadecimal = true;
            Num_Health.Location = new Point(89, 139);
            Num_Health.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            Num_Health.Minimum = new decimal(new int[] { int.MaxValue, 0, 0, int.MinValue });
            Num_Health.Name = "Num_Health";
            Num_Health.Size = new Size(69, 23);
            Num_Health.TabIndex = 68;
            Num_Health.ValueChanged += Num_Health_ValueChanged;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label31.Location = new Point(3, 142);
            label31.Name = "label31";
            label31.Size = new Size(56, 20);
            label31.TabIndex = 67;
            label31.Text = "Health:";
            // 
            // Lbl_CompletionCalc
            // 
            Lbl_CompletionCalc.AutoSize = true;
            Lbl_CompletionCalc.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            Lbl_CompletionCalc.ForeColor = Color.FromArgb(64, 64, 64);
            Lbl_CompletionCalc.Location = new Point(150, 120);
            Lbl_CompletionCalc.Name = "Lbl_CompletionCalc";
            Lbl_CompletionCalc.Size = new Size(34, 15);
            Lbl_CompletionCalc.TabIndex = 66;
            Lbl_CompletionCalc.Text = "temp";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label30.Location = new Point(485, 171);
            label30.Name = "label30";
            label30.Size = new Size(108, 15);
            label30.TabIndex = 65;
            label30.Text = "Timers (in frames)";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label29.Location = new Point(235, 171);
            label29.Name = "label29";
            label29.Size = new Size(133, 15);
            label29.TabIndex = 64;
            label29.Text = "MiniGame Collectables";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label28.Location = new Point(2, 171);
            label28.Name = "label28";
            label28.Size = new Size(120, 15);
            label28.TabIndex = 63;
            label28.Text = "Generic Collectables";
            // 
            // Num_DoubleGemMax
            // 
            Num_DoubleGemMax.Location = new Point(619, 333);
            Num_DoubleGemMax.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            Num_DoubleGemMax.Minimum = new decimal(new int[] { 9999999, 0, 0, int.MinValue });
            Num_DoubleGemMax.Name = "Num_DoubleGemMax";
            Num_DoubleGemMax.Size = new Size(86, 23);
            Num_DoubleGemMax.TabIndex = 62;
            Num_DoubleGemMax.ValueChanged += Num_DoubleGemMax_ValueChanged;
            // 
            // Num_DoubleGem
            // 
            Num_DoubleGem.Location = new Point(619, 304);
            Num_DoubleGem.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            Num_DoubleGem.Minimum = new decimal(new int[] { 9999999, 0, 0, int.MinValue });
            Num_DoubleGem.Name = "Num_DoubleGem";
            Num_DoubleGem.Size = new Size(86, 23);
            Num_DoubleGem.TabIndex = 61;
            Num_DoubleGem.ValueChanged += Num_DoubleGem_ValueChanged;
            // 
            // Num_Supercharge
            // 
            Num_Supercharge.Location = new Point(619, 188);
            Num_Supercharge.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            Num_Supercharge.Minimum = new decimal(new int[] { 9999999, 0, 0, int.MinValue });
            Num_Supercharge.Name = "Num_Supercharge";
            Num_Supercharge.Size = new Size(86, 23);
            Num_Supercharge.TabIndex = 60;
            Num_Supercharge.ValueChanged += Num_Supercharge_ValueChanged;
            // 
            // Num_SuperchargeMax
            // 
            Num_SuperchargeMax.Location = new Point(619, 217);
            Num_SuperchargeMax.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            Num_SuperchargeMax.Minimum = new decimal(new int[] { 9999999, 0, 0, int.MinValue });
            Num_SuperchargeMax.Name = "Num_SuperchargeMax";
            Num_SuperchargeMax.Size = new Size(86, 23);
            Num_SuperchargeMax.TabIndex = 59;
            Num_SuperchargeMax.ValueChanged += Num_SuperchargeMax_ValueChanged;
            // 
            // Num_Invincibility
            // 
            Num_Invincibility.Location = new Point(619, 246);
            Num_Invincibility.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            Num_Invincibility.Minimum = new decimal(new int[] { 9999999, 0, 0, int.MinValue });
            Num_Invincibility.Name = "Num_Invincibility";
            Num_Invincibility.Size = new Size(86, 23);
            Num_Invincibility.TabIndex = 58;
            Num_Invincibility.ValueChanged += Num_Invincibility_ValueChanged;
            // 
            // Num_InvincibilityMax
            // 
            Num_InvincibilityMax.Location = new Point(619, 275);
            Num_InvincibilityMax.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            Num_InvincibilityMax.Minimum = new decimal(new int[] { 9999999, 0, 0, int.MinValue });
            Num_InvincibilityMax.Name = "Num_InvincibilityMax";
            Num_InvincibilityMax.Size = new Size(86, 23);
            Num_InvincibilityMax.TabIndex = 57;
            Num_InvincibilityMax.ValueChanged += Num_InvincibilityMax_ValueChanged;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label27.Location = new Point(485, 336);
            label27.Name = "label27";
            label27.Size = new Size(128, 20);
            label27.TabIndex = 56;
            label27.Text = "Double Gem Max:";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label26.Location = new Point(485, 307);
            label26.Name = "label26";
            label26.Size = new Size(96, 20);
            label26.TabIndex = 55;
            label26.Text = "Double Gem:";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label25.Location = new Point(485, 278);
            label25.Name = "label25";
            label25.Size = new Size(119, 20);
            label25.TabIndex = 54;
            label25.Text = "Invincibility Max:";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label24.Location = new Point(485, 249);
            label24.Name = "label24";
            label24.Size = new Size(87, 20);
            label24.TabIndex = 53;
            label24.Text = "Invincibility:";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label23.Location = new Point(485, 220);
            label23.Name = "label23";
            label23.Size = new Size(127, 20);
            label23.TabIndex = 52;
            label23.Text = "Supercharge Max:";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label22.Location = new Point(485, 191);
            label22.Name = "label22";
            label22.Size = new Size(95, 20);
            label22.TabIndex = 51;
            label22.Text = "Supercharge:";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label21.ForeColor = Color.Gray;
            label21.Location = new Point(267, 330);
            label21.Name = "label21";
            label21.Size = new Size(190, 26);
            label21.TabIndex = 50;
            label21.Text = "(Note: These are usually overwritten\r\nwhen starting a minigame)";
            label21.TextAlign = ContentAlignment.TopCenter;
            // 
            // Num_BlinkBombs
            // 
            Num_BlinkBombs.Location = new Point(393, 304);
            Num_BlinkBombs.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            Num_BlinkBombs.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            Num_BlinkBombs.Name = "Num_BlinkBombs";
            Num_BlinkBombs.Size = new Size(86, 23);
            Num_BlinkBombs.TabIndex = 49;
            Num_BlinkBombs.ValueChanged += Num_BlinkBombs_ValueChanged;
            // 
            // Num_SgtByrdBombs
            // 
            Num_SgtByrdBombs.Location = new Point(393, 188);
            Num_SgtByrdBombs.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            Num_SgtByrdBombs.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            Num_SgtByrdBombs.Name = "Num_SgtByrdBombs";
            Num_SgtByrdBombs.Size = new Size(86, 23);
            Num_SgtByrdBombs.TabIndex = 48;
            Num_SgtByrdBombs.ValueChanged += Num_SgtByrdBombs_ValueChanged;
            // 
            // Num_SgtByrdMissiles
            // 
            Num_SgtByrdMissiles.Location = new Point(393, 217);
            Num_SgtByrdMissiles.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            Num_SgtByrdMissiles.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            Num_SgtByrdMissiles.Name = "Num_SgtByrdMissiles";
            Num_SgtByrdMissiles.Size = new Size(86, 23);
            Num_SgtByrdMissiles.TabIndex = 47;
            Num_SgtByrdMissiles.ValueChanged += Num_SgtByrdMissiles_ValueChanged;
            // 
            // Num_SparxBombs
            // 
            Num_SparxBombs.Location = new Point(393, 246);
            Num_SparxBombs.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            Num_SparxBombs.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            Num_SparxBombs.Name = "Num_SparxBombs";
            Num_SparxBombs.Size = new Size(86, 23);
            Num_SparxBombs.TabIndex = 46;
            Num_SparxBombs.ValueChanged += Num_SparxBombs_ValueChanged;
            // 
            // Num_SparxMissiles
            // 
            Num_SparxMissiles.Location = new Point(393, 275);
            Num_SparxMissiles.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            Num_SparxMissiles.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            Num_SparxMissiles.Name = "Num_SparxMissiles";
            Num_SparxMissiles.Size = new Size(86, 23);
            Num_SparxMissiles.TabIndex = 45;
            Num_SparxMissiles.ValueChanged += Num_SparxMissiles_ValueChanged;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label20.Location = new Point(235, 307);
            label20.Name = "label20";
            label20.Size = new Size(94, 20);
            label20.TabIndex = 44;
            label20.Text = "Blink Bombs:";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label19.Location = new Point(235, 278);
            label19.Name = "label19";
            label19.Size = new Size(152, 20);
            label19.TabIndex = 43;
            label19.Text = "Sparx Seeker Missiles:";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label18.Location = new Point(235, 249);
            label18.Name = "label18";
            label18.Size = new Size(142, 20);
            label18.TabIndex = 42;
            label18.Text = "Sparx Smart Bombs:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label17.Location = new Point(235, 220);
            label17.Name = "label17";
            label17.Size = new Size(126, 20);
            label17.TabIndex = 41;
            label17.Text = "Sgt. Byrd Missiles:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label16.Location = new Point(235, 191);
            label16.Name = "label16";
            label16.Size = new Size(121, 20);
            label16.TabIndex = 40;
            label16.Text = "Sgt. Byrd Bombs:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label15.Location = new Point(2, 307);
            label15.Name = "label15";
            label15.Size = new Size(135, 20);
            label15.TabIndex = 39;
            label15.Text = "Total Dragon Eggs:";
            // 
            // Num_DragonEggs
            // 
            Num_DragonEggs.Location = new Point(143, 304);
            Num_DragonEggs.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            Num_DragonEggs.Minimum = new decimal(new int[] { 128, 0, 0, int.MinValue });
            Num_DragonEggs.Name = "Num_DragonEggs";
            Num_DragonEggs.Size = new Size(86, 23);
            Num_DragonEggs.TabIndex = 38;
            Num_DragonEggs.ValueChanged += Num_DragonEggs_ValueChanged;
            // 
            // Num_DarkGems
            // 
            Num_DarkGems.Location = new Point(143, 275);
            Num_DarkGems.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            Num_DarkGems.Minimum = new decimal(new int[] { 128, 0, 0, int.MinValue });
            Num_DarkGems.Name = "Num_DarkGems";
            Num_DarkGems.Size = new Size(86, 23);
            Num_DarkGems.TabIndex = 37;
            Num_DarkGems.ValueChanged += Num_DarkGems_ValueChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.Location = new Point(2, 278);
            label14.Name = "label14";
            label14.Size = new Size(121, 20);
            label14.TabIndex = 36;
            label14.Text = "Total Dark Gems:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label13.Location = new Point(2, 249);
            label13.Name = "label13";
            label13.Size = new Size(123, 20);
            label13.TabIndex = 35;
            label13.Text = "Total Light Gems:";
            // 
            // Num_LightGems
            // 
            Num_LightGems.Location = new Point(143, 246);
            Num_LightGems.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            Num_LightGems.Minimum = new decimal(new int[] { 128, 0, 0, int.MinValue });
            Num_LightGems.Name = "Num_LightGems";
            Num_LightGems.Size = new Size(86, 23);
            Num_LightGems.TabIndex = 34;
            Num_LightGems.ValueChanged += Num_LightGems_ValueChanged;
            // 
            // FlowPanel_PowerUps
            // 
            FlowPanel_PowerUps.Location = new Point(3, 374);
            FlowPanel_PowerUps.Name = "FlowPanel_PowerUps";
            FlowPanel_PowerUps.Size = new Size(912, 130);
            FlowPanel_PowerUps.TabIndex = 33;
            FlowPanel_PowerUps.WrapContents = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.Location = new Point(2, 220);
            label12.Name = "label12";
            label12.Size = new Size(86, 20);
            label12.TabIndex = 32;
            label12.Text = "Total Gems:";
            // 
            // Num_TotalGems
            // 
            Num_TotalGems.Location = new Point(143, 217);
            Num_TotalGems.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            Num_TotalGems.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            Num_TotalGems.Name = "Num_TotalGems";
            Num_TotalGems.Size = new Size(86, 23);
            Num_TotalGems.TabIndex = 31;
            Num_TotalGems.ValueChanged += Num_TotalGems_ValueChanged;
            // 
            // Num_Gems
            // 
            Num_Gems.Location = new Point(143, 188);
            Num_Gems.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            Num_Gems.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            Num_Gems.Name = "Num_Gems";
            Num_Gems.Size = new Size(86, 23);
            Num_Gems.TabIndex = 30;
            Num_Gems.ValueChanged += Num_Gems_ValueChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(2, 191);
            label11.Name = "label11";
            label11.Size = new Size(49, 20);
            label11.TabIndex = 29;
            label11.Text = "Gems:";
            // 
            // Btn_ClearAllAbilityFlags
            // 
            Btn_ClearAllAbilityFlags.BackColor = Color.FromArgb(255, 192, 192);
            Btn_ClearAllAbilityFlags.Location = new Point(695, 907);
            Btn_ClearAllAbilityFlags.Name = "Btn_ClearAllAbilityFlags";
            Btn_ClearAllAbilityFlags.Size = new Size(75, 23);
            Btn_ClearAllAbilityFlags.TabIndex = 28;
            Btn_ClearAllAbilityFlags.Text = "Clear All";
            Btn_ClearAllAbilityFlags.UseVisualStyleBackColor = false;
            Btn_ClearAllAbilityFlags.Click += Btn_ClearAllAbilityFlags_Click;
            // 
            // Btn_SetAllAbilityFlags
            // 
            Btn_SetAllAbilityFlags.BackColor = Color.FromArgb(192, 255, 192);
            Btn_SetAllAbilityFlags.Location = new Point(614, 907);
            Btn_SetAllAbilityFlags.Name = "Btn_SetAllAbilityFlags";
            Btn_SetAllAbilityFlags.Size = new Size(75, 23);
            Btn_SetAllAbilityFlags.TabIndex = 27;
            Btn_SetAllAbilityFlags.Text = "Set All";
            Btn_SetAllAbilityFlags.UseVisualStyleBackColor = false;
            Btn_SetAllAbilityFlags.Click += Btn_SetAllAbilityFlags_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(614, 514);
            label10.Name = "label10";
            label10.Size = new Size(93, 20);
            label10.TabIndex = 26;
            label10.Text = "Ability Flags:";
            // 
            // CheckList_AbilityFlags
            // 
            CheckList_AbilityFlags.FormattingEnabled = true;
            CheckList_AbilityFlags.Location = new Point(614, 537);
            CheckList_AbilityFlags.Name = "CheckList_AbilityFlags";
            CheckList_AbilityFlags.Size = new Size(300, 364);
            CheckList_AbilityFlags.TabIndex = 25;
            CheckList_AbilityFlags.ItemCheck += CheckList_AbilityFlags_ItemCheck;
            // 
            // Lbl_SlotIndex
            // 
            Lbl_SlotIndex.AutoSize = true;
            Lbl_SlotIndex.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_SlotIndex.Location = new Point(3, 9);
            Lbl_SlotIndex.Name = "Lbl_SlotIndex";
            Lbl_SlotIndex.Size = new Size(63, 25);
            Lbl_SlotIndex.TabIndex = 24;
            Lbl_SlotIndex.Text = "Slot #";
            // 
            // Btn_DoAllTasks
            // 
            Btn_DoAllTasks.BackColor = Color.FromArgb(192, 255, 192);
            Btn_DoAllTasks.Location = new Point(389, 907);
            Btn_DoAllTasks.Name = "Btn_DoAllTasks";
            Btn_DoAllTasks.Size = new Size(75, 23);
            Btn_DoAllTasks.TabIndex = 23;
            Btn_DoAllTasks.Text = "Do All";
            Btn_DoAllTasks.UseVisualStyleBackColor = false;
            Btn_DoAllTasks.Click += Btn_DoAllTasks_Click;
            // 
            // Btn_FindAllTasks
            // 
            Btn_FindAllTasks.BackColor = Color.FromArgb(192, 255, 192);
            Btn_FindAllTasks.Location = new Point(308, 907);
            Btn_FindAllTasks.Name = "Btn_FindAllTasks";
            Btn_FindAllTasks.Size = new Size(75, 23);
            Btn_FindAllTasks.TabIndex = 22;
            Btn_FindAllTasks.Text = "Find All";
            Btn_FindAllTasks.UseVisualStyleBackColor = false;
            Btn_FindAllTasks.Click += Btn_FindAllTasks_Click;
            // 
            // Btn_ClearAllTasks
            // 
            Btn_ClearAllTasks.BackColor = Color.FromArgb(255, 192, 192);
            Btn_ClearAllTasks.Location = new Point(470, 907);
            Btn_ClearAllTasks.Name = "Btn_ClearAllTasks";
            Btn_ClearAllTasks.Size = new Size(75, 23);
            Btn_ClearAllTasks.TabIndex = 21;
            Btn_ClearAllTasks.Text = "Clear All";
            Btn_ClearAllTasks.UseVisualStyleBackColor = false;
            Btn_ClearAllTasks.Click += Btn_ClearAllTasks_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(308, 514);
            label9.Name = "label9";
            label9.Size = new Size(65, 20);
            label9.TabIndex = 20;
            label9.Text = "Task List:";
            // 
            // FlowPanel_Tasks
            // 
            FlowPanel_Tasks.AutoScroll = true;
            FlowPanel_Tasks.BackColor = Color.White;
            FlowPanel_Tasks.BorderStyle = BorderStyle.FixedSingle;
            FlowPanel_Tasks.FlowDirection = FlowDirection.TopDown;
            FlowPanel_Tasks.ForeColor = SystemColors.ControlText;
            FlowPanel_Tasks.Location = new Point(308, 537);
            FlowPanel_Tasks.Name = "FlowPanel_Tasks";
            FlowPanel_Tasks.Size = new Size(300, 364);
            FlowPanel_Tasks.TabIndex = 19;
            FlowPanel_Tasks.WrapContents = false;
            // 
            // Btn_ClearAllObjectives
            // 
            Btn_ClearAllObjectives.BackColor = Color.FromArgb(255, 192, 192);
            Btn_ClearAllObjectives.Location = new Point(83, 907);
            Btn_ClearAllObjectives.Name = "Btn_ClearAllObjectives";
            Btn_ClearAllObjectives.Size = new Size(75, 23);
            Btn_ClearAllObjectives.TabIndex = 18;
            Btn_ClearAllObjectives.Text = "Clear All";
            Btn_ClearAllObjectives.UseVisualStyleBackColor = false;
            Btn_ClearAllObjectives.Click += Btn_ClearAllObjectives_Click;
            // 
            // Btn_SetAllObjectives
            // 
            Btn_SetAllObjectives.BackColor = Color.FromArgb(192, 255, 192);
            Btn_SetAllObjectives.Location = new Point(2, 907);
            Btn_SetAllObjectives.Name = "Btn_SetAllObjectives";
            Btn_SetAllObjectives.Size = new Size(75, 23);
            Btn_SetAllObjectives.TabIndex = 17;
            Btn_SetAllObjectives.Text = "Set All";
            Btn_SetAllObjectives.UseVisualStyleBackColor = false;
            Btn_SetAllObjectives.Click += Btn_SetAllObjectives_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(2, 514);
            label8.Name = "label8";
            label8.Size = new Size(81, 20);
            label8.TabIndex = 16;
            label8.Text = "Objectives:";
            // 
            // CheckList_Objectives
            // 
            CheckList_Objectives.FormattingEnabled = true;
            CheckList_Objectives.Location = new Point(2, 537);
            CheckList_Objectives.Name = "CheckList_Objectives";
            CheckList_Objectives.Size = new Size(300, 364);
            CheckList_Objectives.TabIndex = 15;
            CheckList_Objectives.ItemCheck += CheckList_Objectives_ItemCheck;
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
            Check_FileUsed.Location = new Point(72, 12);
            Check_FileUsed.Name = "Check_FileUsed";
            Check_FileUsed.Size = new Size(90, 19);
            Check_FileUsed.TabIndex = 0;
            Check_FileUsed.Text = "File Is In Use";
            Check_FileUsed.UseVisualStyleBackColor = true;
            Check_FileUsed.CheckedChanged += Check_FileUsed_CheckedChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { quickActionsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1243, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // quickActionsToolStripMenuItem
            // 
            quickActionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { MenuItem_ResetSlot });
            quickActionsToolStripMenuItem.Name = "quickActionsToolStripMenuItem";
            quickActionsToolStripMenuItem.Size = new Size(93, 20);
            quickActionsToolStripMenuItem.Text = "Quick Actions";
            // 
            // MenuItem_ResetSlot
            // 
            MenuItem_ResetSlot.Name = "MenuItem_ResetSlot";
            MenuItem_ResetSlot.Size = new Size(180, 22);
            MenuItem_ResetSlot.Text = "Reset This Slot";
            MenuItem_ResetSlot.Click += MenuItem_ResetSlot_Click;
            // 
            // SaveSlotEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1243, 953);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "SaveSlotEditor";
            ShowIcon = false;
            Text = "Save Slot Editor";
            FormClosed += SaveSlotEditor_FormClosed;
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
            ((System.ComponentModel.ISupportInitialize)Num_Health).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_DoubleGemMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_DoubleGem).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_Supercharge).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_SuperchargeMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_Invincibility).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_InvincibilityMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_BlinkBombs).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_SgtByrdBombs).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_SgtByrdMissiles).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_SparxBombs).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_SparxMissiles).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_DragonEggs).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_DarkGems).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_LightGems).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_TotalGems).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_Gems).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_PlayTimeSeconds).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_PlayTimeMinutes).EndInit();
            ((System.ComponentModel.ISupportInitialize)Num_PlayTimeHours).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private CheckedListBox CheckList_Objectives;
        private Label label8;
        private Button Btn_ClearAllObjectives;
        private Button Btn_SetAllObjectives;
        private FlowLayoutPanel FlowPanel_Tasks;
        private Label label9;
        private Button Btn_DoAllTasks;
        private Button Btn_FindAllTasks;
        private Button Btn_ClearAllTasks;
        private Label Lbl_SlotIndex;
        private CheckedListBox CheckList_AbilityFlags;
        private Label label10;
        private Button Btn_ClearAllAbilityFlags;
        private Button Btn_SetAllAbilityFlags;
        private Label label11;
        private NumericUpDown Num_Gems;
        private Label label12;
        private NumericUpDown Num_TotalGems;
        private FlowLayoutPanel FlowPanel_PowerUps;
        private Label label15;
        private NumericUpDown Num_DragonEggs;
        private NumericUpDown Num_DarkGems;
        private Label label14;
        private Label label13;
        private NumericUpDown Num_LightGems;
        private Label label16;
        private Label label17;
        private Label label19;
        private Label label18;
        private Label label20;
        private NumericUpDown Num_BlinkBombs;
        private NumericUpDown Num_SgtByrdBombs;
        private NumericUpDown Num_SgtByrdMissiles;
        private NumericUpDown Num_SparxBombs;
        private NumericUpDown Num_SparxMissiles;
        private Label label21;
        private Label label22;
        private NumericUpDown Num_DoubleGem;
        private NumericUpDown Num_Supercharge;
        private NumericUpDown Num_SuperchargeMax;
        private NumericUpDown Num_Invincibility;
        private NumericUpDown Num_InvincibilityMax;
        private Label label27;
        private Label label26;
        private Label label25;
        private Label label24;
        private Label label23;
        private NumericUpDown Num_DoubleGemMax;
        private Label label30;
        private Label label29;
        private Label label28;
        private Label Lbl_CompletionCalc;
        private NumericUpDown Num_Health;
        private Label label31;
        private Button Btn_Health_Heal;
        private Button Btn_Health_Damage;
        private Label Lbl_HealthDescriptor;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem quickActionsToolStripMenuItem;
        private ToolStripMenuItem MenuItem_ResetSlot;
    }
}