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
    public partial class Form1 : Form
    {
        public OrderService myService = new OrderService();

        public Form1()
        {
            InitializeComponent();
            this.orderBindingSource.DataSource = myService.List;

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            String type = (String)this.comboBoxType.SelectedItem;
            String content = this.textBox1.Text;
            if (content != "")
            {
                switch (type)
                {
                    case "按照ID":
                        int id = -1;
                        int.TryParse(content, out id);
                        List<Order> mylist = new List<Order>();
                        mylist.Add(myService.selectById(id));
                        orderBindingSource.DataSource = mylist;
                        //orderBindingSource.ResetBindings(false);
                        break;
                    case "按照客户":
                        orderBindingSource.DataSource = myService.selectByCustomer(content).ToList();
                        break;
                    case "按照商品名":
                        orderBindingSource.DataSource = myService.selectByName(content).ToList();
                        break;

                }
                //this.dataGridView2.DataSource = orderBindingSource;
                //this.dataGridView2.Update();
            }

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            myService.List = myService.Import("D:\\practice_c#\\prac_3_6\\test_stander.xml");
            this.orderBindingSource.DataSource = myService.List;
            
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            this.orderBindingSource.ResetBindings(false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //this.orderBindingSource.ResetBindings(false);
            new Form2(myService).ShowDialog();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            this.orderBindingSource.DataSource = myService.List;
            this.orderBindingSource.ResetBindings(false);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.orderBindingSource.ResetBindings(false);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            myService.Export("D:\\practice_c#\\prac_3_6\\export_winform.xml");
            MessageBox.Show("导出成功");
        }
    }
}
