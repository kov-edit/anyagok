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

namespace LabirintusGUI
{
    public partial class Form1 : Form
    {

        CheckBox[,] labirintus = new CheckBox[20, 20];

        public Form1()
        {
            InitializeComponent();
        }

        private void ujrairas (int oszlopok, int sorok)
        {

            for (int i = 0; i < oszlopok; i++)
            {
                for (int j = 0; j < sorok; j++)
                {
                    Controls.Remove(labirintus[i, j]);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //generálás készítése előtt a controls (checkboxok) üresek legyenek


            
        }

        private void letrehozas_Click(object sender, EventArgs e)
        {
           int sorok = int.Parse(sor.Text);
           int oszlopok = int.Parse(oszlop.Text);

            ujrairas(oszlopok, sorok);


            for (int i = 0; i < oszlopok; i++)
            {
                for (int j = 0; j < sorok; j++)
                {
                    labirintus[i, j] = new CheckBox();
                    labirintus[i, j].Size = new Size(25, 25);
                    labirintus[i, j].Location = new Point(15 + i * 25, 100 + j * 25);  //i = oszlop, j = sor
                    labirintus[i, j].Visible = false;

                    Controls.Add(labirintus[i, j]);
                }
            }

            for (int i = 0; i < oszlopok; i++)
            {
                for (int j = 0; j < sorok; j++)
                {
                    if (i == 0 && j == 1)
                    {
                        labirintus[i, j].Visible = true;
                        labirintus[i, j].Enabled = false;
                    }
                    else if (i == oszlopok - 1 && j == sorok - 2)
                    {
                        labirintus[i, j].Visible = true;
                        labirintus[i, j].Enabled = false;
                    }

                    else if (j == 0 || i == 0 || j == sorok - 1 || i == oszlopok - 1)
                    {
                        labirintus[i, j].Visible = true;
                        labirintus[i, j].Enabled = false;
                        labirintus[i, j].Checked = true;
                    }

                    else
                    {
                        labirintus[i, j].Visible = true;
                        labirintus[i, j].Enabled = true;
                    }
                }
            }

        }

        private void sor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mentes_Click(object sender, EventArgs e)
        {
            int fajlIndex = int.Parse(index.Text);
            StreamWriter kiiras = new StreamWriter($"Lab{fajlIndex}.txt", false, Encoding.UTF8);

            int sorok = int.Parse(sor.Text);
            int oszlopok = int.Parse(oszlop.Text);

            try
            {
                for (int i = 0; i < oszlopok; i++)
                {
                    for (int j = 0; j < sorok; j++)
                    {
                        if (labirintus[i, j].Checked) kiiras.Write("X");  //fordítva írja ki
                        else kiiras.Write(".");
                    }
                    kiiras.WriteLine();
                }
                MessageBox.Show("Az állomány mentése sikeres");
            }
            catch (Exception)
            {

                MessageBox.Show("Hiba");
            }

            kiiras.Close();
        }
    }
}
