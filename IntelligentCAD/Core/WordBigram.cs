using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class WordBigram
    {
        private const char separator = ' ';
        public string FirstWord { get; private set; }
        public string SecondWord { get; private set; }

        public WordBigram(string firstWord, string secondWord)
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
            var dObj = obj as WordBigram;
            if (dObj == null)
                return base.Equals(obj);
            return dObj.FirstWord == this.FirstWord && dObj.SecondWord == SecondWord;
        }

        public override int GetHashCode()
        {
            return FirstWord.GetHashCode() ^ SecondWord.GetHashCode();
        }
    }
}
