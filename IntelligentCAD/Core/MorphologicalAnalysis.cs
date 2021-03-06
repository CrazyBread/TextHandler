﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class MorphologicalAnalysis
    {
        /// <summary>
        /// Получение лемм по типу речи
        /// </summary>
        /// <param name="lemms"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Lemm> GetWordsByType(List<Lemm> lemms, string type) 
        {
            return lemms.Where(i => i.analysis.Length > 0 && i.analysis[0].wordType == type).ToList();
        }

        /// <summary>
        /// Исключение лемм по типу речи
        /// </summary>
        /// <param name="lemms"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Lemm> ExcludeWordsByType(List<Lemm> lemms, params string[] types)
        {
            return lemms.Where(i => i.analysis.Length > 0 && !i.analysis.Any(j => types.Contains(j.wordType))).ToList();
        }

        /// <summary>
        /// Исключение лемм по типу речи
        /// </summary>
        /// <param name="lemms"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MystemData ExcludeWordsByType(MystemData mstData, params string[] types)
        {
            mstData.List = mstData.List.Where(i => i.analysis.Length > 0 && !i.analysis.Any(j => types.Contains(j.wordType))).ToList();
            return mstData;
        }
    }
}
