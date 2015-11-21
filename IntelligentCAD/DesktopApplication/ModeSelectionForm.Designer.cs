namespace DesktopApplication
{
    partial class ModeSelectionForm
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
            this.btn_Continue = new System.Windows.Forms.Button();
            this.lbl_PlainText = new System.Windows.Forms.Label();
            this.rbtn_MultiThreading = new System.Windows.Forms.RadioButton();
            this.rbnt_SingleThread = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Continue);
            this.panel1.Controls.Add(this.lbl_PlainText);
            this.panel1.Controls.Add(this.rbtn_MultiThreading);
            this.panel1.Controls.Add(this.rbnt_SingleThread);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 151);
            this.panel1.TabIndex = 0;
            // 
            // btn_Continue
            // 
            this.btn_Continue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Continue.Location = new System.Drawing.Point(318, 116);
            this.btn_Continue.Name = "btn_Continue";
            this.btn_Continue.Size = new System.Drawing.Size(101, 30);
            this.btn_Continue.TabIndex = 3;
            this.btn_Continue.Text = "Продолжить";
            this.btn_Continue.UseVisualStyleBackColor = true;
            this.btn_Continue.Click += new System.EventHandler(this.btn_Continue_Click);
            // 
            // lbl_PlainText
            // 
            this.lbl_PlainText.AutoSize = true;
            this.lbl_PlainText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_PlainText.Location = new System.Drawing.Point(3, 7);
            this.lbl_PlainText.Name = "lbl_PlainText";
            this.lbl_PlainText.Size = new System.Drawing.Size(415, 18);
            this.lbl_PlainText.TabIndex = 2;
            this.lbl_PlainText.Text = "Для продолжения работы с программой выберите режим";
            // 
            // rbtn_MultiThreading
            // 
            this.rbtn_MultiThreading.AutoSize = true;
            this.rbtn_MultiThreading.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbtn_MultiThreading.Location = new System.Drawing.Point(3, 68);
            this.rbtn_MultiThreading.Name = "rbtn_MultiThreading";
            this.rbtn_MultiThreading.Size = new System.Drawing.Size(248, 22);
            this.rbtn_MultiThreading.TabIndex = 1;
            this.rbtn_MultiThreading.Text = "Обработка нескольких файлов";
            this.rbtn_MultiThreading.UseVisualStyleBackColor = true;
            this.rbtn_MultiThreading.CheckedChanged += new System.EventHandler(this.rbtn_MultiThreading_CheckedChanged);
            // 
            // rbnt_SingleThread
            // 
            this.rbnt_SingleThread.AutoSize = true;
            this.rbnt_SingleThread.Checked = true;
            this.rbnt_SingleThread.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbnt_SingleThread.Location = new System.Drawing.Point(3, 40);
            this.rbnt_SingleThread.Name = "rbnt_SingleThread";
            this.rbnt_SingleThread.Size = new System.Drawing.Size(208, 22);
            this.rbnt_SingleThread.TabIndex = 0;
            this.rbnt_SingleThread.TabStop = true;
            this.rbnt_SingleThread.Text = "Обработка одного файла";
            this.rbnt_SingleThread.UseVisualStyleBackColor = true;
            this.rbnt_SingleThread.CheckedChanged += new System.EventHandler(this.rbnt_SingleThread_CheckedChanged);
            // 
            // ModeSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 176);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(471, 215);
            this.MinimumSize = new System.Drawing.Size(471, 215);
            this.Name = "ModeSelectionForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тип работы программы";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Continue;
        private System.Windows.Forms.Label lbl_PlainText;
        private System.Windows.Forms.RadioButton rbtn_MultiThreading;
        private System.Windows.Forms.RadioButton rbnt_SingleThread;
    }
}

