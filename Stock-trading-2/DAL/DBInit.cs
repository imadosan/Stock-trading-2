using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Stock_trading_2.DAL
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<AksjeContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var person1 = new Personer { Fornavn = "Ole", Etternavn = "Hansen" };
                var person2 = new Personer { Fornavn = "Line", Etternavn = "Jensen" };

                var aksje1 = new Aksjer { Navn = "Apple", Pris = 151.29, Antall = 2, Person = person1 };
                var aksje2 = new Aksjer { Navn = "Meta", Pris = 112.05, Antall = 3, Person = person2 };

                db.Aksjer.Add(aksje1);
                db.Aksjer.Add(aksje2);

                // lag en påoggingsbruker
                var bruker = new Brukere();
                bruker.Brukernavn = "Admin";
                string passord = "Test11";
                byte[] salt = AksjeRepository.LagSalt();
                byte[] hash = AksjeRepository.LagHash(passord, salt);
                bruker.Passord = hash;
                bruker.Salt = salt;
                db.Brukere.Add(bruker);

                db.SaveChanges();
            }
        }
    }
}
