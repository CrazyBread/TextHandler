﻿using System;
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
            mps.MultiprocesingWordStatsAnalysis(list);
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
            mps.MultiprocesingDigramsStatsAnalysis(list);
            return mps.DigramsStatsAnalysisCache;
        }
        #endregion

        #region Морфологический анализ (4-уровень)
        public void ProvideMorphAnalysis(List<Lemm> list)
        {

        }
        public void ProvideMorphAnalysisMulticore(List<MystemData> list)
        {

        }
        #endregion

        #region Кластерный анализ (5-уровень)
        public void ProvideClusterAnalysis()
        {

        }
        public void ProvideClusterAnalysisMulticore()
        {

        }
        #endregion
    }
}
