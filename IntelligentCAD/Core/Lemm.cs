using System.Runtime.Serialization;

namespace Core
{
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
    }
}
