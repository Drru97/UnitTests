using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
