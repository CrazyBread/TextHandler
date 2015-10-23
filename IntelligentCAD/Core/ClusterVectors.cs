using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class WordVector
    {
        public string WordValue { get; set; }
        public double Frequency { get; private set; }
        public double TF_IDF { get; private set; }

        public WordVector(string wordValue,double frequency, double tf_idf)
        {
            WordValue = wordValue;
            Frequency = frequency;
            TF_IDF = tf_idf;
        }

        public static WordVector operator + (WordVector first, WordVector second)
        {
            return new WordVector("sum", first.Frequency + second.Frequency, first.TF_IDF + second.TF_IDF);
        }

        public static WordVector operator * (WordVector first, double val)
        {
            return new WordVector(first.WordValue, first.Frequency * val, first.TF_IDF * val);
        }

        public static WordVector operator * (double val, WordVector first)
        {
            return first * val;
        }

        public static WordVector operator / (WordVector first, double val)
        {
            return new WordVector(first.WordValue, first.Frequency / val, first.TF_IDF / val);
        }

        public static WordVector operator / (double val, WordVector first)
        {
            return first / val;
        }

        public static double Distance(WordVector first, WordVector second)
        {
            return Math.Pow(first.Frequency - second.Frequency, 2) + Math.Pow(first.TF_IDF - second.TF_IDF, 2);
        }

        public override string ToString()
        {
            return string.Format("{0} ({1},{2})", WordValue, Frequency, TF_IDF);
        }
    }

    public class DigramVector
    {
        public WordDigram DigramValue { get; private set; }
        public double Frequency { get; private set; }
        public double Mutualinformation { get; private set; }
        public double TScore { get; private set; }
        public double LogLikelihood { get; private set; }

        public DigramVector(WordDigram digramValue,double frequency,
            double mutualInformation,double tScore,double logLikelihood)
        {
            DigramValue = digramValue;
            Frequency = frequency;
            Mutualinformation = mutualInformation;
            TScore = tScore;
            LogLikelihood = logLikelihood;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1},{2},{3},{4})", DigramValue, Frequency, Mutualinformation,TScore,LogLikelihood);
        }

    }
}
