using Domain.Entidade;
using Domain.Entidade.Request;
using Domain.Entidade.View;
using Domain.Service;
using Microsoft.AspNetCore.Authorization;
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
        private PetService _ServicePet;
        private UsuarioService _ServiceUsuario;
        private AgendamentoService _ServiceAgendamento;
        private ProntuarioService _ServiceProntuario;
        private DocumentoService _ServiceDocumento;

        public AgendamentoController(PetService servicePet, UsuarioService serviceUsuario, AgendamentoService serviceAgendamento, ProntuarioService serviceProntuario, DocumentoService serviceDocumento)
        {
            _ServicePet = servicePet;
            _ServiceUsuario = serviceUsuario;
            _ServiceDocumento = serviceDocumento;
            _ServiceProntuario = serviceProntuario;
            _ServiceAgendamento = serviceAgendamento;
        }

        [HttpGet("getAll")]
        [Authorize]
        public ActionResult GetAll()
        {
            var getAllAgendamento = _ServiceAgendamento.GetAll();

            return Ok(getAllAgendamento);
        }

        [HttpGet("{id:Guid}")]
        [Authorize]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var agendamento = _ServiceAgendamento.GetAgendamentoById(id);

            if (agendamento == null)
                return NoContent();

            return Ok(agendamento);
        }

        [HttpGet("GetAllAgendamentoById/{id:Guid}")]
        [Authorize]
        public ActionResult GetAllAgendamentoById([FromRoute] Guid id)
        {
            var usuario = _ServiceUsuario.GetUsuarioById(id);
            var Detalhes = new ViewModel()
            {
                Usuario = usuario
            };
            if (usuario.TipoUsuario == ETipoUsuario.Tutor)
            {
                foreach (var itemPet in usuario.Pets)
                {
                    var pet = _ServicePet.GetPetById(itemPet.PetId);
                    Detalhes.ListaPets.Add(pet);
                    Detalhes.ListaPets.First(x => x.Id == itemPet.PetId).Tutor = itemPet;

                    foreach (var item in pet.Agendamentos)
                    {
                        Detalhes.Agendamentos.Add(_ServiceAgendamento.GetAgendamentoById(item.AgendamentoId));
                        Detalhes.Agendamentos.First(x => x.Id == item.AgendamentoId).Pet = item;
                    }

                    foreach (var item in pet.Prontuarios)
                    {
                        Detalhes.Prontuarios.Add(_ServiceProntuario.GetProntuarioById(item.ProntuarioId));
                        Detalhes.Prontuarios.First(x => x.Id == item.ProntuarioId).Pet = item;
                    }
                }

                foreach (var item in Detalhes.Agendamentos)
                {
                    if (Detalhes.ListaMedicos.Where(x => x.Id == item.MedicoResponsavel.UsuarioId).Count() == 0)
                        Detalhes.ListaMedicos.Add(_ServiceUsuario.GetUsuarioById(item.MedicoResponsavel.UsuarioId));
                }
            }

            if (Detalhes == null)
                return NoContent();

            return Ok(Detalhes);
        }

        [HttpPost("{idMedico:Guid}/{idPet:Guid}")]
        [Authorize]
        public ActionResult Agendamento([FromRoute] Guid idMedico, [FromRoute] Guid idPet, [FromBody] CreateAgendamento create)
        {

            var agendamento = _ServiceAgendamento.CreateAgendamento(idMedico, idPet, create.Data);

            return Created("api/[controller]", agendamento);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize]
        public ActionResult Delete(Guid id)
        {

            _ServiceAgendamento.DeleteAgendamento(id);

            return NoContent();
        }

        [HttpPut("{id:Guid}")]
        [Authorize]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Agendamento update)
        {
            Agendamento agendamentoUpdate = update;
            agendamentoUpdate.Id = id;
            var updateAgendamento = _ServiceAgendamento.UpdateAgendamento(agendamentoUpdate);

            return Ok(updateAgendamento);

        }

        [HttpGet("GetAgendamentoDetalhes/{id:Guid}")]
        [Authorize]
        public ActionResult GetAgendamentoDetalhes([FromRoute] Guid id)
        {
            var agendamento = _ServiceAgendamento.GetAgendamentoById(id);
            var Detalhes = new ViewModel()
            {
                Pet = _ServicePet.GetPetById(agendamento.Pet.PetId),
                Usuario = _ServiceUsuario.GetUsuarioById(agendamento.MedicoResponsavel.UsuarioId)

            };
            Detalhes.Agendamentos.Add(agendamento);


            //foreach (var item in agendamento)
            //{
            //    Detalhes.Agendamentos.Add(_ServiceUsuario.GetUsuarioById(item.AgendamentoId));
            //    Detalhes.Agendamentos.First(x => x.Id == item.AgendamentoId).Pet = item;
            //}

            //foreach (var item in pet.Prontuarios)
            //{
            //    Detalhes.Prontuarios.Add(_ServiceProntuario.GetProntuarioById(item.ProntuarioId));
            //    Detalhes.Prontuarios.First(x => x.Id == item.ProntuarioId).Pet = item;
            //}

            //foreach (var item in pet.Documentos)
            //{
            //    Detalhes.Documentos.Add(_ServiceDocumento.GetDocumentoById(item.DocumentoId));
            //    Detalhes.Documentos.First(x => x.Id == item.DocumentoId).Pet = item;
            //}

            if (Detalhes == null)
                return NoContent();

            return Ok(Detalhes);
        }

    }
}
