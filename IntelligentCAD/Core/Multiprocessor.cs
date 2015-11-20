using System.Collections.Generic;
using System.Threading;
using System.IO;
using FileLib;
using HelperLib;
using System;
using CoreLib;

namespace Core
{
    public class Multiprocessor
    {
        private Thread[] threads; //потоки
        private List<FileData> multiFileCache; //кэш с данными
        private List<MystemData> multiMystemCache; //кэш с данными mystem
        private List<StatsAnalysisResult<string>> multiWordsStatsAnalysisCache; //кэш для хранения результата по статистическому анализу для слов
        private List<StatsAnalysisResult<WordDigram>> multiDigramsStatsAnalysisCache; //кэш для хранения результата по статистическому анализу для биграмм
        private List<ClasterAnalysisResult<string>> multiWordsClusterAnalysisCache; //кэш для хранения результата кластерного анализа слов
        private List<ClasterAnalysisResult<WordDigram>> multiDigramsClusterAnalysisCache; //кэш для хранения результата кластерного анализа биграмм
        private List<MystemData> multiMorphAnalysisCache; //кэш для хранения результата морфологического анализа лемм

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
        public List<StatsAnalysisResult<string>> WordsStatsAnalysisCache
        {
            get { return multiWordsStatsAnalysisCache; }
            private set { multiWordsStatsAnalysisCache = value; }
        }
        public List<StatsAnalysisResult<WordDigram>> DigramsStatsAnalysisCache
        {
            get { return multiDigramsStatsAnalysisCache; }
            private set { multiDigramsStatsAnalysisCache = value; }
        }

        public List<ClasterAnalysisResult<string>> MultiWordsClusterAnalysisCache
        {
            get { return multiWordsClusterAnalysisCache; }
            private set { multiWordsClusterAnalysisCache = value; }
        }

        public List<ClasterAnalysisResult<WordDigram>> MultiWordDigramsClusterAnalysisCache
        {
            get { return multiDigramsClusterAnalysisCache; }
            private set { multiDigramsClusterAnalysisCache = value; }
        }

        public List<MystemData> MultiMorphAnalysisCache
        {
            get { return multiMorphAnalysisCache; }
            private set { multiMorphAnalysisCache = value; }
        }

