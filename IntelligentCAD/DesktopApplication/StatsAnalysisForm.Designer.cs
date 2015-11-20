namespace DesktopApplication
{
    partial class StatsAnalysisForm
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
            this.cbx_StatSelection = new System.Windows.Forms.ComboBox();
            this.cbx_TextSelection = new System.Windows.Forms.ComboBox();
            this.btn_ProvideStatsAnalysis = new System.Windows.Forms.Button();
            this.lstb_StatsAnalysisResult = new System.Windows.Forms.ListBox();
            this.btn_Continue = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbx_StatSelection);
            this.panel1.Controls.Add(this.cbx_TextSelection);
            this.panel1.Controls.Add(this.btn_ProvideStatsAnalysis);
            this.panel1.Controls.Add(this.lstb_StatsAnalysisResult);
            this.panel1.Controls.Add(this.btn_Continue);
            this.panel1.Location = new System.Drawing.Point(16, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(656, 442);
            this.panel1.TabIndex = 4;
            // 
            // cbx_StatSelection
            // 
            this.cbx_StatSelection.FormattingEnabled = true;
            this.cbx_StatSelection.Location = new System.Drawing.Point(255, 10);
            this.cbx_StatSelection.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_StatSelection.Name = "cbx_StatSelection";
            this.cbx_StatSelection.Size = new System.Drawing.Size(235, 24);
            this.cbx_StatSelection.TabIndex = 4;
            this.cbx_StatSelection.SelectedIndexChanged += new System.EventHandler(this.cbx_StatSelection_SelectedIndexChanged);
            // 
            // cbx_TextSelection
            // 
            this.cbx_TextSelection.FormattingEnabled = true;
            this.cbx_TextSelection.Location = new System.Drawing.Point(4, 10);
            this.cbx_TextSelection.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_TextSelection.Name = "cbx_TextSelection";
            this.cbx_TextSelection.Size = new System.Drawing.Size(241, 24);
            this.cbx_TextSelection.TabIndex = 3;
            this.cbx_TextSelection.SelectedIndexChanged += new System.EventHandler(this.cbx_TextSelection_SelectedIndexChanged);
            // 
            // btn_ProvideStatsAnalysis
            // 
            this.btn_ProvideStatsAnalysis.Location = new System.Drawing.Point(499, 10);
            this.btn_ProvideStatsAnalysis.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ProvideStatsAnalysis.Name = "btn_ProvideStatsAnalysis";
            this.btn_ProvideStatsAnalysis.Size = new System.Drawing.Size(153, 377);
            this.btn_ProvideStatsAnalysis.TabIndex = 2;
            this.btn_ProvideStatsAnalysis.Text = "Провести статистический анализ";
            this.btn_ProvideStatsAnalysis.UseVisualStyleBackColor = true;
            this.btn_ProvideStatsAnalysis.Click += new System.EventHandler(this.btn_ProvideStatsAnalysis_Click);
            // 
            // lstb_StatsAnalysisResult
            // 
            this.lstb_StatsAnalysisResult.FormattingEnabled = true;
            this.lstb_StatsAnalysisResult.ItemHeight = 16;
            this.lstb_StatsAnalysisResult.Location = new System.Drawing.Point(4, 47);
            this.lstb_StatsAnalysisResult.Margin = new System.Windows.Forms.Padding(4);
            this.lstb_StatsAnalysisResult.Name = "lstb_StatsAnalysisResult";
            this.lstb_StatsAnalysisResult.Size = new System.Drawing.Size(485, 388);
            this.lstb_StatsAnalysisResult.TabIndex = 1;
            // 
            // btn_Continue
            // 
            this.btn_Continue.Enabled = false;
            this.btn_Continue.Location = new System.Drawing.Point(499, 394);
            this.btn_Continue.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Continue.Name = "btn_Continue";
            this.btn_Continue.Size = new System.Drawing.Size(153, 42);
            this.btn_Continue.TabIndex = 0;
            this.btn_Continue.Text = "Продолжить";
            this.btn_Continue.UseVisualStyleBackColor = true;
            this.btn_Continue.Click += new System.EventHandler(this.btn_Continue_Click);
            // 
            // StatsAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 462);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(703, 509);
            this.MinimumSize = new System.Drawing.Size(703, 509);
            this.Name = "StatsAnalysisForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Статистический анализ";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_ProvideStatsAnalysis;
        private System.Windows.Forms.ListBox lstb_StatsAnalysisResult;
        private System.Windows.Forms.Button btn_Continue;
        private System.Windows.Forms.ComboBox cbx_StatSelection;
        private System.Windows.Forms.ComboBox cbx_TextSelection;
    }
}