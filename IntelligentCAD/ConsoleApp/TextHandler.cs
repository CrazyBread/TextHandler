using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System;

namespace ConsoleApp
{
    public static class TextHandler
    {
        public static List<string> GetWords(string text, string pattern)
        {
            return Regex.Split(text, pattern)
                .Where(i => !string.IsNullOrEmpty(i))
                .Select(i => i.ToLower())
                .ToList();
        }

        public static List<string> ApplyExclusionMask(this List<string> textList, List<string> maskWords)
        {
            return textList.Where(i => !maskWords.Contains(i)).ToList();
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

        public static void PrintWordsTop<T>(this Dictionary<string, T> dictionary, int topCount, string phrase)
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
