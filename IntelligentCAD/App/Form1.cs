using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class Form1 : Form
    {
        private int current_index = 0;
        private int prev_index = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            LoadPanel.Visible = true;
            MystemPanel.Visible = false;
            MorphPanel.Visible = false;
            StatPanel.Visible = false;
            ClasterPanel.Visible = false;
        }

        private void MystemBtn_Click(object sender, EventArgs e)
        {
            LoadPanel.Visible = false;
            MystemPanel.Visible = true;
            MorphPanel.Visible = false;
            StatPanel.Visible = false;
            ClasterPanel.Visible = false;
        }

        private void MorphBtn_Click(object sender, EventArgs e)
        {
            LoadPanel.Visible = false;
            MystemPanel.Visible = false;
            MorphPanel.Visible = true;
            StatPanel.Visible = false;
            ClasterPanel.Visible = false;
        }

        private void StatBtn_Click(object sender, EventArgs e)
        {
            LoadPanel.Visible = false;
            MystemPanel.Visible = false;
            MorphPanel.Visible = false;
            StatPanel.Visible = true;
            ClasterPanel.Visible = false;
        }

        private void ClasterBtn_Click(object sender, EventArgs e)
        {
            LoadPanel.Visible = false;
            MystemPanel.Visible = false;
            MorphPanel.Visible = false;
            StatPanel.Visible = false;
            ClasterPanel.Visible = true;
        }

        #region Вспом. функции
        private bool SwitchedNext()
        {
            if 
        }
        #endregion
    }
}
