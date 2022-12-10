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
    public class ProntuarioController : ControllerBase
    {
        private PetService _ServicePet;
        private UsuarioService _ServiceUsuario;
        private ProntuarioService _ServiceProntuario;
        private DocumentoService _ServiceDocumento;

        public ProntuarioController(PetService servicePet, UsuarioService serviceUsuario, ProntuarioService serviceProntuario, DocumentoService serviceDocumento)
        {
            _ServicePet = servicePet;
            _ServiceUsuario = serviceUsuario;
            _ServiceDocumento = serviceDocumento;
            _ServiceProntuario = serviceProntuario;
        }


        [HttpGet("getAll")]
      
        public ActionResult GetAll()
        {
            var getAllPet = _ServiceProntuario.GetAll();

            return Ok(getAllPet);
        }



        [HttpGet("{id:Guid}")]
       
        public ActionResult GetById([FromRoute] Guid id)
        {

            var prontuario = _ServiceProntuario.GetProntuarioById(id);

            if (prontuario == null)
                return NoContent();

            return Ok(prontuario);
        }

        [HttpGet("GetAllProntuariosById/{Id:Guid}")]
        [Authorize]
        public ActionResult GetAllProntuariosById([FromRoute] Guid id)
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
                    Detalhes.ListaPets.Add(_ServicePet.GetPetById(itemPet.PetId));
                    Detalhes.ListaPets.First(x => x.Id == itemPet.PetId).Tutor = itemPet;
                }

                foreach (var itemPet in Detalhes.ListaPets)
                {
                    foreach (var item in itemPet.Prontuarios)
                    {
                        Detalhes.Prontuarios.Add(_ServiceProntuario.GetProntuarioById(item.ProntuarioId));
                        Detalhes.Prontuarios.First(x => x.Id == item.ProntuarioId).Pet = item;

                    }
                }

                foreach (var item in Detalhes.Prontuarios)
                {
                    if (Detalhes.ListaMedicos.Where(x => x.Id == item.Medico.UsuarioId).Count() == 0)
                        Detalhes.ListaMedicos.Add(_ServiceUsuario.GetUsuarioById(item.Medico.UsuarioId));
                }
            }
            else
            {
                Detalhes.ListaMedicos.Add(usuario);

                foreach (var item in usuario.Prontuarios)
                {
                    Detalhes.Prontuarios.Add(_ServiceProntuario.GetProntuarioById(item.ProntuarioId));
                    Detalhes.Prontuarios.First(x => x.Id == item.ProntuarioId).Medico = item;

                }

                foreach (var itemPet in Detalhes.Prontuarios)
                {
                    Detalhes.ListaPets.Add(_ServicePet.GetPetById(itemPet.Pet.PetId));
                }
            }

            if (Detalhes == null)
                return NoContent();

            return Ok(Detalhes);
        }



        [HttpPost("{idMedico:Guid}/{idPet:Guid}")]
        [Authorize]
        public ActionResult Prontuario([FromRoute] Guid idMedico, [FromRoute] Guid idPet, [FromBody] CreateProntuario prontuarioCreate)
        {

            var prontuario = _ServiceProntuario.CreateProntuario(idMedico, idPet, prontuarioCreate.Resumo, prontuarioCreate.Descricao);

            return Created("api/[controller]", prontuario);
        }


        [HttpDelete("{id:Guid}")]
        [Authorize]
        public ActionResult Delete(Guid id)
        {

            _ServiceProntuario.DeleteProntuario(id);

            return NoContent();
        }


        [HttpPut("{id:Guid}")]
        [Authorize]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Prontuario update)
        {
            Prontuario prontuarioUpdate = update;
            prontuarioUpdate.Id = id;
            var updateProntuario = _ServiceProntuario.UpdateProntuario(prontuarioUpdate);

            return Ok(updateProntuario);

        }

        [HttpGet("GetProntuarioDetalhesById/{Id:Guid}")]
        public ActionResult GetProntuarioDetalhesById([FromRoute] Guid id)
        {
            var prontuario = _ServiceProntuario.GetProntuarioById(id);

            var Detalhes = new ViewModel()
            {
                Prontuario = prontuario,
                Pet = _ServicePet.GetPetById(prontuario.Pet.PetId)
            };

            Detalhes.Usuario = _ServiceUsuario.GetUsuarioById(Detalhes.Pet.Tutor.UsuarioId);
            Detalhes.ListaMedicos.Add(_ServiceUsuario.GetUsuarioById(prontuario.Medico.UsuarioId));


            foreach (var itemPet in Detalhes.Usuario.Pets)
            {
                Detalhes.ListaPets.Add(_ServicePet.GetPetById(itemPet.PetId));
                Detalhes.ListaPets.First(x => x.Id == itemPet.PetId).Tutor = itemPet;
            }

            foreach (var item in prontuario.Documentos)
            {
                Detalhes.Documentos.Add(_ServiceDocumento.GetDocumentoById(item.DocumentoId));
                Detalhes.Documentos.First(x => x.Id == item.DocumentoId).Prontuario = item;
            }


            if (Detalhes == null)
                return NoContent();

            return Ok(Detalhes);
        }

    }
}
