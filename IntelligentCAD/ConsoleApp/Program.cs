using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TextHandler handler = new TextHandler("prepositions.txt", "unions.txt");
            var wordsList = handler.CutOutWords("text.txt", "[^a-zA-Zа-яА-ЯёЁ0-9-]+");

            //1. Частотный словарь
            FrequencyDictionary fd = new FrequencyDictionary();
            var dictionary = fd.GetWordsCount(wordsList);
            int wordsCount = fd.GetWordsCount(dictionary);
            //Чистим от предлогов/союзов
            dictionary = handler.ApplyExclusionMask(handler.Prepositions, dictionary);
            dictionary = handler.ApplyExclusionMask(handler.Unions, dictionary);
            handler.PrintWordsTop(dictionary, 20, "Частотный словарь");

            //2.TF_IDF
            TF_IDF ti = new TF_IDF();
            var tf_dictionary = ti.GetTF(dictionary, wordsCount);
            handler.PrintWordsTop(tf_dictionary, 20, "TF (абсолютная частота встречаемости)");
            var tfidf_dictionary = ti.GetTF_IDF(100000, tf_dictionary);
            handler.PrintWordsTop(tfidf_dictionary, 20, "TF*IDF(обратная частота документа)");
            Console.WriteLine("Done!");
        }
    }
}
