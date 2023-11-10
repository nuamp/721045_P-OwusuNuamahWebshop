using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class SyncContext : DbContext
    {
        public DbSet<Domain.Customer> Customers { get; set; }
        
        public SyncContext(DbContextOptions<Context> options) : base(options) { }
        public SyncContext() { }
    }
}
