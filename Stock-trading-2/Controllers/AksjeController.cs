using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_trading_2.DAL;
using Stock_trading_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Stock_trading_2.Controllers
{
    [Route("[controller]/[action]")]
    public class AksjeController : ControllerBase
    {
        private readonly IAksjeRepository _db;
        public AksjeController(IAksjeRepository db)
        {
            _db = db;
        }

        public async Task<bool> Lagre(Aksje innAksje)
        {
            return await _db.Lagre(innAksje);
        }

        public async Task<List<Aksje>> HentAlle()
        {
            return await _db.HentAlle();
        }

        public async Task<bool> Slett(int id)
        {
            return await _db.Slett(id);
        }

        public async Task<Aksje> HentEn(int id)
        {
            return await _db.HentEn(id);
        }

        public async Task<bool> Endre(Aksje endreAksje)
        {
            return await _db.Endre(endreAksje);
        }
    }
}
