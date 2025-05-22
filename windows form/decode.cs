using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dekódoló
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //toInt32(valtozo, 2) <- kettes számrendszer; PadLeft(8, 0) <- 8 karakterenként odarak valamit (itt nullát)

        Label idoLbl = new Label();
        Label uzenetLbl = new Label();
        Label adoIPLbl = new Label();
        Label vevoIPLbl = new Label();
        Label dekodoloLbl = new Label();

        TextBox idoTxb = new TextBox();
        RichTextBox uzenetTxb = new RichTextBox();
        TextBox adoIPTxb = new TextBox();
        TextBox vevoIPTxb = new TextBox();
        RichTextBox dekodoloTxb = new RichTextBox();

        Button vege = new Button();
        Button uzenetKuld = new Button();
        Button kodoloBtn = new Button();
        Button dekodoloBtn = new Button();

        List<string> uzenetek = new List<string>();
        List<kodoloSorok> egyesUzenetek = new List<kodoloSorok>();

        const string kezdetStr = "xxx";
        const string vegeStr = ".-+.";
        const string reszekStr = ".x.";
        const string fileVege = ".-.-.+.+.";

        Font alap = new Font("Arial", 10);

        private void Form1_Load(object sender, EventArgs e)
        {
            idoLbl.Text = "Mikor küldi az üzenetet? (éé. hh. nn. óó:pp:mm)";
            idoLbl.Location = new Point(40, 40);
            idoLbl.Font = alap;
            idoLbl.AutoSize = true;
            Controls.Add(idoLbl);

            idoTxb.Text = DateTime.Now.ToString();
            idoTxb.Location = new Point(40, 60);
            idoTxb.Font = alap;
            idoTxb.Width = 165;
            idoTxb.AutoSize = true;
            Controls.Add(idoTxb);


            uzenetLbl.Text = "Mi az üzenet?";
            uzenetLbl.Location = new Point(40, 105);
            uzenetLbl.Font = alap;
            uzenetLbl.AutoSize = true;
            Controls.Add(uzenetLbl);

            uzenetTxb.Location = new Point(40, 130);
            uzenetTxb.Multiline = true;
            uzenetTxb.Font = alap;
            uzenetTxb.MinimumSize = new Size(185, 25);
            Controls.Add(uzenetTxb);


            vevoIPLbl.Text = "Mi a vevő IP címe?";
            vevoIPLbl.Location = new Point(40, 240);
            vevoIPLbl.Font = alap;
            vevoIPLbl.AutoSize = true;
            Controls.Add(vevoIPLbl);

            vevoIPTxb.Location = new Point(40, 260);
            vevoIPTxb.Font = alap;
            vevoIPTxb.Width = 165;
            vevoIPTxb.AutoSize = true;
            Controls.Add(vevoIPTxb);


            adoIPLbl.Text = "Mi az adó IP címe?";
            adoIPLbl.Location = new Point(40, 305);
            adoIPLbl.Font = alap;
            adoIPLbl.AutoSize = true;
            Controls.Add(adoIPLbl);

            adoIPTxb.Location = new Point(40, 325);
            adoIPTxb.Font = alap;
            adoIPTxb.Width = 165;
            adoIPTxb.AutoSize = true;
            Controls.Add(adoIPTxb);


            uzenetKuld.Location = new Point(40, 380);
            uzenetKuld.Text = "Üzenet elküldése";
            uzenetKuld.Font = alap;
            uzenetKuld.AutoSize = true;
            uzenetKuld.Click += new System.EventHandler(uzenetKuld_Click);
            Controls.Add(uzenetKuld);

            vege.Location = new Point(80, 425);
            vege.Text = "Vége (nincs több üzenet)";
            vege.Font = alap;
            vege.AutoSize = true;
            vege.Click += new System.EventHandler(vege_Click);
            Controls.Add(vege);


            this.Size = new Size(1000, 600);
        }

        public void vege_Click (object sender, EventArgs e)
        {
            dekodoloLbl.Text = "Dekódolt üzenet:";
            dekodoloLbl.Location = new Point(640, 40);
            dekodoloLbl.Font = alap;
            dekodoloLbl.AutoSize = true;
            Controls.Add(dekodoloLbl);

            dekodoloTxb.Location = new Point(500, 80);
            dekodoloTxb.Multiline = true;
            dekodoloTxb.Font = alap;
            dekodoloTxb.MinimumSize = new Size(430, 230);
            Controls.Add(dekodoloTxb);

            dekodoloTxb.Text = uzenetek[0];
        }

        class kodoloSorok
        {
            public long mikor;
            public string mitUzent;
            public int vevoIP;
            public int adoIP;
            public kodoloSorok (string sorok)
            {
                string[] sor = sorok.Split(new string[] { ".x." }, StringSplitOptions.None);
                this.mikor = long.Parse(sor[0]);
                this.mitUzent = sor[1];
                string[] ipV = sor[2].Split('.');
                foreach (string s in ipV)
                {
                    int a = Convert.ToInt32(s, 2);
                    this.vevoIP += a + 0;
                }
                string[] ipA = sor[3].Split('.');
                foreach (string s in ipA)
                {
                    int a = Convert.ToInt32(s, 2);
                    this.adoIP += a + 0;
                }
            }
        }

        public void uzenetKuld_Click(object sender, EventArgs e)
        {
            uzenetek.Add(kezdetStr + idoTxb.Text + reszekStr + uzenetTxb.Text + reszekStr + vevoIPTxb + reszekStr + adoIPTxb + vegeStr);
        }
    }
}
