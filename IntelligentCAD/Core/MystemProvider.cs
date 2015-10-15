using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class MystemProvider
    {
        private string path;

        public MystemProvider(string path = @"mystem\mystem.exe")
        {
            this.path = path;
        }

        /// <summary>
        /// Получение списка слов в канонической форме
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        private List<string> GetMystemResult(StreamReader sr)
        {
            List<string> list = new List<string>();
            string line = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                list.Add(line);
                line = sr.ReadLine();
            }

            return list;
        }

        /// <summary>
        /// Запуск mystem.exe. Чтение из файла (удаление временного файла). Возвращение слов в канонической форме.
        /// </summary>
        /// <param name="args">строка аргументов</param>
        public List<string> LaunchMystem(string inputFile, string flags = "-n")
        {
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    Arguments = String.Format("{0} {1} -", flags, inputFile),
                    FileName = path,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    StandardOutputEncoding = Encoding.UTF8
                }
            };
            process.Start();
            StreamReader outStream = process.StandardOutput;
            process.WaitForExit();

            return GetMystemResult(outStream);
        }


    }
}
