using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TextHandler handler = new TextHandler();
            string text = handler.ConvertTextToString("text.txt");
            var words = handler.GetWords(text, "[^а-яА-Я]+");
            var dictionary = handler.GetWordsCount(words);
            Console.WriteLine("Done!");
        }
    }
}
