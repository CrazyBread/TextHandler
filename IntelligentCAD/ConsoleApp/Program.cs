﻿using System.Collections.Generic;
using Core;
using System.Linq;
using System;
using CoreLib;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public class Program
    {
        //https://msdn.microsoft.com/en-us/library/windows/apps/ff402526(v=vs.105).aspx
        static void Main(string[] args)
        {
            API client = new API();
            var data = client.LoadFile("mainText.txt");
            //var multiData = client.LoadFilesMulticore(new List<string>() { "mainText.txt", "mainTextShort.txt", "prepositions.txt", "unions.txt" });
            var mst_one = client.HandleByMystem(data);
            //var mst_several = client.HandleByMystemMulticore(multiData);
            
            var stats_words_one = client.ProvideWordsStatsAnalysis(mst_one);
            var dict = client.GetDataReady<string>(stats_words_one);
            var cl_centers = client.GetDefaultClustersCenters(dict);
            var cl_settings = new ClasterSettings<string>(cl_centers, 0.000000000000000000000000001, 1.5f, 10000, dict);
            var result = client.ProvideClusterAnalysis<string>(cl_settings, "1");
            string json = JsonConvert.SerializeObject(result.Result.Select(i => new { name = i.Key, values = i.Value }));

            //ClasterAnalysis<string> ca = new ClasterAnalysis<string>(d, 0.000000000000000000000000001, 1.5f, 10000, clusters);
            //var res = ca.Clusterize();

            //var stats_digrams_one = client.ProvideDigramsStatsAnalysis(mst_one);

            //var stats_words_several = client.ProvideWordsStatsAnalysisMulticore(mst_several);
            //var stats_digrams_several = client.ProvideDigramsStatsAnalysisMulticore(mst_several);

            Console.WriteLine("That's all!");
            /*Multiprocessor mps = new Multiprocessor();
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
            //var r = res.Where(i => i.Key.FirstWord == "взаимодействие");*/
        }
    }
}
