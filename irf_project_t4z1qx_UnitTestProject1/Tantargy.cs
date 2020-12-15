using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace irf_project_t4z1x
{
    public class Tantargy
    {
		public String Nev;

		private Int32 Kreditertek;
		private String TargyKod;
		private String Kovetelmeny;
		private Int32 Oraszam;
		private int tantargySorszama;

		public Tantargy(String Nev = " ", Int32 kredit = 0, String TKod = " ", String kov = "Vizsga", Int32 Oszam = 28, int sorsz = 0)
		{
			this.Nev = Nev.ToString();
			this.Kreditertek = kredit;
			this.TargyKod = TKod.ToString();
			this.Kovetelmeny = kov.ToString();
			this.Oraszam = Oszam;
			this.tantargySorszama = sorsz;
		}

		public void SetKreditErtek(Int32 kr)
		{ this.Kreditertek = kr; }

		public Int32 GetKreditErtek()
		{ return this.Kreditertek; }

		public void SetTargykod(String newtk)
		{ this.TargyKod = newtk.ToString(); }

		public string GetTargykod()
		{ return this.TargyKod.ToString(); }

		public void SetKovetelmeny(String newKov)
		{ this.Kovetelmeny = newKov.ToString(); }

		public string GetKovetelmeny()
		{ return this.Kovetelmeny.ToString(); }

		public void SetOraszam(Int32 newOsz)
		{ this.Oraszam = newOsz; }

		public Int32 GetOraszam()
		{ return this.Oraszam; }

		public void exportToCSV(FileStream sf)
		{
			string write1 = this.Nev.ToString() + "," + this.Kreditertek.ToString() + "," + this.TargyKod.ToString() + "," + this.Kovetelmeny.ToString() + "," + this.Oraszam.ToString();
			byte[] info1 = new UTF8Encoding(true).GetBytes(write1);
			sf.Write(info1, 0, info1.Length);

			string write4 = ";\n";
			byte[] info4 = new UTF8Encoding(true).GetBytes(write4);
			sf.Write(info4, 0, info4.Length);
		}


		public override bool Equals(object obj)
		{
			Tantargy eq = obj as Tantargy;
			return this.Nev.Equals(eq.Nev);
		}

		public override int GetHashCode()
		{ return this.tantargySorszama; }

		public dynamic GetParam(int k) //https://stackoverflow.com/questions/744401/dynamic-return-type-of-a-function    2. answer
		{
			switch (k)
			{
				case 0:
					return this.Nev;
				case 1:
					return this.Kreditertek;
				case 2:
					return this.TargyKod;
				case 3:
					return this.Oraszam;
				case 4:
					return this.Kovetelmeny;
				default:
					return 0;
			}
		}
	}
}
