using System.Collections.Generic;

namespace FileLib
{
    /// <summary>
    /// Абс. класс для обработчика файлов
    /// </summary>
    public abstract class FileHandler
    {
        public string path { get; set; }

        public FileHandler(string path)
        {
            this.path = path;
        }
        public abstract void ReadFile(out List<string> lines);
    }
}
