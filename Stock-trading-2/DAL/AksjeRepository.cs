using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stock_trading_2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Stock_trading_2.DAL
{
    public class AksjeRepository : IAksjeRepository
    {
        private readonly AksjeContext _db;

        private ILogger<AksjeRepository> _log;


        public AksjeRepository(AksjeContext db, ILogger<AksjeRepository> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<bool> Lagre(Aksje innAksje)
        {
            try
            {
                var nyAksjeRad = new Aksjer();
                nyAksjeRad.Navn = innAksje.Aksjenavn;
                nyAksjeRad.Pris = innAksje.Pris;
                nyAksjeRad.Antall = innAksje.Antall;

                var sjekkPerson = await _db.Personer.FindAsync(innAksje.Fornavn);
                if (sjekkPerson == null)
                {
                    var nyPersonRad = new Personer();
                    nyPersonRad.Fornavn = innAksje.Fornavn;
                    nyPersonRad.Etternavn = innAksje.Etternavn;
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
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<List<Aksje>> HentAlle()
        {
            try
            {
                List<Aksje> alleAksjer = await _db.Aksjer.Select(k => new Aksje
                {
                    Id = k.Id,
                    Aksjenavn = k.Navn,
                    Pris = k.Pris,
                    Antall = k.Antall,
                    Fornavn = k.Person.Fornavn,
                    Etternavn = k.Person.Etternavn,
                }).ToListAsync();
                return alleAksjer;
            }
            catch(Exception e)
            {
                _log.LogInformation(e.Message);
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
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
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
                    Id = enAksje.Id,
                    Aksjenavn = enAksje.Navn,
                    Pris = enAksje.Pris,
                    Antall = enAksje.Antall,
                    Fornavn = enAksje.Person.Fornavn,
                    Etternavn = enAksje.Person.Etternavn,
                };
                return hentetAksje;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<bool> Endre(Aksje endreAksje)
        {
            try
            {
                var endreObjekt = await _db.Aksjer.FindAsync(endreAksje.Id);

                if (endreObjekt.Person.Fornavn != endreAksje.Fornavn)
                {
                    var sjekkPerson = _db.Personer.Find(endreAksje.Fornavn);
                    if (sjekkPerson == null)
                    {
                        var nyPersonRad = new Personer();
                        nyPersonRad.Fornavn = endreAksje.Fornavn;
                        nyPersonRad.Etternavn = endreAksje.Etternavn;
                        endreObjekt.Person = nyPersonRad;
                    }
                    else
                    {
                        endreObjekt.Person = sjekkPerson;
                    }
                }

                endreObjekt.Navn = endreAksje.Aksjenavn;
                endreObjekt.Pris = endreAksje.Pris;
                endreObjekt.Antall = endreAksje.Antall;
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
            return true;
        }

        public static byte[] LagHash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                                password: passord,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 1000,
            numBytesRequested: 32);
        }

        public static byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        public async Task<bool> LoggInn(Bruker bruker)
        {
            try
            {
                Brukere funnetBruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
                // sjekk passordet
                byte[] hash = LagHash(bruker.Passord, funnetBruker.Salt);
                bool ok = hash.SequenceEqual(funnetBruker.Passord);
                if (ok)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }
    }
}
