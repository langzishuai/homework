using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebOrder.Context;
using WebOrder.Models;

namespace WebOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderSystemController : ControllerBase
    {
        private readonly OrderSystemContext orderSystemContext;

        public OrderSystemController(OrderSystemContext context)
        {
            this.orderSystemContext = context;
        }

        [HttpPost]
        [Route("addOrder")]
        public ReturnMessage AddOrder(Order o)
        {
            ReturnMessage returnMessage = new ReturnMessage();
            try
            {
                orderSystemContext.Orders.Add(o);
                orderSystemContext.SaveChanges();
                returnMessage.returnMessage = "add order successfully";
            }
            catch (Exception ex)
            {
                returnMessage.returnMessage = ex.InnerException.Message;
                return returnMessage;
            }

            return returnMessage;
        }

        [HttpDelete("deleteOrder/{id}")]
        public ReturnMessage DeleteOrder(long id)
        {
            ReturnMessage ret = new ReturnMessage();
            try
            {
                var o = orderSystemContext.Orders.Include("List_orderitem").FirstOrDefault(p => p.OrderId == id);
                if (o != null)
                {
                    int num = o.List_orderitem.Count;
                    for (int i = 0; i < num; i++)
                        orderSystemContext.OrderItems.Remove(o.List_orderitem[i]);
                    orderSystemContext.Orders.Remove(o);
                    orderSystemContext.SaveChanges();
                }
                ret.returnMessage = "delete order successfully";
            }
            catch (Exception ex)
            {
                ret.returnMessage = ex.InnerException.Message;
                return ret;
            }

            return ret;

        }
        [HttpPost]
        [Route("changeOrder")]
        public ReturnMessage ChangeOrder(Order o)
        {
            ReturnMessage ret = new ReturnMessage();
            try
            {
                var order = orderSystemContext.Orders.Include("List_orderitem").FirstOrDefault(p => p.OrderId == o.OrderId);
                if (order != null)
                {
                    int num = order.List_orderitem.Count;
                    for (int i = 0; i < num; i++)
                        orderSystemContext.OrderItems.Remove(order.List_orderitem[i]);
                    order.Customer = o.Customer;
                    for (int i = 0; i < o.List_orderitem.Count; i++)
                        order.List_orderitem.Add(o.List_orderitem[i]);

                    orderSystemContext.SaveChanges();
                }
                ret.returnMessage = "change order successfully";
            }
            catch (Exception ex)
            {
                ret.returnMessage = ex.InnerException.Message;
                return ret;
            }
            return ret;
        }

        [HttpGet]
        [Route("getOrderById")]
        public Order GetOrderById(long id)
        {
            Order returnOrder = new Order();
            try
            {
                var o = orderSystemContext.Orders.Include("List_orderitem").FirstOrDefault(p => p.OrderId == id);
                returnOrder = o;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return returnOrder;
            }

            return returnOrder;
        }

        [HttpGet]
        [Route("getOrdersByItem")]
        public ActionResult<List<Order>> GetOrdersByItem(string item)
        {
            List<Order> orders, retList;
            try
            {
                orders = orderSystemContext.Orders.Include("List_orderitem").ToList<Order>();
                var query = orders.Where((p) =>
                  {
                      for (int i = 0; i < p.List_orderitem.Count; i++)
                          if (p.List_orderitem[i].Name == item)
                              return true;

                      return false;
                  });
                retList = query.ToList<Order>();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return new List<Order>();
            }

            return retList;
        }

        [HttpGet]
        [Route("getOrderByCustomer")]
        public ActionResult<List<Order>> GetOrderByCustomer(string name)
        {
            return orderSystemContext.Orders.Include("List_orderitem").Where(p => p.Customer == name).ToList<Order>();

        }

        [HttpGet]
        [Route("sortByTotalAmount")]
        public ActionResult<List<Order>> SortByTotalAmount()
        {
            return orderSystemContext.Orders.Include("List_orderitem").OrderBy(p => p.TotalAmount).ToList<Order>();
        }
    }
}