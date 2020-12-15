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
    public partial class Form2 : Form
    {
        FileStream HallgatoiAdat;
        FileStream TantargyAdat;
        List<Tantargy> TantargyakListaja;
        List<Hallgato> HallgatokListaja;

        public Form2(FileStream Hallgatoi, FileStream Tantargy, List<Hallgato> Hallgatok, List<Tantargy> Tantargyak)
        {
            InitializeComponent();
            HallgatoiAdat = Hallgatoi;
            TantargyAdat = Tantargy;
            TantargyakListaja = Tantargyak;
            HallgatokListaja = Hallgatok;


        }
    }
}
