using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Lemm
    {
        public string Value { get; private set; }
        public string[] BeginForm { get; private set; }

        public Lemm(string value, string[] beginForm)
        {
            Value = value;
            BeginForm = beginForm;
        }
    };

    public class MorphologyAnalises
    {
        private const string inputFile = @"mystem\input.txt";
        private const string outputFile = @"mystem\output.txt";
        private const string mystem = @"mystem\mystem.exe";

        public static List<Lemm> Analize(string str)
        {
            using (var input = new StreamWriter(inputFile, false, Encoding.UTF8))
                input.Write(str);

            Process.Start(new ProcessStartInfo
            {
                Arguments = string.Format("{0} {1}", inputFile, outputFile),
                UseShellExecute = false,
                FileName = mystem
            }).WaitForExit();

            string res = "";
            using (var output = new StreamReader(outputFile, Encoding.UTF8))
            {
                res = output.ReadToEnd();
            }
            File.Delete(inputFile);
            File.Delete(outputFile);

            return ParseMystemString(res);
        }

        private static List<Lemm> ParseMystemString(string str)
        {
            var result = new List<Lemm>();
            var splitStrings = str.Trim().Split(new char[] { '}' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var splitString in splitStrings)
            {
                var pair = splitString.Split('{');
                result.Add(new Lemm(pair[0], pair[1].Split('|')));
            }
            return result;
        }
    }
}
