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
    public class ProntuarioController : ControllerBase
    {
        private ProntuarioService _ServiceProntuario;

        public ProntuarioController(ProntuarioService serviceProntuario)
        {
            _ServiceProntuario = serviceProntuario;
        }


        [HttpGet("getAll")]
        //[Authorize]
        public ActionResult GetAll()
        {
            var getAllPet = _ServiceProntuario.GetAll();

            return Ok(getAllPet);
        }



        [HttpGet("{id:Guid}")]
        //[Authorize]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var prontuario = _ServiceProntuario.GetProntuarioById(id);

            if (prontuario == null)
                return NoContent();

            return Ok(prontuario);
        }


        [HttpGet("{idPet:Guid}")]
        public ActionResult GetProntuariosByPetId([FromRoute] Guid idPet)
        {
            var prontuarios = _ServiceProntuario.GetAll().Where(x => x.Pet.PetId == idPet);

            if (prontuarios == null)
                return NoContent();

            return Ok(prontuarios);
        }



        [HttpPost("{idMedico:Guid}/{idPet:Guid}")]
        public ActionResult Prontuario([FromRoute] Guid idMedico, [FromRoute] Guid idPet, [FromBody] CreateProntuario prontuarioCreate)
        {

            var prontuario = _ServiceProntuario.CreateProntuario(idMedico, idPet, prontuarioCreate.Resumo, prontuarioCreate.Descricao);

            return Created("api/[controller]", prontuario);
        }


        [HttpDelete("{id:Guid}")]
        //[Authorize]
        public ActionResult Delete(Guid id)
        {

            _ServiceProntuario.DeleteProntuario(id);

            return NoContent();
        }


        [HttpPut("{id:Guid}")]
        //[Authorize]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Prontuario update)
        {
            Prontuario prontuarioUpdate = update;
            prontuarioUpdate.Id = id;
            var updateProntuario = _ServiceProntuario.UpdateProntuario(prontuarioUpdate);

            return Ok(updateProntuario);

        }
    }
}
