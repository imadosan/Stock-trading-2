using Stock_trading_2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stock_trading_2.DAL
{
    public interface IAksjeRepository
    {
        Task<bool> Lagre(Aksje innAksje);
        Task<List<Aksje>> HentAlle();
        Task<bool> Slett(int id);
        Task<Aksje> HentEn(int id);
        Task<bool> Endre(Aksje endreAksje);
        Task<bool> LoggInn(Bruker bruker);
    }
}
