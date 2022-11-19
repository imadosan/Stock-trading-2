﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Stock_trading_2.Models
{
    public class DBInit
    {
        public static void Initialize (IApplicationBuilder app) 
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AksjeContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var person1 = new Personer { Fornavn = "Ole", Etternavn = "Hansen" };
                var person2 = new Personer { Fornavn = "Line", Etternavn = "Jensen" };

                var aksje1 = new Aksjer { Navn = "Apple", Pris = 151.29, Antall = 2, Person = person1 };
                var aksje2 = new Aksjer { Navn = "Meta", Pris = 112.05, Antall = 3, Person = person2 };

                context.Aksjer.Add(aksje1);
                context.Aksjer.Add(aksje2);

                context.SaveChanges();
            }
        }
    }
}