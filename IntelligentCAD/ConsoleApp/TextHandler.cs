using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System;

namespace ConsoleApp
{
    /// <summary>
    /// Статистический метод TF_IDF
    /// </summary>
    public static class TF_IDF
    {
        public static Dictionary<string, double> GetTF_IDF(int ccSize, Dictionary<string, double> tf_dictionary, int docNumber = 1)
        {
            return tf_dictionary.ToDictionary(i => i.Key, i => i.Value * Math.Log((double)(ccSize - docNumber) / docNumber));
        }

        public static Dictionary<string, double> GetTF(Dictionary<string, int> words, int count)
        {
            return words.ToDictionary(i => i.Key, i => ((double)i.Value / count));
        }
    }

    public static class FrequencyDictionary
    {
        public static Dictionary<string, int> GetFrequenceDictionary(List<string> words)
        {
            return
                (
                    from word in words
                    group words by word into grp
                    select new { word = grp.Key, count = grp.Count() }
                )
                .ToDictionary(i => i.word, i => i.count);
        }

        public static int GetWordsCount(List<string> wordList)
        {
            return wordList.Count;
        }
    }

    public static class TextHandler
    {
        public static List<string> GetWords(string text, string pattern)
        {
            return Regex.Split(text, pattern)
                .Where(i => !string.IsNullOrEmpty(i))
                .Select(i => i.ToLower())
                .ToList();
        }

        public static Dictionary<string, int> ApplyExclusionMask(List<string> maskWords, Dictionary<string, int> words)
        {
            return words.Where(i => !maskWords.Contains(i.Key)).ToDictionary(i => i.Key, i => i.Value);
        }

        //Временно
        //http://habrahabr.ru/post/147927/
        public static List<string> CutOutWords(string filePath, string pattern)
        {
            string str = string.Empty;
            List<string> words = new List<string>();
            using (var reader = new StreamReader(filePath))
                while (!reader.EndOfStream)
                {
                    str = reader.ReadLine();
                    words.AddRange(GetWords(str, pattern));
                }

            return words;
        }

        public static List<string> LoadLinesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            var list = new List<string>();

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    list.Add(reader.ReadLine());
                }
            }

            return list;
        }

        public static void PrintWordsTop<T>(Dictionary<string, T> dictionary, int topCount, string phrase)
        {
            Console.WriteLine("==============={0}===============", phrase);
            var top = dictionary.OrderByDescending(i => i.Value).Take(topCount);
            foreach (var item in top)
            {
                Console.WriteLine("{0} - {1}", item.Key, item.Value);
            }
        }
    }
}
