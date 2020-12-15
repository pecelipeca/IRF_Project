using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace irf_project_t4z1x
{
    public class Hallgato : IComparable
    {
		public String NeptunKod;
		public List<Tantargy> HallgatoTargyai;
		public List<int> HallgatoTargyaiJegy;


		private String VezetekNev;
		private String KeresztNev;
		private String AnyjaNeve;
		private String Nem;
		private String Lakhely;
		private String OktatasiAzonosito;
		private Double Atlag;
		private Double GorgetettAtlag;
		private Double Osztondij;
		private Double TeljesitettKredit;
		private Boolean AllamiOsztindijas;
		private DateTime SzuletesiDatum;

		private DateTime defaultDate = DateTime.MinValue;

		public Hallgato(String NK = "BATMAN", String VezNev = "Henger", String KerNev = "Elek", String AnNev = "Kiss János", String Neme = "De", String lakhly = "Jupiter", String oktAz = "8x1", double atlg = -1, double gAtlg = -1, double osztndj = -1, double tejKrdt = 0, Boolean allamise = false)
		{
			this.NeptunKod = NK.ToString();
			this.VezetekNev = VezNev.ToString();
			this.KeresztNev = KerNev.ToString();
			this.AnyjaNeve = AnNev.ToString();
			this.Nem = Neme.ToString();
			this.Lakhely = lakhly.ToString();
			this.OktatasiAzonosito = oktAz.ToString();
			this.Atlag = atlg;
			this.GorgetettAtlag = gAtlg;
			this.Osztondij = osztndj;
			this.TeljesitettKredit = tejKrdt;
			this.AllamiOsztindijas = allamise;
			this.SzuletesiDatum = new DateTime();
			this.HallgatoTargyai = new List<Tantargy>();

			this.HallgatoTargyaiJegy = new List<int>();
		}

		public void SetNeptunKod(String newNK)
		{ this.NeptunKod = newNK.ToString(); }

		public void SetVezetekNev(String newVN)
		{ this.VezetekNev = newVN.ToString(); }

		public string GetVezetekNev()
		{ return this.VezetekNev.ToString(); }

		public void SetKeresztNev(String newKN)
		{ this.KeresztNev = newKN.ToString(); }

		public string GetKeresztNev()
		{ return this.KeresztNev.ToString(); }

		public string GetTeljesNev()
		{ return this.VezetekNev.ToString() + " " + this.KeresztNev.ToString(); }

		public override string ToString()
		{
			return this.VezetekNev.ToString() + " " + this.KeresztNev.ToString();
		}

		public void SetAnyjaNeve(String newAN)
		{ this.AnyjaNeve = newAN.ToString(); }

		public string GetAnyjaNeve()
		{ return this.AnyjaNeve.ToString(); }

		public void SetNem(String newNem)
		{
			if (newNem.ToString() == "Férfi")
				this.Nem = "Férfi";
			else if (newNem.ToString() == "Nő")
				this.Nem = "Nő";
			
		}

		public string GetNem()
		{ return this.Nem.ToString(); }

		public void SetLakhely(String newLkhly)
		{ this.Lakhely = newLkhly.ToString(); }

		public string GetLakhely()
		{ return this.Lakhely.ToString(); }

		public void SetOktatasiAzonosito(String newOM)
		{ this.OktatasiAzonosito = newOM.ToString(); }

		public string GetOktatasiAzonosito()
		{ return this.OktatasiAzonosito.ToString(); }


		public void SetAtlag(double newAtlg)
		{ this.Atlag = newAtlg; }

		public double GetAtlag()
		{ return this.Atlag; }

		public void SetGorgetettAtlag(double newGAtlag)
		{ this.GorgetettAtlag = newGAtlag; }

		public double GetGorgetettAtlag()
		{ return this.GorgetettAtlag; }

		public void SetOsztondij(double newOsztnd)
		{ this.Osztondij = newOsztnd; }

		public double GetOsztöndij()
		{ return this.Osztondij; }

		public void SetTeljesitettKredit(double newTk)
		{ this.TeljesitettKredit = newTk; }

		public double GetTeljesitettKredit()
		{ return this.TeljesitettKredit; }

		public void SetAllamiOsztondijas(bool newStatus)
		{ this.AllamiOsztindijas = newStatus; }

		public Boolean GetAllamiOsztondijas()
		{ return AllamiOsztindijas; }

		public void SetSzuletesiDatum(DateTime newTime)
		{ this.SzuletesiDatum = newTime; }

		public DateTime GetSzuletesiDatum()
		{ return this.SzuletesiDatum; }

		public void TargyFelvetele(Tantargy newTargy, int jegy)
		{
			this.HallgatoTargyai.Add(newTargy);
			this.HallgatoTargyaiJegy.Add(jegy);
			atlagszamolas();

		}


		private void atlagszamolas()
		{
			double atlag = 0;
			for (int i = 0; i < HallgatoTargyaiJegy.Count; ++i)
			{
				atlag += HallgatoTargyaiJegy[i];
			}
			this.Atlag = atlag / (HallgatoTargyaiJegy.Count);
		}

		public void exportToCSV(FileStream sf)
		{ 
		  //kiiratashoz  https://docs.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-5.0
			string write1 = this.NeptunKod.ToString() + "," + this.VezetekNev.ToString() + "," + this.KeresztNev.ToString() + "," + this.AnyjaNeve.ToString() + "," + this.Nem.ToString() + "," + this.Lakhely.ToString() + "," + this.OktatasiAzonosito.ToString() + "," + this.Atlag.ToString() + "," + this.Osztondij.ToString() + "," + this.TeljesitettKredit.ToString() + "," + AllamiOsztindijas.ToString() + "," + SzuletesiDatum.ToShortDateString();
			byte[] info1 = new UTF8Encoding(true).GetBytes(write1);
			sf.Write(info1, 0, info1.Length);
			string write2 = ":";
			byte[] info2 = new UTF8Encoding(true).GetBytes(write2);
			sf.Write(info2, 0, info2.Length);
			for (int i = 0; i < HallgatoTargyai.Count; ++i)
			{
				string write3 = HallgatoTargyai[i].GetTargykod() + ",";
				byte[] info3 = new UTF8Encoding(true).GetBytes(write3);
				sf.Write(info3, 0, info3.Length);
				string write4 = HallgatoTargyaiJegy[i].ToString() + "|";
				byte[] info4 = new UTF8Encoding(true).GetBytes(write4);
				sf.Write(info4, 0, info4.Length);



			}
			string write6 = ";\n";
			byte[] info6 = new UTF8Encoding(true).GetBytes(write6);
			sf.Write(info6, 0, info6.Length);
		}

		// https://docs.microsoft.com/en-us/dotnet/api/system.icomparable?view=net-5.0
		public int CompareTo(object obj)
		{
			if (obj == null) return 1;

			Hallgato masikHallgato = obj as Hallgato;
			if (masikHallgato != null)
				return this.NeptunKod.CompareTo(masikHallgato.NeptunKod);
			else
				throw new ArgumentException("Háde ez nem is hallgato nem is létezik");

		}


		public Hallgato(Hallgato eq)
		{
			this.NeptunKod = eq.NeptunKod;
			this.VezetekNev = eq.VezetekNev;
			this.KeresztNev = eq.KeresztNev;
			this.AnyjaNeve = eq.AnyjaNeve;
			this.Nem = eq.Nem;
			this.Lakhely = eq.Lakhely;
			this.OktatasiAzonosito = eq.OktatasiAzonosito;
			this.Atlag = eq.Atlag;
			this.GorgetettAtlag = eq.GorgetettAtlag;
			this.Osztondij = eq.Osztondij;
			this.TeljesitettKredit = eq.TeljesitettKredit;
			this.AllamiOsztindijas = eq.AllamiOsztindijas;
			this.SzuletesiDatum = eq.SzuletesiDatum;
			this.HallgatoTargyai = eq.HallgatoTargyai;
			this.HallgatoTargyaiJegy = eq.HallgatoTargyaiJegy;
		}


		public dynamic GetParam(int k) //https://stackoverflow.com/questions/744401/dynamic-return-type-of-a-function    2. answer
		{
			switch (k)
			{
				case 0:
					return this.NeptunKod;
				case 1:
					return this.VezetekNev;
				case 2:
					return this.KeresztNev;
				case 3:
					return this.ToString();
				case 4:
					return this.AnyjaNeve;
				case 5:
					return this.Nem;
				case 6:
					return this.Lakhely;
				case 7:
					return this.OktatasiAzonosito;
				case 8:
					return this.Atlag;
				case 9:
					return this.TeljesitettKredit;
				case 10:
					return this.AllamiOsztindijas;
				case 11:
					return this.SzuletesiDatum;
				default:
					return 0;
			}
		}

		public bool ValidateHallgatoNeptunKod(string NeptunKod)
		{
			if (NeptunKod.Length == 6)
				return true;
			else
				return false;
		}

		public bool ValidateHallgatoAtlag(double testatlag)
		{
			if ((testatlag <= 1) || (testatlag >= 5))
				return false;
			else
				return true;
		}

		public bool ValidateSzuletesiDatum(int y, int m, int d)
		{
			if (y < 0 || y > 2020)
				return false;
			if (m > 12 || m < 0)
				return false;
			if (m == 2 || d > 28)
				return false;
			if (d < 0 || d > 31)
				return false;


			if (new DateTime(y, m, d) > new DateTime(2020 - 17, 1, 1))
				return false;
			else
				return true;
		}
	}
}
