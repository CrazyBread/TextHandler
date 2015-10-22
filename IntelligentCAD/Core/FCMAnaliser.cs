using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class FCMAnaliser
    {
        public const double Default_m = 1.6;
        public const double Default_e = 0.001;
        public const int Defaul_it_count = 1000;

        public static void CalucateFCMForWords(WordVector[] words, int clustersCount, double m = Default_m, double e = Default_e, int iterationCount = Defaul_it_count)
        {
            var rand = new Random();
            var u = new double[words.Length, clustersCount];
            var c = new WordVector[clustersCount];
            for (var i = 0; i < words.Length; i++)
                for (var j = 0; j < clustersCount; j++)
                    u[i, j] = rand.NextDouble();

            for (var k = 0; k < clustersCount; k++)
            {
                WordVector newCluster = new WordVector("null", 0, 0);
                double uSum = 0;
                for (var i = 0; i < words.Length; i++)
                {
                    var uik = Math.Pow(u[i, k], e);
                    newCluster += (uik * words[i]);
                    uSum += uik;
                }
                c[k] = newCluster / uSum;
            }


        }
    }
}
