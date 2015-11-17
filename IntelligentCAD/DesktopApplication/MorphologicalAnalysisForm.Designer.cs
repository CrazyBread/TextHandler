namespace DesktopApplication
{
    partial class MorphologicalAnalysisForm
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
            this.grpbox_Exclusions = new System.Windows.Forms.GroupBox();
            this.chbx_Particles = new System.Windows.Forms.CheckBox();
            this.chbx_Prepositions = new System.Windows.Forms.CheckBox();
            this.chbx_Conjunction = new System.Windows.Forms.CheckBox();
            this.btn_CheckExclusions = new System.Windows.Forms.Button();
            this.lstb_MorphResult = new System.Windows.Forms.ListBox();
            this.btn_Continue = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.grpbox_Exclusions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grpbox_Exclusions);
            this.panel1.Controls.Add(this.btn_CheckExclusions);
            this.panel1.Controls.Add(this.lstb_MorphResult);
            this.panel1.Controls.Add(this.btn_Continue);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 359);
            this.panel1.TabIndex = 3;
            // 
            // grpbox_Exclusions
            // 
            this.grpbox_Exclusions.Controls.Add(this.chbx_Particles);
            this.grpbox_Exclusions.Controls.Add(this.chbx_Prepositions);
            this.grpbox_Exclusions.Controls.Add(this.chbx_Conjunction);
            this.grpbox_Exclusions.Location = new System.Drawing.Point(218, 8);
            this.grpbox_Exclusions.Name = "grpbox_Exclusions";
            this.grpbox_Exclusions.Size = new System.Drawing.Size(150, 342);
            this.grpbox_Exclusions.TabIndex = 3;
            this.grpbox_Exclusions.TabStop = false;
            this.grpbox_Exclusions.Text = "Исключения";
            // 
            // chbx_Particles
            // 
            this.chbx_Particles.AutoSize = true;
            this.chbx_Particles.Checked = true;
            this.chbx_Particles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Particles.Location = new System.Drawing.Point(7, 70);
            this.chbx_Particles.Name = "chbx_Particles";
            this.chbx_Particles.Size = new System.Drawing.Size(71, 17);
            this.chbx_Particles.TabIndex = 2;
            this.chbx_Particles.Text = "Частицы";
            this.chbx_Particles.UseVisualStyleBackColor = true;
            // 
            // chbx_Prepositions
            // 
            this.chbx_Prepositions.AutoSize = true;
            this.chbx_Prepositions.Checked = true;
            this.chbx_Prepositions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Prepositions.Location = new System.Drawing.Point(7, 46);
            this.chbx_Prepositions.Name = "chbx_Prepositions";
            this.chbx_Prepositions.Size = new System.Drawing.Size(75, 17);
            this.chbx_Prepositions.TabIndex = 1;
            this.chbx_Prepositions.Text = "Предлоги";
            this.chbx_Prepositions.UseVisualStyleBackColor = true;
            // 
            // chbx_Conjunction
            // 
            this.chbx_Conjunction.AutoSize = true;
            this.chbx_Conjunction.Checked = true;
            this.chbx_Conjunction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Conjunction.Location = new System.Drawing.Point(7, 22);
            this.chbx_Conjunction.Name = "chbx_Conjunction";
            this.chbx_Conjunction.Size = new System.Drawing.Size(61, 17);
            this.chbx_Conjunction.TabIndex = 0;
            this.chbx_Conjunction.Text = "Союзы";
            this.chbx_Conjunction.UseVisualStyleBackColor = true;
            // 
            // btn_CheckExclusions
            // 
            this.btn_CheckExclusions.Location = new System.Drawing.Point(374, 8);
            this.btn_CheckExclusions.Name = "btn_CheckExclusions";
            this.btn_CheckExclusions.Size = new System.Drawing.Size(115, 306);
            this.btn_CheckExclusions.TabIndex = 2;
            this.btn_CheckExclusions.Text = "Убрать выбранные типы";
            this.btn_CheckExclusions.UseVisualStyleBackColor = true;
            // 
            // lstb_MorphResult
            // 
            this.lstb_MorphResult.FormattingEnabled = true;
            this.lstb_MorphResult.Location = new System.Drawing.Point(3, 8);
            this.lstb_MorphResult.Name = "lstb_MorphResult";
            this.lstb_MorphResult.Size = new System.Drawing.Size(209, 342);
            this.lstb_MorphResult.TabIndex = 1;
            // 
            // btn_Continue
            // 
            this.btn_Continue.Enabled = false;
            this.btn_Continue.Location = new System.Drawing.Point(374, 320);
            this.btn_Continue.Name = "btn_Continue";
            this.btn_Continue.Size = new System.Drawing.Size(115, 30);
            this.btn_Continue.TabIndex = 0;
            this.btn_Continue.Text = "Продолжить";
            this.btn_Continue.UseVisualStyleBackColor = true;
            // 
            // MorphologicalAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 383);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(532, 422);
            this.MinimumSize = new System.Drawing.Size(532, 422);
            this.Name = "MorphologicalAnalysisForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Морфологический анализ";
            this.panel1.ResumeLayout(false);
            this.grpbox_Exclusions.ResumeLayout(false);
            this.grpbox_Exclusions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_CheckExclusions;
        private System.Windows.Forms.ListBox lstb_MorphResult;
        private System.Windows.Forms.Button btn_Continue;
        private System.Windows.Forms.GroupBox grpbox_Exclusions;
        private System.Windows.Forms.CheckBox chbx_Particles;
        private System.Windows.Forms.CheckBox chbx_Prepositions;
        private System.Windows.Forms.CheckBox chbx_Conjunction;
    }
}