using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileLib;
using HelperLib;

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
        public List<Lemm> HandleByMystem(List<string> fileLines)
        {
            MystemProvider mst = new MystemProvider();
            return mst.LaunchMystem(fileLines);
        }
        public List<MystemData> HandleByMystemMulticore(List<FileData> dataList)
        {
            Multiprocessor mps = new Multiprocessor();
            mps.MultiprocessorMystemHandler(dataList);
            return mps.MystemCache;
        }
        #endregion

        #region Статистический анализ (3-уровень)
        public void ProvideStatsAnalysis()
        {

        }
        public void ProvideStatsAnalysisMulticore()
        {

        }
        #endregion

        #region Морфологический анализ (4-уровень)
        public void ProvideMorphAnalysis()
        {

        }
        public void ProvideMorphAnalysisMulticore()
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
