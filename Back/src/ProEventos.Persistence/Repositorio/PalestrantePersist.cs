using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProEventos.Domain;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Contextos;

namespace ProEventos.Persistence.Repository
{
    public class ProEventosPersistence : IPalestrantePersist
    {
        private readonly ProEventosContext _context;
        public ProEventosPersistence(ProEventosContext context)
        {
            this._context = context;

        }
       
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                                                .Include(p=> p.RedesSociais);
            if(includeEventos)
            {
                query = query.Include(p=> p.PalestrantesEventos)
                            .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p=> p.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                                                .Include(p=> p.RedesSociais);
            if(includeEventos)
            {
                query = query.Include(p=> p.PalestrantesEventos)
                            .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p=> p.Id)
            .Where(palestrante => palestrante.Nome.ToLower().Contains(nome.ToLower()));
            return await query.ToArrayAsync();
        }

       

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                                                .Include(p=> p.RedesSociais);
            if(includeEventos)
            {
                query = query.Include(p=> p.PalestrantesEventos)
                            .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p=> p.Id)
            .Where(palestrante => palestrante.Id == palestranteId);
            return await query.FirstOrDefaultAsync();
        }


    }
}