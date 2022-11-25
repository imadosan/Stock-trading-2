using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        private ILogger<AksjeController> _log;
        public AksjeController(IAksjeRepository db, ILogger<AksjeController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> Lagre(Aksje innAksje)
        {
            bool returOK = await _db.Lagre(innAksje);
            if (!returOK)
            {
                _log.LogInformation("Aksje ble ikke lagret!");
                return BadRequest("Aksje ble ikke lagret!");
            }
            return Ok("Aksje lagret!");
        }

        public async Task<ActionResult> HentAlle()
        {
            List<Aksje> alleAksjer = await _db.HentAlle();
            return Ok(alleAksjer);
        }

        public async Task<ActionResult> Slett(int id)
        {
            bool returOK = await _db.Slett(id);
            if (!returOK)
            {
                _log.LogInformation("Aksje ble ikke slettet!");
                return NotFound("Aksje ble ikke slettet!");
            }
            return Ok("Aksje slettet!");
        }

        public async Task<ActionResult> HentEn(int id)
        {
            Aksje enAksje = await _db.HentEn(id);
            if (enAksje == null)
            {
                _log.LogInformation("Fant ikke aksjen!");
                return NotFound("Fant ikke aksjen!");
            }
            return Ok(enAksje);
        }

        public async Task<ActionResult> Endre(Aksje endreAksje)
        {
            bool returOK = await _db.Endre(endreAksje);
            if (!returOK)
            {
                _log.LogInformation("Aksje ble ikke endret!");
                return NotFound("Aksje ble ikke endret!");
            }
            return Ok("Aksje endret!");
        }
    }
}
