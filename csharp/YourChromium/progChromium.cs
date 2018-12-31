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
    public partial class progChromium : Form
    {
        string url = "";

        ChromiumHelper chrh = new ChromiumHelper();
        ChromiumHelper.Platforms cplf = new ChromiumHelper.Platforms();
        ChromiumHelper.Platforms.ZipNames cplfn = new ChromiumHelper.Platforms.ZipNames();

        public progChromium()
        {
            InitializeComponent();
        }

        private void progChromium_Load(object sender, EventArgs e)
        {
            button1.Click += button1_Click;
            comboBox1.TextChanged += ComboBox1_TextChanged;
            fresh();
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            fresh();
        }

        private void fresh()
        {
            url = chrh.getZipUrl(comboBox1.Text);
            textBox2.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + chrh.getPlfZ(comboBox1.Text);
        }

        public void DownloadFile(string URL, string filename, System.Windows.Forms.ProgressBar prog, System.Windows.Forms.Label label1)
        {
            float percent = 0;
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);
                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);

                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    label1.Text = percent.ToString() + "%";
                    System.Windows.Forms.Application.DoEvents();
                }
                so.Close();
                st.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.FileName = textBox2.Text;
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = open.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
            button1.Enabled = false;
            button2.Enabled = false;
            DownloadFile(url, textBox2.Text, progressBar1, label5);
            textBox2.ReadOnly = false;
            button1.Enabled = true;
            button2.Enabled = true;
        }
    }
}
