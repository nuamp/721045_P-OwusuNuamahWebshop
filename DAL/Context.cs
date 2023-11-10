using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Context : DbContext
    {
        public DbSet<Domain.Customer> Customers { get; set; }
        public DbSet<Domain.Product> Products { get; set; }
        public DbSet<Domain.Order> Orders { get; set; }
        public DbSet<Domain.Review> Reviews { get; set; }
        public Context(DbContextOptions<Context> options) : base(options){}
        public Context(){}
    }
}
