using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class StatisticsMetrics
    {
        public static Dictionary<WordDigram, double> CalculateMutualInformation(this Dictionary<WordDigram, int> digramFrequece,
            Dictionary<string, int> freqDictionary, int wordsCount)
        {
            var result = new Dictionary<WordDigram, double>();
            foreach (var pair in digramFrequece)
            {
                var key = pair.Key;
                //частота первого слова
                var fx = freqDictionary[key.FirstWord];
                //частота второго слова
                var fy = freqDictionary[key.SecondWord];
                result.Add(key, Math.Log(
                    (pair.Value * wordsCount) / fx * fy
                    , 2));
            }
            return result;
        }

        public static Dictionary<WordDigram, double> CalculateLogLikelihood(this Dictionary<WordDigram, int> digramFrequece)
        {
            var result = new Dictionary<WordDigram, double>();
            foreach (var pair in digramFrequece)
            {
                var a = pair.Value;
                var b = digramFrequece
                    .Where(el => el.Key.FirstWord == pair.Key.FirstWord && el.Key.SecondWord != pair.Key.SecondWord)
                    .Sum(el => el.Value);
                var c = digramFrequece
                    .Where(el => el.Key.FirstWord != pair.Key.FirstWord && el.Key.SecondWord == pair.Key.SecondWord)
                    .Sum(el => el.Value);
                var d = digramFrequece
                    .Where(el => el.Key.FirstWord != pair.Key.FirstWord && el.Key.SecondWord != pair.Key.SecondWord)
                    .Sum(el => el.Value);

                var value = a * Math.Log(a + 1)
                    + b * Math.Log(b + 1) 
                    + c * Math.Log(c + 1) 
                    + d * Math.Log(d + 1) 
                    - (a + b) * Math.Log(a + b + 1) 
                    - (a + c) * Math.Log(a + c + 1) 
                    - (b + d) * Math.Log(b + d + 1) 
                    - (c + d) * Math.Log(c + d + 1) 
                    + (a + b + c + d) * Math.Log(a + b + c + d + 1);
                result.Add(pair.Key, value);
            }
            return result;
        }

        public static Dictionary<WordDigram, double> CalculateTSorce(this Dictionary<WordDigram, int> digramFrequece,
            Dictionary<string, int> freqDictionary, int wordsCount)
        {
            var result = new Dictionary<WordDigram, double>();
            foreach (var pair in digramFrequece)
            {
                var key = pair.Key;
                //частота первого слова
                var fx = freqDictionary[key.FirstWord];
                //частота второго слова
                var fy = freqDictionary[key.SecondWord];
                result.Add(key,
                    (pair.Value - (fx * fy / wordsCount)) / (pair.Value * pair.Value)
                    );
            }
            return result;
        }

        public static Dictionary<string, double> GetTF_IDF(int ccSize, Dictionary<string, double> tf_dictionary, int docNumber = 1)
        {
            return tf_dictionary.ToDictionary(i => i.Key, i => i.Value * Math.Log((double)(ccSize - docNumber) / docNumber));
        }

        public static Dictionary<string, double> GetTF(Dictionary<string, int> words, int count)
        {
            return words.ToDictionary(i => i.Key, i => ((double)i.Value / count));
        }
    }
}
