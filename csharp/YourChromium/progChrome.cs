using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YourChromium
{
    public partial class progChrome : Form
    {
        string url = "";
        string installerName = "";
        string ver = "";
        string dlPath = "";

        public progChrome()
        {
            InitializeComponent();
        }

        private void progChrome_Load(object sender, EventArgs e)
        {
            comboBox1.TextChanged += ComboBox1_TextChanged;
            comboBox2.TextChanged += ComboBox1_TextChanged;
            refreshInfo();
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            refreshInfo();
        }

        public void refreshInfo()
        {
            JObject jobj;
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://tools.shuax.com/update/chrome/");//这里的url指要获取的数据网址
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                jobj = JObject.Parse(reader.ReadToEnd());
            }
            parseToUI4RI(jobj);
        }

        public void parseToUI4RI(JObject jojo)
        {
            JObject info = (JObject)jojo[comboBox1.Text][comboBox2.Text];
            url = (string)info["cdn"];
            installerName = (string)info["name"];
            ver = (string)info["version"];
            textBox1.Text = ver;
            dlPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + installerName;
            textBox2.Text = dlPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
            button1.Enabled = false;
            button2.Enabled = false;
            DownloadFile(url, textBox2.Text, progressBar1, label5);
            textBox2.ReadOnly = false;
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.FileName = textBox2.Text;
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = open.FileName;
            }
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
}
}
