using Domain.Entidade;
using Domain.Entidade.Request;
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
        private DocumentoService _ServiceDocumento;
        public DocumentoController(DocumentoService serviceDocumento)
        {
            _ServiceDocumento = serviceDocumento;
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
        public ActionResult GetDocumentosByProntuarioId([FromRoute] Guid idProntuario)
        {
            var documentos = _ServiceDocumento.GetAll().Where(x => x.Prontuario.ProntuarioId == idProntuario);

            if (documentos == null)
                return NoContent();

            return Ok(documentos);
        }



        [HttpPost("{idProntuario:Guid}/{idPet:Guid}")]
        public ActionResult Documento([FromRoute] Guid idProntuario, [FromRoute] Guid idPet, [FromBody] CreateDocumento documentoCreate)
        {

            FileStream stream = new FileStream(
                documentoCreate.url_documento, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            var documento = _ServiceDocumento.CreateDocumento(idProntuario, idPet, documentoCreate.Descricao, documentoCreate.Quantidade, documentoCreate.Nome, documentoCreate.TipoAnexo, photo, documentoCreate.TipoDocumento, documentoCreate.DataInicio, documentoCreate.DataFim);

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

            return Ok(updateDocumento);

        }
    }
}
