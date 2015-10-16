using System.Collections.Generic;
using System.Threading;
using System.IO;
using FileLib;
using HelperLib;

namespace MultiprocessingLib
{
    /// <summary>
    /// Данные файла
    /// </summary>
    public class FileData
    {
        public List<string> List { get; private set; }
        public string Name { get; private set; }

        public FileData(string name, List<string> list)
        {
            this.Name = name;
            this.List = list;
        }
    }

    /// <summary>
    /// Класс для реализации параллельной обработки
    /// </summary>
    public class Multiprocessor
    {
        private Thread[] threads;
        private List<FileData> multiCache;

        public List<FileData> Cache
        {
            get { return multiCache; }
            private set { multiCache = value; }
        }

        public Multiprocessor()
        {
            multiCache = new List<FileData>();
        }

        private void _cleanCache()
        {
            multiCache.Clear();
        }

        #region functions for parallel computing
        private void _readFile(string path)
        {
            FileHandler fh = null;
            List<string> lines;
            switch (Path.GetExtension(path))
            {
                case ".txt": fh = new TxtHandler(path); break;
                case ".pdf": fh = new PdfHandler(path); break;
                default: break; //неизв. формат файла
            }
            if (fh == null)
                return;

            fh.ReadFile(out lines);
            multiCache.Add(new FileData(path, lines));
        }
        #endregion

        public void MultiprocessorFileRead(List<string> paths)
        {
            _cleanCache();
            paths = FileHelper.CheckFiles(paths);
            if (paths.Count > 0)
            {
                threads = new Thread[paths.Count];
                for (int i = 0; i < threads.Length; i++)
                {
                    string path = paths[i];
                    threads[i] = new Thread(new ThreadStart(() => _readFile(path)));
                    threads[i].Start();
                }
                foreach (var th in threads)
                {
                    th.Join();
                }
            }
            threads = null;
        }
    }
}