        public Multiprocessor()
        {
            multiFileCache = new List<FileData>();
            multiMystemCache = new List<MystemData>();
            multiWordsStatsAnalysisCache = new List<StatsAnalysisResult<string>>();
            multiDigramsStatsAnalysisCache = new List<StatsAnalysisResult<WordDigram>>();
            multiWordsClusterAnalysisCache = new List<ClasterAnalysisResult<string>>();
            multiDigramsClusterAnalysisCache = new List<ClasterAnalysisResult<WordDigram>>();
            multiMorphAnalysisCache = new List<MystemData>();
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

        private void _provideWordsStatsAnalysis(MystemData data)
        {
            List<string> words = data.List.GetWords();
            StatsAnalysisResult<string> analysisResult = new StatsAnalysisResult<string>();

            analysisResult.Frequency_Dictionary = StatisticsAnalysis.GetFrequencyDictionary(words);
            analysisResult.TF_Dictionary = StatisticsAnalysis.GetTF(analysisResult.Frequency_Dictionary, words.Count);
            analysisResult.TF_IDF_Dictionary = StatisticsAnalysis.GetTF_IDF(Configuration.CCSize, analysisResult.TF_Dictionary);

            multiWordsStatsAnalysisCache.Add(analysisResult);
        }
        private void _provideDigramsStatsAnalysis(MystemData data)
        {
            List<string> words = data.List.GetWords();
            StatsAnalysisResult<WordDigram> analysisResult = new StatsAnalysisResult<WordDigram>();
            var wordsFrequency = StatisticsAnalysis.GetFrequencyDictionary(words);

            analysisResult.Name = data.Name;
            analysisResult.Frequency_Dictionary = StatisticsAnalysis.GetDigramFrequenceDictionary(words);
            analysisResult.MutualInformation_Dictionary = StatisticsAnalysis.CalculateMutualInformation(analysisResult.Frequency_Dictionary, wordsFrequency, words.Count);
            analysisResult.TScore_Dictionary = StatisticsAnalysis.CalculateTScore(analysisResult.Frequency_Dictionary, wordsFrequency, words.Count);
            analysisResult.LogLikelihood_Dictionary = StatisticsAnalysis.CalculateLogLikelihood(analysisResult.Frequency_Dictionary);

            multiDigramsStatsAnalysisCache.Add(analysisResult);
        }

        private void _provideWordClusterAnalysis(ClasterAnalysisData<string> data)
        {
            ClasterAnalysis<string> ca = new ClasterAnalysis<string>(data.Settings);
            multiWordsClusterAnalysisCache.Add(new ClasterAnalysisResult<string>(data.Name, ca.Clasterize()));
        }

        private void _provideDigramClusterAnalysis(ClasterAnalysisData<WordDigram> data)
        {
            ClasterAnalysis<WordDigram> ca = new ClasterAnalysis<WordDigram>(data.Settings);
            multiDigramsClusterAnalysisCache.Add(new ClasterAnalysisResult<WordDigram>(data.Name, ca.Clasterize()));
        }

        private void _provideMorphAnalysis(MystemData data, string[] excludedTypes)
        {
            var result = MorphologicalAnalysis.ExcludeWordsByType(data, excludedTypes);
            multiMorphAnalysisCache.Add(result);
        }
        #endregion

        public void MultiprocessorMorphAnalysis(List<MystemData> list, string[] excludedTypes)
        {
            _cleanCache(multiMorphAnalysisCache);
            if (list != null && list.Count > 0)
            {
                threads = new Thread[list.Count];
                for (int i = 0; i < threads.Length; i++)
                {
                    MystemData data = list[i];
                    threads[i] = new Thread(() => _provideMorphAnalysis(data, excludedTypes));
                    threads[i].Start();
                }
                foreach (Thread th in threads)
                {
                    th.Join();
                }
                threads = null;
            }
        }

        public void MultiprocessorWordDigramClasterAnalysis(List<ClasterAnalysisData<WordDigram>> list)
        {
            _cleanCache(multiWordsClusterAnalysisCache);
            if (list != null && list.Count > 0)
            {
                threads = new Thread[list.Count];
                for (int i = 0; i < threads.Length; i++)
                {
                    ClasterAnalysisData<WordDigram> data = list[i];
                    threads[i] = new Thread(() => _provideDigramClusterAnalysis(data));
                    threads[i].Start();
                }
                foreach (Thread th in threads)
                {
                    th.Join();
                }
                threads = null;
            }
        }

        public void MultiprocessorWordClasterAnalysis(List<ClasterAnalysisData<string>> list)
        {
            _cleanCache(multiWordsClusterAnalysisCache);
            if (list != null && list.Count > 0)
            {
                threads = new Thread[list.Count];
                for (int i = 0; i < threads.Length; i++)
                {
                    ClasterAnalysisData<string> data = list[i];
                    threads[i] = new Thread(() => _provideWordClusterAnalysis(data));
                    threads[i].Start();
                }
                foreach (Thread th in threads)
                {
                    th.Join();
                }
                threads = null;
            }
        }

        public void MultiprocessorWordStatsAnalysis(List<MystemData> list)
        {
            _cleanCache(multiWordsStatsAnalysisCache);
            if (list != null && list.Count > 0)
            {
                threads = new Thread[list.Count];
                for (int i = 0; i < threads.Length; i++)
                {
                    MystemData data = list[i];
                    threads[i] = new Thread(() => _provideWordsStatsAnalysis(data));
                    threads[i].Start();
                }
                foreach (Thread th in threads)
                {
                    th.Join();
                }
                threads = null;
            }
        }

        public void MultiprocessorDigramsStatsAnalysis(List<MystemData> list)
        {
            _cleanCache(multiWordsStatsAnalysisCache);
            if (list != null && list.Count > 0)
            {
                threads = new Thread[list.Count];
                for (int i = 0; i < threads.Length; i++)
                {
                    MystemData data = list[i];
                    threads[i] = new Thread(() => _provideDigramsStatsAnalysis(data));
                    threads[i].Start();
                }
                foreach (Thread th in threads)
                {
                    th.Join();
                }
                threads = null;
            }
        }

        public void MultiprocessorMystemHandler(List<FileData> list)
        {
            _cleanCache(multiMystemCache);
            if (list != null && list.Count > 0)
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
            if (paths != null && paths.Count > 0)
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
