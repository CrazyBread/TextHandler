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
        public class ClasterAnalysis<T>
        {
            private int clustersCount;
            private int dataVectorsCount;
            private int dimensionsCount;
            private int iterations;
            private double[,] memberDegree;
            private double epsilon;
            private double fuzziness;
            private double[,] data;
            private double[,] clusterCenters;
            private List<T> wordsDigrams;

            #region Конструкторы
            public ClasterAnalysis(Dictionary<T, double[]> wordStats, double epsilon, double fuzziness, int iterations, double[,] clusters)
            {
                this.epsilon = epsilon;
                this.fuzziness = fuzziness;
                this.clusterCenters = clusters;
                this.iterations = iterations;
                memberDegree = new double[wordStats.Count, clusters.GetLength(0)];
                data = new double[wordStats.Count, wordStats.First().Value.Length];
                wordsDigrams = FillDataMatrix(wordStats);
                InitializeMatrix();

                clustersCount = clusterCenters.GetLength(0);
                dataVectorsCount = data.GetLength(0);
                dimensionsCount = data.GetLength(1);
            }
            #endregion    

            /// <summary>
            /// Сохранение данных
            /// </summary>
            /// <param name="wordStats"></param>
            private List<T> FillDataMatrix(Dictionary<T, double[]> wordStats)
            {
                wordsDigrams = wordStats.Keys.ToList();
                int n = 0;
                foreach (var item in wordStats)
                {
                    double[] stats = item.Value;
                    for (int j = 0; j < stats.Length; j++)
                    {
                        data[n, j] = stats[j];
                    }
                    n++; 
                }
                return wordsDigrams;
            }

            /// <summary>
            /// Инициализация матрицы принадлежности, с условием, что сумма всех значений не превышает 1.0
            /// </summary>
            private void InitializeMatrix()
            {
                double s;
                int r = 100, rval;
                Random rnd = new Random();
                for (int i = 0; i < memberDegree.GetLength(0); i++)
                {
                    s = 0.0f;
                    r = 100;
                    for (int j = 1; j < memberDegree.GetLength(1); j++)
                    {
                        rval = rnd.Next(1, r);
                        r -= rval;
                        memberDegree[i, j] = rval / 100.0f;
                        s += memberDegree[i, j];
                    }
                    memberDegree[i, 0] = 1.0 - s;
                }
            }

            /// <summary>
            /// Получение Евклидова расстояния
            /// </summary>
            /// <returns></returns>
            private double CalculateEuclidLength(int i_data, int j_cl)
            {
                double sum = 0f;
                for (int d = 0; d < dimensionsCount; d++)
                {
                    sum += Math.Pow(data[i_data, d] - clusterCenters[j_cl, d], 2);
                }
                return Math.Sqrt(sum);
            }

            /// <summary>
            /// Расчет нового значения матрицы принадлежности
            /// </summary>
            /// <param name="i"></param>
            /// <param name="j"></param>
            /// <returns></returns>
            private double CalculateNewMemberDegreeItem(int i, int j)
            {
                double power = 2 / (fuzziness - 1), sum = 0.0f;
                for (int k = 0; k < clustersCount; k++)
                {
                    sum += Math.Pow(CalculateEuclidLength(i, j) / CalculateEuclidLength(i, k), power);
                }
                return 1.0f / sum;
            }

            /// <summary>
            /// Перерасчет центров кластеров
            /// SUM(k = 1, K)(M_jk)^q * x_k
            /// ___________________________
            /// SUM(k = 1, K)(M_jk)^q
            /// </summary>
            private void CalculateClusterCenters()
            {
                double numerator, denominator;
                double[,] M_jk = new double[dataVectorsCount, clustersCount];

                //расчет M_jk
                for (int i = 0; i < dataVectorsCount; i++)
                {
                    for (int j = 0; j < clustersCount; j++)
                    {
                        M_jk[i,j] = Math.Pow(memberDegree[i, j], fuzziness);
                    }
                }

                //перерасчет центров кластеров
                for (int j = 0; j < clustersCount; j++)
                {
                    for (int k = 0; k < dimensionsCount; k++)
                    {
                        numerator = 0f;
                        denominator = 0f;
                        for (int i = 0; i < dataVectorsCount; i++)
                        {
                            numerator += M_jk[i, j] * data[i, k];
                            denominator += M_jk[i, j];
                        }
                        clusterCenters[j, k] = numerator / denominator;
                    }
                }
            }

            /// <summary>
            /// Перерасчет матрицы принадлежности
            ///                        1 
            /// __________________________________________________
            ///                   |x_i - c_j|
            /// SUM(k = 1, C) (____________________) ^ (2/(q - 1))
            ///                   |x_i - x_k|
            /// </summary>
            /// <returns></returns>
            private double RecalculateMemberDegree()
            {
                double maxDiff = 0.0f, diff = 0.0f, mdValue;

                for (int j = 0; j < clustersCount; j++)
                {
                    for (int i = 0; i < dataVectorsCount; i++)
                    {
                        mdValue = CalculateNewMemberDegreeItem(i, j);
                        diff = Math.Abs(mdValue - memberDegree[i, j]);
                        //diff = mdValue - memberDegree[i, j];
                        if (diff > maxDiff)
                            maxDiff = diff;
                        memberDegree[i,j] = mdValue;
                    }
                }
                return maxDiff;
            }

            public Dictionary<T, double[]> Clusterize()
            {
                double maxDiff = 0.0f;
                int i = 0;
                do
                {
                    CalculateClusterCenters();
                    maxDiff = RecalculateMemberDegree();
                    i++;
                    Console.WriteLine(maxDiff);
                }
                while (maxDiff > epsilon && i < iterations);

                //формирование выходного словаря
                var dict = new Dictionary<T, double[]>();
                int n = 0;
                foreach (var key in wordsDigrams)
                {
                    double[] analysis = new double[clustersCount];
                    for (i = 0; i < clustersCount; i++)
                    {
                        analysis[i] = memberDegree[n, i];
                    }
                    dict.Add(key, analysis);
                    n++;
                }
                return dict;
            }
        }

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
