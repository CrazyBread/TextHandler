using Core;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace DesktopApplication
{
    public partial class MystemHandlerForm : Form
    {
        private List<string> files = new List<string>();
        private List<Lemm> singleResult;
        private List<MystemData> multiResult;
        private MorphologicalAnalysisForm nextForm;

        public MystemHandlerForm(List<string> files)
        {
            InitializeComponent();
            cbx_Files.Items.AddRange(files.ToArray());
            cbx_Files.SelectedIndex = 0;
            this.files = files;
        }

        #region Auxillary methods
        private void fillListbox(List<Lemm> list)
        {
            lstb_MystemResult.Items.AddRange(list.Select(i => i.text).ToArray());
        }
        #endregion

        private void btn_LaunchMystem_Click(object sender, System.EventArgs e)
        {
            cbx_Files.Enabled = false;
            //single core
            if (Program.isSingleRegime)
            {
                singleResult = Program.client.HandleByMystem(files[0]);
                fillListbox(singleResult);
            }
            //multicore
            else
            {
                multiResult = Program.client.HandleByMystemMulticore(files);
                fillListbox(multiResult[0].List);
            }
            cbx_Files.Enabled = true;
            btn_Continue.Enabled = true;
        }

        private void btn_Continue_Click(object sender, System.EventArgs e)
        {
            if (Program.isSingleRegime)
                nextForm = new MorphologicalAnalysisForm(new MystemData(files[0], singleResult)); //временно
            else
                nextForm = new MorphologicalAnalysisForm(multiResult);
            this.Close();
            nextForm.Show();
        }

        private void cbx_Files_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!Program.isSingleRegime && multiResult != null)
                fillListbox(multiResult[cbx_Files.SelectedIndex].List);
        }
    }
}
