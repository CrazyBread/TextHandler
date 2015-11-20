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
    public partial class ClasterAnalysisForm : Form
    {
        private StatsAnalysisResult<WordDigram> singleData;
        private List<StatsAnalysisResult<WordDigram>> multiData;

        private ClasterSettings<WordDigram> singleClasterAnalysisSettings;
        private List<ClasterAnalysisData<WordDigram>> multiClasterAnalysisSettings;

        private ClasterAnalysisResult<WordDigram> singleResult;
        private List<ClasterAnalysisResult<WordDigram>> multiResult;

        public ClasterAnalysisForm(StatsAnalysisResult<WordDigram> singleData)
        {
            InitializeComponent();
            cbx_TextSelection.Enabled = false;
            cbx_TextSelection.Items.Add(singleData.Name);
            this.singleData = singleData;
        }

        public ClasterAnalysisForm(List<StatsAnalysisResult<WordDigram>> multiData)
        {
            InitializeComponent();
            cbx_TextSelection.Enabled = false;
            cbx_TextSelection.Items.AddRange(multiData.Select(i => i.Name).ToArray());
            this.multiData = multiData;
            multiClasterAnalysisSettings = new List<ClasterAnalysisData<WordDigram>>();
        }

        #region auxillary methods
        private void PrepareClasterData()
        {
            if (!Program.isSingleRegime)
            {
                foreach (var item in multiData)
                {
                    var mergedData = Program.client.GetDataReady(item);
                    var clasters = Program.client.GetDefaultClustersCenters(mergedData);
                    ClasterAnalysisData<WordDigram> cad = new ClasterAnalysisData<WordDigram>(item.Name, new ClasterSettings<WordDigram>(clasters, 0.000000000000000000000000001, 1.5f, 10000, mergedData));
                    multiClasterAnalysisSettings.Add(cad);
                }
            }
            else
            {
                var mergedData = Program.client.GetDataReady(singleData);
                var clasters = Program.client.GetDefaultClustersCenters(mergedData);
                singleClasterAnalysisSettings = new ClasterSettings<WordDigram>(clasters, 0.000000000000000000000000001, 1.5f, 10000, mergedData);
            }
        }

        private void fillListbox()
        {

        }
        #endregion


        private void cbx_TextSelection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_ProvideClasterAnalysis_Click(object sender, EventArgs e)
        {
            PrepareClasterData();
            if (!Program.isSingleRegime)
            {
                multiResult = Program.client.ProvideWordDigramClusterAnalysisMulticore(multiClasterAnalysisSettings);
                cbx_TextSelection.Enabled = true;
            }
            else
            {
                singleResult = Program.client.ProvideClusterAnalysis<WordDigram>(singleClasterAnalysisSettings, singleData.Name);
            }
        }

        private void btn_CloseApp_Click(object sender, EventArgs e)
        {

        }
    }
}
