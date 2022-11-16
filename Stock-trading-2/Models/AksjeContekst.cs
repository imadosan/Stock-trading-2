using Microsoft.EntityFrameworkCore;

namespace Stock_trading_2.Models
{
    public class AksjeContext : DbContext
    {
        public AksjeContext(DbContextOptions<AksjeContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Aksje> Aksjer { get; set; }
    }
}
