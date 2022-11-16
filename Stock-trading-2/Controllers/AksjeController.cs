using Microsoft.AspNetCore.Mvc;
using Stock_trading_2.Models;
using System.Collections.Generic;
using System.Linq;

namespace Stock_trading_2.Controllers
{
    [Route("[controller]/[action]")]
    public class AksjeController : ControllerBase
    {
        private readonly AksjeDB _aksjeDB;

        public AksjeController(AksjeDB aksjeDB)
        {
            _aksjeDB = aksjeDB;
        }

        public List<Aksje> HentAlle()
        {
            List<Aksje> alleAksjene = _aksjeDB.Aksjer.ToList();
            return alleAksjene;
        }
    }
}
