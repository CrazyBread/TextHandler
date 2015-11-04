using System.Collections.Generic;
using System.Threading;
using System.IO;
using FileLib;
using HelperLib;
using System;

namespace Core
{
    public class Multiprocessor
    {
        private Thread[] threads; //потоки
        private List<FileData> multiFileCache; //кэш с данными
        private List<MystemData> multiMystemCache; //кэш с данными mystem

        public List<FileData> FileCache
        {
            get { return multiFileCache; }
            private set { multiFileCache = value; }
        }

        public List<MystemData> MystemCache
        {
            get { return multiMystemCache; }
            private set { multiMystemCache = value; }
        }

        public Multiprocessor()
        {
            multiFileCache = new List<FileData>();
            multiMystemCache = new List<MystemData>();
        }

        private void _cleanCache<T>(List<T> cache)
        {
            cache.Clear();
        }

        #region functions for parallel computing
        private void _readFile(string path)
        {
            FileHandler fh = FileHelper.GetFileHandler(path);
            if (fh == null)
                return;

            List<string> lines;
            fh.ReadFile(out lines);
            multiFileCache.Add(new FileData(path, lines));
        }

        private void _runMystem(FileData data, string index)
        {
            MystemProvider mp = new MystemProvider(index);
            List<Lemm> list = mp.LaunchMystem(data.List);
            multiMystemCache.Add(new MystemData(data.Name, list));
        }
        #endregion

        public void MultiprocessorMystemHandler(List<FileData> list)
        {
            _cleanCache(multiMystemCache);
            if (list.Count > 0)
            {
                threads = new Thread[list.Count];
                for (int i = 0; i < threads.Length; i++)
                {
                    FileData data = list[i];
                    threads[i] = new Thread(() => _runMystem(data, Guid.NewGuid().ToString()));
                    threads[i].Start();
                }
                foreach (Thread th in threads)
                {
                    th.Join();
                }
                threads = null;
            }
        }

        public void MultiprocessorFileRead(List<string> paths)
        {
            _cleanCache(multiFileCache);
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
                foreach (Thread th in threads)
                {
                    th.Join();
                }
                threads = null;
            }
        }
    }
}
