using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form_ugras
{
    public partial class Form1 : Form
    {
        static int x = 0;
        static int y = 0;

        public void mozgat(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void bal_fel_Click(object sender, EventArgs e)
        {
            x = 0; y = 0;
            mozgat(x, y);
        }

        private void bal_le_Click(object sender, EventArgs e)
        {
            x = 0; y = Screen.PrimaryScreen.WorkingArea.Height - Height;  //a képernyő magasságából form magassága
            mozgat(x, y);
        }

        private void jobb_fel_Click(object sender, EventArgs e)
        {
            x = Screen.PrimaryScreen.WorkingArea.Width - Width; y = 0;
            mozgat(x, y);
        }

        private void jobb_le_Click(object sender, EventArgs e)
        {
            x = Screen.PrimaryScreen.WorkingArea.Width - Width; y = Screen.PrimaryScreen.WorkingArea.Height - Height;
            mozgat(x, y);
        }
    }
}
