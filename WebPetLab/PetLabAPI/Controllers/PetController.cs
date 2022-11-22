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
    public class PetController : ControllerBase
    {
        private PetService _ServicePet;
        private UsuarioService _ServiceUsuario;

        public PetController(PetService servicePet, UsuarioService ServiceUsuario)
        {
            _ServicePet = servicePet;
            _ServiceUsuario = ServiceUsuario;
        }


        [HttpGet("getAll")]
        //[Authorize]
        public ActionResult GetAll()
        {
            var getAllPet = _ServicePet.GetAll();

            return Ok(getAllPet);
        }



        [HttpGet("{id:Guid}")]
        //[Authorize]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var usuario = _ServicePet.GetPetById(id);

            if (usuario == null)
                return NoContent();

            return Ok(usuario);
        }


        [HttpGet("{email}")]
        public ActionResult GetPetsByEmail([FromRoute] string email)
        {
            var usuario = _ServiceUsuario.GetAll().Where(x => x.Email == email).First();
            var pets = _ServicePet.GetAll().Where(x => x.Tutor.UsuarioId == usuario.Id);

            if (pets == null)
                return NoContent();

            return Ok(pets);
        }



        [HttpPost("{id:Guid}")]
        public ActionResult Pet([FromRoute] Guid id, [FromBody] CreatePet create)
        {

            var pet = _ServicePet.CreatePet(id, create.Nome, create.DataNascimento, create.Especie);
            return Created("api/[controller]", pet);
        }


        [HttpDelete("{id:Guid}")]
        //[Authorize]
        public ActionResult Delete(Guid id)
        {

            _ServicePet.DeletePet(id);

            return NoContent();
        }


        [HttpPut("{id:Guid}")]
        //[Authorize]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Pet update)
        {
            Pet petUpdate = update;
            petUpdate.Id = id;
            var updatePet = _ServicePet.UpdatePet(petUpdate);

            return Ok(updatePet);

        }
    }
}
