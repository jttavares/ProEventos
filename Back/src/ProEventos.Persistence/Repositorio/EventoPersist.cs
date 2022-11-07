using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProEventos.Domain;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Contextos;

namespace ProEventos.Persistence.Repository
{
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;
        public EventoPersist(ProEventosContext context)
        {
            this._context = context;
            // _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
       public async Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                                .Include(e=> e.Lotes)
                                                .Include(e=> e.RedesSociais);
            if(includePalestrante)
            {
                query = query.Include(e=> e.PalestranteEventos)
                            .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e=> e.EventoId);
            return await query.ToArrayAsync();

        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                                                .Include(e=> e.Lotes)
                                                .Include(e=> e.RedesSociais);
            if(includePalestrante)
            {
                query = query.Include(e=> e.PalestranteEventos)
                            .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e=> e.EventoId).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));
            return await query.ToArrayAsync();
        }

         public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante = false)
        {
            
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                                                .Include(e=> e.Lotes)
                                                .Include(e=> e.RedesSociais);
            if(includePalestrante)
            {
                query = query.Include(e=> e.PalestranteEventos)
                            .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e=> e.EventoId).Where(e => e.EventoId == eventoId);
            return await query.FirstOrDefaultAsync();
        }

       
    }
}