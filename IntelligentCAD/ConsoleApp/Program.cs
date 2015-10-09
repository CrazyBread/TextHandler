﻿using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var prepositions = TextHandler.LoadLinesFromFile("prepositions.txt");
            var unions = TextHandler.LoadLinesFromFile("unions.txt");
            var wordsList = TextHandler.CutOutWords("text.txt", "[^a-zA-Zа-яА-ЯёЁ0-9-]+");

            //Чистим от предлогов/союзов
            wordsList = wordsList.ApplyExclusionMask(prepositions);
            wordsList = wordsList.ApplyExclusionMask(unions);
            

            //1. Частотный словарь
            var dictionary = FrequencyDictionary.GetFrequenceDictionary(wordsList);
            int wordsCount = wordsList.Count;
            TextHandler.PrintWordsTop(dictionary, 20, "Частотный словарь");            

            //2.TF_IDF
            var tf_dictionary = StatisticsMetrics.GetTF(dictionary, wordsCount);
            tf_dictionary.PrintWordsTop(20, "TF (абсолютная частота встречаемости)");
            var tfidf_dictionary = StatisticsMetrics.GetTF_IDF(100000, tf_dictionary);
            tf_dictionary.PrintWordsTop(20, "TF*IDF(обратная частота документа)");
            Console.WriteLine("Done!");
        }
    }
}
