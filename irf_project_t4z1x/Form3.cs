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
    public partial class Form3 : Form
    {
        FileStream HallgatoiAdat;
        FileStream TantargyAdat;
        List<Tantargy> TantargyakListaja;
        List<Hallgato> HallgatokListaja;

        Tantargy ment;
        public Form3(FileStream Hallgatoi, FileStream Tantargy, List<Hallgato> Hallgatok, List<Tantargy> Tantargyak)
        {
            InitializeComponent();
            HallgatoiAdat = Hallgatoi;
            TantargyAdat = Tantargy;
            TantargyakListaja = Tantargyak;
            HallgatokListaja = Hallgatok;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            int Yoffset = 30;

            tabControl1.TabPages[0].Text = "Új tárgy felvétele a rendszerbe";
            tabControl1.TabPages[1].Text = "Létező tágy módosítása";

            label1.Text = "Tantárgy neve";
            label2.Text = "Tárgy kreditértéke";
            label3.Text = "Tantárgy kódja";
            label4.Text = "Tantárgy követelménye";
            label5.Text = "Tantárgy féléves óraszáma";

            comboBox1.Items.Add("Vizsga");
            comboBox1.Items.Add("Félévközi");
            comboBox1.Text = comboBox1.Items[0].ToString();
            textBox1.Location = new Point(label1.Location.X + 180, label1.Location.Y);
            label2.Location = new Point(label1.Location.X, label1.Location.Y + Yoffset); numericUpDown1.Location = new Point(label2.Location.X + 180, label2.Location.Y);
            label3.Location = new Point(label1.Location.X, label1.Location.Y + Yoffset * 2); textBox3.Location = new Point(label3.Location.X + 180, label3.Location.Y);
            label4.Location = new Point(label1.Location.X, label1.Location.Y + Yoffset * 3); comboBox1.Location = new Point(label4.Location.X + 180, label4.Location.Y);
            label5.Location = new Point(label1.Location.X, label1.Location.Y + Yoffset * 4); textBox5.Location = new Point(label5.Location.X + 180, label5.Location.Y);

            textBox3.Width = textBox5.Width = numericUpDown1.Width = comboBox1.Width = textBox1.Width;


            button1.Text = "Tantárgy mentése az adatbázisba";
            button1.Location = new Point(label1.Location.X, label5.Location.Y + Yoffset);


            label6.Text = "Tantárgy";
            label7.Text = "Név";
            label8.Text = "Kreditérték";
            label9.Text = "Kód";
            label10.Text = "Követelmény";
            label11.Text = "Óraszám";

            comboBox3.Items.Add("Vizsga");
            comboBox3.Items.Add("Félévközi");

            numericUpDown2.Maximum = 100000000;
            numericUpDown2.Minimum = 0;
            numericUpDown3.Maximum = 100000000;
            numericUpDown3.Minimum = 0;

            comboBox2.Location = new Point(label6.Location.X + 120, label6.Location.Y);
            label7.Location = new Point(label6.Location.X, label6.Location.Y + Yoffset); textBox2.Location = new Point(label7.Location.X + 120, label7.Location.Y);
            label8.Location = new Point(label7.Location.X, label7.Location.Y + Yoffset); numericUpDown2.Location = new Point(label8.Location.X + 120, label8.Location.Y);
            label9.Location = new Point(label8.Location.X, label8.Location.Y + Yoffset); textBox4.Location = new Point(label9.Location.X + 120, label9.Location.Y);
            label10.Location = new Point(label9.Location.X, label9.Location.Y + Yoffset); comboBox3.Location = new Point(label10.Location.X + 120, label10.Location.Y);
            label11.Location = new Point(label10.Location.X, label10.Location.Y + Yoffset); numericUpDown3.Location = new Point(label11.Location.X + 120, label11.Location.Y);
            button2.Location = new Point(label11.Location.X, label11.Location.Y + Yoffset); button2.Text = "Tantárgy mentese";
            button3.Text = "Frissites";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ment = new Tantargy();
            ment.Nev = textBox1.Text;
            ment.SetKreditErtek(Convert.ToInt32(numericUpDown1.Value));
            ment.SetTargykod(textBox3.Text);
            ment.SetKovetelmeny(comboBox1.Text);
            ment.SetOraszam(Convert.ToInt32(textBox5.Text));

            TantargyakListaja.Add(ment);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count != TantargyakListaja.Count)
            {
                for (int i = 0; i < TantargyakListaja.Count; ++i)
                    comboBox2.Items.Add(TantargyakListaja[i].Nev.ToString());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tantargy valtozo = TantargyakListaja[comboBox2.SelectedIndex];
            textBox2.Text = valtozo.Nev.ToString();
            numericUpDown2.Value = valtozo.GetKreditErtek();
            comboBox2.Text = valtozo.GetKovetelmeny();
            textBox4.Text = valtozo.GetTargykod();
            numericUpDown3.Value = valtozo.GetOraszam();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TantargyakListaja[comboBox2.SelectedIndex] = new Tantargy(textBox2.Text, Convert.ToInt32(numericUpDown2.Value), textBox4.Text, comboBox3.Text, Convert.ToInt32(numericUpDown3.Value));
            comboBox2.Items.Clear();
            for (int i = 0; i < TantargyakListaja.Count; ++i)
                comboBox2.Items.Add(TantargyakListaja[i].Nev.ToString());
        }
    }
}
