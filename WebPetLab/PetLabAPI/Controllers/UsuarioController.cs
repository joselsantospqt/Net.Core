using Domain.Entidade;
using Domain.Entidade.Request;
using Domain.Entidade.View;
using Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetLabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private PetService _ServicePet;
        private UsuarioService _ServiceUsuario;
        private AgendamentoService _ServiceAgendamento;
        private ProntuarioService _ServiceProntuario;
        private DocumentoService _ServiceDocumento;

        public UsuarioController(PetService servicePet, UsuarioService serviceUsuario, AgendamentoService serviceAgendamento, ProntuarioService serviceProntuario, DocumentoService serviceDocumento)
        {
            _ServicePet = servicePet;
            _ServiceUsuario = serviceUsuario;
            _ServiceDocumento = serviceDocumento;
            _ServiceProntuario = serviceProntuario;
            _ServiceAgendamento = serviceAgendamento;
        }


        [HttpGet("GetAll")]
        [Authorize]
        public ActionResult GetAll()
        {
            var getAllUsuario = _ServiceUsuario.GetAll();

            if (getAllUsuario == null)
                return NoContent();

            return Ok(getAllUsuario);
        }

        [HttpGet("GetAllTutor")]
        [Authorize]
        public ActionResult GetAllTutor()
        {
            var getAllUsuario = _ServiceUsuario.GetAll().Where(x => x.TipoUsuario == ETipoUsuario.Tutor).ToList();

            if (getAllUsuario == null)
                return NoContent();

            return Ok(getAllUsuario);
        }


        [HttpGet("GetAllMedico")]
        [Authorize]
        public ActionResult GetAllMedico()
        {
            var getAllUsuario = _ServiceUsuario.GetAll().Where(x => x.TipoUsuario == ETipoUsuario.Medico).ToList();

            if (getAllUsuario == null)
                return NoContent();

            return Ok(getAllUsuario);
        }

        [HttpGet("{id:Guid}")]
        [Authorize]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var usuario = _ServiceUsuario.GetUsuarioById(id);

            if (usuario == null)
                return NoContent();

            return Ok(usuario);
        }


        [HttpGet("{email}")]
        [Authorize]
        public ActionResult GetByEmail([FromRoute] string email)
        {

            var usuario = _ServiceUsuario.GetUsuarioByEmail(email);

            if (usuario == null)
                return NoContent();

            return Ok(usuario);
        }



        [HttpPost]
        public ActionResult Usuario([FromBody] CreateUsuario create)
        {
            var usuario = _ServiceUsuario.GetUsuarioById(create.Id);

            if (usuario != null)
                return NoContent();

            usuario = _ServiceUsuario.CreateUsuario(create.Id, create.Nome, create.Sobrenome, create.Telefone, create.Cpf, create.Cnpj, create.Crm, create.DataNascimento, create.Email, create.Senha, create.TipoUsuario, create.Anexo, create.TipoAnexo);

            return Created("api/[controller]", usuario);
        }


        [HttpDelete("{id:Guid}")]
        [Authorize]
        public ActionResult Delete(Guid id)
        {

            _ServiceUsuario.DeleteUsuario(id);

            return NoContent();
        }


        [HttpPut("{id:Guid}")]
        [Authorize]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Usuario update)
        {
            Usuario usuarioUpdate = update;
            usuarioUpdate.Id = id;
            var updateUsuario = _ServiceUsuario.UpdateUsuario(usuarioUpdate);

            return Ok(updateUsuario);

        }
        [HttpGet("GetUsuarioDetalhes/{id:Guid}")]
        [Authorize]
        public ActionResult GetUsuarioDetalhes([FromRoute] Guid id)
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

                    foreach (var item in pet.Documentos)
                    {
                        Detalhes.Documentos.Add(_ServiceDocumento.GetDocumentoById(item.DocumentoId));
                        Detalhes.Documentos.First(x => x.Id == item.DocumentoId).Pet = item;
                    }
                }
            }
            else
            {

                Detalhes.ListaMedicos.Add(usuario);

                foreach (var item in Detalhes.Usuario.Agendamentos)
                {
                    Detalhes.Agendamentos.Add(_ServiceAgendamento.GetAgendamentoById(item.AgendamentoId));
                    Detalhes.Agendamentos.First(x => x.Id == item.AgendamentoId).MedicoResponsavel = item;
                }

                foreach (var item in Detalhes.Agendamentos)
                {
                    Detalhes.ListaPets.Add(_ServicePet.GetPetById(item.Pet.PetId));
                }
            }

            if (Detalhes == null)
                return NoContent();

            return Ok(Detalhes);
        }

    }
}
