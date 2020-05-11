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
    public partial class Form_add : Form
    {
        public Order CurrentOrder { get; set; }

        public DB_Service dbs;
        public Form_add()
        {
            InitializeComponent();
        }

        public Form_add(Order order,DB_Service dbs, bool editMode = false) : this()
        {
            this.dbs = dbs;
            CurrentOrder = order;
            List<Order> bindlist = new List<Order>();
            bindlist.Add(CurrentOrder);
            orderBindingSource.DataSource = bindlist;

            textBox1.Enabled = !editMode;
            textBox2.Enabled = !editMode;
        }

        //添加明细
        private void button1_Click(object sender, EventArgs e)
        {
            Form_itemEdit form_item = new Form_itemEdit(new OrderItem(CurrentOrder.OrderID));
            try
            {
                if (form_item.ShowDialog() == DialogResult.OK)
                {
                    /*
                    int index = 0;
                    if (CurrentOrder.OrderItems.Count != 0)
                    {
                        index = CurrentOrder.OrderItems.Max(i => i.ItemID) + 1;
                    }
                    form_item.Orderitem.ItemID = index;
                    */
                    CurrentOrder.OrderItems.Add(form_item.Orderitem);
                    itemBindingSource.ResetBindings(false);
                    orderBindingSource.ResetBindings(false);
                }
            }catch(Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }

        private void EditItem()
        {
            OrderItem orderitem = itemBindingSource.Current as OrderItem;
            if(orderitem==null)
            {
                MessageBox.Show("请选择一个订单项");
                return;
            }
            Form_itemEdit form_Item = new Form_itemEdit(orderitem);
            if (form_Item.ShowDialog() == DialogResult.OK)
                itemBindingSource.ResetBindings(false);
        }

        //修改订单明细
        private void button2_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        //删除订单明细
        private void button3_Click(object sender, EventArgs e)
        {
            OrderItem orderItem = itemBindingSource.Current as OrderItem;
            if(orderItem==null)
            {
                MessageBox.Show("请选择一个订单项");
                return;
            }
            CurrentOrder.OrderItems.Remove(orderItem);
            itemBindingSource.ResetBindings(false);
        }

        //保存
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Enabled == true)//代表是添加订单
                dbs.AddOrder(CurrentOrder);
            else//此时是修改订单
                dbs.changeOrder(CurrentOrder);

            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            orderBindingSource.ResetBindings(true);
            itemBindingSource.ResetBindings(true);
        }
    }
}
