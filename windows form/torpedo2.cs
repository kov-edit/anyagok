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

namespace torpedoJatek
{
    public partial class Form1 : Form
    {
        static CheckBox[,] jatekos = new CheckBox[10, 10];
        static CheckBox[,] ellenfel = new CheckBox[10, 10];
        static CheckBox[,] segitseg = new CheckBox[10, 10];
        static int[] maxhosszok = { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
        static int[] hosszok = new int[10];
        Button jatek = new Button();
        Button vege = new Button();
        string[] palyak = System.IO.File.ReadAllLines("palya.txt", Encoding.UTF8);
        int valasztott = 0;


        public Form1()
        {
            InitializeComponent();

            //előrébb van a gombok mint a mátrixok, mert másképp a gomb nem aktiválódik amikor jó helyen van minden hajó (ahogy kell neki)
            jatek.Location = new Point(200, 40);
            jatek.Size = new Size(100, 30);
            jatek.Text = "Játék";
            jatek.Enabled = false;
            jatek.Click += new System.EventHandler(jatek_Click);
            Controls.Add(jatek);

            vege.Location = new Point(320, 40);
            vege.Size = new Size(100, 30);
            vege.Text = "Vége";
            //vege.Click += new System.EventHandler(vege_Click);
            Controls.Add(vege);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    jatekos[i, j] = new CheckBox();
                    jatekos[i, j].Location = new Point(75 + i * 20, 120 + j * 20);
                    jatekos[i, j].Size = new Size(20, 20);
                    jatekos[i, j].CheckedChanged += new System.EventHandler(jatekos_CheckedChange);
                    Controls.Add(jatekos[i, j]);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    ellenfel[i, j] = new CheckBox();
                    ellenfel[i, j].Location = new Point(320 + i * 20, 120 + j * 20);
                    ellenfel[i, j].Size = new Size(20, 20);
                    ellenfel[i, j].Enabled = false;
                    ellenfel[i, j].CheckedChanged += new System.EventHandler(ellenfelChange);
                    Controls.Add(ellenfel[i, j]);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    segitseg[i, j] = new CheckBox();
                    segitseg[i, j].Location = new Point(200 + i * 20, 350 + j * 20);
                    segitseg[i, j].Size = new Size(20, 20);
                    Controls.Add(segitseg[i, j]);
                }
            }

            jatekos[2, 2].Checked = true;

            jatekos[4, 0].Checked = true;
            jatekos[5, 0].Checked = true;
            jatekos[6, 0].Checked = true;

            jatekos[4, 5].Checked = true;
            jatekos[5, 5].Checked = true;

            jatekos[2, 8].Checked = true;
            jatekos[3, 8].Checked = true;
            jatekos[4, 8].Checked = true;
            jatekos[5, 8].Checked = true;

            jatekos[8, 2].Checked = true;
            jatekos[8, 3].Checked = true;
            jatekos[8, 4].Checked = true;
            jatekos[8, 5].Checked = true;
            jatekos[8, 6].Checked = true;

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void jatek_Click (object sender, EventArgs e)
        {
           
            foreach (var item in jatekos) item.BackColor = Color.White;
            foreach (var item in jatekos)
            {
                item.Enabled = false;
            }
            foreach (var item in ellenfel)
            {
                item.Enabled = true;
            }
            valasztott = new Random().Next(0, palyak.Length);
            int ii = 0;
            foreach (var item in segitseg)
            {
                item.Checked = palyak[valasztott][ii] == '1';
                ii++;
            }

        }

