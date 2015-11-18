namespace DesktopApplication
{
    partial class MystemHandlerForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbx_Files = new System.Windows.Forms.ComboBox();
            this.btn_LaunchMystem = new System.Windows.Forms.Button();
            this.lstb_MystemResult = new System.Windows.Forms.ListBox();
            this.btn_Continue = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbx_Files);
            this.panel1.Controls.Add(this.btn_LaunchMystem);
            this.panel1.Controls.Add(this.lstb_MystemResult);
            this.panel1.Controls.Add(this.btn_Continue);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 359);
            this.panel1.TabIndex = 2;
            // 
            // cbx_Files
            // 
            this.cbx_Files.FormattingEnabled = true;
            this.cbx_Files.Location = new System.Drawing.Point(4, 8);
            this.cbx_Files.Name = "cbx_Files";
            this.cbx_Files.Size = new System.Drawing.Size(388, 21);
            this.cbx_Files.TabIndex = 3;
            this.cbx_Files.SelectedIndexChanged += new System.EventHandler(this.cbx_Files_SelectedIndexChanged);
            // 
            // btn_LaunchMystem
            // 
            this.btn_LaunchMystem.Location = new System.Drawing.Point(401, 8);
            this.btn_LaunchMystem.Name = "btn_LaunchMystem";
            this.btn_LaunchMystem.Size = new System.Drawing.Size(88, 306);
            this.btn_LaunchMystem.TabIndex = 2;
            this.btn_LaunchMystem.Text = "Запустить Mystem";
            this.btn_LaunchMystem.UseVisualStyleBackColor = true;
            this.btn_LaunchMystem.Click += new System.EventHandler(this.btn_LaunchMystem_Click);
            // 
            // lstb_MystemResult
            // 
            this.lstb_MystemResult.FormattingEnabled = true;
            this.lstb_MystemResult.Location = new System.Drawing.Point(3, 34);
            this.lstb_MystemResult.Name = "lstb_MystemResult";
            this.lstb_MystemResult.Size = new System.Drawing.Size(389, 316);
            this.lstb_MystemResult.TabIndex = 1;
            // 
            // btn_Continue
            // 
            this.btn_Continue.Enabled = false;
            this.btn_Continue.Location = new System.Drawing.Point(400, 320);
            this.btn_Continue.Name = "btn_Continue";
            this.btn_Continue.Size = new System.Drawing.Size(89, 30);
            this.btn_Continue.TabIndex = 0;
            this.btn_Continue.Text = "Продолжить";
            this.btn_Continue.UseVisualStyleBackColor = true;
            this.btn_Continue.Click += new System.EventHandler(this.btn_Continue_Click);
            // 
            // MystemHandlerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 383);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(532, 422);
            this.MinimumSize = new System.Drawing.Size(532, 422);
            this.Name = "MystemHandlerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Обработка mystem.exe";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_LaunchMystem;
        private System.Windows.Forms.ListBox lstb_MystemResult;
        private System.Windows.Forms.Button btn_Continue;
        private System.Windows.Forms.ComboBox cbx_Files;
    }
}