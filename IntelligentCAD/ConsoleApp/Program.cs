using System;
using MultiprocessingLib;
using System.Collections.Generic;
using System.Text;
using Core;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var prepositions = TextHandler.LoadLinesFromFile("prepositions.txt");
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
            Console.WriteLine("Done!");*/
            /*Multiprocessor mps = new Multiprocessor();
            mps.MultiprocessorFileRead(new List<string>() { "1.txt", "2.txt", "3.txt", "4.pdf" });*/

            //MorphologyAnalises.Analize("Ехал грека через реку.");
            MystemProvider mst = new MystemProvider();
            var list = mst.LaunchMystem("input.txt");
        }
    }
}
