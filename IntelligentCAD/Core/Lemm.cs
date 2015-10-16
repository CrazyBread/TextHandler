using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Описание слова и его начальных форм
    /// </summary>
    //public class Lemm
    //{
    //    public string Value { get; private set; }
    //    public string[] InitialForms { get; private set; }

    //    public Lemm(string value, string[] beginForm)
    //    {
    //        Value = value;
    //        InitialForms = beginForm;
    //    }
    //}

    [DataContract]
    public class Lemm2
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
    }
}
