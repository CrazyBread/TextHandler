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
        /// <summary>
        /// Получение частотного словаря (слова)
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetFrequencyDictionary(this List<string> words)
        {
            return
                (
                    from i in words
                    group i by i into grp
                    select new { word = grp.Key, count = grp.Count() }
                )
                .ToDictionary(i => i.word, i => i.count);
        }

        //TODO получение частотного словаря биграмм
        public static Dictionary<WordBigram, int> GetDigramFrequenceDictionary(List<string> wordList)
        {
            var result = new Dictionary<WordBigram, int>();
            List<string> digram;
            for (var i = 0; i > wordList.Count - 1; i++)
            {
                digram = wordList.Skip(0).Take(2).ToList();
                //Возможно хардкодно, но по мне это всегда будет работать, т.к. метод применяется только к диграммам
                var digramKey = new WordBigram(digram[0], digram[1]);
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
        public static Dictionary<WordBigram, double> CalculateMutualInformation(this Dictionary<WordBigram, int> frequencyDiagram,
            Dictionary<string, int> frequencyDictionary, int wordsCount)
        {
            var result = new Dictionary<WordBigram, double>();
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
        public static Dictionary<WordBigram, double> CalculateTSorce(this Dictionary<WordBigram, int> frequencyDiagram,
            Dictionary<string, int> frequencyDictionary, int wordsCount)
        {
            var result = new Dictionary<WordBigram, double>();
            foreach (var pair in frequencyDiagram)
            {
                var key = pair.Key;
                //частота первого слова
                var fx = frequencyDictionary[key.FirstWord];
                //частота второго слова
                var fy = frequencyDictionary[key.SecondWord];
                result.Add(key,
                    (pair.Value - (fx * fy / wordsCount)) / (pair.Value * pair.Value)
                    );
            }
            return result;
        }

        /// <summary>
        /// Метод Log-Likelihood (выявление наиболее статистически значимых двусловий)
        /// </summary>
        /// <param name="frequencyDiagram"></param>
        /// <returns></returns>
        public static Dictionary<WordBigram, double> CalculateLogLikelihood(this Dictionary<WordBigram, int> frequencyDiagram)
        {
            var result = new Dictionary<WordBigram, double>();
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

        /// <summary>
        /// Метод TF (Абсолютная частота встречаемости слова в тексте)
        /// </summary>
        /// <param name="words"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Dictionary<string, double> GetTF(Dictionary<string, int> words, int count)
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

        /// <summary>
        /// Общее количество слов
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static int GetWordsCount(Dictionary<string, int> words)
        {
            return words.Sum(i => i.Value);
        }

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
    }
}
