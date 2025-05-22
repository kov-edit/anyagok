using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace meretezes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_ChangeSize (object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Size.Width < 400)
            {
                tableLayoutPanel1.SetColumnSpan(listBox1, 4);
            }
            
            else
            {
                tableLayoutPanel1.SetColumnSpan(listBox1, 2);
            }

        }
    }
}
