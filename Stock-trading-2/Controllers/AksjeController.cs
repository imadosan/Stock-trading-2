using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_trading_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

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

        public async Task<bool> Lagre(Aksje innAksje)
        {
            try
            {
                _db.Aksjer.Add(innAksje);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Aksje>> HentAlle()
        {
            try
            {
                List<Aksje> alleAksjene = await _db.Aksjer.ToListAsync();
                return alleAksjene;
            } catch
            {
                return null;
            }
        }

        public async Task<bool> Slett(int id)
        {
            try
            {
                Aksje enAksje = await _db.Aksjer.FindAsync(id);
                _db.Aksjer.Remove(enAksje);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Aksje> HentEn(int id)
        {
            try
            {
                Aksje enAksje = await _db.Aksjer.FindAsync(id);
                return enAksje;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Endre(Aksje endreAksje)
        {
            try
            {
                Aksje enAksje = await _db.Aksjer.FindAsync(endreAksje.id);
                enAksje.navn = endreAksje.navn;
                enAksje.pris = endreAksje.pris;
                enAksje.antall = endreAksje.antall;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
