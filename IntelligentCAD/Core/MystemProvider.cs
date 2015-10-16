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
        private int index;
        private string inputFileName;
        private string outputFileName;

        public MystemProvider(int index, string mystemPath = @"mystem\mystem.exe")
        {
            this.mystemPath = mystemPath;
            this.index = index;
            this.inputFileName = "tmp_input_" + index + ".txt";
            this.outputFileName = "tmp_output_" + index + ".json";
        }

        /// <summary>
        /// Парсер для строки (слово + начальные формы) mystem
        /// </summary>
        /// <param name="str">mystem-строка</param>
        /// <returns></returns>
        private Lemm ParseMystemString(string str)
        {
            var splitStrings = str.Split(new char[] { '{' }, StringSplitOptions.RemoveEmptyEntries);
            string word = splitStrings[0];
            string[] initialForms = (splitStrings[1].Trim().Split(new char[] { '}' }, StringSplitOptions.RemoveEmptyEntries)[0]).Split('|');

            return new Lemm(word, initialForms);
        }

        /// <summary>
        /// Парсинг json-файла результата выполнения mystem
        /// </summary>
        /// <param name="srdr"></param>
        /// <returns></returns>
        private List<Lemm2> GetMystemResult(StreamReader srdr) 
        {
            List<Lemm2> lemms = new List<Lemm2>();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Lemm2));

            string line = srdr.ReadLine();
            while (!srdr.EndOfStream)
            {
                Lemm2 obj = (Lemm2)ser.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(line)));
                if (obj.analysis != null)
                    lemms.Add(obj);
                line = srdr.ReadLine();
            }
            srdr.Close();

            return lemms;
        }

        /// <summary>
        /// Запуск mystem.exe. Чтение из файла (удаление временного файла). Возвращение слов в канонической форме.
        /// </summary>
        /// <param name="args">строка аргументов</param>
        public List<Lemm2> LaunchMystem(List<string> lines, string flags = "-cgin --format json")
        {
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

            List<Lemm2> lemms = GetMystemResult(new StreamReader(outputFileName));

            FileHelper.DeleteFile(inputFileName);
            FileHelper.DeleteFile(outputFileName);

            return lemms;
        }
    }
}
