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
    public partial class StatsAnalysisForm : Form
    {
        private MystemData data;
        private List<MystemData> listData;

        public StatsAnalysisForm(MystemData data)
        {
            InitializeComponent();
            this.data = data;
        }

        public StatsAnalysisForm(List<MystemData> listData)
        {
            InitializeComponent();
            this.listData = listData;
        }

        //...
    }
}
