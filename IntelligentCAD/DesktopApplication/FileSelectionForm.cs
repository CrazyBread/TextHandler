using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DesktopApplication
{
    public partial class FileSelectionForm : Form
    {
        private MystemHandlerForm nextForm;

        public FileSelectionForm()
        {
            InitializeComponent();
            fileDialog.Multiselect = !Program.isSingleRegime ? true : false;
        }

        private void btn_FileSelection_Click(object sender, EventArgs e)
        {
            DialogResult result = fileDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                if (fileDialog.Multiselect)
                    lstb_Files.Items.AddRange(fileDialog.FileNames);
                else
                    lstb_Files.Items.Add(fileDialog.FileName);
            }
        }

        private void btn_Continue_Click(object sender, EventArgs e)
        {
            if (lstb_Files.Items.Count > 0)
            {
                List<string> selectedFiles = new List<string>();
                for (int i = 0; i < lstb_Files.Items.Count; i++)
                {
                    selectedFiles.Add((string)lstb_Files.Items[i]);
                }
                nextForm = new MystemHandlerForm(selectedFiles);
                this.Close();
                nextForm.Show();
            }
            else
            {
                MessageBox.Show("Файлы не выбраны");
            }
        }

        private void btn_CleanListbx_Click(object sender, EventArgs e)
        {
            lstb_Files.Items.Clear();
        }
    }
}