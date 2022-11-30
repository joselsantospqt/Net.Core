using Domain.Entidade;
using Domain.Entidade.Request;
using Domain.Entidade.View;
using Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetLabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private PetService _ServicePet;
        private UsuarioService _ServiceUsuario;
        private ProntuarioService _ServiceProntuario;
        private DocumentoService _ServiceDocumento;

        public DocumentoController(PetService servicePet, UsuarioService serviceUsuario, ProntuarioService serviceProntuario, DocumentoService serviceDocumento)
        {
            _ServicePet = servicePet;
            _ServiceUsuario = serviceUsuario;
            _ServiceDocumento = serviceDocumento;
            _ServiceProntuario = serviceProntuario;
        }


        [HttpGet("getAll")]
        [Authorize]
        public ActionResult GetAll()
        {
            var getAllDocumentos = _ServiceDocumento.GetAll();

            return Ok(getAllDocumentos);
        }



        [HttpGet("{id:Guid}")]
        [Authorize]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var documento = _ServiceDocumento.GetDocumentoById(id);

            if (documento == null)
                return NoContent();

            return Ok(documento);
        }


        [HttpGet("GetDocumentosByProntuarioId/{idProntuario:Guid}")]
        [Authorize]
        public ActionResult GetDocumentosByProntuarioId([FromRoute] Guid idProntuario)
        {
            var documentos = _ServiceDocumento.GetAll().Where(x => x.Prontuario.ProntuarioId == idProntuario);

            if (documentos == null)
                return NoContent();

            return Ok(documentos);
        }

        [HttpGet("GetDocumentosAllById/{Id:Guid}")]
        [Authorize]
        public ActionResult GetDocumentosAllById([FromRoute] Guid id)
        {
            var usuario = _ServiceUsuario.GetUsuarioById(id);
            var Detalhes = new ViewModel()
            {
                Usuario = _ServiceUsuario.GetUsuarioById(id)
            };

            foreach (var itemPet in usuario.Pets)
            {
                var pet = _ServicePet.GetPetById(itemPet.PetId);
                Detalhes.ListaPets.Add(pet);
                foreach (var doc in pet.Documentos)
                {
                    Detalhes.Documentos.Add(_ServiceDocumento.GetDocumentoById(doc.DocumentoId));
                    Detalhes.Documentos.First(x => x.Id == doc.DocumentoId).Pet = doc;
                }

                foreach (var item in pet.Prontuarios)
                {
                    Detalhes.Prontuarios.Add(_ServiceProntuario.GetProntuarioById(item.ProntuarioId));
                    Detalhes.Prontuarios.First(x => x.Id == item.ProntuarioId).Pet = item;
                }
            }

            if (Detalhes == null)
                return NoContent();

            return Ok(Detalhes);
        }



        [HttpPost("{idProntuario:Guid}/{idPet:Guid}")]
        [Authorize]
        public ActionResult Documento([FromRoute] Guid idProntuario, [FromRoute] Guid idPet, [FromBody] CreateDocumento documentoCreate)
        {

            var documento = _ServiceDocumento.CreateDocumento(idProntuario, idPet, documentoCreate.Descricao, documentoCreate.Quantidade, documentoCreate.Nome, documentoCreate.TipoAnexo, documentoCreate.Anexo, documentoCreate.TipoDocumento, documentoCreate.DataInicio, documentoCreate.DataFim);

            return Created("api/[controller]", documento);
        }


        [HttpDelete("{id:Guid}")]
        [Authorize]
        public ActionResult Delete([FromRoute] Guid id)
        {

            _ServiceDocumento.DeleteDocumento(id);

            return NoContent();
        }


        [HttpPut("{id:Guid}")]
        [Authorize]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Documento update)
        {
            Documento documentoUpdate = update;
            documentoUpdate.Id = id;
            var updateDocumento = _ServiceDocumento.UpdateDocumento(documentoUpdate);

            if (updateDocumento != null)
                return Ok(updateDocumento);

            return NoContent();
        }

        [HttpGet("GetDocumentoDetalhesById/{id:Guid}")]
        [Authorize]
        public ActionResult GetDocumentoDetalhesById([FromRoute] Guid id)
        {
            var documento = _ServiceDocumento.GetDocumentoById(id);
            var Detalhes = new ViewModel() { Pet = _ServicePet.GetPetById(documento.Pet.PetId), Documento = documento };

            Detalhes.Usuario = _ServiceUsuario.GetUsuarioById(Detalhes.Pet.Tutor.UsuarioId);

            foreach (var itemPet in Detalhes.Usuario.Pets)
            {
                var pet = _ServicePet.GetPetById(itemPet.PetId);
                Detalhes.ListaPets.Add(pet);

                foreach (var item in pet.Prontuarios)
                {
                    Detalhes.Prontuarios.Add(_ServiceProntuario.GetProntuarioById(item.ProntuarioId));
                    Detalhes.Prontuarios.First(x => x.Id == item.ProntuarioId).Pet = item;
                }
            }

            if (documento.Prontuario != null && documento.Prontuario.ProntuarioId != new Guid("{00000000-0000-0000-0000-000000000000}"))
            {
                Detalhes.Prontuario = _ServiceProntuario.GetProntuarioById(documento.Prontuario.ProntuarioId);
            }


            if (Detalhes == null)
                return NoContent();

            return Ok(Detalhes);
        }


    }
}
