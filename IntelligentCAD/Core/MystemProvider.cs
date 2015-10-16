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
                byte[] bts = Encoding.UTF8.GetBytes(line);
                //list.Add(line);
                list.Add(Encoding.UTF8.GetString(bts));
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
                    Arguments = "-n -e cp866 - -",//String.Format("input.txt -", flags),
                    FileName = path,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    StandardOutputEncoding = Encoding.GetEncoding(866),
                }
            };
            process.Start();
            //byte[] bytes = Encoding.Default.GetBytes("Привет меня зовут Алексей");
            //string myString = Encoding.Unicode.GetString(bytes);
            string myString = "Это тестовая строка для mystem";
            process.StandardInput.WriteLine(myString);
            StreamReader outStream = process.StandardOutput;
            StreamReader error = process.StandardError;
            process.StandardInput.Close();
            //StreamWriter inStream = process.StandardInput;

            /*string str = "";
            byte[] byteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream(byteArray);
            inStream = new StreamWriter(ms);*/


            process.WaitForExit();

            return GetMystemResult(outStream);
        }


    }
}
