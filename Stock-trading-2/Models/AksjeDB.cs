using Microsoft.EntityFrameworkCore;

namespace Stock_trading_2.Models
{
    public class AksjeDB : DbContext
    {
        public AksjeDB(DbContextOptions<AksjeDB> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Aksje> Aksjer { get; set; }
    }
}
