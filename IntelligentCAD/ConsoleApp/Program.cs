using MultiprocessingLib;
using System.Collections.Generic;
using Core;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Multiprocessor mps = new Multiprocessor();
            mps.MultiprocessorFileRead(new List<string>() { "mainText.txt" });
            var texts = mps.Cache;

            MystemProvider mst = new MystemProvider(1);
            var list = mst.LaunchMystem(texts[0].List);
        }
    }
}
