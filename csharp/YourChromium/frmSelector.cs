using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YourChromium
{
    public partial class frmSelector : Form
    {
        public frmSelector()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Random pgRandom = new Random();
            int result = pgRandom.Next(1, 50);
            switch (result)
            {
                case 1+4:
                    new progChrome().Show();
                    break;
                case 2+4:
                    new progGC().Show();
                    break;
                case 3+4:
                    new progChromium().Show();
                    break;
                case 4+4:
                    new AboutBox1().ShowDialog();
                    break;
                default:
                    break;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new progChromium().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new progChrome().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new progGC().Show();
        }
    }
}
