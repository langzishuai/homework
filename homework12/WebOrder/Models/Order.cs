using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebOrder.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { set; get; }
        public String Customer { set; get; }
        public List<OrderItem> List_orderitem { set; get; }
        public int TotalAmount
        {
            get
            {
                if (List_orderitem == null)
                    return 0;
                int sum = 0;
                foreach (OrderItem oi in List_orderitem)
                    sum += oi.Price * oi.Count;
                return sum;

            }
            set
            {
            }
        }

    }
}
