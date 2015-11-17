using System;
using System.Windows.Forms;

namespace DesktopApplication
{
    public partial class ModeSelectionForm : Form
    {
        private FileSelectionForm nextForm;

        public ModeSelectionForm()
        {
            InitializeComponent();
            Program.isSingleRegime = true;        
        }

        private void rbnt_SingleThread_CheckedChanged(object sender, EventArgs e)
        {
            if(sender is RadioButton)
                Program.isSingleRegime = true;
        }

        private void rbtn_MultiThreading_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
                Program.isSingleRegime = false;
        }

        private void btn_Continue_Click(object sender, EventArgs e)
        {
            this.Hide();
            nextForm = new FileSelectionForm();
            nextForm.Show();
        }
    }
}
