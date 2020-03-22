using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prac_3_6
{
    class Program
    {
        static void Main(string[] args)
        {
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
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.AddOrder(new Order(3, "Hong", items3));
            myService.AddOrder(new Order(5, "Liang", items5));
            myService.AddOrder(new Order(2, "Qaing", items2));
            myService.AddOrder(new Order(4, "Ming", items4));
            myService.Sort();
            myService.Show();
            var query=myService.selectByName("apple");
            foreach(Order o in query)
            {
                Console.WriteLine(o.ToString());
                Console.WriteLine();
            }

            Console.WriteLine(myService.selectById(2));
            
        }
    }

    class Order:IComparable
    {
        public int Id { set; get; }
        public String Customer { set; get; }
        public int TotalAmount
        {
            get
            {
                int sum = 0;
                foreach (OrderItem oi in List_orderitem)
                    sum += oi.Price * oi.Count;
                return sum;

            }
        }
        public List<OrderItem> List_orderitem { set; get; }

        public Order(int id, String customer, List<OrderItem> list)
        {
            Id = id;
            List_orderitem = list;
            Customer = customer;
        }

        public override string ToString()
        {
            String s = $"ID:{Id}    Customer:{Customer}\n";
            foreach (OrderItem oi in List_orderitem)
                s += oi.ToString() + "\n";
            s += $"totolAmount:{TotalAmount}\n";
            return s;
        }

        public override bool Equals(object obj)
        {
            Order o = obj as Order;
            return o != null && o.Id == Id;//Id作为订单的标识
        }
        public override int GetHashCode()
        {
            return Id;
        }

        public int CompareTo(object obj)
        {
            Order o = obj as Order;
            if (o == null)
                throw new System.ArgumentException();
            return this.Id.CompareTo(o.Id);

        }
    }
    class OrderItem
    {
        public int Price { set; get; }
        public int Count { set; get; }
        public String Name { set; get; }

        public OrderItem(int p, int c, String n)
        {
            Price = p;
            Count = c;
            Name = n;
        }

        public override string ToString()
        {
            return $"{Name}     数量:{Count}      单价：{Price}";
        }

        public override bool Equals(object obj)
        {
            OrderItem oi = obj as OrderItem;
            return oi != null && oi.Count == Count && oi.Price == Price && oi.Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() * Price * Count;
        }
    }

    class OrderService
    {
        List<Order> List { get; set; }

        public OrderService() { List = new List<Order>(); }

        public bool AddOrder(Order o)
        {
            foreach(Order order in List)
                if (order.Equals(o))
                    return false;
            List.Add(o);
            return true;
        }

        public bool DelateOrder(int id)
        {
            for (int i = 0; i < List.Count; i++)
                if (List[i].Id == id)
                {
                    List.Remove(List[i]);
                    return true;
                }
            return false;
        }

        public bool ChangeOrder(Order o)
        {
            for (int i = 0; i < List.Count; i++)
                if (List[i].Id == o.Id)
                {
                    List[i] = o;
                    return true;
                }
            return false;
        }

        public Order selectById(int id)
        {
            //return (Order)List.Where((o) => { return o.Id == id; });
            Order ret = null;
            for (int i = 0; i < List.Count; i++)
                if (List[i].Id == id)
                    ret = List[i];
            return ret;
        }// 这个直接返回 不太好。。。

        public IEnumerable<Order> selectByName(String name)
        {
            var query = List.Where((o) =>
              {
                  for (int i = 0; i < o.List_orderitem.Count; i++)
                      if (o.List_orderitem[i].Name == name)
                          return true;
                  return false;
              }).OrderBy(o => o.TotalAmount);

            return query;
        }

        public IEnumerable<Order> selectByCustomer(String name)
        {
            var query = List.Where(o => { return o.Customer == name; }).OrderBy(o => o.TotalAmount);

            return query;

        }

        public void SortByAmount()
        {
            List.Sort((o1, o2) => { return o1.TotalAmount - o2.TotalAmount; });
        }

        public void Sort()//默认排序
        {
            List.Sort();
        }

        public void Show()
        {
            foreach(Order o in List)
            {
                Console.WriteLine(o.ToString());
                Console.WriteLine();
            }
        }
    }
}
