namespace DesktopApplication
{
    partial class ClasterAnalysisForm
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
            this.cbx_TextSelection = new System.Windows.Forms.ComboBox();
            this.btn_ProvideClasterAnalysis = new System.Windows.Forms.Button();
            this.lstb_ClusterAnalysisResult = new System.Windows.Forms.ListBox();
            this.btn_CloseApp = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbx_TextSelection);
            this.panel1.Controls.Add(this.btn_ProvideClasterAnalysis);
            this.panel1.Controls.Add(this.lstb_ClusterAnalysisResult);
            this.panel1.Controls.Add(this.btn_CloseApp);
            this.panel1.Location = new System.Drawing.Point(16, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(656, 442);
            this.panel1.TabIndex = 5;
            // 
            // cbx_TextSelection
            // 
            this.cbx_TextSelection.FormattingEnabled = true;
            this.cbx_TextSelection.Location = new System.Drawing.Point(4, 10);
            this.cbx_TextSelection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbx_TextSelection.Name = "cbx_TextSelection";
            this.cbx_TextSelection.Size = new System.Drawing.Size(485, 24);
            this.cbx_TextSelection.TabIndex = 3;
            this.cbx_TextSelection.SelectedIndexChanged += new System.EventHandler(this.cbx_TextSelection_SelectedIndexChanged);
            // 
            // btn_ProvideClasterAnalysis
            // 
            this.btn_ProvideClasterAnalysis.Location = new System.Drawing.Point(499, 10);
            this.btn_ProvideClasterAnalysis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_ProvideClasterAnalysis.Name = "btn_ProvideClasterAnalysis";
            this.btn_ProvideClasterAnalysis.Size = new System.Drawing.Size(153, 377);
            this.btn_ProvideClasterAnalysis.TabIndex = 2;
            this.btn_ProvideClasterAnalysis.Text = "Провести кластерный анализ";
            this.btn_ProvideClasterAnalysis.UseVisualStyleBackColor = true;
            this.btn_ProvideClasterAnalysis.Click += new System.EventHandler(this.btn_ProvideClasterAnalysis_Click);
            // 
            // lstb_ClusterAnalysisResult
            // 
            this.lstb_ClusterAnalysisResult.FormattingEnabled = true;
            this.lstb_ClusterAnalysisResult.ItemHeight = 16;
            this.lstb_ClusterAnalysisResult.Location = new System.Drawing.Point(4, 47);
            this.lstb_ClusterAnalysisResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstb_ClusterAnalysisResult.Name = "lstb_ClusterAnalysisResult";
            this.lstb_ClusterAnalysisResult.Size = new System.Drawing.Size(485, 388);
            this.lstb_ClusterAnalysisResult.TabIndex = 1;
            // 
            // btn_CloseApp
            // 
            this.btn_CloseApp.Enabled = false;
            this.btn_CloseApp.Location = new System.Drawing.Point(499, 394);
            this.btn_CloseApp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_CloseApp.Name = "btn_CloseApp";
            this.btn_CloseApp.Size = new System.Drawing.Size(153, 42);
            this.btn_CloseApp.TabIndex = 0;
            this.btn_CloseApp.Text = "Завершить работу";
            this.btn_CloseApp.UseVisualStyleBackColor = true;
            this.btn_CloseApp.Click += new System.EventHandler(this.btn_CloseApp_Click);
            // 
            // ClasterAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 462);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(703, 509);
            this.MinimumSize = new System.Drawing.Size(703, 509);
            this.Name = "ClasterAnalysisForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Кластерный анализ";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbx_TextSelection;
        private System.Windows.Forms.Button btn_ProvideClasterAnalysis;
        private System.Windows.Forms.ListBox lstb_ClusterAnalysisResult;
        private System.Windows.Forms.Button btn_CloseApp;
    }
}