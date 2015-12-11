﻿using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

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
            cbx_TextSelection.SelectedIndex = 0;
            this.singleData = singleData;
        }

        public ClasterAnalysisForm(List<StatsAnalysisResult<WordDigram>> multiData)
        {
            InitializeComponent();
            cbx_TextSelection.Enabled = false;
            cbx_TextSelection.Items.AddRange(multiData.Select(i => i.Name).ToArray());
            cbx_TextSelection.SelectedIndex = 0;
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

        private void fillListbox(Dictionary<WordDigram, double[]> dictionary)
        {
            lstb_ClusterAnalysisResult.Items.Clear();
            lstb_ClusterAnalysisResult.Items.AddRange(dictionary.Select(i => string.Format("{0} {1} : {2} ; {3}", i.Key.FirstWord, i.Key.SecondWord, i.Value[0].ToString("0.000"), i.Value[1].ToString("0.000"))).ToArray());
        }
        #endregion


        private void cbx_TextSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!Program.isSingleRegime && multiResult != null)
                fillListbox(multiResult.First(i => i.Name == (string)cbx_TextSelection.SelectedItem).Result);
        }

        private void btn_ProvideClasterAnalysis_Click(object sender, EventArgs e)
        {
            PrepareClasterData();
            if (!Program.isSingleRegime)
            {
                multiResult = Program.client.ProvideWordDigramClusterAnalysisMulticore(multiClasterAnalysisSettings);
                cbx_TextSelection.Enabled = true;
                fillListbox(multiResult.First(i => i.Name == (string)cbx_TextSelection.SelectedItem).Result);
                btn_CloseApp.Enabled = true;
            }
            else
            {
                singleResult = Program.client.ProvideClusterAnalysis<WordDigram>(singleClasterAnalysisSettings, singleData.Name);
                fillListbox(singleResult.Result);
                btn_CloseApp.Enabled = true;
                string json = JsonConvert.SerializeObject(singleResult.Result.Select(i => new { name = i.Key.FirstWord + " " + i.Key.SecondWord, values = i.Value }));
            }
        }

        private void btn_CloseApp_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
