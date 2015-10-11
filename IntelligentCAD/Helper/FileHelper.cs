using System.Collections.Generic;
using System.IO;
using System.Linq;

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
    }
}
