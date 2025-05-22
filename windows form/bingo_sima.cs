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

namespace bingo2
{
    public partial class Form1 : Form
    {
        public int[] tól = new int[] { 1, 16, 31, 46, 61 };  //megadjuk a kezdőszámokat
        public int[] ig = new int[] { 16, 31, 46, 61, 76 };  //megadjuk a végszámokat

        public int[,] szamok = new int[5, 5];

        TextBox[,] boxes = new TextBox[5, 5];  //textboxok létrehozása

        public TextBox txbFileName = new TextBox();
        public Form1()
        {
            InitializeComponent();
        }

        private void kozepso()
        {
            boxes[2, 2].Text = "X";
            boxes[2, 2].Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Bingo";
            Size = new Size(220, 320);  //ablak mérete
            MinimumSize = Size;
            MaximumSize = Size;
            ClientSize = new Size(200, 301);  //a "munkaterület" mérete

            //alapvető gombok
            Button btnGeneral = new Button();
            btnGeneral.Text = "Kártya generálása";
            btnGeneral.Size = new Size(150, 50);
            btnGeneral.Location = new Point(25, 10); //x koordináta 25, y koordináta 10
            btnGeneral.Click += new EventHandler(btnGeneral_Click); //lenti függvény
            Controls.Add(btnGeneral);  //a vezérlőhöz adja a gombot

            Button btnSave = new Button();
            btnSave.Text = "Mentés";
            btnSave.Size = new Size(150, 50);
            btnSave.Location = new Point(25, 231);
            btnSave.Click += new EventHandler(btnSave_Click);
            Controls.Add(btnSave);


            txbFileName.Text = "bingo.txt";
            txbFileName.Size = new Size(150, 50);
            txbFileName.Location = new Point(25, 211);
            Controls.Add(txbFileName);



            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    boxes[i, j] = new TextBox();
                    boxes[i, j].Text = i.ToString()+j.ToString();
                    boxes[i, j].Size = new Size(25, 25);
                    boxes[i, j].Location = new Point(25 + i * 31, 60 + j * 31);  //i = oszlop, j = sor
                    boxes[i, j].Visible = false;
                    boxes[i, j].AutoSize = false;
                    boxes[i, j].TextAlign =HorizontalAlignment.Center;

                    Controls.Add(boxes[i, j]);
                }
            }
        }
        private void btnGeneral_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            for (int i = 0; i < 5; i++)  //y koordináta
            {
                HashSet<int> set = new HashSet<int>();
                for (int j = 0; j < 5; j++)  //x koordináta
                {
                    int hossz = set.Count;
                    int a = 0;
                    while (set.Count != hossz + 1)
                    {
                        a = rnd.Next(tól[i], ig[i]);  //számok abba a tartományban lesznek létrehozva, oszloponként nézzük
                        set.Add(a);
                    }
                    boxes[i, j].Visible = true;
                    boxes[i, j].Text = a.ToString();
                    szamok[i, j] = a;
                }
            }
            kozepso();



            foreach (var item in boxes)
            {
                item.LostFocus += new EventHandler(boxes_TextChanged);  //amikor kilépünk az adott textbox-ből, akkor fut le
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            StreamWriter kiir = new StreamWriter(txbFileName.Text, false, Encoding.UTF8);

            int ii = 0;
            foreach (var item in boxes)  //rossz irányba írja ki
            {
                ii++;
                if(ii % 5 == 0)
                {
                    kiir.WriteLine(item.Text);
                    continue;
                }
                else kiir.Write($"{item.Text}; ");
            }

            kiir.Close();
        }

        private void boxes_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool hiba = false;
                for (int i = 0; i < 5; i++)  //ha olyan számot adunk az adott sorba, ami nagyobb vagy kisebb mint lehetne, visszaírja az eredetire
                {
                    if (hiba) break;

                    for (int j = 0; j < 5; j++)
                    {
                        if (i == 2 && j == 2) continue;  //csak megy a következőre

                        if (int.Parse(boxes[i, j].Text) < tól[i] || int.Parse(boxes[i, j].Text) >= ig[i])
                        {
                            boxes[i, j].Text = szamok[i, j].ToString();
                            kozepso();
                            hiba = true;
                        }


                        HashSet<string> vizsga = new HashSet<string>();
                        for (int k = 0; k < 5; k++)
                        {
                            vizsga.Add(boxes[i, k].Text);
                        }
                        if (vizsga.Count != 5)
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                boxes[i, k].Text = szamok[i, k].ToString();
                            }
                            kozepso();
                            hiba = true;
                        }

                        /*for (int k = 0; k < 5; k++)  //ugyan azt csinálja mint a fenti hashset-es
                        {
                            if (j == k) continue;
                            if (int.Parse(boxes[i, j].Text) == szamok[i, k])
                            {
                                boxes[i, j].Text = szamok[i, j].ToString();
                                hiba = true;
                            }
                        }*/

                        if (hiba) break;
                    }

                    for (int k = 0; k < 5; k++)
                    {
                        if (i == 2 && k == 2) continue;
                        szamok[i, k] = int.Parse(boxes[i, k].Text);
                    }
                }
            }
            catch (Exception)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        boxes[i, j].Text = szamok[i, j].ToString();
                    }
                    kozepso();
                }
            }

        }
    }
}
