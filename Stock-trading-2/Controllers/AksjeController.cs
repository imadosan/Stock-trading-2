using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly AksjeContext _db;

        public AksjeController(AksjeContext db)
        {
            _db = db;
        }

        public async Task<bool> Lagre(Aksje innAksje)
        {
            try
            {
                var nyAksjeRad = new Aksjer();
                nyAksjeRad.navn = innAksje.navn;
                nyAksjeRad.pris = innAksje.pris;
                nyAksjeRad.antall = innAksje.antall;

                var sjekkPerson = await _db.Personer.FindAsync(innAksje.fornavn);
                if (sjekkPerson == null)
                {
                    var nyPersonRad = new Personer();
                    nyPersonRad.fornavn = innAksje.fornavn;
                    nyPersonRad.etternavn = innAksje.etternavn;
                    nyAksjeRad.Person = nyPersonRad;
                }
                else
                {
                    nyAksjeRad.Person = sjekkPerson;
                }
                _db.Aksjer.Add(nyAksjeRad);
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
                List<Aksje> alleAksjer = await _db.Aksjer.Select(k => new Aksje
                {
                    id = k.id,
                    navn = k.navn,
                    pris = k.pris,
                    antall = k.antall,
                    fornavn = k.Person.fornavn,
                    etternavn= k.Person.etternavn,
                }).ToListAsync();
                return alleAksjer;
            } catch
            {
                return null;
            }
        }

        public async Task<bool> Slett(int id)
        {
            try
            {
                Aksjer enAksje = await _db.Aksjer.FindAsync(id);
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
                Aksjer enAksje = await _db.Aksjer.FindAsync(id);
                var hentetAksje = new Aksje()
                {
                    id = enAksje.id,
                    navn = enAksje.navn,
                    pris = enAksje.pris,
                    antall = enAksje.antall,
                    fornavn = enAksje.Person.fornavn,
                    etternavn = enAksje.Person.etternavn,
                };
                return hentetAksje;
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
                Aksjer enAksje = await _db.Aksjer.FindAsync(endreAksje.id);

                if (enAksje.Person.fornavn != endreAksje.fornavn)
                {
                    var sjekkPerson = _db.Personer.Find(endreAksje.fornavn);
                    if (sjekkPerson == null)
                    {
                        var nyPersonRad = new Personer();
                        nyPersonRad.fornavn = endreAksje.fornavn;
                        nyPersonRad.etternavn = endreAksje.etternavn;
                        enAksje.Person = nyPersonRad;
                    }
                    else
                    {
                        enAksje.Person = sjekkPerson;
                    }
                }

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
