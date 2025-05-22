using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace szalloda
{
    //27 szoba, 1-31 napok a hónapban kiválasztva, checkbox hogy melyik szoba milyen állapotban van (foglalt, nem kifizetett (ami itt van mind kivan fizetve) stb),
    //reggeli: főszezoni szobaár 10%-a, pótágy: szezon 10%-a,
    //szinek: piros - foglalt, sárga - telefonon foglalt nem fizetett, zöld - szabad
    //statisztika: éves teljes bevétel, éves %-os foglaltság szobánként, éves szálloda komplett (%-os foglalt napok száma)
                   //éves reggelik száma, vendégek száma, vendégéjszaka
    public partial class Form1 : Form
    {
        Button telefon = new Button();
        Button statisztika = new Button();
        Button foglalasok = new Button();
        Button kilepes = new Button();

        ComboBox evek = new ComboBox();
        ComboBox honap = new ComboBox();

        static Label[,] szazalek = new Label[27, 12];
        static Label[] napok = new Label[32];
        static Label[] szobaszamok = new Label[28];
        string kivalasztottEv = "";
        string kivalasztottHonap = "";

        //List<int> szobaElfogaltsag = new List<int>();   jó ötletnek tűnt
        List<Adatok> ev2023 = new List<Adatok>();
        List<Arak> arak = new List<Arak>();
        List<string> honapok = new List<string> {"Január", "Február", "Március", "Április", "Május", "Június", "Július", "Augusztus", "Szeptember", "Október", "November", "December"};
        List<int> honapLength = new List<int> {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        List<int> szokoevekHonapLength = new List<int> { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        static Label[] honapokKiir = new Label[12];
        static Label[,] szazalekFoglalt = new Label[13, 27];
        static double[,] szamok = new double[13, 27];

        CheckBox[,] szobak = new CheckBox[27, 31];
        Label[,] stat_szobak = new Label[27, 32];
        Label potagyak = new Label();
        Label[] potagyOssz = new Label[27];
        static int[] potagyOsszInt = new int[27];
        public int osszesBevetel = 0;
        public int potagyakSzama = 0;
        public int igaziBevetel = 0;
        Label evesbevetel = new Label();
        Label evesreggelik = new Label();
        Label evesvendegek = new Label();
        Label evesvendegejszakak = new Label();

        TextBox nev = new TextBox();
        Label foglalasAr = new Label();
        Button foglalas = new Button();

        static int reggelik = 0;
        //pótágy alkalom valami számláló


        class Adatok
        {
            public int sorszam;
            public int ev;
            public int szobaszam;
            public int erkezes;
            public int tavozas;
            public int lakok;
            public int reggeli;
            public bool potagy;
            public string name;
            public int ErkezesHonapNap;
            public int TavozasHonapNap;
            public int ejszakak;
            public Adatok(string dLines)
            {
                string[] lines = dLines.Split(' ');
                string[] b = lines[0].Split('/');
                this.sorszam = Convert.ToInt32(b[0]);
                this.ev = Convert.ToInt32(b[1]);
                this.szobaszam = Convert.ToInt32(lines[1]);
                this.erkezes = Convert.ToInt32(lines[2]);
                this.tavozas = Convert.ToInt32(lines[3]);
                this.lakok = Convert.ToInt32(lines[4]);
                this.reggeli = Convert.ToInt32(lines[5]);
                if (Convert.ToInt32(lines[5]) == 1)
                {
                    reggelik++;
                }
                
                if (Convert.ToInt32(lines[4]) > 2)
                {
                    this.potagy = true;
                }
                else
                {
                    this.potagy = false;
                }
                this.ejszakak = this.tavozas - this.erkezes;
            }
        }


        class Arak
        {
            //2023 elő = 120, 2024 elő = 121, 2023, 2024 fő = 123, 2023, 2024 utó = 122
            public string szezon;
            public string kezdet;
            public string vege;
            public int ar;
            public Arak(string sorok)
            {
                string[] sor = sorok.Split(';');
                this.szezon = sor[0];
                this.kezdet = sor[1];
                this.vege = sor[2];
                this.ar = int.Parse(sor[3]);
            }
        }

        public Form1()
        {
            InitializeComponent();

            telefon.Location = new Point(20, 120);
            telefon.Text = "Telefon";
            telefon.Size = new Size(100, 40);
            telefon.Click += new System.EventHandler(telefon_Click);
            Controls.Add(telefon);

            statisztika.Location = new Point(20, 190);
            statisztika.Text = "Statisztika";
            statisztika.Size = new Size(100, 40);
            statisztika.Click += new System.EventHandler(statisztika_Click);
            Controls.Add(statisztika);

            foglalasok.Location = new Point(20, 260);
            foglalasok.Text = "Foglalások";
            foglalasok.Size = new Size(100, 40);
            foglalasok.Click += new System.EventHandler(foglalasok_Click);
            Controls.Add(foglalasok);

            kilepes.Location = new Point(20, 330);
            kilepes.Text = "Kilépés";
            kilepes.Size = new Size(100, 40);
            kilepes.Click += new System.EventHandler(kilepes_Click);
            Controls.Add(kilepes);

            for (int i = 2023; i < 2051; i++)
            {
                evek.Items.Add(i);
            }
            evek.SelectedIndex = 0;


            foreach (string s in honapok)
            {
                honap.Items.Add(s.ToString());
            }
            honap.SelectedIndex = 0;

            this.Size = new Size(900, 500);

            for (int i = 1; i < 32; i++)
            {
                napok[i] = new Label();
                napok[i].Text = i.ToString();
                napok[i].Size = new Size(20, 20);
                napok[i].Location = new Point(168 + i * 20, 90);
                napok[i].Visible = false;
                Controls.Add(napok[i]);
            }

            for (int i = 0; i < 12; i++)
            {
                honapokKiir[i] = new Label();
                honapokKiir[i].AutoSize = true;
                honapokKiir[i].Text = honapok[i].ToString();
                honapokKiir[i].Location = new Point(190 + i * 70, 90);
                honapokKiir[i].Visible = false;
                Controls.Add(honapokKiir[i]);
            }

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    szazalekFoglalt[i, j] = new Label();
                    szazalekFoglalt[i, j].Text = szamok[i, j].ToString();
                    szazalekFoglalt[i, j].Visible = false;
                    szazalekFoglalt[i, j].Size = new Size(20, 20);
                    szazalekFoglalt[i, j].Location = new Point(200 + i * 70, 115 + j * 25);
                    Controls.Add(szazalekFoglalt[i, j]);
                }
            }

            for (int i = 1; i < 28; i++)
            {
                szobaszamok[i] = new Label();
                szobaszamok[i].Text = i.ToString() + ". szoba";
                szobaszamok[i].Visible = false;
                szobaszamok[i].Size = new Size(70, 20);
                szobaszamok[i].Location = new Point(125, 90 + i * 20);
                Controls.Add(szobaszamok[i]);
            }

            for (int i = 0; i < 27; i++)
            {
                potagyOssz[i] = new Label();
                potagyOssz[i].Text = potagyOsszInt[i].ToString();
                potagyOssz[i].Location = new Point(1040, 115 + i * 25);
                potagyOssz[i].Visible = false;
                Controls.Add(potagyOssz[i]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader("foglalasokv2.txt");

            while (!reader.EndOfStream)
            {
                try
                {
                    ev2023.Add(new Adatok(reader.ReadLine()));
                }
                catch (Exception)
                {
                    continue;
                }
            }

            reader.Close();

            StreamReader beolvas = new StreamReader("arak.txt");
            while (!beolvas.EndOfStream)
            {
                try
                {
                    arak.Add(new Arak(beolvas.ReadLine()));
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        private void torles()
        {
            //nem jó ez így
            List<Control> controlsToRemove = new List<Control>();
            foreach (Control control in Controls)
            {
                if (control is CheckBox)
                {
                    controlsToRemove.Add(control);
                }
            }
            foreach (Control control in controlsToRemove)
            {
                Controls.Remove(control);
                control.Dispose();
            }

        }

        public void foglalasokfgv()
        {
            torles();
            int evItem = Convert.ToInt32(evek.SelectedItem);
            int hoIndex = Convert.ToInt32(honap.SelectedIndex);

            int napokSzama = honapLength[hoIndex];
            if (((evItem % 4 == 0 && evItem % 100 != 0) || evItem % 400 == 0) && hoIndex == 1)
            {
                napokSzama = 29;
            }


            for (int i = 0; i < 27; i++)
            {
                for (int j = 0; j < napokSzama; j++)
                {
                    szobak[i, j] = new CheckBox();
                    szobak[i, j].Location = new Point(190 + j * 20, 110 + i * 20);
                    szobak[i, j].Size = new Size(20, 20);
                    szobak[i, j].Visible = true;
                    szobak[i, j].BackColor = Color.LightGreen;
                    Controls.Add(szobak[i, j]);
                }
            }


            string Evhanyadiknapja(int a, int evs)
            {
                DateTime date = new DateTime(evs, 1, 1).AddDays(a - 1);
                int honap = date.Month;
                int nap = date.Day;
                return $"{honap}-{nap}";
            }

            int length = ev2023.Count;
            for (int i = 0; i < length; i++)
            {
                string[] hoNapErk = (Evhanyadiknapja(ev2023[i].erkezes, ev2023[i].ev)).Split('-');
                string[] hoNapTav = (Evhanyadiknapja(ev2023[i].tavozas, ev2023[i].ev)).Split('-');
                int erkHo = Convert.ToInt32(hoNapErk[0]);
                int erkNap = Convert.ToInt32(hoNapErk[1]);
                int tavHo = Convert.ToInt32(hoNapTav[0]);
                int tavNap = Convert.ToInt32(hoNapTav[1]);

                if (ev2023[i].ev == evItem && erkHo == (hoIndex + 1))
                {
                    for (int j = -1; j < 28; j++)
                    {
                        if (ev2023[i].szobaszam == j)
                        {
                            if (erkHo == tavHo)
                            {
                                for (int k = erkNap - 1; k < tavNap; k++)
                                {
                                    szobak[j - 1, k].BackColor = Color.Red;
                                    szobak[j - 1, k].Checked = true;
                                }
                            }
                            else
                            {
                                    for (int k = erkNap - 1; k < napokSzama; k++)
                                    {
                                        szobak[j - 1, k].BackColor = Color.Red;
                                        szobak[j - 1, k].Checked = true;
                                    }
                                
                                    

                                    /*for (int k = 0; k < tavNap; k++)
                                    {
                                        szobak[j - 1, k].BackColor = Color.Red;
                                        szobak[j - 1, k].Checked = true;
                                    }*/
                            }
                        }
                    }
                }
            }
        }

        public void bevetel()
        {
            //2023 elő = 120, 2024 elő = 121, 2023, 2024 fő = 123, 2023, 2024 utó = 122
            var elo2023 = arak[0].kezdet;
            var fo2023 = arak[1].kezdet;
            var uto2023 = arak[2].kezdet;

            for (int i = 0; i < ev2023.Count; i++)
            {
                int ar = 0;
                int reggeliAr = arak[1].ar / 10;
                int potagyAr = 1;
                int emberenkent = 0;

                if (ev2023[i].erkezes <= 120)
                {
                    ar = arak[0].ar / 10;
                    if (ev2023[i].lakok > 2)
                    {
                        potagyAr = ev2023[i].ejszakak * ar;
                        potagyakSzama++;
                    }
                }

                if (ev2023[i].erkezes > 120 && ev2023[i].erkezes <= 243)
                {
                    ar = arak[1].ar / 10;
                    if (ev2023[i].lakok > 2)
                    {
                        potagyAr = ev2023[i].ejszakak * ar;
                        potagyakSzama++;
                    }
                }

                else
                {
                    ar = arak[2].ar / 10;
                    if (ev2023[i].lakok > 2)
                    {
                        potagyAr = ev2023[i].ejszakak * ar;
                        potagyakSzama++;
                    }
                }

                emberenkent = (ev2023[i].ejszakak * ev2023[i].lakok * ar) + (ev2023[i].ejszakak * ev2023[i].reggeli * reggeliAr) + potagyAr;

                osszesBevetel += emberenkent;
            }

           igaziBevetel = osszesBevetel;

        }

        public void szazalekosFoglaltsag()
        {
            string Evhanyadiknapja(int a, int evs)
            {
                DateTime date = new DateTime(evs, 1, 1).AddDays(a - 1);
                int honap = date.Month;
                int nap = date.Day;
                return $"{honap}-{nap}";
            }

            
            for (int i = 0; i < ev2023.Count; i++)
            {
                string[] hoNapErk = (Evhanyadiknapja(ev2023[i].erkezes, ev2023[i].ev)).Split('-');
                string[] hoNapTav = (Evhanyadiknapja(ev2023[i].tavozas, ev2023[i].ev)).Split('-');
                int erkHo = Convert.ToInt32(hoNapErk[0]);
                int tavHo = Convert.ToInt32(hoNapTav[0]);

                int foglaltNapok;
                if (ev2023[i].tavozas < 32)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        if (ev2023[i].szobaszam == j + 1)
                        {
                            foglaltNapok = ev2023[i].ejszakak;
                            szamok[0, j] += foglaltNapok;
                        }
                    }
                }

                else if (ev2023[i].tavozas > 32 && ev2023[i].tavozas < 60)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        if (ev2023[i].szobaszam == j + 1)
                        {
                            foglaltNapok = ev2023[i].ejszakak;
                            szamok[1, j] += foglaltNapok;
                        }
                    }
                }

                else if(ev2023[i].tavozas > 60 && ev2023[i].tavozas < 91)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        if (ev2023[i].szobaszam == j + 1)
                        {
                            foglaltNapok = ev2023[i].ejszakak;
                            szamok[2, j] += foglaltNapok;
                        }
                    }
                }

                else if(ev2023[i].tavozas > 91 && ev2023[i].tavozas < 121)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        if (ev2023[i].szobaszam == j + 1)
                        {
                            foglaltNapok = ev2023[i].ejszakak;
                            szamok[3, j] += foglaltNapok;
                        }
                    }
                }

                else if(ev2023[i].tavozas > 121 && ev2023[i].tavozas < 152)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        if (ev2023[i].szobaszam == j + 1)
                        {
                            foglaltNapok = ev2023[i].ejszakak;
                            szamok[4, j] += foglaltNapok;
                        }
                    }
                }

                else if(ev2023[i].tavozas > 152 && ev2023[i].tavozas < 182)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        if (ev2023[i].szobaszam == j + 1)
                        {
                            foglaltNapok = ev2023[i].ejszakak;
                            szamok[5, j] += foglaltNapok;
                        }
                    }
                }

                else if(ev2023[i].tavozas > 182 && ev2023[i].tavozas < 213)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        if (ev2023[i].szobaszam == j + 1)
                        {
                            foglaltNapok = ev2023[i].ejszakak;
                            szamok[6, j] += foglaltNapok;
                        }
                    }
                }

                else if(ev2023[i].tavozas > 213 && ev2023[i].tavozas < 244)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        if (ev2023[i].szobaszam == j + 1)
                        {
                            foglaltNapok = ev2023[i].ejszakak;
                            szamok[7, j] += foglaltNapok;
                        }
                    }
                }

                else if(ev2023[i].tavozas > 244 && ev2023[i].tavozas < 274)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        if (ev2023[i].szobaszam == j + 1)
                        {
                            foglaltNapok = ev2023[i].ejszakak;
                            szamok[8, j] += foglaltNapok;
                        }
                    }
                }

                else if(ev2023[i].tavozas > 274 && ev2023[i].tavozas < 305)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        if (ev2023[i].szobaszam == j + 1)
                        {
                            foglaltNapok = ev2023[i].ejszakak;
                            szamok[9, j] += foglaltNapok;
                        }
                    }
                }

                else if(ev2023[i].tavozas > 305 && ev2023[i].tavozas < 335)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        if (ev2023[i].szobaszam == j + 1)
                        {
                            foglaltNapok = ev2023[i].ejszakak;
                            szamok[10, j] += foglaltNapok;
                        }
                    }
                }

                else if(ev2023[i].tavozas > 335 && ev2023[i].tavozas < 365)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        if (ev2023[i].szobaszam == j + 1)
                        {
                            foglaltNapok = ev2023[i].ejszakak;
                            szamok[11, j] += foglaltNapok;
                        }
                    }
                }


            }
        }

        private void telefon_Click(object sender, System.EventArgs e)
        {
            potagyak.Visible = false;

            honap.Visible = true;
            evek.Location = new Point(325, 35);
            evek.SelectedIndexChanged += new System.EventHandler(ev_SelectedIndexChanged);
            Controls.Add(evek);

            honap.Location = new Point(480, 35);
            honap.Size = new Size(100, 40);
            honap.SelectedIndexChanged += new System.EventHandler(honap_SelectedIndexChanged);
            Controls.Add(honap);

            for (int i = 0; i < 12; i++)
            {
                honapokKiir[i].Visible = false;
            }

            for (int i = 1; i < 32; i++)
            {
                napok[i].Visible = true;
            }

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    szazalekFoglalt[i, j].Visible = false;
                }
            }

            for (int i = 1; i < 28; i++)
            {
                szobaszamok[i].Size = new Size(70, 20);
                szobaszamok[i].Location = new Point(125, 90 + i * 20);
                szobaszamok[i].Visible = true;
            }

            for (int i = 0; i < 27; i++)
            {
                potagyOssz[i].Visible = false;
            }

            evesbevetel.Visible = false;
            evesreggelik.Visible = false;
            evesvendegek.Visible = false;
            evesvendegejszakak.Visible = false;

            this.Size = new Size(1300, 750);



        }

        private void statisztika_Click(object sender, System.EventArgs e)
        {
            torles();
            
            honap.Visible = false;
            evek.Visible = true;
            evek.Location = new Point(1120, 100);

            this.Size = new Size(1400, 880);

            potagyak.Location = new Point(1020, 90);
            potagyak.Text = "Pótágyak";
            potagyak.Visible = true;
            Controls.Add(potagyak);

            for (int i = 1; i < 28; i++)
            {
                szobaszamok[i].Size = new Size(70, 20);
                szobaszamok[i].Location = new Point(125, 90 + i * 25);
                szobaszamok[i].Visible = true;
            }

            for (int i = 1; i < 32; i++)
            {
                napok[i].Visible = false;
            }

            for (int i = 0; i < 12; i++)
            {
                honapokKiir[i].Visible = true;
            }

            //éves bevétel számolás
            bevetel();

            evesbevetel.Text = $"Éves bevétel: {igaziBevetel} ";
            evesbevetel.Visible = true;
            evesbevetel.AutoSize = true;
            evesbevetel.Location = new Point(1120, 150);
            Controls.Add(evesbevetel);

            evesreggelik.Text = $"Éves reggelik száma: {reggelik}";
            evesreggelik .Visible = true;
            evesreggelik.AutoSize = true;
            evesreggelik.Location = new Point(1120, 180);
            Controls.Add(evesreggelik);

            int osszVendeg = 0;
            for (int i = 1; i < ev2023.Count; i++)
            {
                osszVendeg += ev2023[i].lakok;
            }

            evesvendegek.Text = $"Éves vendégek száma: {osszVendeg}";
            evesvendegek.Visible = true;
            evesvendegek.AutoSize = true;
            evesvendegek.Location = new Point(1120, 210);
            Controls.Add(evesvendegek);

            int osszEjszaka = 0;
            for (int i = 1; i < ev2023.Count; i++)
            {
                osszEjszaka += ev2023[i].ejszakak;
            }
            evesvendegejszakak.Text = $"Éves vendégéjszakák száma: {osszEjszaka}";
            evesvendegejszakak.Visible = true;
            evesvendegejszakak.AutoSize = true;
            evesvendegejszakak.Location = new Point(1120, 240);
            Controls.Add(evesvendegejszakak);

            szazalekosFoglaltsag();

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    szazalekFoglalt[i, j].Text = szamok[i, j].ToString();
                    szazalekFoglalt[i, j].Visible = true;
                }
            }

            for (int i = 0; i < ev2023.Count; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    if (ev2023[i].szobaszam == j+1 && ev2023[i].potagy)
                    {
                        potagyOsszInt[j] += ev2023[i].ejszakak;
                    }
                }
            }

            for (int i = 0; i < 27; i++)
            {
                potagyOssz[i] = new Label();
                potagyOssz[i].Text = potagyOsszInt[i].ToString();
                potagyOssz[i].Location = new Point(1040, 115 + i * 25);
                potagyOssz[i].Visible = true;
                Controls.Add(potagyOssz[i]);
            }
        }

        private void foglalasok_Click(object sender, System.EventArgs e)
        {
            potagyak.Visible = false;

            honap.Visible = true;
            evek.Location = new Point(325, 35);
            evek.SelectedIndexChanged += new System.EventHandler(ev_SelectedIndexChanged);
            Controls.Add(evek);

            honap.Location = new Point(480, 35);
            honap.Size = new Size(100, 40);
            honap.SelectedIndexChanged += new System.EventHandler(honap_SelectedIndexChanged);
            Controls.Add(honap);

            for (int i = 0; i < 12; i++)
            {
                honapokKiir[i].Visible = false;
            }

            for (int i = 1; i < 32; i++)
            {
                napok[i].Visible = true;
            }

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    szazalekFoglalt[i, j].Visible = false;
                }
            }

            for (int i = 1; i < 28; i++)
            {
                szobaszamok[i].Size = new Size(70, 20);
                szobaszamok[i].Location = new Point(125, 90 + i * 20);
                szobaszamok[i].Visible = true;
            }

            for (int i = 0; i < 27; i++)
            {
                potagyOssz[i].Visible = false;
            }

            evesbevetel.Visible = false;
            evesreggelik.Visible = false;
            evesvendegek.Visible = false;
            evesvendegejszakak.Visible = false;

            this.Size = new Size(900, 750);
        }



        private void ev_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            
                foglalasokfgv();
            
        }

        private void honap_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            
                foglalasokfgv();
            
        }

        private void kilepes_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}
