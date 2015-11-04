using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreLib
{
    /// <summary>
    /// Статистический анализ текстов
    /// </summary>
    public static class StatisticsAnalysis
    {
        #region Однословия
        /// <summary>
        /// Получение частотного словаря (слова)
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static Dictionary<string, double> GetFrequencyDictionary(List<string> words)
        {
            return
                (
                    from i in words
                    group i by i into grp
                    select new { word = grp.Key, count = (double) grp.Count() }
                )
                .ToDictionary(i => i.word, i => i.count);
        }
        
        /// <summary>
        /// Метод TF (Абсолютная частота встречаемости слова в тексте)
        /// </summary>
        /// <param name="words"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Dictionary<string, double> GetTF(Dictionary<string, double> words, int count)
        {
            return words.ToDictionary(i => i.Key, i => ((double)i.Value / count));
        }

        /// <summary>
        /// Метод TFxIDF (наиболее статистически значимые однословия)
        /// </summary>
        /// <param name="ccSize"></param>
        /// <param name="tf_dictionary"></param>
        /// <param name="docNumber"></param>
        /// <returns></returns>
        public static Dictionary<string, double> GetTF_IDF(int ccSize, Dictionary<string, double> tf_dictionary, int docNumber = 1)
        {
            return tf_dictionary.ToDictionary(i => i.Key, i => i.Value * Math.Log((double)(ccSize - docNumber) / docNumber));
        }
        #endregion

        #region Биграммы (Digrams)
        /// <summary>
        /// Получение частотного словаря биграмм
        /// </summary>
        /// <param name="wordList"></param>
        /// <returns></returns>
        public static Dictionary<WordDigram, double> GetDigramFrequenceDictionary(List<string> wordList)
        {
            var result = new Dictionary<WordDigram, double>();
            List<string> digram;
            for (var i = 0; i < wordList.Count - 1; i++)
            {
                digram = wordList.Skip(i).Take(2).ToList();
                var digramKey = new WordDigram(digram[0], digram[1]);
                if (!result.ContainsKey(digramKey))
                    result.Add(digramKey, GetNgramFrequence(wordList, digram));
            }
            return result;
        }

        /// <summary>
        /// Метод Mutual Information (поиск наиболее статистически значимых двусловий)
        /// </summary>
        /// <param name="frequencyDiagram"></param>
        /// <param name="frequencyDictionary"></param>
        /// <param name="wordsCount"></param>
        /// <returns></returns>
        public static Dictionary<WordDigram, double> CalculateMutualInformation(Dictionary<WordDigram, double> frequencyDiagram,
            Dictionary<string, double> frequencyDictionary, int wordsCount)
        {
            var result = new Dictionary<WordDigram, double>();
            foreach (var pair in frequencyDiagram)
            {
                var key = pair.Key;
                //частота первого слова
                var fx = frequencyDictionary[key.FirstWord];
                //частота второго слова
                var fy = frequencyDictionary[key.SecondWord];
                result.Add(key, Math.Log(
                    (pair.Value * wordsCount) / fx * fy
                    , 2));
            }
            return result;
        }

        /// <summary>
        /// Метод TScore ()
        /// </summary>
        /// <param name="frequencyDiagram"></param>
        /// <param name="frequencyDictionary"></param>
        /// <param name="wordsCount"></param>
        /// <returns></returns>
        public static Dictionary<WordDigram, double> CalculateTScore(Dictionary<WordDigram, double> frequencyDiagram,
            Dictionary<string, double> frequencyDictionary, int wordsCount)
        {
            var result = new Dictionary<WordDigram, double>();
            foreach (var pair in frequencyDiagram)
            {
                var key = pair.Key;
                //частота первого слова
                var fx = frequencyDictionary[key.FirstWord];
                //частота второго слова
                var fy = frequencyDictionary[key.SecondWord];
                result.Add(key,
                    ((pair.Value - (fx * fy / (double)wordsCount)) / (pair.Value * pair.Value))
                    );
            }
            return result;
        }

        /// <summary>
        /// Метод Log-Likelihood (выявление наиболее статистически значимых двусловий)
        /// </summary>
        /// <param name="frequencyDiagram"></param>
        /// <returns></returns>
        public static Dictionary<WordDigram, double> CalculateLogLikelihood(Dictionary<WordDigram, double> frequencyDiagram)
        {
            var result = new Dictionary<WordDigram, double>();
            foreach (var pair in frequencyDiagram)
            {
                var a = pair.Value;
                var b = frequencyDiagram
                    .Where(el => el.Key.FirstWord == pair.Key.FirstWord && el.Key.SecondWord != pair.Key.SecondWord)
                    .Sum(el => el.Value);
                var c = frequencyDiagram
                    .Where(el => el.Key.FirstWord != pair.Key.FirstWord && el.Key.SecondWord == pair.Key.SecondWord)
                    .Sum(el => el.Value);
                var d = frequencyDiagram
                    .Where(el => el.Key.FirstWord != pair.Key.FirstWord && el.Key.SecondWord != pair.Key.SecondWord)
                    .Sum(el => el.Value);

                var value = a * Math.Log(a + 1)
                    + b * Math.Log(b + 1)
                    + c * Math.Log(c + 1)
                    + d * Math.Log(d + 1)
                    - (a + b) * Math.Log(a + b + 1)
                    - (a + c) * Math.Log(a + c + 1)
                    - (b + d) * Math.Log(b + d + 1)
                    - (c + d) * Math.Log(c + d + 1)
                    + (a + b + c + d) * Math.Log(a + b + c + d + 1);
                result.Add(pair.Key, value);
            }
            return result;
        }
        #endregion

        #region N-граммы
        /// <summary>
        /// Частота N-граммы в предложении
        /// </summary>
        /// <param name="wordList"></param>
        /// <param name="ngramList"></param>
        /// <returns></returns>
        public static int GetNgramFrequence(List<string> wordList, List<string> ngramList)
        {
            if (wordList == null || ngramList == null)
                return 0;
            if (wordList.Count < ngramList.Count || ngramList.Count <= 1)
                return 0;

            var result = 0;
            //Возможна также реализация через IndexOf.
            var startNgramWord = ngramList[0];
            for (var i = 0; i < wordList.Count - ngramList.Count + 1; i++)
            {
                //если не первое слово энГраммы не совпадает с текущим в списке слов, катимся дальше 
                if (wordList[i] != startNgramWord)
                    continue;

                var isEqual = true;
                //начинаем цикл со 2 слова, т.к. первое проверили на предыдущем шаге
                for (var j = 1; j < ngramList.Count; j++)
                {
                    if (wordList[i + j] != ngramList[j])
                    {
                        isEqual = false;
                        continue;
                    }
                }
                if (isEqual) result++;
            }
            return result;
        }
        #endregion
    }

    /// <summary>
    /// Extension-методы для статистического анализа
    /// </summary>
    public static class StatisticsAnalysisExtensions
    {
        /// <summary>
        /// Возвращает словарь со всеми проведенными исследованиями
        /// </summary>
        /// <param name="dictionaries"></param>
        /// <returns></returns>
        public static Dictionary<T, double[]> MergeDictionaries<T>(this List<Dictionary<T, double>> dictionaries)
        {
            Dictionary<T, double[]> mergedDictionary = new Dictionary<T, double[]>();

            foreach (var key in dictionaries[0].Keys)
            {
                List<double> metrics = new List<double>();
                foreach (var dic in dictionaries)
                {
                    double val;
                    if (dic.TryGetValue(key, out val))
                        metrics.Add(dic[key]);
                    else
                        metrics.Add(0.0f);
                }
                mergedDictionary.Add(key, metrics.ToArray());
            }
            return mergedDictionary;
        }

        /// <summary>
        /// Общее количество слов
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static int GetWordsCount(this Dictionary<string, int> words)
        {
            return words.Sum(i => i.Value);
        }

        /// <summary>
        /// Получить список слов на основе List<Lemm>
        /// </summary>
        /// <param name="list">Данные mystem</param>
        /// <returns></returns>
        public static List<string> GetWords(this List<Lemm> list)
        {
            return list.Select(el =>
            {
                if (el.analysis.Length == 0)
                    return el.text;
                return el.analysis[0].lex;
            }).ToList();
        }
    }
}
