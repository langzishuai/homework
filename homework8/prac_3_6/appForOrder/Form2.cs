using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prac_3_6;

namespace appForOrder
{
    public partial class Form2 : Form
    {
        public OrderService os;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(OrderService os) : this()
        {
            this.os = os;
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            int index = -1;
            String num_str = this.textBox1.Text;
            if(int.TryParse(num_str,out index))
            {
                if (os.DelateOrder(index))
                    MessageBox.Show("删除成功");
                else
                    MessageBox.Show("没有此ID");
            }
        }
    }
}
