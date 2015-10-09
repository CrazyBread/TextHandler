using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class StatisticsMetrics
    {

        public static Dictionary<string, double> CalculateMutualInformation(List<string> textWords, Dictionary<string, int> freqDictionary)
        {
            var result = new Dictionary<string, double>();
            List<string> digram;
            for (var i = 0; i > textWords.Count-1;i++ )
            {
                digram = textWords.Skip(0).Take(2).ToList();
                //Защита от дурака, вдруг кто пришлет какашню
                if(!freqDictionary.ContainsKey(digram[0])||!freqDictionary.ContainsKey(digram[1]))
                    continue;
                //Возможно хардкодно, но по мне это всегда будет работать, т.к. метод применяется только к диграммам
                var digramKey = string.Concat(digram[0], "-", digram[1]);
                if (result.ContainsKey(digramKey))
                    continue;
                //частота биграммы
                var fxy=FrequencyDictionary.NgramFrequence(textWords, digram);
                //частота первого слова
                var fx = freqDictionary[digram[0]];
                //частота второго слова
                var fy = freqDictionary[digram[1]];

                result.Add(digramKey, Math.Log(
                    (fxy*textWords.Count)/fx*fy
                    ,2));
            }
            return result;
        }

#warning Слишком много повторений с MutualInformation, вынести в отдельный метод общее
        public static Dictionary<string, double> CalculateTSorce(List<string> textWords, Dictionary<string, int> freqDictionary)
        {
            var result = new Dictionary<string, double>();
            List<string> digram;
            for (var i = 0; i > textWords.Count - 1; i++)
            {
                digram = textWords.Skip(0).Take(2).ToList();
                //Защита от дурака, вдруг кто пришлет какашню
                if (!freqDictionary.ContainsKey(digram[0]) || !freqDictionary.ContainsKey(digram[1]))
                    continue;
                //Возможно хардкодно, но по мне это всегда будет работать, т.к. метод применяется только к диграммам
                var digramKey = string.Concat(digram[0], "-", digram[1]);
                if (result.ContainsKey(digramKey))
                    continue;
                //частота биграммы
                var fxy = FrequencyDictionary.NgramFrequence(textWords, digram);
                //частота первого слова
                var fx = freqDictionary[digram[0]];
                //частота второго слова
                var fy = freqDictionary[digram[1]];

                result.Add(digramKey, 
                    (fxy-(fx*fy/textWords.Count))/(fxy*fxy)
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
