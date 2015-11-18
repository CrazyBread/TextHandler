using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    #region Дополнительные классы для мультипроцессорной обработки
    /// <summary>
    /// Данные файла
    /// </summary>
    public class FileData
    {
        public List<string> List { get; private set; }
        public string Name { get; private set; }

        public FileData(string name, List<string> list)
        {
            Name = name;
            List = list;
        }
    }

    /// <summary>
    /// Данные Mystem
    /// </summary>
    public class MystemData
    {
        public List<Lemm> List { get; set; }
        public string Name { get; private set; }
        public MystemData(string name, List<Lemm> list)
        {
            Name = name;
            List = list;
        }
    }
    #endregion

    #region Лемма
    [DataContract]
    public class Lemm
    {
        [DataMember(Name = "text")]
        public string text { get; set; } //слово в тексте
        [DataMember(Name = "analysis")]
        public Analysis[] analysis { get; set; } //анализ

        public override string ToString()
        {
            return text;
        }
    }
    [DataContract]
    public class Analysis
    {
        [DataMember(Name = "lex")]
        public string lex { get; set; }
        [DataMember(Name = "gr")]
        public string gr { get; set; }

        public string wordType
        {
            get
            {
                return GetWordType(gr);
            }
        }
        
        /// <summary>
        /// Получение части речи
        /// </summary>
        /// <param name="gr"></param>
        /// <returns></returns>
        private string GetWordType(string gr)
        {
            return gr.Split(new[] { ',', '=' }, System.StringSplitOptions.RemoveEmptyEntries)[0];
        }
    }
    #endregion

    #region Биграмма
    /// <summary>
    /// Класс, описывающий биграмму
    /// </summary>
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
            if (dObj == null)
                return base.Equals(obj);
            return dObj.FirstWord == this.FirstWord && dObj.SecondWord == SecondWord;
        }

        public override int GetHashCode()
        {
            return FirstWord.GetHashCode() ^ SecondWord.GetHashCode();
        }
    }
    #endregion

    #region Анализы
    /// <summary>
    /// Хранение данных для статистического анализа
    /// </summary>
    /// <typeparam name="T">Биграмма, слово (WordDigram, string)</typeparam>
    public class StatsAnalysisResult<T>
    {
        public string Name { get; private set; }
        public Dictionary<T, double> Frequency_Dictionary { get; set; }
        public Dictionary<T, double> TF_Dictionary { get; set; }
        public Dictionary<T, double> TF_IDF_Dictionary { get; set; }
        public Dictionary<T, double> MutualInformation_Dictionary { get; set; }
        public Dictionary<T, double> TScore_Dictionary { get; set; }
        public Dictionary<T, double> LogLikelihood_Dictionary { get; set; }

        public StatsAnalysisResult(string name = "")
        {
            Name = name;
        }
    }
    #endregion

    #region Кластерный анализ
    /// <summary>
    /// Класс, содержащий настройки проводимого кластерного анализа (для UI)
    /// </summary>
    public class ClasterSettings<T>
    {
        public double[,] ClusterCenters { get; private set; }
        public double Epsilon { get; private set; }
        public double Fuzziness { get; private set; }
        public int IterationCount { get; private set; }
        public Dictionary<T, double[]> Data { get; private set; }

        public ClasterSettings(double[,] cts, double e, double f, int ic, Dictionary<T, double[]> data)
        {
            ClusterCenters = cts;
            Epsilon = e;
            Fuzziness = f;
            IterationCount = ic;
            Data = data;
        }
    }

    /// <summary>
    /// Класс, описывающий набор параметров (Settings) для проведения кластерного анализа с указанием имени файла (Name)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClasterAnalysisData<T>
    {
        public string Name { get; private set; }
        public ClasterSettings<T> Settings { get; private set; }

        public ClasterAnalysisData(string name, ClasterSettings<T> settings)
        {
            Name = name;
            Settings = settings;
        }
    }

    /// <summary>
    /// Класс, описывающий результат выполнения кластерного анализа
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClasterAnalysisResult<T>
    {
        public string Name { get; private set; }
        public Dictionary<T, double[]> Result { get; private set; }

        public ClasterAnalysisResult(string name, Dictionary<T, double[]> result)
        {
            Name = name;
            Result = result;
        }
    }
    #endregion
}
