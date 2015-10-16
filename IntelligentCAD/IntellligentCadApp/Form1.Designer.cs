namespace IntellligentCadApp
{
    partial class TextAnalysisForm
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
            this.loadFileButton = new System.Windows.Forms.Button();
            this.fileTextContent = new System.Windows.Forms.RichTextBox();
            this.usePrepCheck = new System.Windows.Forms.CheckBox();
            this.useUnionsCheckbox = new System.Windows.Forms.CheckBox();
            this.useParticleCheckbox = new System.Windows.Forms.CheckBox();
            this.settingsGroup = new System.Windows.Forms.GroupBox();
            this.FreqButton = new System.Windows.Forms.Button();
            this.mutInfButton = new System.Windows.Forms.Button();
            this.tSourceButton = new System.Windows.Forms.Button();
            this.LogLinkButton = new System.Windows.Forms.Button();
            this.IDFButton = new System.Windows.Forms.Button();
            this.analizeGroup = new System.Windows.Forms.GroupBox();
            this.saveResultsCheckbox = new System.Windows.Forms.CheckBox();
            this.outputFileNameLabel = new System.Windows.Forms.Label();
            this.inputListBox = new System.Windows.Forms.ListBox();
            this.settingsGroup.SuspendLayout();
            this.analizeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(12, 13);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(100, 23);
            this.loadFileButton.TabIndex = 0;
            this.loadFileButton.Text = "Загрузить файл";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // fileTextContent
            // 
            this.fileTextContent.Location = new System.Drawing.Point(15, 42);
            this.fileTextContent.Name = "fileTextContent";
            this.fileTextContent.Size = new System.Drawing.Size(223, 290);
            this.fileTextContent.TabIndex = 2;
            this.fileTextContent.Text = "";
            // 
            // usePrepCheck
            // 
            this.usePrepCheck.AutoSize = true;
            this.usePrepCheck.Location = new System.Drawing.Point(6, 19);
            this.usePrepCheck.Name = "usePrepCheck";
            this.usePrepCheck.Size = new System.Drawing.Size(131, 17);
            this.usePrepCheck.TabIndex = 3;
            this.usePrepCheck.Text = "Учитывать предлоги";
            this.usePrepCheck.UseVisualStyleBackColor = true;
            // 
            // useUnionsCheckbox
            // 
            this.useUnionsCheckbox.AutoSize = true;
            this.useUnionsCheckbox.Location = new System.Drawing.Point(6, 42);
            this.useUnionsCheckbox.Name = "useUnionsCheckbox";
            this.useUnionsCheckbox.Size = new System.Drawing.Size(118, 17);
            this.useUnionsCheckbox.TabIndex = 4;
            this.useUnionsCheckbox.Text = "Учитывать союзы";
            this.useUnionsCheckbox.UseVisualStyleBackColor = true;
            // 
            // useParticleCheckbox
            // 
            this.useParticleCheckbox.AutoSize = true;
            this.useParticleCheckbox.Location = new System.Drawing.Point(6, 65);
            this.useParticleCheckbox.Name = "useParticleCheckbox";
            this.useParticleCheckbox.Size = new System.Drawing.Size(126, 17);
            this.useParticleCheckbox.TabIndex = 5;
            this.useParticleCheckbox.Text = "Учитывать частицы";
            this.useParticleCheckbox.UseVisualStyleBackColor = true;
            // 
            // settingsGroup
            // 
            this.settingsGroup.Controls.Add(this.useParticleCheckbox);
            this.settingsGroup.Controls.Add(this.usePrepCheck);
            this.settingsGroup.Controls.Add(this.useUnionsCheckbox);
            this.settingsGroup.Enabled = false;
            this.settingsGroup.Location = new System.Drawing.Point(244, 13);
            this.settingsGroup.Name = "settingsGroup";
            this.settingsGroup.Size = new System.Drawing.Size(147, 89);
            this.settingsGroup.TabIndex = 6;
            this.settingsGroup.TabStop = false;
            this.settingsGroup.Text = "Настройка анализа";
            // 
            // FreqButton
            // 
            this.FreqButton.Location = new System.Drawing.Point(6, 19);
            this.FreqButton.Name = "FreqButton";
            this.FreqButton.Size = new System.Drawing.Size(188, 23);
            this.FreqButton.TabIndex = 7;
            this.FreqButton.Text = "Частотность";
            this.FreqButton.UseVisualStyleBackColor = true;
            this.FreqButton.Click += new System.EventHandler(this.FreqButton_Click);
            // 
            // mutInfButton
            // 
            this.mutInfButton.Location = new System.Drawing.Point(6, 49);
            this.mutInfButton.Name = "mutInfButton";
            this.mutInfButton.Size = new System.Drawing.Size(188, 23);
            this.mutInfButton.TabIndex = 8;
            this.mutInfButton.Text = "Mutual Information";
            this.mutInfButton.UseVisualStyleBackColor = true;
            this.mutInfButton.Click += new System.EventHandler(this.mutInfButton_Click);
            // 
            // tSourceButton
            // 
            this.tSourceButton.Location = new System.Drawing.Point(6, 79);
            this.tSourceButton.Name = "tSourceButton";
            this.tSourceButton.Size = new System.Drawing.Size(188, 23);
            this.tSourceButton.TabIndex = 9;
            this.tSourceButton.Text = "T-Score";
            this.tSourceButton.UseVisualStyleBackColor = true;
            this.tSourceButton.Click += new System.EventHandler(this.tSourceButton_Click);
            // 
            // LogLinkButton
            // 
            this.LogLinkButton.Location = new System.Drawing.Point(6, 108);
            this.LogLinkButton.Name = "LogLinkButton";
            this.LogLinkButton.Size = new System.Drawing.Size(188, 23);
            this.LogLinkButton.TabIndex = 10;
            this.LogLinkButton.Text = "Log-Likelihood";
            this.LogLinkButton.UseVisualStyleBackColor = true;
            this.LogLinkButton.Click += new System.EventHandler(this.LogLinkButton_Click);
            // 
            // IDFButton
            // 
            this.IDFButton.Location = new System.Drawing.Point(6, 137);
            this.IDFButton.Name = "IDFButton";
            this.IDFButton.Size = new System.Drawing.Size(188, 23);
            this.IDFButton.TabIndex = 11;
            this.IDFButton.Text = "TF*IDF";
            this.IDFButton.UseVisualStyleBackColor = true;
            this.IDFButton.Click += new System.EventHandler(this.IDFButton_Click);
            // 
            // analizeGroup
            // 
            this.analizeGroup.Controls.Add(this.FreqButton);
            this.analizeGroup.Controls.Add(this.mutInfButton);
            this.analizeGroup.Controls.Add(this.IDFButton);
            this.analizeGroup.Controls.Add(this.tSourceButton);
            this.analizeGroup.Controls.Add(this.LogLinkButton);
            this.analizeGroup.Enabled = false;
            this.analizeGroup.Location = new System.Drawing.Point(244, 119);
            this.analizeGroup.Name = "analizeGroup";
            this.analizeGroup.Size = new System.Drawing.Size(200, 170);
            this.analizeGroup.TabIndex = 13;
            this.analizeGroup.TabStop = false;
            this.analizeGroup.Text = "Методы анализа";
            // 
            // saveResultsCheckbox
            // 
            this.saveResultsCheckbox.AutoSize = true;
            this.saveResultsCheckbox.Enabled = false;
            this.saveResultsCheckbox.Location = new System.Drawing.Point(244, 295);
            this.saveResultsCheckbox.Name = "saveResultsCheckbox";
            this.saveResultsCheckbox.Size = new System.Drawing.Size(179, 17);
            this.saveResultsCheckbox.TabIndex = 14;
            this.saveResultsCheckbox.Text = "Сохранить результаты в файл";
            this.saveResultsCheckbox.UseVisualStyleBackColor = true;
            this.saveResultsCheckbox.CheckedChanged += new System.EventHandler(this.saveResultsCheckbox_CheckedChanged);
            // 
            // outputFileNameLabel
            // 
            this.outputFileNameLabel.AutoSize = true;
            this.outputFileNameLabel.Location = new System.Drawing.Point(244, 319);
            this.outputFileNameLabel.Name = "outputFileNameLabel";
            this.outputFileNameLabel.Size = new System.Drawing.Size(0, 13);
            this.outputFileNameLabel.TabIndex = 15;
            // 
            // inputListBox
            // 
            this.inputListBox.FormattingEnabled = true;
            this.inputListBox.Location = new System.Drawing.Point(457, 13);
            this.inputListBox.Name = "inputListBox";
            this.inputListBox.Size = new System.Drawing.Size(262, 316);
            this.inputListBox.TabIndex = 16;
            // 
            // TextAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 339);
            this.Controls.Add(this.inputListBox);
            this.Controls.Add(this.outputFileNameLabel);
            this.Controls.Add(this.saveResultsCheckbox);
            this.Controls.Add(this.analizeGroup);
            this.Controls.Add(this.settingsGroup);
            this.Controls.Add(this.fileTextContent);
            this.Controls.Add(this.loadFileButton);
            this.Name = "TextAnalysisForm";
            this.Text = "Текстовый анализатор";
            this.settingsGroup.ResumeLayout(false);
            this.settingsGroup.PerformLayout();
            this.analizeGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.RichTextBox fileTextContent;
        private System.Windows.Forms.CheckBox usePrepCheck;
        private System.Windows.Forms.CheckBox useUnionsCheckbox;
        private System.Windows.Forms.CheckBox useParticleCheckbox;
        private System.Windows.Forms.GroupBox settingsGroup;
        private System.Windows.Forms.Button FreqButton;
        private System.Windows.Forms.Button mutInfButton;
        private System.Windows.Forms.Button tSourceButton;
        private System.Windows.Forms.Button LogLinkButton;
        private System.Windows.Forms.Button IDFButton;
        private System.Windows.Forms.GroupBox analizeGroup;
        private System.Windows.Forms.CheckBox saveResultsCheckbox;
        private System.Windows.Forms.Label outputFileNameLabel;
        private System.Windows.Forms.ListBox inputListBox;
    }
}

