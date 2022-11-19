using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_trading_2.Models
{
    public class Aksjer
    {
        public int id { get; set; }
        public string navn { get; set; }
        public double pris { get; set; }
        public int antall { get; set; }
      
        virtual public Personer Person { get; set; }
    }

    public class Personer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string fornavn { get; set; }
        public string etternavn { get; set; }
    }


    public class AksjeContext : DbContext
    {
        public AksjeContext(DbContextOptions<AksjeContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Aksjer> Aksjer { get; set; }
        public DbSet<Personer> Personer { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
