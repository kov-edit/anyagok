using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator2024
{
    public partial class Calculator2024 : Form
    {
        public Calculator2024()
        {
            InitializeComponent();
        }

        #region változók
        static int sign = 0;
        static int dot = 0;
        static double result = 0.0;
        static bool resultBool = false;
        static bool operBool = false;
        static string oper = "";
        static int btnColorIndex = 0;
        static List<Color> listColor = new List<Color>();
        #endregion

        static int length(string displaytext)
        {
            if (displaytext.Contains(","))
            {
                dot = 1;
            }
            else dot = 0;

            if (displaytext.Contains("-"))
            {
                sign = 1;
            }
            else sign = 0;

            return 14 + sign + dot;  //ilyen hosszig lehet elmenni
        }

        private void operation()
        {
            if (oper == "+")
            {
                result += double.Parse(txbDisplay.Text);
            }
            else if (oper == "/" && double.Parse(txbDisplay.Text) != 0)
            {
                result /= double.Parse(txbDisplay.Text);
            }
            else if (oper == "*")
            {
                result *= double.Parse(txbDisplay.Text);
            }
            else if (oper == "-")
            {
                result -= double.Parse(txbDisplay.Text);
            }

            string resultText = result.ToString();
            int resultLength = length(resultText);
            if (resultText.Length < resultLength) txbDisplay.Text = resultText;
            else txbDisplay.Text = resultText.Substring(0, resultLength);
            resultBool = true;

            if (double.Parse(txbDisplay.Text) == 0 || Math.Abs(result) > 99999999999999 || Math.Abs(result) < 0.0000000000000)
            {
                txbDisplay.Text = "hiba";
                result = 0;
                resultBool = false;
                operBool = false;
            }
        }

        private void Display(string btn)  //átküldjük a lenyomott gomb jelét
        {
            string textDisplay = txbDisplay.Text;  //átküldött szöveg
            int maxLength = length(textDisplay);
            if (btn == "+-")
            {
                txbDisplay.Text = (double.Parse(textDisplay) * -1).ToString();
                return;
            }

            if (btn == "C")
            {
                txbDisplay.Text = "0";
                return;
            }

            if (btn == "AC")
            {
                txbDisplay.Text = "0";
                result = 0;
                return;
            }

            //gyök
            if (btn == "gy")
            {
                result = Math.Sqrt(double.Parse(textDisplay));
                string resultText = result.ToString();
                int resultLength = length(resultText);

                if (resultText.Length < resultLength) txbDisplay.Text = resultText;  //ha rövidebb mint a max hossz, csak adig írja ki
                else txbDisplay.Text = resultText.Substring(0, resultLength);

                resultBool = true;
                operBool = false;
                return;
            }

            //osztás
            if (btn == "/")
            {
                if (!resultBool && !operBool)
                {
                    result = double.Parse(textDisplay);
                    operBool = true;
                    resultBool = true;
                    oper = "/";
                    return;
                }
                else if (resultBool && !operBool)
                {
                    operBool = true;
                    oper = "/";
                    return;
                }
                else if (resultBool && operBool)
                {
                    operation();
                    oper = "/";
                }
                return;
            }

            //szorzás
            if (btn == "*")
            {
                if (!resultBool && !operBool)
                {
                    result = double.Parse(textDisplay);
                    operBool = true;
                    resultBool = true;
                    oper = "*";
                    return;
                }
                else if (resultBool && !operBool)
                {
                    operBool = true;
                    oper = "*";
                    return;
                }
                else if (resultBool && operBool)
                {
                    operation();
                    oper = "*";
                }
                return;
            }


            if (textDisplay.Length == maxLength)  //ha több mint 14 karakter van a textboxban
            {
                return; //ez utáőn bármit nyom nem jelenik meg
            }

            else  //az itt lévők befolyásolhatják a hosszt, a fentieknek pedig folyamatosan le kell tudni futnia
            {
                //vessző
                if (btn == "," && dot == 0)
                {
                    txbDisplay.Text += ",";
                    return;
                }
                else if (btn == "," && dot == 1)
                {
                    return;
                }
                //számbillentyűk
                else
                {
                    if (textDisplay == "0") txbDisplay.Text = btn;  //kettő nulla az elején nem lehet egymás mellett
                    else txbDisplay.Text += btn;  //egyébként hozzáadja a btn-t
                }
            }
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            txbDisplay.Text = "0";
            listColor.Add(Color.Pink);
            listColor.Add(Color.White);
            listColor.Add(Color.Red);
            listColor.Add(Color.Green);
            listColor.Add(Color.Blue);
            listColor.Add(Color.Yellow);
            int btnColorIndex = 0;
            txbDisplay.BackColor = listColor[btnColorIndex];
        }

        private void btnColor_Click(object sender, EventArgs e)  //gombnyomásra színt vált ↑ alapján
        {
            btnColorIndex++;
            if (btnColorIndex >= listColor.Count) btnColorIndex = 0;
            txbDisplay.BackColor = listColor[btnColorIndex];
        }


        #region billentyűk
        private void btnPluszMin_Click(object sender, EventArgs e)
        {
            Display("+-");  //display eljárásba küldjük a gombot, később ez alapján adjuk meg hogy mit csináljon
        }
        private void btnComma_Click(object sender, EventArgs e)
        {
            Display(",");
        }
        private void btn0_Click(object sender, EventArgs e)
        {
            Display("0");
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            Display("1");
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            Display("2");
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            Display("3");
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            Display("4");
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            Display("5");
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            Display("6");
        }
        private void btn7_Click(object sender, EventArgs e)
        {
            Display("7");
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            Display("8");
        }
        private void btn9_Click(object sender, EventArgs e)
        {
            Display("9");
        }
        private void btnTorol_Click(object sender, EventArgs e)
        {
            Display("C");
        }
        private void btnTorolEgesz_Click(object sender, EventArgs e)
        {
            Display("AC");
        }
        private void btnGyok_Click(object sender, EventArgs e)
        {
            Display("gy");
        }
        private void btnSzoroz_Click(object sender, EventArgs e)
        {
            Display("*");
        }
        private void btnKivon_Click(object sender, EventArgs e)
        {
            Display("-");
        }
        private void btnOsztas_Click(object sender, EventArgs e)
        {
            Display("/");
        }
        private void btnHozzaad_Click(object sender, EventArgs e)
        {
            Display("+");
        }
        #endregion
    }
}
