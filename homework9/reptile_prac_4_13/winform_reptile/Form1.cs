using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using reptile_prac_4_13;
using System.Threading;
using System.Threading.Tasks;

namespace winform_reptile
{
    public partial class Form1 : Form
    {
        SimpleCrawler mycrawler = new SimpleCrawler();
        public Form1()
        {
            InitializeComponent();
            mycrawler.PageDownloaded += Crawler_PadgeDownLoader;
        }

        private void Crawler_PadgeDownLoader(string obj)
        {
            if (this.listBox1.InvokeRequired)
            {
                Action<String> action = this.AddUrl;
                this.Invoke(action, new object[] { obj });
            }else
            {
                listBox1.Items.Add(obj);
            }
        }
        private void AddUrl(string url)
        {
            listBox1.Items.Add(url);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mycrawler.startUrl = textBox1.Text;
            mycrawler.myurls.Enqueue(mycrawler.startUrl);
            listBox1.Items.Clear();
            new Thread(mycrawler.Crawl).Start();
        }
    }
}
