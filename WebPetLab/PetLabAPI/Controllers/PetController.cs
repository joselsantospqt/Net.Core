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
    public class PetController : ControllerBase
    {
        private PetService _ServicePet;
        private UsuarioService _ServiceUsuario;
        private AgendamentoService _ServiceAgendamento;
        private ProntuarioService _ServiceProntuario;
        private DocumentoService _ServiceDocumento;

        public PetController(PetService servicePet, UsuarioService serviceUsuario, AgendamentoService serviceAgendamento, ProntuarioService serviceProntuario, DocumentoService serviceDocumento)
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
            var getAllPet = _ServicePet.GetAll();

            return Ok(getAllPet);
        }

        [HttpGet("{id:Guid}")]
        [Authorize]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var usuario = _ServicePet.GetPetById(id);

            if (usuario == null)
                return NoContent();

            return Ok(usuario);
        }

        [HttpPost("{id:Guid}")]
        [Authorize]
        public ActionResult Pet([FromRoute] Guid id, [FromBody] CreatePet create)
        {
            var pet = _ServicePet.CreatePet(id, create.Nome, create.DataNascimento, create.Especie);
            return Created("api/[controller]", pet);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize]
        public ActionResult Delete(Guid id)
        {

            _ServicePet.DeletePet(id);

            return NoContent();
        }

        [HttpPut("{id:Guid}")]
        [Authorize]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Pet update)
        {
            Pet petUpdate = update;
            petUpdate.Id = id;
            var updatePet = _ServicePet.UpdatePet(petUpdate);

            return Ok(updatePet);

        }

        [HttpGet("GetAllPetsByEmail/{email}")]
        [Authorize]
        public ActionResult GetAllPetsByEmail([FromRoute] string email)
        {
            var usuario = _ServiceUsuario.GetUsuarioByEmail(email);

            IList<Pet> Pets = new List<Pet>();
            foreach (var pet in usuario.Pets)
            {
                Pets.Add(_ServicePet.GetPetById(pet.PetId));
            }

            if (Pets == null)
                return NoContent();

            return Ok(Pets);
        }

        [HttpGet("GetAllPetsById/{idTutor:Guid}")]
        [Authorize]
        public ActionResult GetAllPetsById([FromRoute] Guid idTutor)
        {
            var usuario = _ServiceUsuario.GetUsuarioById(idTutor);
            IList<Pet> Pets = new List<Pet>();
            foreach (var pet in usuario.Pets)
            {
                Pets.Add(_ServicePet.GetPetById(pet.PetId));
                Pets.First(x => x.Id == pet.PetId).Tutor = pet;

            }

            if (Pets == null)
                return NoContent();

            return Ok(Pets);
        }

        [HttpGet("GetPetsDetalhes/{id:Guid}")]
        [Authorize]
        public ActionResult GetPetsDetalhes([FromRoute] Guid id)
        {
            var pet = _ServicePet.GetPetById(id);
            var Detalhes = new ViewModel()
            {
                Pet = pet,
                Usuario = _ServiceUsuario.GetUsuarioById(pet.Tutor.UsuarioId)
            };

            Detalhes.ListaPets.Add(pet);

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

            foreach (var item in pet.Documentos)
            {
                Detalhes.Documentos.Add(_ServiceDocumento.GetDocumentoById(item.DocumentoId));
                Detalhes.Documentos.First(x => x.Id == item.DocumentoId).Pet = item;
            }

            if (Detalhes == null)
                return NoContent();

            return Ok(Detalhes);
        }

    }
}
