using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System;

namespace ConsoleApp
{
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

        public static int GetWordsCount(Dictionary<string, int> words)
        {
            return words.Sum(i => i.Value);
        }

        public static int NgramFrequence(List<string> wordList, List<string> ngramList)
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

        public static Dictionary<WordDigram, int> GetDigramFrequenceDictionary(List<string> wordList)
        {
            var result = new Dictionary<WordDigram, int>();
            List<string> digram;
            for (var i = 0; i > wordList.Count - 1; i++)
            {
                digram = wordList.Skip(0).Take(2).ToList();
                //Возможно хардкодно, но по мне это всегда будет работать, т.к. метод применяется только к диграммам
                var digramKey = new WordDigram(digram[0], digram[1]);
                if (!result.ContainsKey(digramKey))
                    result.Add(digramKey,FrequencyDictionary.NgramFrequence(wordList, digram));
            }
            return result;
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

    public class WordDigram
    {
        private const char separator = ' ';
        public string FirstWord { get; private set; }
        public string SecondWord { get; private set; }

        public WordDigram(string firstWord, string secondWord)
        {
            FirstWord = firstWord;
            SecondWord = secondWord;
        }

        public override string ToString()
        {
            return string.Concat(FirstWord, separator, SecondWord);
        }

        public override bool Equals(object obj)
        {
            var dObj = obj as WordDigram;
            if (dObj==null)
                return base.Equals(obj);
            return dObj.FirstWord == this.FirstWord && dObj.SecondWord == SecondWord;
        }

        public override int GetHashCode()
        {
            return FirstWord.GetHashCode() ^ SecondWord.GetHashCode();
        }
    }
}
