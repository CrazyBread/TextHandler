using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApplication
{
    public partial class MorphologicalAnalysisForm : Form
    {
        private MystemData data;
        private List<MystemData> listData;
        private StatsAnalysisForm nextForm;


        public MorphologicalAnalysisForm(MystemData data)
        {
            InitializeComponent();
            this.data = data;
            cbx_FileSelection.Enabled = false;
            cbx_FileSelection.Items.Add(data.Name);
            cbx_FileSelection.SelectedIndex = 0;
            lstb_MorphResult.Items.Clear();
            lstb_MorphResult.Items.AddRange(data.List.Select(i => i.ToString()).ToArray());    
        }

        public MorphologicalAnalysisForm(List<MystemData> listData)
        {
            InitializeComponent();
            this.listData = listData;
            cbx_FileSelection.Items.AddRange(this.listData.Select(i => i.Name).ToArray());
            cbx_FileSelection.SelectedIndex = 0;
            lstb_MorphResult.Items.Clear();
            lstb_MorphResult.Items.AddRange(this.listData.First(i => i.Name == (string)cbx_FileSelection.SelectedItem).List.Select(i => i.ToString()).ToArray());
        }

        #region auxillary methods
        private void fillListbox(List<Lemm> list)
        {
            lstb_MorphResult.Items.Clear();
            lstb_MorphResult.Items.AddRange(list.Select(i => i.ToString()).ToArray());
        }
        #endregion

        private void btn_CheckExclusions_Click(object sender, EventArgs e)
        {
            List<string> excludedTypes = new List<string>();

            if (chbx_Conjunction.Checked)
                excludedTypes.Add(Configuration.WordType.conjunction);
            if (chbx_Particles.Checked)
                excludedTypes.Add(Configuration.WordType.particle);
            if (chbx_Prepositions.Checked)
                excludedTypes.Add(Configuration.WordType.preposition);
            
            //single core
            if (Program.isSingleRegime)
            {
                data = Program.client.ProvideMorphAnalysis(data, excludedTypes.ToArray());
                fillListbox(data.List);
            }
            //multicore
            else
            {
                cbx_FileSelection.Enabled = false;
                listData = Program.client.ProvideMorphAnalysisMulticore(listData, excludedTypes.ToArray());
                fillListbox(listData.First(i => i.Name == (string)cbx_FileSelection.SelectedItem).List);
                cbx_FileSelection.Enabled = true;
            }
        }

        private void cbx_FileSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Program.isSingleRegime)
                fillListbox(listData.First(i => i.Name == (string)cbx_FileSelection.SelectedItem).List);
        }

        private void btn_Continue_Click(object sender, EventArgs e)
        {
            if (Program.isSingleRegime)
                nextForm = new StatsAnalysisForm(data);
            else
                nextForm = new StatsAnalysisForm(listData);

            this.Close();
            nextForm.Show();
        }
    }
}
