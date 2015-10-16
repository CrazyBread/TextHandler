using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Описание слова и его начальных форм
    /// </summary>
    public class Lemm
    {
        public string Value { get; private set; }
        public string[] InitialForms { get; private set; }

        public Lemm(string value, string[] beginForm)
        {
            Value = value;
            InitialForms = beginForm;
        }
    }
}
