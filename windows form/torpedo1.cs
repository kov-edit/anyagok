using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace torpedo
{
    public partial class Form1 : Form
    {
        CheckBox[,] boxes = new CheckBox[10, 10];
        static int[] maxhosszok = { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
        static int[] hosszok = new int[10];

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    boxes[i, j] = new CheckBox();
                    boxes[i, j].Size = new Size(20, 20);
                    boxes[i, j].Location = new Point(25 + i * 20, 95 + j * 20);  //i = x, j = y
                    boxes[i, j].AutoSize = false;
                    boxes[i, j].CheckedChanged += new System.EventHandler(boxes_CheckedChange);
                    Controls.Add(boxes[i, j]);
                }
            }
        }

        private bool ellenorzes(CheckBox[,] board)
        {
            Array.Clear(hosszok, 0, hosszok.Length);
            bool[,] megszamolt = new bool[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0;j < 10; j++)
                {
                    if (!boxes[i, j].Checked || megszamolt[i, j]) continue;

                    int horizontalLength = 1;
                    while (j + horizontalLength < 10 && boxes[i, j + horizontalLength].Checked && !megszamolt[i, j + horizontalLength])
                    {
                        
                        if ((i > 0 && boxes[i - 1, j + horizontalLength].Checked) || (i < 9 && boxes[i + 1, j + horizontalLength].Checked))
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
                    while (i + verticalLength < 10 && boxes[i + verticalLength, j].Checked && !megszamolt[i + verticalLength, j])
                    {
                        
                        if ((j > 0 && boxes[i + verticalLength, j - 1].Checked) ||
                            (j < 9 && boxes[i + verticalLength, j + 1].Checked))
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadBoard(CheckBox[,] board)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    boxes[i, j].Checked = board[i, j].Checked;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool joE = ellenorzes(boxes);

            if (joE)
            {
                StreamWriter kiir = new StreamWriter(fileName.Text, true, Encoding.UTF8);
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        kiir.Write(boxes[i, j].Checked ? "1" : "0");

                    }
                }
                kiir.Write("\n");
                kiir.Close();
                torles();
                MessageBox.Show("Sikeres mentés");
            }
            else
            {
                MessageBox.Show("Nem jó a hajók elhelyezkedése");
            }
        }

        private void torles()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    boxes[i, j].Checked = false;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            torles();
        }

        private void fileName_TextChanged(object sender, EventArgs e)
        {
            if (fileName.Text.Length > 0) btnSave.Enabled = true;
            else btnSave.Enabled = false;
        }

        private void boxes_CheckedChange(object sender, System.EventArgs e)
        {
            int db = 0;

            bool joE = ellenorzes(boxes);

            foreach (var item in boxes)
            {
                if (item.Checked) db++;
            }

            if (db != 15)
            {
                foreach (var item in boxes) item.BackColor = Color.Red;
            }
            else if (joE) 
            {
                foreach (var item in boxes) item.BackColor = Color.Green;
            }
        }
    }
}
