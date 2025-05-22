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

namespace barlang_kozos
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public void kiiras()
        {
            lbldarab.Visible = true;
            lbldarab.Text = $": {lista.Count} db";
        }

        class Barlang
        {
            public int azon { get; private set; }
            public string nev { get; private set; }
            public string telepules { get; private set; }
            public string vedettseg { get; set; }

            private int h = 0;
            public int hossz
            {
                get
                {
                    return h;
                }
                set
                {
                    if (h <= value || value == 0)
                    {
                        h = value;
                    }
                }
            }
            private int m = 0;
            public int melyseg
            {
                get
                {
                    return m;
                }
                set
                {
                    if (m < value)
                    {
                        m = value;
                    }
                }
            }

            public Barlang(string sorok)
            {
                try
                {
                    string[] sor = sorok.Split(';');
                    azon = int.Parse(sor[0]);
                    nev = sor[1];
                    hossz = int.Parse(sor[2]);
                    melyseg = int.Parse(sor[3]);
                    telepules = sor[4];
                    vedettseg = sor[5];
                }
                catch (Exception)
                {
                    hossz = 0;
                }

            }

            public override string ToString()
            {
                return $"Azon: {azon}\nNév: {nev}\nHossz: {hossz}\nMélység: {melyseg}\nTelepülés: {telepules}\nVédettség: {vedettseg}";
            }
        }

        static List<Barlang> lista = new List<Barlang>();
        static SortedSet<string> vedettseg = new SortedSet<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Barlangok";
            lbldarab.Visible = false;
            checkedListBox1.Visible = false;
            richTextBox1.Visible = false;
            //masiklabel.Visible = false;
            checkedListBox1.AutoSize = true;
        }


        private void filelabel_Click(object sender, EventArgs e)
        {
            /*
            openFileDialog1.ShowDialog(this)

           StreamReader sr = new StreamReader("..\\..\\..\\barlangok.txt", Encoding.UTF8);
           while (!sr.EndOfStream)
           {
               Barlang tmp = new Barlang(sr.ReadLine());
               if (tmp.hossz != 0)
               {
                   barlangok.Add(tmp);
               }
           }

           sr.Close();

           kiiras();

           //console feladatok megoldása:
           //3. feladat    
                       int db = 1;
           int melysegek = 0;

           for (int i = 0; i < barlangok.Count; i++)
           {
               if (barlangok[i].telepules == "Miskolc")
               {
                   melysegek += i;
                   db ++;
               }
           }

           double atlag = melysegek / db;

            */

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader beolvas = new StreamReader(ofd.FileName);
                    while (!beolvas.EndOfStream)
                    {
                        Barlang tmp = new Barlang(beolvas.ReadLine());
                        if (tmp.hossz != 0)
                        {
                            lista.Add(tmp);
                        }
                    }
                    filelabel.Text = ofd.FileName;
                    beolvas.Close();
                }
                catch
                {
                    MessageBox.Show("Nem megfelő fájl");
                }

            }
        }

    }
}
