using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var prepositions = TextHandler.LoadLinesFromFile("prepositions.txt");
            var unions = TextHandler.LoadLinesFromFile("unions.txt");
            var wordsList = TextHandler.CutOutWords("text.txt", "[^a-zA-Zа-яА-ЯёЁ0-9-]+");

            //1. Частотный словарь
            var dictionary = FrequencyDictionary.GetFrequenceDictionary(wordsList);
            int wordsCount = FrequencyDictionary.GetWordsCount(wordsList);

            //Чистим от предлогов/союзов
            dictionary = TextHandler.ApplyExclusionMask(prepositions, dictionary);
            dictionary = TextHandler.ApplyExclusionMask(unions, dictionary);
            TextHandler.PrintWordsTop(dictionary, 20, "Частотный словарь");

            //2.TF_IDF
            var tf_dictionary = TF_IDF.GetTF(dictionary, wordsCount);
            TextHandler.PrintWordsTop(tf_dictionary, 20, "TF (абсолютная частота встречаемости)");
            var tfidf_dictionary = TF_IDF.GetTF_IDF(100000, tf_dictionary);
            TextHandler.PrintWordsTop(tfidf_dictionary, 20, "TF*IDF(обратная частота документа)");
            Console.WriteLine("Done!");
        }
    }
}
