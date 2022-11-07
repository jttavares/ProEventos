using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;
        }
       
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                this._geralPersist.Add<Evento>(model);
                if(await this._geralPersist.SaveChangesAsync())
                {
                    return await this._eventoPersist.GetEventoByIdAsync(model.EventoId, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Erro em EventoService.AddEventos() => {ex.Message}");
            }
        }
        public async Task<Evento> UpdateEventos(int evenotId, Evento model)
        {
            try
            {
                var evento = await this._eventoPersist.GetEventoByIdAsync(evenotId, false);
                if(evento == null) return null;

                model.EventoId = evento.EventoId;

                this._geralPersist.Update(model);
                if(await this._geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventoByIdAsync(model.EventoId, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu erro em EventoService.UpdateEventos() => {ex.Message}");
            }
        }
        public async Task<bool> DeleteEventos(int eventoId)
        {
            try
            {
                var evento = await this._eventoPersist.GetEventoByIdAsync(eventoId, false);
                if(evento == null) throw new Exception("Evento paea delete não enconrado");

                this._geralPersist.Delete<Evento>(evento);
                return await this._geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu erro em EventoService.UpdateEventos() => {ex.Message}");
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrante);

                return eventos ?? null;

            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu erro em EventoService.GetAllEventosAsync() => {ex.Message}");
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false)
        {
            try
            {
                var eventos =await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrante);

                return eventos ?? null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu erro em EventoService.GetAllEventosByTemaAsync() => {ex.Message}");
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante = false)
        {
           try
           {
               var eventos  = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrante);

               return eventos ?? null;
           }
           catch (Exception ex)
           {
               throw new Exception($"Ocorreu erro em EventoService.GetEventoByIdAsync() => {ex.Message}");
           }
        }

        
    }
}
