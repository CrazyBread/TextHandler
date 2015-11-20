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
    public partial class StatsAnalysisForm : Form
    {
        private MystemData data;
        private List<MystemData> listData;

        private StatsAnalysisResult<WordDigram> singleData;
        private List<StatsAnalysisResult<WordDigram>> multiData;
        
        public StatsAnalysisForm(MystemData data)
        {
            InitializeComponent();
            this.data = data;

            cbx_TextSelection.Enabled = false;
            cbx_StatSelection.Enabled = false;
            cbx_StatSelection.Items.AddRange(new string[] { "Частотный словарь", "Loglikelihood", "Mutual information", "Tscore" });
            cbx_TextSelection.Items.Add(data.Name);
            cbx_TextSelection.SelectedIndex = 0;
            cbx_StatSelection.SelectedIndex = 0;
        }

        public StatsAnalysisForm(List<MystemData> listData)
        {
            InitializeComponent();
            this.listData = listData;

            cbx_TextSelection.Enabled = false;
            cbx_StatSelection.Enabled = false;
            cbx_StatSelection.Items.AddRange(new string[] { "Частотный словарь", "Loglikelihood", "Mutual information", "Tscore" });
            cbx_TextSelection.Items.AddRange(listData.Select(i => i.Name).ToArray());
            cbx_TextSelection.SelectedIndex = 0;
            cbx_StatSelection.SelectedIndex = 0;
        }

        private void btn_ProvideStatsAnalysis_Click(object sender, EventArgs e)
        {
            if (Program.isSingleRegime)
            {
                singleData = Program.client.ProvideDigramsStatsAnalysis(data);
                fillListbox(singleData, (string)cbx_StatSelection.SelectedItem);
            }
            else
            {
                multiData = Program.client.ProvideDigramsStatsAnalysisMulticore(listData);
                cbx_TextSelection.Enabled = true;
                fillListbox(multiData.First(i => i.Name == (string)cbx_TextSelection.SelectedItem), (string)cbx_StatSelection.SelectedItem);
            }
            cbx_StatSelection.Enabled = true;
            btn_Continue.Enabled = true;
        }

        #region auxillary methods
        private void fillListbox(StatsAnalysisResult<WordDigram> data, string statFilter)
        {
            lstb_StatsAnalysisResult.Items.Clear();
            switch (statFilter)
            {
                case "Частотный словарь":
                    lstb_StatsAnalysisResult.Items.AddRange(data.Frequency_Dictionary.Select(i => string.Format("{0} {1} : {2}", i.Key.FirstWord, i.Key.SecondWord, (int)i.Value)).ToArray());
                    break;
                case "Loglikelihood":
                    lstb_StatsAnalysisResult.Items.AddRange(data.LogLikelihood_Dictionary.Select(i => string.Format("{0} {1} : {2}", i.Key.FirstWord, i.Key.SecondWord, i.Value)).ToArray());
                    break;
                case "Mutual information":
                    lstb_StatsAnalysisResult.Items.AddRange(data.MutualInformation_Dictionary.Select(i => string.Format("{0} {1} : {2}", i.Key.FirstWord, i.Key.SecondWord, i.Value)).ToArray());
                    break;
                case "Tscore":
                    lstb_StatsAnalysisResult.Items.AddRange(data.TScore_Dictionary.Select(i => string.Format("{0} {1} : {2}", i.Key.FirstWord, i.Key.SecondWord, i.Value)).ToArray());
                    break;
            }
        }
        #endregion

        //Выбор текста
        private void cbx_TextSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Program.isSingleRegime && multiData != null)
                fillListbox(multiData.First(i => i.Name == (string)cbx_TextSelection.SelectedItem), (string)cbx_StatSelection.SelectedItem);
        }

        //Выбор статистики
        private void cbx_StatSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Program.isSingleRegime && multiData != null)
                fillListbox(multiData.First(i => i.Name == (string)cbx_TextSelection.SelectedItem), (string)cbx_StatSelection.SelectedItem);
            else if (Program.isSingleRegime && singleData != null)
                fillListbox(singleData, (string)cbx_StatSelection.SelectedItem);
        }

        //Загрузка формы
        private void StatsAnalysisForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
