using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileLib;
using HelperLib;
using CoreLib;

namespace Core
{
    /// <summary>
    /// API для подключения в приложение
    /// </summary>
    public class API
    {
        #region Работа с файлами (1-уровень)
        /// <summary>
        /// Загрузка одного файла
        /// </summary>
        /// <param name="path">Путь до файла</param>
        /// <returns></returns>
        public List<string> LoadFile(string path)
        {
            FileHandler fh = FileHelper.GetFileHandler(path);
            if (fh == null)
                return null;

            List<string> lines;
            fh.ReadFile(out lines);

            return lines;
        }
        /// <summary>
        /// Загрузка нескольких файлов (мультипроцессорная)
        /// </summary>
        /// <param name="paths">Пути до файлов</param>
        public List<FileData> LoadFilesMulticore(List<string> paths)
        {
            Multiprocessor mps = new Multiprocessor();
            mps.MultiprocessorFileRead(paths);
            return mps.FileCache;
        }
        #endregion

        #region Mystem-api (2-уровень)
        /// <summary>
        /// Обработка строк файла программой mystem
        /// </summary>
        /// <param name="fileLines"></param>
        /// <returns></returns>
        public List<Lemm> HandleByMystem(List<string> fileLines)
        {
            MystemProvider mst = new MystemProvider(Guid.NewGuid().ToString());
            return mst.LaunchMystem(fileLines);
        }

        /// <summary>
        /// Обработка конкретного файла с помощью Mystem
        /// </summary>
        /// <param name="filePath">Путь до файла</param>
        /// <returns></returns>
        public MystemData HandleByMystem(string filePath)
        {
            var fileLines = LoadFile(filePath);
            MystemProvider mst = new MystemProvider(Guid.NewGuid().ToString());
            return new MystemData(filePath, mst.LaunchMystem(fileLines));
        }

        /// <summary>
        /// Мультипроцуессорная обработка файлов программой mystem 
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public List<MystemData> HandleByMystemMulticore(List<FileData> dataList)
        {
            Multiprocessor mps = new Multiprocessor();
            mps.MultiprocessorMystemHandler(dataList);
            return mps.MystemCache;
        }

        /// <summary>
        /// Обработка нескольких файлов с помощью Mystem
        /// </summary>
        /// <param name="filesPaths"></param>
        /// <returns></returns>
        public List<MystemData> HandleByMystemMulticore(List<string> filesPaths)
        {
            Multiprocessor mps = new Multiprocessor();
            mps.MultiprocessorFileRead(filesPaths);
            mps.MultiprocessorMystemHandler(mps.FileCache);
            return mps.MystemCache;
        }

        #endregion

        #region Статистический анализ (3-уровень)
        /// <summary>
        /// Статистический анализ файла для отдельных слов
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public StatsAnalysisResult<string> ProvideWordsStatsAnalysis(List<Lemm> list)
        {
            List<string> words = list.GetWords();
            StatsAnalysisResult<string> analysisResult = new StatsAnalysisResult<string>();

            analysisResult.Frequency_Dictionary = StatisticsAnalysis.GetFrequencyDictionary(words);
            analysisResult.TF_Dictionary = StatisticsAnalysis.GetTF(analysisResult.Frequency_Dictionary, words.Count);
            analysisResult.TF_IDF_Dictionary = StatisticsAnalysis.GetTF_IDF(Configuration.CCSize, analysisResult.TF_Dictionary);

            return analysisResult;
        }
        /// <summary>
        /// Статистический анализ для биграмм
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public StatsAnalysisResult<WordDigram> ProvideDigramsStatsAnalysis(List<Lemm> list)
        {
            List<string> words = list.GetWords();
            StatsAnalysisResult<WordDigram> analysisResult = new StatsAnalysisResult<WordDigram>();
            var wordsFrequency = StatisticsAnalysis.GetFrequencyDictionary(words);

            analysisResult.Frequency_Dictionary = StatisticsAnalysis.GetDigramFrequenceDictionary(words);
            analysisResult.MutualInformation_Dictionary = StatisticsAnalysis.CalculateMutualInformation(analysisResult.Frequency_Dictionary, wordsFrequency, words.Count);
            analysisResult.TScore_Dictionary = StatisticsAnalysis.CalculateTScore(analysisResult.Frequency_Dictionary, wordsFrequency, words.Count);
            analysisResult.LogLikelihood_Dictionary = StatisticsAnalysis.CalculateLogLikelihood(analysisResult.Frequency_Dictionary);

            return analysisResult;
        }
        /// <summary>
        /// Мультипроцессорный статистический анализ для слов
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<StatsAnalysisResult<string>> ProvideWordsStatsAnalysisMulticore(List<MystemData> list)
        {
            Multiprocessor mps = new Multiprocessor();
            mps.MultiprocessorWordStatsAnalysis(list);
            return mps.WordsStatsAnalysisCache;
        }
        /// <summary>
        /// Мультипроцессорный статистический анализ для биграмм
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<StatsAnalysisResult<WordDigram>> ProvideDigramsStatsAnalysisMulticore(List<MystemData> list)
        {
            Multiprocessor mps = new Multiprocessor();
            mps.MultiprocessorDigramsStatsAnalysis(list);
            return mps.DigramsStatsAnalysisCache;
        }
        #endregion

