using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace prac_3_6.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        
        public void OrderServiceTest()
        {
           
        }

        [TestMethod()]
        public void AddOrderTest()
        {
            List<OrderItem> items1 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            Assert.IsTrue(myService.List.Count==1);
        }

        [TestMethod()]
        public void AddOrderTest2()// 测试不可重复添加
        {
            List<OrderItem> items1 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.AddOrder(new Order(1, "Ming", items1));
            Assert.IsTrue(myService.List.Count == 1);
        }


        [TestMethod()]
        public void DelateOrderTest()
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.AddOrder(new Order(2, "Qaing", items2));
            myService.DelateOrder(2);
            Order test = myService.selectById(2);

            Assert.IsTrue(test == null);
        }

        [TestMethod()]
        public void DelateOrderTest2()//要删除的订单对应的订单号不存在时，原订单列表中每一项都应存在
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.AddOrder(new Order(2, "Qaing", items2));
            myService.DelateOrder(3);
            Order test1 = myService.selectById(1);
            Order test2 = myService.selectById(2);
            
            Assert.IsTrue(test2 != null&&test1!=null&&myService.List.Count==2);
        }

        [TestMethod()]
        public void ChangeOrderTest()
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.AddOrder(new Order(2, "Qaing", items2));
            List<OrderItem> items4 = new List<OrderItem>();
            items4.Add(new OrderItem(10, 4, "apple"));
            items4.Add(new OrderItem(2, 2, "banana"));
            Order changrOrder = new Order(2, "Qiang", items4);
            bool isOk = myService.ChangeOrder(changrOrder);
            //myService.Show();
            Assert.IsTrue(isOk&&myService.List[1].List_orderitem.Equals(items4));
        }

        [TestMethod()]
        public void ChangeOrderTest2()//当要改变的订单订单号不存在时，原订单列表不发生改变
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.AddOrder(new Order(2, "Qaing", items2));
            List<OrderItem> items4 = new List<OrderItem>();
            items4.Add(new OrderItem(10, 4, "apple"));
            items4.Add(new OrderItem(2, 2, "banana"));
            Order changrOrder = new Order(3, "Qiang", items4);
            bool isOk = myService.ChangeOrder(changrOrder);
            //myService.Show();
            Assert.IsTrue(!isOk && myService.List[0].List_orderitem.Equals(items1)&& myService.List[1].List_orderitem.Equals(items2));
        }

        [TestMethod()]
        public void selectByIdTest()
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.AddOrder(new Order(2, "Qaing", items2));
            Order ret = myService.selectById(1);


            Assert.IsTrue(ret != null && ret.Id == 1 && ret.Customer == "Ming" && ret.List_orderitem.Equals(items1));
        }

        [TestMethod()]
        public void selectByIdTest2()
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.AddOrder(new Order(2, "Qaing", items2));
            Order ret = myService.selectById(3);


            Assert.IsTrue(ret == null);
        }


        [TestMethod()]
        public void selectByNameTest()//通过表单号是否对应来判断
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            List<OrderItem> items4 = new List<OrderItem>();
            items4.Add(new OrderItem(10, 4, "apple"));
            items4.Add(new OrderItem(2, 2, "banana"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.AddOrder(new Order(2, "Ming", items2));
            myService.AddOrder(new Order(3, "Qiang", items4));
            var query = myService.selectByName("banana");
            List<Order> result = query.ToList();
            Assert.IsTrue(result.Count == 2 && result[0].Id == 1 && result[1].Id == 3);
        }

        [TestMethod()]
        public void selectByNameTest2()//订单中没有一个含有该商品名称的情况
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            List<OrderItem> items4 = new List<OrderItem>();
            items4.Add(new OrderItem(10, 4, "apple"));
            items4.Add(new OrderItem(2, 2, "banana"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.AddOrder(new Order(2, "Ming", items2));
            myService.AddOrder(new Order(3, "Qiang", items4));
            var query = myService.selectByName("car");
            List<Order> result = query.ToList();
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod()]
        public void selectByCustomerTest()//通过查询结果中表单中Order的个数来判断
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            List<OrderItem> items4 = new List<OrderItem>();
            items4.Add(new OrderItem(10, 4, "apple"));
            items4.Add(new OrderItem(2, 2, "banana"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.AddOrder(new Order(2, "Ming", items2));
            myService.AddOrder(new Order(3, "Qiang", items4));
            var query = myService.selectByCustomer("Ming");
            List<Order> result = query.ToList();

            Assert.IsTrue(result.Count==2);
        }
        [TestMethod()]
        public void selectByCustomerTest2()//通过查询结果中表单中Order的个数来判断
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            List<OrderItem> items4 = new List<OrderItem>();
            items4.Add(new OrderItem(10, 4, "apple"));
            items4.Add(new OrderItem(2, 2, "banana"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.AddOrder(new Order(2, "Ming", items2));
            myService.AddOrder(new Order(3, "Qiang", items4));
            var query = myService.selectByCustomer("Hong");
            List<Order> result = query.ToList();

            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod()]
        public void SortByAmountTest() // 通过验证排序后 订单Id序列时候和预期一致
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            List<OrderItem> items4 = new List<OrderItem>();
            items4.Add(new OrderItem(10, 4, "apple"));
            items4.Add(new OrderItem(2, 2, "banana"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(3, "Ming", items2));
            myService.AddOrder(new Order(2, "Qiang", items4));
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.SortByAmount();
            //myService.Show();
           
            Assert.IsTrue(myService.List[0].Id==1&&myService.List[1].Id==3&&myService.List[2].Id==2);
        }

        [TestMethod()]
        public void SortTest()
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            List<OrderItem> items4 = new List<OrderItem>();
            items4.Add(new OrderItem(10, 4, "apple"));
            items4.Add(new OrderItem(2, 2, "banana"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(3, "Ming", items2));
            myService.AddOrder(new Order(2, "Qiang", items4));
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.Sort();
            //myService.Show();

            Assert.IsTrue(myService.List[0].Id == 1 && myService.List[1].Id == 2 && myService.List[2].Id == 3);
        }

        [TestMethod()]
        public void ExportTest()// 比较生成的xml 和与其标准xml 文件内容是否相同
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            List<OrderItem> items4 = new List<OrderItem>();
            items4.Add(new OrderItem(10, 4, "apple"));
            items4.Add(new OrderItem(2, 2, "banana"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(3, "Ming", items2));
            myService.AddOrder(new Order(2, "Qiang", items4));
            myService.AddOrder(new Order(1, "Ming", items1));
            myService.Export("D:\\practice_c#\\prac_3_6\\test.xml");
            String s1 = File.ReadAllText("D:\\practice_c#\\prac_3_6\\test.xml");
            String s2 = File.ReadAllText("D:\\practice_c#\\prac_3_6\\test_stander.xml");

            Assert.IsTrue(s1==s2);
        }

        [TestMethod()]
        public void ImportTest()
        {
            List<OrderItem> items1 = new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            items1.Add(new OrderItem(10, 1, "apple"));
            items1.Add(new OrderItem(2, 5, "banana"));
            items2.Add(new OrderItem(3, 3, "orange"));
            items2.Add(new OrderItem(10, 2, "apple"));
            List<OrderItem> items4 = new List<OrderItem>();
            items4.Add(new OrderItem(10, 4, "apple"));
            items4.Add(new OrderItem(2, 2, "banana"));
            OrderService myService = new OrderService();
            myService.AddOrder(new Order(3, "Ming", items2));
            myService.AddOrder(new Order(2, "Qiang", items4));
            myService.AddOrder(new Order(1, "Ming", items1));
            List<Order> list2 = myService.Import("D:\\practice_c#\\prac_3_6\\test_stander.xml");
            Assert.IsTrue(list2.Count == myService.List.Count);
            bool isOk = true;
            for(int i=0;i<list2.Count;i++)
                if (!(list2[i].Equals(myService.List[i])))
                {
                    isOk = false;
                    break;
                }
            Assert.IsTrue(isOk);

        }

    }
}