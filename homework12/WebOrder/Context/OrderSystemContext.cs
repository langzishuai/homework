using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebOrder.Models;

namespace WebOrder.Context
{
    public class OrderSystemContext:DbContext
    {
        public OrderSystemContext(DbContextOptions<OrderSystemContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
