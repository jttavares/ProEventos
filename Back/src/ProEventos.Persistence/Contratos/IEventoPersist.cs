using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {
       

         //EVENTOS
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrante);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante);

    }
}