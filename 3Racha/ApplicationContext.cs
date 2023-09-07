using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace _3Racha
{
    internal class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Orderitem> OrderItems { get; set; }
        public DbSet<OrderComplete> OrderComplete { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }
    }
}
