using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

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

        public void TantargyAdatbazisBetoltese()
        {
            TantargyAdatbazis.Position = 0;
            StreamReader sr = new StreamReader(TantargyAdatbazis);
            string sor;

            while ((sor = sr.ReadLine()) != null)
            {
                Tantargy betolt = new Tantargy();
                string[] sordata = sor.Split(';')[0].Split(',');
                betolt.Nev = sordata[0];
                betolt.SetKreditErtek(Convert.ToInt32(sordata[1]));
                betolt.SetTargykod(sordata[2]);
                betolt.SetKovetelmeny(sordata[3]);
                betolt.SetOraszam(Convert.ToInt32(sordata[4]));
                TantargyakListaja.Add(betolt);
            }
        }

        public Form1()
        {
            InitializeComponent();

            programStarted = false;


            button2.Text = "Új hallagató generálása";
            button3.Text = "Tantárgyak kezelése";

            HallgatokListaja = new List<Hallgato>();
            TantargyakListaja = new List<Tantargy>();
            timer1.Interval = 1;
            timer1.Enabled = true;
            button2.Visible = false;
            button3.Visible = false;
            label1.Text = "A betöltött hallgatoi adatbázis:";
            label2.Text = "A betölött tantárgy adatbazis:";
            label3.Text = "";
            label4.Text = "";
            button1.Visible = true;
            button4.Visible = true;
            SettingsRead = new StreamReader("..\\..\\settings.txt"); //a settings txt helye 

            String settingHallgato = SettingsRead.ReadLine();
            String settingsTantargy = SettingsRead.ReadLine();

            string[] hallgatoiAdat = settingHallgato.Split(';');
            string[] tantargyAdat = settingsTantargy.Split(';');

            label3.Text = hallgatoiAdat[1];
            label4.Text = tantargyAdat[1];

            checkBox1.Text = "";
            checkBox2.Text = "";
            checkBox1.Visible = false;
            checkBox2.Visible = false;

            button1.Text = "Új adatbazis betoltese";
            button4.Text = "Új adatbazis betoltese";

            label5.Text = "aktuális adatbazis elfogadása:";
            label6.Text = "aktuális adatbazis elfogadása:";
            label5.Visible = false;
            label6.Visible = false;


            validHallgatoiAdat = false;
            validTantargyAdat = false;

            if (hallgatoiAdat[1] != "-")
                validHallgatoiAdat = true;
            if (tantargyAdat[1] != "-")
                validTantargyAdat = true;
        }
    }
}
