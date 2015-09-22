using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace ConsoleApp
{
    public class TextHandler
    {
        public TextHandler() { }

        public List<string> GetWords(string text, string pattern) 
        {
            return Regex.Split(text, pattern)
                .Where(i => !string.IsNullOrEmpty(i))
                .Select(i => i.ToLower())
                .ToList();
        }
        
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

        //Временно
        //http://habrahabr.ru/post/147927/
        public string ConvertTextToString(string filePath) 
        {
            Stream stream = new FileStream(filePath, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            StringBuilder text = new StringBuilder();
            while (!reader.EndOfStream) 
            {
                text.AppendLine(reader.ReadLine());
            }
            return text.ToString();
        }
    }
}
