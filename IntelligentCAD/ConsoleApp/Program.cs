using MultiprocessingLib;
using System.Collections.Generic;
using Core;
using System.Linq;
using System;
using CoreLib;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //double[,] data = new double[,]
            //{
            //    { 8.5f, 4.8f, 0.0f },
            //    { 2.5f, 7.8f, 32.0f },
            //    { 3.5f, 16.8f, 40.4f },
            //    { 5.5f, 12.8f, 34.9f },
            //    { 2.5f, 3.8f, 32.3f },
            //};
            //double[,] clusters = new double[,]
            //{
            //    { 15f, 15f, 15f },
            //    { 0f, 0f, 0f }
            //};
            //ClasterAnalysis c = new ClasterAnalysis(1.6f, 0.005f, data, clusters);
            //c.Print(c.degree_of_member);

            Multiprocessor mps = new Multiprocessor();
            mps.MultiprocessorFileRead(new List<string>() { "mainText.txt" });
            var texts = mps.Cache;

            MystemProvider mst = new MystemProvider(1);
            var list = mst.LaunchMystem(texts[0].List);

            var words = list.Select(el =>
            {
                if (el.analysis.Length == 0)
                    return el.text;
                return el.analysis[0].lex;
            }).ToList();

            var frequencyWordDict = StatisticsAnalysis.GetFrequencyDictionary(words);

            var frequencyDigramDict = StatisticsAnalysis.GetDigramFrequenceDictionary(words);
            var mutualInf = StatisticsAnalysis.CalculateMutualInformation(frequencyDigramDict, frequencyWordDict, words.Count);
            var tScore = StatisticsAnalysis.CalculateTScore(frequencyDigramDict, frequencyWordDict, words.Count);
            var llh = StatisticsAnalysis.CalculateLogLikelihood(frequencyDigramDict);

            List<Dictionary<WordDigram, double>> dList = new List<Dictionary<WordDigram, double>>();
            dList.Add(frequencyDigramDict.ToDictionary(i => i.Key, i => (double)i.Value));
            //dList.Add(mutualInf);
            //dList.Add(tScore);
            //dList.Add(llh);
            var mergedDictionary = dList.MergeDictionaries<WordDigram>();

            //СЛОВА
            List<Dictionary<string, double>> wlist = new List<Dictionary<string, double>>();
            wlist.Add(frequencyWordDict.ToDictionary(i => i.Key, i => (double)i.Value));
            var d = wlist.MergeDictionaries<string>();
            var m = d.Select(i => new { i.Key, v = i.Value[0] }).ToList();
            var c = m.OrderByDescending(i => i.v).ToList();

            double[,] clusters = new double[,]
            {
                //{ 14.8f, 0.96f, 3.29 }
                //{ 1, 10, 0.2f, 2 }
                //{ 10, 0.1f, 1 }
                { 25 },
                //{ 10 },
                { 5 }
            };
            ClasterAnalysis<string> ca = new ClasterAnalysis<string>(d, 0.000000000000000000000000001, 1.5f, 10000, clusters);
            var res = ca.Clusterize();

            var n = (
                from i in c
                join j in res on i.Key equals j.Key
                select new { word = i.Key, values = j.Value, count = i.v }
            ).ToList();
            //var r = res.Where(i => i.Key.FirstWord == "взаимодействие");
        }
    }
}
