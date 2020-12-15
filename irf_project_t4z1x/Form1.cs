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

        public FileStream TantargyAdatbazis;

        public FileStream HallgatoiAdatbazis;

        StreamReader SettingsRead;

        public List<Hallgato> HallgatokListaja;
        public List<Tantargy> TantargyakListaja;

        private bool validHallgatoiAdat;
        private bool validTantargyAdat;
        private bool programStarted;

        public void HallgatoiAdatbazisBetoltese()
        {
            HallgatoiAdatbazis.Position = 0;
            StreamReader sr = new StreamReader(HallgatoiAdatbazis);
            string sor;

            while ((sor = sr.ReadLine()) != null)
            {
                Hallgato betolt = new Hallgato();
                string[] sordata = sor.Split(':')[0].Split(',');
                string[] tantargysor = sor.Split(':')[1].Split('|');
                betolt.NeptunKod = sordata[0];
                betolt.SetVezetekNev(sordata[1]);
                betolt.SetKeresztNev(sordata[2]);
                betolt.SetAnyjaNeve(sordata[3]);
                betolt.SetNem(sordata[4]);
                betolt.SetLakhely(sordata[5]);
                betolt.SetOktatasiAzonosito(sordata[6]);
                betolt.SetAtlag(Convert.ToDouble(sordata[7]));

                betolt.SetOsztondij(Convert.ToDouble(sordata[8]));
                betolt.SetTeljesitettKredit(Convert.ToDouble(sordata[9]));
                betolt.SetAllamiOsztondijas(Convert.ToBoolean(sordata[10]));
                betolt.SetSzuletesiDatum(Convert.ToDateTime(sordata[11]));

                for (int i = 1; i < tantargysor.Length; ++i)
                {
                    string[] tgy = tantargysor[0].Split(',');
                    string targyneve = tgy[0];
                    int j = 0;
                    for (; j < TantargyakListaja.Count; ++j)
                    {
                        if (TantargyakListaja[j].Nev.Equals(targyneve))
                            break;
                    }
                    betolt.TargyFelvetele(TantargyakListaja[j - 1], Convert.ToInt32(tgy[1]));
                }
                HallgatokListaja.Add(betolt);
            }
        }

        public Form1()
        {
            InitializeComponent();


        }


        }
    }
}
