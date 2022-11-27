using Microsoft.AspNetCore.Http;
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

        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        public AksjeController(IAksjeRepository db, ILogger<AksjeController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> Lagre(Aksje innAksje)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if(ModelState.IsValid) 
            {
                bool returOK = await _db.Lagre(innAksje);
                if (!returOK)
                {
                    _log.LogInformation("Aksje ble ikke lagret!");
                    return BadRequest("Aksje ble ikke lagret!");
                }
                return Ok("Aksje lagret!");
            }
            _log.LogInformation("Feil i inputvalidering!");
            return BadRequest("Feil i inputvalidering!");

        }

        public async Task<ActionResult> HentAlle()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            List<Aksje> alleAksjer = await _db.HentAlle();
            return Ok(alleAksjer);
        }

        public async Task<ActionResult> Slett(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if(ModelState.IsValid) 
            {
                bool returOK = await _db.Endre(endreAksje);
                if (!returOK)
                {
                    _log.LogInformation("Aksje ble ikke endret!");
                    return NotFound("Aksje ble ikke endret!");
                }
                return Ok("Aksje endret!");
            }
            _log.LogInformation("Feil i inputvalidering!");
            return BadRequest("Feil i inputvalidering!");
        }

        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInn(bruker);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker");
                    HttpContext.Session.SetString(_loggetInn, _ikkeLoggetInn);
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, _loggetInn);
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering!");
            return BadRequest("Feil i inputvalidering på server!");
        }

        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, _ikkeLoggetInn);
        }
    }
}
