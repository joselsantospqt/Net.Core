using Domain.Entidade;
using Domain.Entidade.Request;
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
        public ActionResult GetExamesByProntuarioId([FromRoute] Guid id)
        {
            var exames = _ServiceExame.GetAll().Where(x => x.Prontuario.ProntuarioId == id);

            if (exames == null)
                return NoContent();

            return Ok(exames);
        }



        [HttpPost]
        public ActionResult Exame([FromRoute] Guid idProntuario, [FromBody] CreateExame exameCreate)
        {

            var exame = _ServiceExame.CreateExame(idProntuario, exameCreate.Descricao, exameCreate.Documento);

            return Created("api/[controller]", exame);
        }


        [HttpDelete("{id:Guid}")]
        //[Authorize]
        public ActionResult Delete(Guid id)
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
