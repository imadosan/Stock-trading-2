using Microsoft.AspNetCore.Mvc;
using Stock_trading_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Stock_trading_2.Controllers
{
    [Route("[controller]/[action]")]
    public class AksjeController : ControllerBase
    {
        private readonly AksjeContext _db;

        public AksjeController(AksjeContext db)
        {
            _db = db;
        }

        public bool Lagre(Aksje innAksje)
        {
            try
            {
                _db.Aksjer.Add(innAksje);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Aksje> HentAlle()
        {
            try
            {
                List<Aksje> alleAksjene = _db.Aksjer.ToList();
                return alleAksjene;
            } catch
            {
                return null;
            }
        }

        public bool Slett(int id)
        {
            try
            {
                Aksje enAksje = _db.Aksjer.Find(id);
                _db.Aksjer.Remove(enAksje);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Aksje HentEn(int id)
        {
            try
            {
                Aksje enAksje = _db.Aksjer.Find(id);
                return enAksje;
            }
            catch
            {
                return null;
            }
        }

        public bool Endre(Aksje endreAksje)
        {
            try
            {
                Aksje enAksje = _db.Aksjer.Find(endreAksje.id);
                enAksje.navn = endreAksje.navn;
                enAksje.pris = endreAksje.pris;
                enAksje.antall = endreAksje.antall;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
