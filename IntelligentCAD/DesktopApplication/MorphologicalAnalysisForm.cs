using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApplication
{
    public partial class MorphologicalAnalysisForm : Form
    {
        public MystemData data;
        public List<MystemData> listData;

        public MorphologicalAnalysisForm(MystemData data)
        {
            InitializeComponent();
        }

        public MorphologicalAnalysisForm(List<MystemData> list)
        {
            InitializeComponent();
            listData = list;
        }
    }
}
