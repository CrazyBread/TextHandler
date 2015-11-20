namespace App
{
    partial class Form1
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
            this.LoadBtn = new System.Windows.Forms.Button();
            this.MystemBtn = new System.Windows.Forms.Button();
            this.MorphBtn = new System.Windows.Forms.Button();
            this.StatBtn = new System.Windows.Forms.Button();
            this.ClasterBtn = new System.Windows.Forms.Button();
            this.LoadPanel = new System.Windows.Forms.Panel();
            this.MystemPanel = new System.Windows.Forms.Panel();
            this.MorphPanel = new System.Windows.Forms.Panel();
            this.StatPanel = new System.Windows.Forms.Panel();
            this.ClasterPanel = new System.Windows.Forms.Panel();
            this.LoadPanel.SuspendLayout();
            this.MystemPanel.SuspendLayout();
            this.StatPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadBtn
            // 
            this.LoadBtn.Location = new System.Drawing.Point(13, 13);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(169, 33);
            this.LoadBtn.TabIndex = 0;
            this.LoadBtn.Text = "Загрузка";
            this.LoadBtn.UseVisualStyleBackColor = true;
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // MystemBtn
            // 
            this.MystemBtn.Location = new System.Drawing.Point(188, 13);
            this.MystemBtn.Name = "MystemBtn";
            this.MystemBtn.Size = new System.Drawing.Size(169, 33);
            this.MystemBtn.TabIndex = 1;
            this.MystemBtn.Text = "Mystem";
            this.MystemBtn.UseVisualStyleBackColor = true;
            this.MystemBtn.Click += new System.EventHandler(this.MystemBtn_Click);
            // 
            // MorphBtn
            // 
            this.MorphBtn.Location = new System.Drawing.Point(363, 12);
            this.MorphBtn.Name = "MorphBtn";
            this.MorphBtn.Size = new System.Drawing.Size(169, 33);
            this.MorphBtn.TabIndex = 2;
            this.MorphBtn.Text = "Морф. анализ";
            this.MorphBtn.UseVisualStyleBackColor = true;
            this.MorphBtn.Click += new System.EventHandler(this.MorphBtn_Click);
            // 
            // StatBtn
            // 
            this.StatBtn.Location = new System.Drawing.Point(538, 12);
            this.StatBtn.Name = "StatBtn";
            this.StatBtn.Size = new System.Drawing.Size(169, 33);
            this.StatBtn.TabIndex = 3;
            this.StatBtn.Text = "Стат. анализ";
            this.StatBtn.UseVisualStyleBackColor = true;
            this.StatBtn.Click += new System.EventHandler(this.StatBtn_Click);
            // 
            // ClasterBtn
            // 
            this.ClasterBtn.Location = new System.Drawing.Point(713, 12);
            this.ClasterBtn.Name = "ClasterBtn";
            this.ClasterBtn.Size = new System.Drawing.Size(169, 33);
            this.ClasterBtn.TabIndex = 4;
            this.ClasterBtn.Text = "Кластерн. анализ";
            this.ClasterBtn.UseVisualStyleBackColor = true;
            this.ClasterBtn.Click += new System.EventHandler(this.ClasterBtn_Click);
            // 
            // LoadPanel
            // 
            this.LoadPanel.Controls.Add(this.MystemPanel);
            this.LoadPanel.Location = new System.Drawing.Point(13, 52);
            this.LoadPanel.Name = "LoadPanel";
            this.LoadPanel.Size = new System.Drawing.Size(869, 403);
            this.LoadPanel.TabIndex = 5;
            // 
            // MystemPanel
            // 
            this.MystemPanel.Controls.Add(this.MorphPanel);
            this.MystemPanel.Location = new System.Drawing.Point(0, 0);
            this.MystemPanel.Name = "MystemPanel";
            this.MystemPanel.Size = new System.Drawing.Size(869, 403);
            this.MystemPanel.TabIndex = 6;
            this.MystemPanel.Visible = false;
            // 
            // MorphPanel
            // 
            this.MorphPanel.Location = new System.Drawing.Point(0, 0);
            this.MorphPanel.Name = "MorphPanel";
            this.MorphPanel.Size = new System.Drawing.Size(869, 403);
            this.MorphPanel.TabIndex = 7;
            this.MorphPanel.Visible = false;
            // 
            // StatPanel
            // 
            this.StatPanel.Controls.Add(this.ClasterPanel);
            this.StatPanel.Location = new System.Drawing.Point(13, 51);
            this.StatPanel.Name = "StatPanel";
            this.StatPanel.Size = new System.Drawing.Size(869, 403);
            this.StatPanel.TabIndex = 8;
            this.StatPanel.Visible = false;
            // 
            // ClasterPanel
            // 
            this.ClasterPanel.Location = new System.Drawing.Point(0, 0);
            this.ClasterPanel.Name = "ClasterPanel";
            this.ClasterPanel.Size = new System.Drawing.Size(869, 403);
            this.ClasterPanel.TabIndex = 9;
            this.ClasterPanel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 467);
            this.Controls.Add(this.StatPanel);
            this.Controls.Add(this.LoadPanel);
            this.Controls.Add(this.ClasterBtn);
            this.Controls.Add(this.StatBtn);
            this.Controls.Add(this.MorphBtn);
            this.Controls.Add(this.MystemBtn);
            this.Controls.Add(this.LoadBtn);
            this.Name = "Form1";
            this.Text = "Intelligent CAD";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LoadPanel.ResumeLayout(false);
            this.MystemPanel.ResumeLayout(false);
            this.StatPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadBtn;
        private System.Windows.Forms.Button MystemBtn;
        private System.Windows.Forms.Button MorphBtn;
        private System.Windows.Forms.Button StatBtn;
        private System.Windows.Forms.Button ClasterBtn;
        private System.Windows.Forms.Panel LoadPanel;
        private System.Windows.Forms.Panel MystemPanel;
        private System.Windows.Forms.Panel MorphPanel;
        private System.Windows.Forms.Panel StatPanel;
        private System.Windows.Forms.Panel ClasterPanel;
    }
}

