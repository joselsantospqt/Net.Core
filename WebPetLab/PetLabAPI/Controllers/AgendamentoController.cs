using Domain.Entidade;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetLabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private AgendamentoService _ServiceAgendamento;

        public AgendamentoController(AgendamentoService serviceAgendamento)
        {
            _ServiceAgendamento = serviceAgendamento;
        }

        [HttpGet("getAll")]
        //[Authorize]
        public ActionResult GetAll()
        {
            var getAllPet = _ServiceAgendamento.GetAll();

            return Ok(getAllPet);
        }

        [HttpGet("{id:Guid}")]
        //[Authorize]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var agendamento = _ServiceAgendamento.GetAgendamentoById(id);

            if (agendamento == null)
                return NoContent();

            return Ok(agendamento);
        }

        [HttpGet("{email}")]
        public ActionResult GetAgendamentoByPet([FromRoute] Guid id)
        {
            var agendamentos = _ServiceAgendamento.GetAll().Where(x => x.Pet.PetId == id);

            if (agendamentos == null)
                return NoContent();

            return Ok(agendamentos);
        }

        [HttpPost("{idMedico:Guid}/{idPet:Guid}/{data}")]
        public ActionResult Agendamento([FromRoute] Guid idMedico, [FromRoute] Guid idPet, [FromRoute] string data)
        {

            var agendamento = _ServiceAgendamento.CreateAgendamento(idMedico, idPet, Convert.ToDateTime(data));

            return Created("api/[controller]", agendamento);
        }

        [HttpDelete("{id:Guid}")]
        //[Authorize]
        public ActionResult Delete(Guid id)
        {

            _ServiceAgendamento.DeleteAgendamento(id);

            return NoContent();
        }

        [HttpPut("{id:Guid}")]
        //[Authorize]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Agendamento update)
        {
            Agendamento agendamentoUpdate = update;
            agendamentoUpdate.Id = id;
            var updateAgendamento = _ServiceAgendamento.UpdateAgendamento(agendamentoUpdate);

            return Ok(updateAgendamento);

        }
    }
}
