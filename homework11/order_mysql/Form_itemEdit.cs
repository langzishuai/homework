using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using order_testForMysql;

namespace order_mysql
{
    public partial class Form_itemEdit : Form
    {
        public OrderItem Orderitem { get; set; }

        public Form_itemEdit()
        {
            InitializeComponent();
        }

        public Form_itemEdit(OrderItem oi):this()
        {
            Orderitem = oi;
            itemBindingSource.DataSource = oi;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            itemBindingSource.ResetBindings(true);
            this.Close();
        }
    }
}
