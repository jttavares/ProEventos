using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
       
        private readonly ILogger<EventoController> _logger;
        private readonly DataContext _context;
        
        public EventoController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return this._context.Eventos;
        }

        [HttpGet("{id}")]
        public Evento GetById(int id)
        {
            return this._context.Eventos.FirstOrDefault(evento=> evento.EventoId == id);
        }

        [HttpPost]
        public string Post(Evento evento)
        {
            this._context.Eventos.Add(evento);
            var result = this._context.SaveChanges();
            return result > 0 ? "Evento inserido na base de dados.":"Erro ao inserir evento na base de dados";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Exemplo de Put com id = {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Exemplo de Delete com id = {id}";
        }
    }
}
