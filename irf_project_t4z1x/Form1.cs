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
            checkBox1.Visible = true;
            checkBox2.Visible = true;
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

            button1.Location = new Point(button1.Location.X, label1.Location.Y);
            label3.Location = new Point(label3.Location.X, label1.Location.Y);
            label5.Location = new Point(label5.Location.X, label1.Location.Y);
            checkBox1.Location = new Point(checkBox1.Location.X, label1.Location.Y);

            button4.Location = new Point(button1.Location.X, label2.Location.Y);
            label4.Location = new Point(label3.Location.X, label2.Location.Y);
            label6.Location = new Point(label5.Location.X, label2.Location.Y);
            checkBox2.Location = new Point(checkBox1.Location.X, label2.Location.Y);

            label10.Visible = label7.Visible = label8.Visible = label9.Visible = richTextBox1.Visible = comboBox1.Visible = comboBox2.Visible = comboBox3.Visible = button5.Visible = comboBox4.Visible = comboBox5.Visible = comboBox6.Visible = false;
            label11.Visible = label12.Visible = label13.Visible = label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = label20.Visible = label21.Visible = label22.Visible = false;
            checkBox3.Visible = checkBox4.Visible = checkBox5.Visible = checkBox6.Visible = checkBox7.Visible = checkBox8.Visible = checkBox9.Visible = checkBox10.Visible = checkBox11.Visible = checkBox12.Visible = checkBox13.Visible = checkBox14.Visible = false;
            checkBox3.Text = ""; checkBox4.Text = ""; checkBox5.Text = ""; checkBox6.Text = ""; checkBox7.Text = ""; checkBox8.Text = ""; checkBox9.Text = ""; checkBox10.Text = ""; checkBox11.Text = ""; checkBox12.Text = ""; checkBox13.Text = ""; checkBox14.Text = "";
            label11.Text = ""; label12.Text = ""; label13.Text = ""; label14.Text = ""; label15.Text = ""; label16.Text = ""; label17.Text = ""; label18.Text = ""; label19.Text = ""; label20.Text = ""; label21.Text = ""; label22.Text = "";

            int chboff = 150;
            int lbloff = 30;

            label12.Location = new Point(label11.Location.X, label11.Location.Y + 30); label13.Location = new Point(label12.Location.X, label12.Location.Y + 30);
            checkBox3.Location = new Point(label11.Location.X + chboff, label11.Location.Y); checkBox4.Location = new Point(label12.Location.X + chboff, label12.Location.Y); checkBox5.Location = new Point(label13.Location.X + chboff, label13.Location.Y);

            label14.Location = new Point(checkBox3.Location.X + lbloff, label11.Location.Y); label15.Location = new Point(checkBox4.Location.X + lbloff, label12.Location.Y); label16.Location = new Point(checkBox5.Location.X + lbloff, label13.Location.Y);
            checkBox6.Location = new Point(label14.Location.X + chboff, label11.Location.Y); checkBox7.Location = new Point(label15.Location.X + chboff, label12.Location.Y); checkBox8.Location = new Point(label16.Location.X + chboff, label13.Location.Y);

            label17.Location = new Point(checkBox6.Location.X + lbloff, label11.Location.Y); label18.Location = new Point(checkBox7.Location.X + lbloff, label12.Location.Y); label19.Location = new Point(checkBox8.Location.X + lbloff, label13.Location.Y);
            checkBox9.Location = new Point(label17.Location.X + chboff, label11.Location.Y); checkBox10.Location = new Point(label18.Location.X + chboff, label12.Location.Y); checkBox11.Location = new Point(label19.Location.X + chboff, label13.Location.Y);

            label20.Location = new Point(checkBox9.Location.X + lbloff, label11.Location.Y); label21.Location = new Point(checkBox10.Location.X + lbloff, label12.Location.Y); label22.Location = new Point(checkBox11.Location.X + lbloff, label13.Location.Y);
            checkBox12.Location = new Point(label20.Location.X + chboff, label11.Location.Y); checkBox13.Location = new Point(label21.Location.X + chboff, label12.Location.Y); checkBox14.Location = new Point(label22.Location.X + chboff, label13.Location.Y);

            comboBox1.Location = new Point(label7.Location.X + 120, label7.Location.Y); comboBox4.Location = new Point(comboBox1.Location.X + 120, comboBox1.Location.Y);
            label8.Location = new Point(label7.Location.X, label7.Location.Y + 30); comboBox2.Location = new Point(label8.Location.X + 120, label8.Location.Y); comboBox5.Location = new Point(comboBox2.Location.X + 120, comboBox2.Location.Y);
            label9.Location = new Point(label8.Location.X, label8.Location.Y + 30); comboBox3.Location = new Point(label9.Location.X + 120, label9.Location.Y); comboBox6.Location = new Point(comboBox3.Location.X + 120, comboBox3.Location.Y);
            label7.Text = "Rendezés 1 rendje";
            label8.Text = "Rendezés 2 rendje";
            label9.Text = "Rendezés 3 rendje";
            label10.Text = "Végeredmény";
            button5.Text = "Szűrés";
            comboBox5.Size = comboBox6.Size = comboBox4.Size;

            button6.Text = "Mentés EXCELBE";
            button6.Visible = false;
        }

        bool comobox2Refresh = true;
        bool comobox3Refresh = true;

        private void button1_Click(object sender, EventArgs e)
        {//uj hallgatoi adatbazis betoltese
            openFileDialog1.ShowDialog();
            label3.Text = openFileDialog1.FileName;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {//uj tantargy adatbazis megadasa
            openFileDialog2.ShowDialog();
            label4.Text = openFileDialog2.FileName;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (validHallgatoiAdat && !programStarted)
            {
                label5.Visible = true;
                checkBox1.Visible = true;
            }

            if (validTantargyAdat && !programStarted)
            {
                label6.Visible = true;
                checkBox2.Visible = true;
            }
            if ((label3.Text != "-") && !programStarted)
                validHallgatoiAdat = true;
            if ((label4.Text != "-") && !programStarted)
                validTantargyAdat = true;
            if (richTextBox1.Text.Length > 6)
                button6.Enabled = true;
            else
                button6.Enabled = false;

            if (checkBox1.Checked && checkBox2.Checked && !programStarted)
            {
                programStarted = true;

                HallgatoiAdatbazis = new FileStream(label3.Text, FileMode.Open);
                TantargyAdatbazis = new FileStream(label4.Text, FileMode.Open);

                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                button1.Visible = false;
                button4.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;

                button2.Visible = true;
                button3.Visible = true;
                label10.Visible = label7.Visible = label8.Visible = label9.Visible = richTextBox1.Visible = comboBox1.Visible = comboBox2.Visible = comboBox3.Visible = button5.Visible = comboBox4.Visible = comboBox5.Visible = comboBox6.Visible = true;

                button6.Visible = true;

                label11.Visible = label12.Visible = label13.Visible = label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = label20.Visible = label21.Visible = label22.Visible = true;

                comboBox1.Items.Add("Hallgato");
                comboBox1.Items.Add("Tantargy");
                comboBox1.SelectedIndex = 0;

                comboBox4.Items.Add("Növekvő");
                comboBox4.Items.Add("Csökkenő");
                comboBox4.SelectedIndex = 0;
                comboBox4.Visible = false;

                comboBox5.Items.Add("Növekvő");
                comboBox5.Items.Add("Csökkenő");
                comboBox5.SelectedIndex = 0;

                comboBox6.Items.Add("Növekvő");
                comboBox6.Items.Add("Csökkenő");
                comboBox6.SelectedIndex = 0;


                TantargyAdatbazisBetoltese();
                HallgatoiAdatbazisBetoltese();

            }
            if (comboBox1.SelectedIndex == 0 && comobox2Refresh)
            {

                comboBox2.Items.Clear();
                comboBox2.Items.Add("Neptun Kód");              //0
                comboBox2.Items.Add("Vezeték Név");             //1
                comboBox2.Items.Add("Kreszet Név");             //2
                comboBox2.Items.Add("Teljes név");              //3
                comboBox2.Items.Add("Anyja Neve");              //4
                comboBox2.Items.Add("Nem");                     //5
                comboBox2.Items.Add("Lakhely");                 //6
                comboBox2.Items.Add("Oktatási Azonosító");      //7
                comboBox2.Items.Add("Átlag");                   //8
                comboBox2.Items.Add("Teljesített Kredit");      //9
                comboBox2.Items.Add("Állami ösztondíjas");      //10
                comboBox2.Items.Add("Születési Dátum");         //11
                comboBox2.SelectedIndex = 0;
                comobox2Refresh = false;

                label11.Text = ""; label12.Text = ""; label13.Text = ""; label14.Text = ""; label15.Text = ""; label16.Text = ""; label17.Text = ""; label18.Text = ""; label19.Text = ""; label20.Text = ""; label21.Text = ""; label22.Text = "";
                label11.Text = "Neptun Kód";
                label12.Text = "Vezetéknév";
                label13.Text = "Kereszt név";
                label14.Text = "Teljes név";
                label15.Text = "Anyja neve";
                label16.Text = "Nem";
                label17.Text = "Lakhely";
                label18.Text = "Oktatási azonosító";
                label19.Text = "Átlag";
                label20.Text = "Teljesített kredit";
                label21.Text = "Állami ösztondíjas";
                label22.Text = "Születési dátum";

                checkBox3.Visible = checkBox4.Visible = checkBox5.Visible = checkBox6.Visible = checkBox7.Visible = checkBox8.Visible = checkBox9.Visible = checkBox10.Visible = checkBox11.Visible = checkBox12.Visible = checkBox13.Visible = checkBox14.Visible = false;
                checkBox3.Visible = checkBox4.Visible = checkBox5.Visible = checkBox6.Visible = checkBox7.Visible = checkBox8.Visible = checkBox9.Visible = checkBox10.Visible = checkBox11.Visible = checkBox12.Visible = checkBox13.Visible = checkBox14.Visible = true;

            }
            else if (comboBox1.SelectedIndex == 1 && comobox2Refresh)
            {

                comboBox2.Items.Clear();
                comboBox2.Items.Add("Név");             //0
                comboBox2.Items.Add("Kreditérték");     //1
                comboBox2.Items.Add("Tantrágy Kód");    //2
                comboBox2.Items.Add("Óraszám");         //3
                comboBox2.Items.Add("Követelmény");     //4
                comboBox2.SelectedIndex = 0;
                comobox2Refresh = false;

                label11.Text = ""; label12.Text = ""; label13.Text = ""; label14.Text = ""; label15.Text = ""; label16.Text = ""; label17.Text = ""; label18.Text = ""; label19.Text = ""; label20.Text = ""; label21.Text = ""; label22.Text = "";
                label11.Text = "Név";
                label12.Text = "Kreditérték";
                label13.Text = "Tantárgy kód";
                label14.Text = "Óraszám";
                label15.Text = "Követelmény";

                checkBox3.Visible = checkBox4.Visible = checkBox5.Visible = checkBox6.Visible = checkBox7.Visible = checkBox8.Visible = checkBox9.Visible = checkBox10.Visible = checkBox11.Visible = checkBox12.Visible = checkBox13.Visible = checkBox14.Visible = false;
                checkBox3.Visible = checkBox4.Visible = checkBox5.Visible = checkBox6.Visible = checkBox7.Visible = true;
            }
            if (comboBox1.SelectedIndex == 0 && comobox3Refresh)
            {
                comboBox3.Items.Clear();
                for (int i = 0; i < comboBox2.Items.Count; ++i)
                    if (i != comboBox2.SelectedIndex)
                        comboBox3.Items.Add(comboBox2.Items[i]);
                comboBox3.Items.Add("None");
                comboBox3.SelectedIndex = comboBox3.Items.Count - 1;
                comobox3Refresh = false;
            }
            else if (comboBox1.SelectedIndex == 1 && comobox3Refresh)
            {
                comboBox3.Items.Clear();
                for (int i = 0; i < comboBox2.Items.Count; ++i)
                    if (i != comboBox2.SelectedIndex)
                        comboBox3.Items.Add(comboBox2.Items[i]);
                comboBox3.Items.Add("None");
                comboBox3.SelectedIndex = comboBox3.Items.Count - 1;
                comobox3Refresh = false;
            }

        }

        private void HallgatoiAdatbázisFrissites()
        {

            HallgatoiAdatbazis.Close();
            HallgatoiAdatbazis = new FileStream(label3.Text, FileMode.Create);
            for (int i = 0; i < HallgatokListaja.Count; ++i)
                HallgatokListaja[i].exportToCSV(HallgatoiAdatbazis);
        }

        private void TantargyAdatbázisFrissitese()
        {
            TantargyAdatbazis.Close();
            TantargyAdatbazis = new FileStream(label4.Text, FileMode.Create);
            for (int i = 0; i < TantargyakListaja.Count; ++i)
                TantargyakListaja[i].exportToCSV(TantargyAdatbazis);
        }

        FileNotFoundException k;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                HallgatoiAdatbázisFrissites();
                TantargyAdatbázisFrissitese();
                TantargyAdatbazis.Close();
                HallgatoiAdatbazis.Close();
                HallgatokListaja.Clear();
                TantargyakListaja.Clear();
            }
            catch (FileNotFoundException k)
            { }
        }
    }
}