        #region Морфологический анализ (4-уровень)
        /// <summary>
        /// Морфологический анализ (Исключение) 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="excludedTypes"></param>
        /// <returns></returns>
        public MystemData ProvideMorphAnalysis(MystemData data, string[] excludedTypes)
        {
            return MorphologicalAnalysis.ExcludeWordsByType(data, excludedTypes);
        }

        /// <summary>
        /// Мультипроцессорный анализ для Лемм 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="excludedTypes"></param>
        /// <returns></returns>
        public List<MystemData> ProvideMorphAnalysisMulticore(List<MystemData> list, string[] excludedTypes)
        {
            Multiprocessor mps = new Multiprocessor();
            mps.MultiprocessorMorphAnalysis(list, excludedTypes);
            return mps.MultiMorphAnalysisCache;
        }
        #endregion

        #region Кластерный анализ (5-уровень)

        public double[,] GetDefaultClustersCenters<T>(Dictionary<T, double[]> statisticDictionary)
        {
            var statisticsElements = statisticDictionary.Values;
            var length = statisticsElements.Max(el => el.Length);
            if (length != statisticsElements.Min(el => el.Length))
                throw new Exception("Differ statistics array lengths!");
            if (length < 1)
                throw new Exception("Statistics is empty!");
            var orderStaticstics = statisticsElements.OrderBy(el => el[0]);
            for (var i = 1; i < length; i++)
                orderStaticstics = orderStaticstics.ThenBy(el => el[i]);
            var first = orderStaticstics.First();
            var last = orderStaticstics.Last();
            var result = new double[2, length];
            for(var i=0;i<length;i++)
            {
                result[0, i] = first[i];
                result[1, i] = last[i];
            }
            return result;
        }

        /// <summary>
        /// Производит кластерный анализ для одного текста
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings"></param>
        /// <returns></returns>
        public Dictionary<T, double[]> ProvideClusterAnalysis<T>(ClasterSettings<T> settings)
        {
            ClasterAnalysis<T> ca = new ClasterAnalysis<T>(settings);
            return ca.Clasterize();
        }

        /// <summary>
        /// Многопроцессорная обработка для кластерного анализа слов
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<ClasterAnalysisResult<string>> ProvideWordClusterAnalysisMulticore(List<ClasterAnalysisData<string>> data)
        {
            Multiprocessor mps = new Multiprocessor();
            mps.MultiprocessorWordClasterAnalysis(data);
            return mps.MultiWordsClusterAnalysisCache;
        }

        /// <summary>
        /// Много процессорная обработка для кластерного анализа биграмм
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<ClasterAnalysisResult<WordDigram>> ProvideWordDigramClusterAnalysisMulticore(List<ClasterAnalysisData<WordDigram>> data)
        {
            Multiprocessor mps = new Multiprocessor();
            mps.MultiprocessorWordDigramClasterAnalysis(data);
            return mps.MultiWordDigramsClusterAnalysisCache;
        }

        /// <summary>
        /// Методы сбора данных для кластерного анализа
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="analysisResult"></param>
        /// <returns></returns>
        public Dictionary<T, double[]> GetDataReady<T>(StatsAnalysisResult<T> analysisResult)
        {
            List<Dictionary<T, double>> data = new List<Dictionary<T, double>>();

            if (analysisResult.Frequency_Dictionary != null)
                data.Add(analysisResult.Frequency_Dictionary);

            if (analysisResult.LogLikelihood_Dictionary != null)
                data.Add(analysisResult.LogLikelihood_Dictionary);

            if (analysisResult.MutualInformation_Dictionary != null)
                data.Add(analysisResult.MutualInformation_Dictionary);

            if (analysisResult.TF_Dictionary != null)
                data.Add(analysisResult.TF_Dictionary);

            if (analysisResult.TF_IDF_Dictionary != null)
                data.Add(analysisResult.TScore_Dictionary);

            if (analysisResult.TScore_Dictionary != null)
                data.Add(analysisResult.TScore_Dictionary);

            return data.MergeDictionaries();
        }
        #endregion
    }
}
