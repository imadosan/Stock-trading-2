﻿using Microsoft.EntityFrameworkCore;
using Stock_trading_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock_trading_2.DAL
{
    public class AksjeRepository : IAksjeRepository
    {
        private readonly AksjeContext _db;


        public AksjeRepository(AksjeContext db)
        {
            _db = db;
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
                    Id = k.Id,
                    Aksjenavn = k.Navn,
                    Pris = k.Pris,
                    Antall = k.Antall,
                    Fornavn = k.Person.Fornavn,
                    Etternavn = k.Person.Etternavn,
                }).ToListAsync();
                return alleAksjer;
            }
            catch
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
                    Id = enAksje.Id,
                    Aksjenavn = enAksje.Navn,
                    Pris = enAksje.Pris,
                    Antall = enAksje.Antall,
                    Fornavn = enAksje.Person.Fornavn,
                    Etternavn = enAksje.Person.Etternavn,
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
                Aksjer enAksje = await _db.Aksjer.FindAsync(endreAksje.Id);

                if (enAksje.Person.Fornavn != endreAksje.Fornavn)
                {
                    var sjekkPerson = _db.Personer.Find(endreAksje.Fornavn);
                    if (sjekkPerson == null)
                    {
                        var nyPersonRad = new Personer();
                        nyPersonRad.Fornavn = endreAksje.Fornavn;
                        nyPersonRad.Etternavn = endreAksje.Etternavn;
                        enAksje.Person = nyPersonRad;
                    }
                    else
                    {
                        enAksje.Person = sjekkPerson;
                    }
                }

                enAksje.Navn = endreAksje.Aksjenavn;
                enAksje.Pris = endreAksje.Pris;
                enAksje.Antall = endreAksje.Antall;
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
