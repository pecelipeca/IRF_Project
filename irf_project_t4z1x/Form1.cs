using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace irf_project_t4z1x
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            readText();
        }

        void readText()
        {
            {
                StreamReader reader = new StreamReader(File.OpenRead(@"C: \Users\pecel\source\Repos\IRF_Project\irf_project_t4z1x\project_test.csv"));
                List<string> year = new List<String>();
                List<string> month = new List<String>();
                List<string> day = new List<String>();
                List<string> value = new List<String>();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        string[] values = line.Split(';');
                        if (values.Length >= 4)
                        {
                            year.Add(values[0]);
                            month.Add(values[1]);
                            day.Add(values[2]);
                            value.Add(values[3]);
                        }
                    }
                }
                string[] firstlistA = year.ToArray();
                string[] firstlistB = month.ToArray();
                string[] firstlistC = day.ToArray();
                string[] firstlistD = value.ToArray();
            }
        }
    }
}
