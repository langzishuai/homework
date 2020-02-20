using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caculate
{
    public partial class Form1 : Form
    {
        private string symble = "";
        private double number1 = double.NaN;
        private double number2 = double.NaN;
        private bool isfirst = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void num1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }

        private void num2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void num3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void num4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void num5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void num6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void num7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void num8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void num9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }

        private void add_Click(object sender, EventArgs e)
        {
            symble = "+";
            //掉处理函数
            dealNum();
        }

        private void sub_Click(object sender, EventArgs e)
        {
            symble = "-";
            dealNum();
        }

        private void multi_Click(object sender, EventArgs e)
        {
            symble = "*";
            dealNum();
        }

        private void devide_Click(object sender, EventArgs e)
        {
            symble = "/";
            dealNum();
        }

        private void dealNum()
        { 
            double temp = double.NaN;
            if(double.TryParse(textBox1.Text,out temp))
            {
                if (isfirst)
                {
                    number1 = temp;
                    isfirst = false;
                }
                else
                    number2 = temp;

            }
            else
            {
                MessageBox.Show("输入不正确");
            }

            textBox1.Text = "";
        }

        private void doit_Click(object sender, EventArgs e)
        {
            if(!isfirst)
            {
                dealNum();
                try
                {
                    switch(symble)
                    {
                        case "+":
                            label2.Text = $"{number1 + number2}";
                            break;
                        case "-":
                            label2.Text = $"{number1 - number2}";
                            break;
                        case "*":
                            label2.Text = $"{number1 * number2}";
                            break;
                        case "/":
                            if (number2 != 0)
                                label2.Text = $"{number1 / number2}";
                            else
                                MessageBox.Show("除数不能为0");
                            break;
                    }
                    number1 = number2=double.NaN;
                    isfirst = true;
                    symble = "";
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void num0_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }
    }
}
