using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_trading_2.Models
{
    public class Aksjer
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public double Pris { get; set; }
        public int Antall { get; set; }
      
        virtual public Personer Person { get; set; }
    }

    public class Personer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
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
