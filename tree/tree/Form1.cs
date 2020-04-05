using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tree
{
    public partial class Form1 : Form
    {
        private Graphics graphics=null;
        int n ;//10
        int leng ;//100
        Pen p ;//blue
        double th1 ;//30度
        double th2 ;//20度
        double per1 ;//0.6
        double per2 ;//0.7

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (graphics == null)graphics = this.panel1.CreateGraphics();
            graphics.Clear(Color.White);
            int.TryParse(this.textBox1.Text, out n);
            int.TryParse(this.textBox2.Text, out leng);
            double.TryParse(this.textBox3.Text, out per1);
            double.TryParse(this.textBox4.Text, out per2);
            double.TryParse(this.textBox5.Text, out th1);
            th1 = th1 * Math.PI / 180;
            double.TryParse(this.textBox6.Text,out th2);
            th2 = th2 * Math.PI / 180;

            switch ((String)this.comboBox1.SelectedItem)
            {
                case "蓝色":
                    p = Pens.Blue;
                    break;
                case "黑色":
                    p = Pens.Black;
                    break;
                case "黄色":
                    p = Pens.Yellow;
                    break;
                case "红色":
                    p = Pens.Red;
                    break;
            }

            drawCayLeyTree(n, 200, 310, leng, -Math.PI / 2);
            
        }

        void drawCayLeyTree(int n, double x0, double y0, double leng, double th)
        {
            if (n == 0) return;

            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);

            drawLine(x0, y0, x1, y1);

            drawCayLeyTree(n - 1, x1, y1, per1 * leng, th + th1);
            drawCayLeyTree(n - 1, x1, y1, per2 * leng, th - th2);

        }
        void drawLine(double x0,double y0,double x1,double y1)
        {
            graphics.DrawLine(p, (int)x0, (int)y0, (int)x1, (int)y1);
        }


    }
}
