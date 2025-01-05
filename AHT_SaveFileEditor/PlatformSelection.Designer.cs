namespace AHT_SaveFileEditor
{
    partial class PlatformSelection
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
            Btn_GameCube = new Button();
            Btn_PS2 = new Button();
            Btn_Xbox = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // Btn_GameCube
            // 
            Btn_GameCube.Location = new Point(12, 127);
            Btn_GameCube.Name = "Btn_GameCube";
            Btn_GameCube.Size = new Size(128, 46);
            Btn_GameCube.TabIndex = 0;
            Btn_GameCube.Text = "GameCube";
            Btn_GameCube.UseVisualStyleBackColor = true;
            Btn_GameCube.Click += Btn_GameCube_Click;
            // 
            // Btn_PS2
            // 
            Btn_PS2.Location = new Point(146, 127);
            Btn_PS2.Name = "Btn_PS2";
            Btn_PS2.Size = new Size(128, 46);
            Btn_PS2.TabIndex = 1;
            Btn_PS2.Text = "PlayStation 2";
            Btn_PS2.UseVisualStyleBackColor = true;
            Btn_PS2.Click += Btn_PS2_Click;
            // 
            // Btn_Xbox
            // 
            Btn_Xbox.Enabled = false;
            Btn_Xbox.Location = new Point(280, 127);
            Btn_Xbox.Name = "Btn_Xbox";
            Btn_Xbox.Size = new Size(128, 46);
            Btn_Xbox.TabIndex = 2;
            Btn_Xbox.Text = "Xbox";
            Btn_Xbox.UseVisualStyleBackColor = true;
            Btn_Xbox.Click += Btn_Xbox_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(21, 52);
            label1.Name = "label1";
            label1.Size = new Size(378, 32);
            label1.TabIndex = 3;
            label1.Text = "What platform is this save file for?";
            // 
            // PlatformSelection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 182);
            Controls.Add(label1);
            Controls.Add(Btn_Xbox);
            Controls.Add(Btn_PS2);
            Controls.Add(Btn_GameCube);
            Name = "PlatformSelection";
            ShowIcon = false;
            Text = "Select Platform";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Btn_GameCube;
        private Button Btn_PS2;
        private Button Btn_Xbox;
        private Label label1;
    }
}