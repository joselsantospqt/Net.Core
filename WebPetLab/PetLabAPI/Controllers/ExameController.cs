using Domain.Entidade;
using Domain.Entidade.Request;
using Domain.Service;
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
    public class ExameController : ControllerBase
    {
        private ExameService _ServiceExame;
        public ExameController(ExameService serviceExame)
        {
            _ServiceExame = serviceExame;
        }


        [HttpGet("getAll")]
        //[Authorize]
        public ActionResult GetAll()
        {
            var getAllPet = _ServiceExame.GetAll();

            return Ok(getAllPet);
        }



        [HttpGet("{id:Guid}")]
        //[Authorize]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var exame = _ServiceExame.GetExameById(id);

            if (exame == null)
                return NoContent();

            return Ok(exame);
        }


        [HttpGet("{idProntuario:Guid}")]
        public ActionResult GetExamesByProntuarioId([FromRoute] Guid idProntuario)
        {
            var exames = _ServiceExame.GetAll().Where(x => x.Prontuario.ProntuarioId == idProntuario);

            if (exames == null)
                return NoContent();

            return Ok(exames);
        }



        [HttpPost("{idProntuario:Guid}")]
        public ActionResult Exame([FromRoute] Guid idProntuario, [FromBody] CreateExame exameCreate)
        {

            FileStream stream = new FileStream(
                exameCreate.Documento, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();



            var exame = _ServiceExame.CreateExame(idProntuario, exameCreate.Descricao, photo);

            return Created("api/[controller]", exame);
        }


        [HttpDelete("{id:Guid}")]
        //[Authorize]
        public ActionResult Delete([FromRoute] Guid id)
        {

            _ServiceExame.DeleteExame(id);

            return NoContent();
        }


        [HttpPut("{id:Guid}")]
        //[Authorize]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Exame update)
        {
            Exame exameUpdate = update;
            exameUpdate.Id = id;
            var updateExame = _ServiceExame.UpdateExame(exameUpdate);

            return Ok(updateExame);

        }
    }
}
