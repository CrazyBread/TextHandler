namespace DesktopApplication
{
    partial class FileSelectionForm
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
            this.btn_FileSelection = new System.Windows.Forms.Button();
            this.lstb_Files = new System.Windows.Forms.ListBox();
            this.btn_Continue = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btn_CleanListbx = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_CleanListbx);
            this.panel1.Controls.Add(this.btn_FileSelection);
            this.panel1.Controls.Add(this.lstb_Files);
            this.panel1.Controls.Add(this.btn_Continue);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 245);
            this.panel1.TabIndex = 1;
            // 
            // btn_FileSelection
            // 
            this.btn_FileSelection.Location = new System.Drawing.Point(256, 106);
            this.btn_FileSelection.Name = "btn_FileSelection";
            this.btn_FileSelection.Size = new System.Drawing.Size(88, 94);
            this.btn_FileSelection.TabIndex = 2;
            this.btn_FileSelection.Text = "Выбрать файлы";
            this.btn_FileSelection.UseVisualStyleBackColor = true;
            this.btn_FileSelection.Click += new System.EventHandler(this.btn_FileSelection_Click);
            // 
            // lstb_Files
            // 
            this.lstb_Files.FormattingEnabled = true;
            this.lstb_Files.Location = new System.Drawing.Point(4, 4);
            this.lstb_Files.Name = "lstb_Files";
            this.lstb_Files.Size = new System.Drawing.Size(245, 238);
            this.lstb_Files.TabIndex = 1;
            // 
            // btn_Continue
            // 
            this.btn_Continue.Location = new System.Drawing.Point(255, 206);
            this.btn_Continue.Name = "btn_Continue";
            this.btn_Continue.Size = new System.Drawing.Size(89, 36);
            this.btn_Continue.TabIndex = 0;
            this.btn_Continue.Text = "Продолжить";
            this.btn_Continue.UseVisualStyleBackColor = true;
            this.btn_Continue.Click += new System.EventHandler(this.btn_Continue_Click);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // btn_CleanListbx
            // 
            this.btn_CleanListbx.Location = new System.Drawing.Point(256, 4);
            this.btn_CleanListbx.Name = "btn_CleanListbx";
            this.btn_CleanListbx.Size = new System.Drawing.Size(88, 96);
            this.btn_CleanListbx.TabIndex = 3;
            this.btn_CleanListbx.Text = "Очистить список";
            this.btn_CleanListbx.UseVisualStyleBackColor = true;
            this.btn_CleanListbx.Click += new System.EventHandler(this.btn_CleanListbx_Click);
            // 
            // FileSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 269);
            this.Controls.Add(this.panel1);
            this.Name = "FileSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор файлов для обработки";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Continue;
        private System.Windows.Forms.Button btn_FileSelection;
        private System.Windows.Forms.ListBox lstb_Files;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.Button btn_CleanListbx;
    }
}