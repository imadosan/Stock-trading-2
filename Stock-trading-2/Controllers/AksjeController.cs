using Microsoft.AspNetCore.Mvc;
using Stock_trading_2.Models;
using System.Collections.Generic;

namespace Stock_trading_2.Controllers
{
    [Route("[controller]/[action]")]
    public class AksjeController : ControllerBase
    {
        public List<Aksje> HentAlle()
        {
            var aksjene = new List<Aksje>();

            var akjse1 = new Aksje();
            akjse1.Navn = "Apple";
            akjse1.Pris = 150;
            akjse1.Antall = 2;

            var akjse2 = new Aksje();
            akjse2.Navn = "Amazon";
            akjse2.Pris = 98;
            akjse2.Antall = 2;

            aksjene.Add(akjse1);
            aksjene.Add(akjse2);

            return aksjene;
        }
    }
}
