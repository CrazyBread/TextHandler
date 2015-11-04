using FileLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace HelperLib
{
    public static class FileHelper
    {
        /// <summary>
        /// Выборка только тех файлов, которые подходят под предписанные расширения
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static List<string> CheckFiles(List<string> paths)
        {
            List<string> exts = new List<string>() { ".pdf", ".txt", ".doc", ".docx" }; //можно в конфиг засунуть
            return paths.Where(i => exts.Contains(Path.GetExtension(i)) && File.Exists(i)).ToList();
        }

        public static void WriteFile(List<string> lines, string path)
        {
            using (StreamWriter wr = new StreamWriter(path, false, Encoding.UTF8))
            {
                foreach (var line in lines)
                {
                    wr.WriteLine(line);
                }
                wr.Close();
            }
        }

        public static bool DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }

        public static FileHandler GetFileHandler(string path)
        {
            if (!File.Exists(path))
                return null;

            FileHandler fh = null;
            switch (Path.GetExtension(path))
            {
                case ".txt": fh = new TxtHandler(path); break;
                case ".pdf": fh = new PdfHandler(path); break;
                default: break; //неизвестный формат файла
            }

            if (fh == null)
                return null;

            return fh;
        }
    }
}
