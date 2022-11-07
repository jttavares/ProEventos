using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence.Contextos;
using ProEventos.Domain;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {

        private readonly ILogger<EventosController> _logger;
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            this._eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await this._eventoService.GetAllEventosAsync(true);
                return eventos == null ?
                        NotFound("Nenhum evento encontrado") :
                        Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu erro em EventoController.Get() => {ex.Message}");
            }
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await this._eventoService.GetEventoByIdAsync(id, true);
                return evento == null ?
                        NotFound("Nenhum evento encontrado") :
                        Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu erro em EventoController.GetId() => {ex.Message}");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await this._eventoService.GetAllEventosByTemaAsync(tema, true);
                return evento == null ?
                        NotFound("Nenhum evento encontrado") :
                        Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu erro em EventoController.GetByTema() => {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await this._eventoService.AddEventos(model);
                return evento == null ?
                BadRequest("Erro ao tentar adicionar evento") :
                Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu erro em EventoController.Post() => {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await this._eventoService.UpdateEventos(id, model);
                return evento == null ?
                        BadRequest("Erro ao tentar atualizar evento") : Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu erro em EventoController.Put() => {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await this._eventoService.DeleteEventos(id);
                return evento == false ?
                        BadRequest("Erro ao tentar remover evento") : Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu erro em EventoController.Delete() => {ex.Message}");
            }
        }
    }
}
