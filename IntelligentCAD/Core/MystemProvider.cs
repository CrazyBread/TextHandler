using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;
using HelperLib;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace Core
{
    /// <summary>
    /// Класс, обеспечивающий запуск mystem.exe
    /// </summary>
    public class MystemProvider
    {
        private string mystemPath;
        private string index;
        private string inputFileName;
        private string outputFileName;

        public MystemProvider(string index, string mystemPath = @"mystem\mystem.exe")
        {
            this.mystemPath = mystemPath;
            this.index = index;
            inputFileName = "tmp_input_" + index + ".txt";
            outputFileName = "tmp_output_" + index + ".json";
        }

        /// <summary>
        /// Парсинг json-файла результата выполнения mystem
        /// </summary>
        /// <param name="srdr"></param>
        /// <returns></returns>
        private List<Lemm> GetMystemResult(StreamReader srdr) 
        {
            List<Lemm> lemms = new List<Lemm>();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Lemm));

            string line = srdr.ReadLine();
            while (!srdr.EndOfStream)
            {
                Lemm obj = (Lemm)ser.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(line)));
                if (obj.analysis != null)
                    lemms.Add(obj);
                line = srdr.ReadLine();
            }
            srdr.Close();

            return lemms;
        }

        /// <summary>
        /// Запуск mystem.exe
        /// </summary>
        public List<Lemm> LaunchMystem(List<string> lines, string flags = "-cgin --format json")
        {
            Console.WriteLine(inputFileName);
            FileHelper.WriteFile(lines, inputFileName);

            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    Arguments = String.Format("{0} {1} {2}", flags, inputFileName, outputFileName),
                    FileName = mystemPath,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.WaitForExit();

            List<Lemm> lemms = GetMystemResult(new StreamReader(outputFileName));

            FileHelper.DeleteFile(inputFileName);
            FileHelper.DeleteFile(outputFileName);

            return lemms;
        }
    }
}
