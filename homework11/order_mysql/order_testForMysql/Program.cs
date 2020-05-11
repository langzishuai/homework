using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order_testForMysql
{
    class Program
    {
        static void Main(string[] args)
        {
            //测试
            DB_Service dbs = new DB_Service();
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
            dbs.deleteOrder(12);
            List<OrderItem> items1 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "pear"));
            items1.Add(new OrderItem(2, 5, "banana"));
            Order order1 = new Order(13, "Hong", items1);
            dbs.AddOrder(order1);
            
            /*
            dbs.deleteOrder(8);
            dbs.deleteOrder(9);
            dbs.deleteOrder(10);
            dbs.deleteOrder(11);
            */
            List<Order> orders = dbs.GetAllOrders();
            foreach (Order o in orders)
            {
                Console.WriteLine(o);
            }
            /*
            List<Order> orders = dbs.QueryOrdersByName("apple");
            foreach(Order o in orders)
            {
                Console.WriteLine(o);
            }
            */
            Console.WriteLine();
            
            order1.OrderItems[0].Name = "apple";
            dbs.changeOrder(order1);
            List<Order> orders2 = dbs.GetAllOrders();
            foreach (Order o in orders2)
            {
                Console.WriteLine(o);
            }
            /*
            dbs.deleteOrder(6);
            dbs.deleteOrder(7);
            dbs.deleteOrder(9);
            
            List<Order> orders2 = dbs.QueryOrdersByName("apple");
            foreach (Order o in orders2)
            {
                Console.WriteLine(o);
            }*/
        }
    }

    public class DB_Service
    {
        //添加订单
        public void AddOrder(Order o)
        {
            using (var context = new OrderingContext())
            {
                context.Orders.Add(o);
                context.SaveChanges();
            }
        }

        public void AddItem(OrderItem oi)
        {
            using(var context = new OrderingContext())
            {
                context.OrderItems.Add(oi);
                context.SaveChanges();
            }
        }

        public void deleteOrder(int orderid)
        {
            using(var context = new OrderingContext())
            {
                var order = context.Orders.FirstOrDefault(p => p.OrderID == orderid);
                if (order != null)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
            }
        }

        public void changeOrder(Order newOrder)
        {
            /*
            int id = newOrder.OrderID;
            Order o = GetOrderByID(id);
            if (o==null)
                return;
            using(var context=new OrderingContext())
            {
                context.Orders.Remove(o);
                context.Orders.Add(newOrder);
            }
            */
            int id = newOrder.OrderID;
            deleteOrder(id);
            AddOrder(newOrder);
        }


        public List<Order> GetAllOrders()
        {
            
            using (var context=new OrderingContext())
            {
                return context.Orders.Include("OrderItems").ToList<Order>();
            }
        }

        //根据id查订单
        public Order GetOrderByID(int id)
        {
            using (var context=new OrderingContext())
            {
                var order = context.Orders.Include("OrderItems").SingleOrDefault(o => o.OrderID == id);
                if (order != null)
                    return (Order)order;
                else
                    return null;
            }
        }

        //根据Customer查订单
        public List<Order> QueryOrdersByCustomer(String CutsomerName)
        {
            using (var context=new OrderingContext())
            {
                var query = context.Orders.Include("OrderItems").Where(p => p.Customer == CutsomerName).OrderBy(p => p.OrderID);
                return query.ToList<Order>();
            }
        }

        //根据商品名字查询
        public List<Order> QueryOrdersByName(String itemName)
        {
            List<Order> orders = new List<Order>();
            using (var context = new OrderingContext())
            {
                orders = context.Orders.Include("OrderItems").ToList();
            }//这里想尝试 先把整个表存到orders这个list中 可以的
            var query = orders.Where((o) =>
            {
                for (int i = 0; i < o.OrderItems.Count; i++)
                    if (o.OrderItems[i].Name == itemName)
                        return true;
                return false;
            }).OrderBy(o => o.OrderID);

            return query.ToList<Order>();
        }

        //

    }   

}
