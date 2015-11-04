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
        public List<Lemm> List { get; private set; }
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
}
