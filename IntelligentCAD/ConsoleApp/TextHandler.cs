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
    public class TF_IDF 
    {
        public Dictionary<string, double> GetTF_IDF(int ccSize, Dictionary<string, double> tf_dictionary, int docNumber = 1) 
        {
            return tf_dictionary.ToDictionary(i => i.Key, i => i.Value * Math.Log((double)(ccSize - docNumber) / docNumber));
        }

        public Dictionary<string, double> GetTF(Dictionary<string, int> words, int count) 
        {
            return words.ToDictionary(i => i.Key, i => ((double)i.Value / count));
        }
    }

    public class FrequencyDictionary 
    {
        public Dictionary<string, int> GetWordsCount(List<string> words)
        {
            return
                (
                    from word in words
                    group words by word into grp
                    select new { word = grp.Key, count = grp.Count() }
                )
                .ToDictionary(i => i.word, i => i.count);
        }

        public int GetWordsCount(Dictionary<string, int> dictionary)  
        {
            return dictionary.Sum(i => i.Value);
        } 
    }

    public class TextHandler
    {
        private List<string> prepositions; //предлоги
        private List<string> unions;

        public List<string> Prepositions { get { return prepositions; } }
        public List<string> Unions { get { return unions; } }

        public TextHandler(string prepositionsFilePath, string unionsFilePath) 
        {
            prepositions = LoadLinesFromFile(prepositionsFilePath);
            unions = LoadLinesFromFile(unionsFilePath);
        }

        public List<string> GetWords(string text, string pattern) 
        {
            return Regex.Split(text, pattern)
                .Where(i => !string.IsNullOrEmpty(i))
                .Select(i => i.ToLower())
                .ToList();
        }

        public Dictionary<string, int> ApplyExclusionMask(List<string> maskWords, Dictionary<string, int> words) 
        {
            return words.Where(i => !maskWords.Contains(i.Key)).ToDictionary(i => i.Key, i => i.Value);
        }

        //Временно
        //http://habrahabr.ru/post/147927/
        public List<string> CutOutWords(string filePath, string pattern) 
        {
            Stream stream = new FileStream(filePath, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            string str = string.Empty;
            List<string> words = new List<string>();

            while (!reader.EndOfStream) 
            {
                str = reader.ReadLine();
                words.AddRange(GetWords(str, pattern));
            }

            return words;
        }

        private List<string> LoadLinesFromFile(string filePath) 
        {
            if (!File.Exists(filePath))
                return null;
            
            List<string> list = new List<string>();

            Stream stream = new FileStream(filePath, FileMode.Open);
            StreamReader reader = new StreamReader(stream);

            while (!reader.EndOfStream) 
            {
                list.Add(reader.ReadLine());
            }

            return list;
        }

        public void PrintWordsTop<T>(Dictionary<string, T> dictionary, int topCount, string phrase)
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
