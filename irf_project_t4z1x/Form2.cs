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

        Hallgato mentenikivant;
        public Form2(FileStream Hallgatoi, FileStream Tantargy, List<Hallgato> Hallgatok, List<Tantargy> Tantargyak)
        {
            InitializeComponent();
            HallgatoiAdat = Hallgatoi;
            TantargyAdat = Tantargy;
            TantargyakListaja = Tantargyak;
            HallgatokListaja = Hallgatok;

            for (int i = 0; i < TantargyakListaja.Count; ++i)
                comboBox2.Items.Add(TantargyakListaja[i].Nev.ToString());

            for (int i = 0; i < 6; ++i)
                comboBox3.Items.Add((i).ToString());

            this.Name = "Új hallgató generálása";
            int X0 = 25;
            int Y0 = 25;
            int YOffset = 30;

            label1.Location = new Point(X0, Y0);
            label2.Location = new Point(X0, Y0 + YOffset);
            label3.Location = new Point(X0, Y0 + YOffset * 2);
            label4.Location = new Point(X0, Y0 + YOffset * 3);
            label5.Location = new Point(X0, Y0 + YOffset * 4);
            label6.Location = new Point(X0, Y0 + YOffset * 5);
            label7.Location = new Point(X0, Y0 + YOffset * 6);
            label15.Location = new Point(X0, Y0 + YOffset * 7);
            label8.Location = new Point(X0, Y0 + YOffset * 8);

            label14.Location = new Point(X0 + 420, Y0);
            checkBox2.Location = new Point(label14.Location.X + 250, Y0);


            label9.Location = new Point(label14.Location.X, label2.Location.Y + YOffset * 0);

            label11.Location = new Point(label14.Location.X, label2.Location.Y + YOffset * 1);

            label13.Location = new Point(label14.Location.X, label2.Location.Y + YOffset * 2);

            button2.Location = new Point(label14.Location.X, label2.Location.Y + YOffset * 3);

            label1.Text = "Neptun kód";
            label2.Text = "Vezeték név";
            label3.Text = "Kereszt név";
            label4.Text = "Nem";
            label5.Text = "Lakhely";
            label6.Text = "Oktatási azonosító";
            label7.Text = "Allami ösztöndíjas";
            label8.Text = "Születési dátum";
            label15.Text = "Anyja neve:";

            label14.Text = "Amennyiben öregdiákot szeretne hozzaadni ->";

            label9.Text = "";
            label11.Text = "";

            button2.Text = "Kiválasztott tantárgy felvétele a hallgatónak";

            label13.Visible = false;
            textBox10.Visible = false;


            label9.Visible = false;

            label11.Visible = false;
            textBox6.Visible = false;
            button2.Visible = false;
            textBox8.Visible = false;


            label12.Visible = false;
            label10.Visible = false;
            textBox7.Visible = false;
            textBox9.Visible = false;
            comboBox1.Items.Add("Férfi");
            comboBox1.Items.Add("Nő");
            comboBox1.Text = comboBox1.Items[1].ToString();

            checkBox2.Text = "";
            checkBox1.Text = "";


            textBox1.Location = new Point(label1.Location.X + 120, label1.Location.Y);
            textBox2.Location = new Point(label2.Location.X + 120, label2.Location.Y);
            textBox3.Location = new Point(label3.Location.X + 120, label3.Location.Y);
            comboBox1.Location = new Point(label4.Location.X + 120, label4.Location.Y);
            textBox4.Location = new Point(label5.Location.X + 120, label5.Location.Y);
            textBox5.Location = new Point(label6.Location.X + 120, label6.Location.Y);
            checkBox1.Location = new Point(label7.Location.X + 120, label7.Location.Y);
            monthCalendar1.Location = new Point(label8.Location.X + 120, label8.Location.Y);
            textBox6.Location = new Point(label9.Location.X + 120, label9.Location.Y);
            textBox8.Location = new Point(label11.Location.X + 120, label11.Location.Y);
            textBox10.Location = new Point(label13.Location.X + 120, label13.Location.Y);
            textBox11.Location = new Point(label15.Location.X + 120, label15.Location.Y);

            monthCalendar1.MaxSelectionCount = 1;
            button1.Text = "Hallgato Mentése";

            label16.Location = new Point(button2.Location.X, button2.Location.Y + 120);
            comboBox2.Location = new Point(label16.Location.X + 120, label16.Location.Y);
            label17.Location = new Point(label16.Location.X, label16.Location.Y + YOffset);
            comboBox3.Location = new Point(label17.Location.X + 120, label17.Location.Y);
            label16.Text = "Tantargy:";
            label17.Text = "Szerzett jegy";

            label16.Visible = false;
            comboBox2.Visible = false;
            label17.Visible = false;
            comboBox3.Visible = false;
            mentenikivant = new Hallgato();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hallgato ment = new Hallgato();
            ment.NeptunKod = textBox1.Text;
            ment.SetVezetekNev(textBox2.Text);
            ment.SetKeresztNev(textBox3.Text);
            ment.SetNem(comboBox1.Text);
            ment.SetLakhely(textBox4.Text);
            ment.SetOktatasiAzonosito(textBox5.Text);
            ment.SetAllamiOsztondijas(checkBox1.Checked);
            ment.SetSzuletesiDatum(monthCalendar1.SelectionStart.Date);
            ment.SetAnyjaNeve(textBox11.Text);

            if (mentenikivant.HallgatoTargyai.Count != 0)
            {
                for (int i = 0; i < mentenikivant.HallgatoTargyai.Count; ++i)
                    ment.TargyFelvetele(mentenikivant.HallgatoTargyai[i], mentenikivant.HallgatoTargyaiJegy[i]);
            }

            HallgatokListaja.Add(ment);
            mentenikivant.HallgatoTargyai.Clear();
            mentenikivant.HallgatoTargyaiJegy.Clear();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            button2.Visible = checkBox2.Checked;
            label16.Visible = checkBox2.Checked;
            comboBox2.Visible = checkBox2.Checked;
            label17.Visible = checkBox2.Checked;
            comboBox3.Visible = checkBox2.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mentenikivant.TargyFelvetele(TantargyakListaja[comboBox2.SelectedIndex], Convert.ToInt32(comboBox3.SelectedItem.ToString()));
        }
    }
}
