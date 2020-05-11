using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order_testForMysql
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public string Customer { get; set; }

        public int Totalamount
        {

            get
            {
                int sum = 0;
                foreach (OrderItem oi in OrderItems)
                    sum += oi.Price * oi.Count;
                return sum;

            }
            set { }
        }
        public List<OrderItem> OrderItems { get; set; }

        public Order(int id, String customer, List<OrderItem> list)
        {
            OrderID = id;
            OrderItems = list;
            Customer = customer;
        }

        public Order() { OrderID = -1; Customer = ""; Totalamount = 0; OrderItems = new List<OrderItem>(); }


        public override string ToString()
        {
            String s = $"ID:{OrderID}    Customer:{Customer}\n";
            foreach (OrderItem oi in OrderItems)
                s += (oi.ToString()+"\n");
            s += $"totalAmount:{Totalamount}\n";

            return s;
        }
    }

    public class OrderItem
    {
        [Key]
        public int ItemID { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int Price { get; set; }

        [Required]
        public int Count { get; set; }

        public int OrderID { get; set; }

        public Order Order { get; set; }

        public OrderItem(int p, int c, String n)
        {
            Price = p;
            Count = c;
            Name = n;
        }

        public OrderItem(int p, int c, String n,int oID)
        {
            Price = p;
            Count = c;
            Name = n;
            OrderID = oID;
        }

        public OrderItem() { Price = 0; Count = 0; Name = ""; }

        public OrderItem(int OID) :this(){ OrderID = OID; }

        public override string ToString()
        {
            return $"{Name}     数量：{Count}      单价：{Price}";
        }
    }
}
