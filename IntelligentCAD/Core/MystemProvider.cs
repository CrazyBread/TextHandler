using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;
using HelperLib;

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
            this.outputFileName = "tmp_output_" + index + ".txt";
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
        /// Получение списка слов в канонической форме
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        private List<Lemm> GetMystemResult(StreamReader sr)
        {
            List<Lemm> list = new List<Lemm>();
            string line = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                list.Add(ParseMystemString(line));
                line = sr.ReadLine();
            }
            sr.Close();

            return list;
        }

        /// <summary>
        /// Запуск mystem.exe. Чтение из файла (удаление временного файла). Возвращение слов в канонической форме.
        /// </summary>
        /// <param name="args">строка аргументов</param>
        public List<Lemm> LaunchMystem(List<string> lines, string flags = "-n")
        {
            FileHelper.WriteFile(lines, inputFileName);

            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    Arguments = String.Format("{0} {1} {2}", flags, inputFileName, outputFileName),
                    FileName = mystemPath,
                    UseShellExecute = false,
                    CreateNoWindow=true
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
