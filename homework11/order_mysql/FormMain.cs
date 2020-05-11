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
    public partial class FormMain : Form
    {
        public DB_Service dbs = new DB_Service();
        public String Keyword { get; set; }

        public FormMain()
        {
            
            InitializeComponent();
            /*
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            List<OrderItem> items3 = new List<OrderItem>();
            List<OrderItem> items4 = new List<OrderItem>();
            List<OrderItem> items5 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            items3.Add(new OrderItem(10000, 1, "car"));
            items4.Add(new OrderItem(10, 4, "apple"));
            items4.Add(new OrderItem(2, 2, "banana"));
            items5.Add(new OrderItem(1000, 1, "phone"));
            items5.Add(new OrderItem(3, 6, "orange"));
            Order order1 = new Order(1, "Ming", items1);
            Order order2 = new Order(2, "Hua", items2);
            Order order3 = new Order(3, "Ming", items3);
            Order order4 = new Order(4, "Hong", items4);
            Order order5 = new Order(5, "Qiang", items5);
            List<Order> list = new List<Order>();
            list.Add(order1);
            list.Add(order2);
            list.Add(order3);
            list.Add(order4);
            list.Add(order5);*/
            //第一次初始化数据库
            /*
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            List<OrderItem> items3 = new List<OrderItem>();
            List<OrderItem> items4 = new List<OrderItem>();
            List<OrderItem> items5 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            items3.Add(new OrderItem(10000, 1, "car"));
            items4.Add(new OrderItem(10, 4, "apple"));
            items4.Add(new OrderItem(2, 2, "banana"));
            items5.Add(new OrderItem(1000, 1, "phone"));
            items5.Add(new OrderItem(3, 6, "orange"));
            Order order1 = new Order(1, "Ming", items1);
            Order order2 = new Order(2, "Hua", items2);
            Order order3 = new Order(3, "Ming", items3);
            Order order4 = new Order(4, "Hong", items4);
            Order order5 = new Order(5, "Qiang", items5);

            dbs.AddOrder(order1);
            dbs.AddOrder(order2);
            dbs.AddOrder(order3);
            dbs.AddOrder(order4);
            dbs.AddOrder(order5);
            */
            OrderBindingSource.DataSource = dbs.GetAllOrders();
            comboBox1.SelectedIndex = 0;
            textBox1.DataBindings.Add("Text", this, "Keyword");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    OrderBindingSource.DataSource = dbs.GetAllOrders();
                    break;
                case 1:
                    int id;
                    int.TryParse(Keyword, out id);
                    Order order = dbs.GetOrderByID(id);
                    List<Order> result = new List<Order>();
                    if (order != null) result.Add(order);
                    OrderBindingSource.DataSource = result;
                    break;
                case 2:
                    OrderBindingSource.DataSource = dbs.QueryOrdersByCustomer(Keyword);
                    break;
                case 3:
                    OrderBindingSource.DataSource = dbs.QueryOrdersByName(Keyword);
                    break;
            }
            OrderBindingSource.ResetBindings(true);
        }

        //添加订单
        private void button2_Click(object sender, EventArgs e)
        {
            Form_add formAdd = new Form_add(new Order(), dbs);
            formAdd.ShowDialog();
            /*
            if (formAdd.ShowDialog() == DialogResult.OK)
            {
                OrderBindingSource.DataSource = dbs.GetAllOrders();
                OrderBindingSource.ResetBindings(false);
            }*/
        }

        //删除
        private void button4_Click(object sender, EventArgs e)
        {
            Order o = OrderBindingSource.Current as Order;
            if(o==null)
            {
                MessageBox.Show("请选择一个订单进行删除");
                return;
            }
            dbs.deleteOrder(o.OrderID);
            OrderBindingSource.DataSource = dbs.GetAllOrders();
            OrderBindingSource.ResetBindings(false);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Order order = OrderBindingSource.Current as Order;
            if (order == null)
            {
                MessageBox.Show("请选择一个订单进行修改");
                return;
            }
            Form_add f = new Form_add(order, dbs,true);
            f.ShowDialog();
            /*
            if (f.ShowDialog() == DialogResult.OK)
            {
                OrderBindingSource.DataSource = dbs.GetAllOrders();
                OrderBindingSource.ResetBindings(false);
            }*/
        }
    }
}
