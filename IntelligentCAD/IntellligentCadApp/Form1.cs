﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MultiprocessingLib;
using Core;
using CoreLib;

namespace IntellligentCadApp
{
    public partial class TextAnalysisForm : Form
    {

        private string saveFilePath;
        private List<string> words=new List<string>();
        public TextAnalysisForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// File loading with file dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadFileButton_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Text file (*.txt)|*.txt|PDF files(*.pdf)|*.pdf";
                var dialogResult = openDialog.ShowDialog();
                if (dialogResult == DialogResult.OK
                    || dialogResult == DialogResult.Yes)
                {
                    var fileName = openDialog.FileName;
                    Multiprocessor mps = new Multiprocessor();
                    mps.MultiprocessorFileRead(new List<string>{fileName});
                    fileTextContent.Lines = mps.Cache
                        .Select(el=>el.List)
                        .SelectMany(el=>el.Select(sub=>sub))
                        .ToArray();

                    words=SplitWords();
                    analizeGroup.Enabled = true;
                    settingsGroup.Enabled = true;
                    saveResultsCheckbox.Enabled = true;
                }
            }
        }

        private List<string> SplitWords()
        {
            var lines = fileTextContent.Lines.ToList();
            var provider = new MystemProvider(0);
#warning Твердый гвоздь 
            return provider.LaunchMystem(lines)
                .Select(el=>el.InitialForms[0])
                .ToList();
        }

        private void Input<T>(Dictionary<string,T> wordsDictionary,string analysesType)
        {
            var stringResults = wordsDictionary
                .Select(el => string.Concat(el.Key, ':', el.Value))
                .ToArray();
            inputListBox.Items.Clear();
            
            inputListBox.Items.AddRange(stringResults);
            if(saveResultsCheckbox.Checked 
                && !string.IsNullOrEmpty(saveFilePath))
            {
                using(var writer=new StreamWriter(saveFilePath,true))
                {
                    writer.WriteLine(analysesType);
                    foreach (var el in stringResults)
                        writer.WriteLine(el);
                }
            }
        }

        private void FreqButton_Click(object sender, EventArgs e)
        {
            var freqDictionary = words.GetFrequencyDictionary();
            Input(freqDictionary,"Частотный анализ");
        }

        private void mutInfButton_Click(object sender, EventArgs e)
        {
            var freqDictionary = words.GetFrequencyDictionary();
            var digramfreqDictionary = StatisticsAnalysis.GetDigramFrequenceDictionary(words);
            var result = digramfreqDictionary.CalculateMutualInformation(freqDictionary, words.Count);
            Input(result.ToDictionary(el => el.Key.ToString(), el => el.Value), "MutualInformation");
        }

        private void tSourceButton_Click(object sender, EventArgs e)
        {
            var freqDictionary = words.GetFrequencyDictionary();
            var digramfreqDictionary = StatisticsAnalysis.GetDigramFrequenceDictionary(words);
            var result = digramfreqDictionary.CalculateTSorce(freqDictionary, words.Count);
            Input(result.ToDictionary(el => el.Key.ToString(), el => el.Value), "TSorce");
        }

        private void LogLinkButton_Click(object sender, EventArgs e)
        {
            var digramfreqDictionary = StatisticsAnalysis.GetDigramFrequenceDictionary(words);
            var result = digramfreqDictionary.CalculateLogLikelihood();
            Input(result.ToDictionary(el => el.Key.ToString(), el => el.Value), "LogLikelihood");
        }

        private void IDFButton_Click(object sender, EventArgs e)
        {
            var freqDictionary = words.GetFrequencyDictionary();
            var tf_dictionary = StatisticsAnalysis.GetTF(freqDictionary, words.Count);
            var tfidf_dictionary = StatisticsAnalysis.GetTF_IDF(100000, tf_dictionary);
            Input(tfidf_dictionary,"TF*IDF");
        }

        private void saveResultsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (saveResultsCheckbox.Checked)
            {
                using(var saveDialog=new SaveFileDialog())
                {
                    var dialogResult=saveDialog.ShowDialog();
                    if (dialogResult == DialogResult.OK
                        || dialogResult == DialogResult.Yes)
                    {
                        saveFilePath = saveDialog.FileName;
                        outputFileNameLabel.Text = Path.GetFileName(saveFilePath);
                    }
                    else { 
                        saveResultsCheckbox.CheckState = CheckState.Unchecked;
                    }
                }
            }
            else
            {
                saveFilePath = string.Empty;
                outputFileNameLabel.Text = string.Empty;
            }
        }

        


    }
}