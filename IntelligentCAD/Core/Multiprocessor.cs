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

        public Multiprocessor()
        {
            multiFileCache = new List<FileData>();
            multiMystemCache = new List<MystemData>();
            WordsStatsAnalysisCache = new List<StatsAnalysisResult<string>>();
            DigramsStatsAnalysisCache = new List<StatsAnalysisResult<WordDigram>>();
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

            analysisResult.Frequency_Dictionary = StatisticsAnalysis.GetDigramFrequenceDictionary(words);
            analysisResult.MutualInformation_Dictionary = StatisticsAnalysis.CalculateMutualInformation(analysisResult.Frequency_Dictionary, wordsFrequency, words.Count);
            analysisResult.TScore_Dictionary = StatisticsAnalysis.CalculateTScore(analysisResult.Frequency_Dictionary, wordsFrequency, words.Count);
            analysisResult.LogLikelihood_Dictionary = StatisticsAnalysis.CalculateLogLikelihood(analysisResult.Frequency_Dictionary);

            multiDigramsStatsAnalysisCache.Add(analysisResult);
        }
        #endregion

        public void MultiprocesingWordStatsAnalysis(List<MystemData> list)
        {
            _cleanCache(multiWordsStatsAnalysisCache);
            if (list.Count > 0)
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

        public void MultiprocesingDigramsStatsAnalysis(List<MystemData> list)
        {
            _cleanCache(multiWordsStatsAnalysisCache);
            if (list.Count > 0)
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
