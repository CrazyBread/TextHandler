using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Configuration
    {
        public static int CCSize = 100000;
        public static class WordType
        {
            //Типы частей речи
            /// <summary>
            /// Прилагательное
            /// </summary>
            public const string adjective = "A";
            /// <summary>
            /// Наречие
            /// </summary>
            public const string adverb = "ADV";
            /// <summary>
            /// Наречие-местоимение
            /// </summary>
            public const string adverb_pronoun = "ADVPRO";
            /// <summary>
            /// Прилагательное-числительное
            /// </summary>
            public const string adjective_numeral = "ANUM";
            /// <summary>
            /// Прилагательное-местоимение
            /// </summary>
            public const string adjective_pronoun = "APRO";
            /// <summary>
            /// Сложное слово
            /// </summary>
            public const string composite = "COM";
            /// <summary>
            /// Союз
            /// </summary>
            public const string conjunction = "CONJ";
            /// <summary>
            /// Междометие
            /// </summary>
            public const string interjection = "INTJ";
            /// <summary>
            /// ЧИслительное
            /// </summary>
            public const string numeral = "NUM";
            /// <summary>
            /// Частица
            /// </summary>
            public const string particle = "PART";
            /// <summary>
            /// Предлог
            /// </summary>
            public const string preposition = "PR";
            /// <summary>
            /// Существительное
            /// </summary>
            public const string noun = "S";
            /// <summary>
            /// Существительное-местоимение
            /// </summary>
            public const string noun_pronoun = "SPRO";
            /// <summary>
            /// Глагол
            /// </summary>
            public static string verb = "V";
        }
    }
}