        private void ellenfelChange(object sender, System.EventArgs e)
        {
            int poz = 0;
            foreach (var item in ellenfel)
            {
                if (item.Enabled && item.Checked)
                {
                    item.Enabled = false;
                    break;
                }
                else poz++;
            }

            if (palyak[valasztott][poz] == '1')
            {
                ellenfel[poz / 10, poz % 10].BackColor = Color.DeepPink;  // / = sor, % = oszlop

                bool x = false;  //oszlop
                bool y = false;  //sor

                int maxOszlop = 0;
                int minOszlop = 0;
                int maxSor = 0;
                int minSor = 0;

                for (global::System.Int32 i = (poz / 10) + 1; i < 10; i++)  //jobbra megy
                {
                    if (segitseg[i, poz % 10].Checked) x = true;  //jobbra ha talál hajót
                    else if (!segitseg[i, poz % 10].Checked)  //ha nem
                    {
                        maxOszlop = i - 1;
                        break;
                    }
                }

                for (global::System.Int32 i = (poz / 10) - 1; i >= 0; i--)  //balra megy
                {
                    if (segitseg[i, poz % 10].Checked) x = true;
                    else if (!segitseg[i, poz % 10].Checked)
                    {
                        minOszlop = i + 1;
                        break;
                    }
                }

                if (!y)
                {
                    for (global::System.Int32 i = (poz % 10) - 1; i >= 0; i--)  //fel megy
                    {
                        if (segitseg[poz / 10, i].Checked) y = true;
                        else if (!segitseg[poz / 10, i].Checked)
                        {
                            minSor = i + 1;
                            break;
                        }
                    }

                    for (global::System.Int32 i = (poz % 10) + 1; i < 10; i++)  //le megy
                    {
                        if (segitseg[poz / 10, i].Checked) y = true;
                        else if (!segitseg[poz / 10, i].Checked)
                        {
                            maxSor = i - 1;
                            break;
                        }
                    }
                }

                if (!x && !y)
                {
                    ellenfel[poz / 10, poz % 10].BackColor = Color.Blue;
                    return;
                }

                if (x)
                {
                    //nem működik még ami benne van
                    bool sullyed = true;
                    for (int i = minSor; i <= maxSor; i++)
                    {
                        if (ellenfel[poz / 10, i].BackColor != Color.DeepPink)
                        {
                            sullyed = false;
                            break;
                        }
                    }

                    if (!sullyed) return;

                    for (int i = minSor; i <= maxSor; i++)
                    {
                        ellenfel[poz / 10, i].BackColor = Color.Blue;
                    }
                }

            }
            else
            {
                ellenfel[poz / 10, poz % 10].BackColor = Color.SandyBrown;
                return;
            }



        }

        private bool ellenorzes(CheckBox[,] board)
        {
            Array.Clear(hosszok, 0, hosszok.Length);
            bool[,] megszamolt = new bool[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (!jatekos[i, j].Checked || megszamolt[i, j]) continue;

                    int horizontalLength = 1;
                    while (j + horizontalLength < 10 && jatekos[i, j + horizontalLength].Checked && !megszamolt[i, j + horizontalLength])
                    {

                        if ((i > 0 && jatekos[i - 1, j + horizontalLength].Checked) || (i < 9 && jatekos[i + 1, j + horizontalLength].Checked))
                        {
                            MessageBox.Show("A hajók nem érhetnek össze!");
                            return false;
                        }
                        megszamolt[i, j + horizontalLength] = true;
                        horizontalLength++;
                    }
                    if (horizontalLength > 1)
                    {
                        hosszok[horizontalLength - 1]++;
                        continue;
                    }

                    int verticalLength = 1;
                    while (i + verticalLength < 10 && jatekos[i + verticalLength, j].Checked && !megszamolt[i + verticalLength, j])
                    {

                        if ((j > 0 && jatekos[i + verticalLength, j - 1].Checked) ||
                            (j < 9 && jatekos[i + verticalLength, j + 1].Checked))
                        {
                            MessageBox.Show("A hajók nem érhetnek össze!");
                            return false;
                        }
                        megszamolt[i + verticalLength, j] = true;
                        verticalLength++;
                    }
                    if (verticalLength > 1)
                    {
                        hosszok[verticalLength - 1]++;
                        continue;
                    }

                    hosszok[0]++;
                }
            }
            return maxhosszok.SequenceEqual(hosszok);
        }

        private void jatekos_CheckedChange(object sender, System.EventArgs e)
        {
            int db = 0;

            bool joE = ellenorzes(jatekos);

            foreach (var item in jatekos)
            {
                if (item.Checked) db++;
            }

            if (db != 15)
            {
                foreach (var item in jatekos) item.BackColor = Color.Red;
            }
            else if (joE)
            {
                foreach (var item in jatekos) item.BackColor = Color.SeaGreen;
                jatek.Enabled = true;
            }
        }
    }
}
