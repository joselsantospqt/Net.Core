using Domain.Entidade;
using Domain.Entidade.Request;
using Domain.Service;
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
        private UsuarioService _ServiceUsuario;

        public UsuarioController(UsuarioService serviceUsuario)
        {
            _ServiceUsuario = serviceUsuario;
        }


        [HttpGet("getAll")]
        //[Authorize]
        public ActionResult GetAll()
        {
            var getAllUsuario = _ServiceUsuario.GetAll();

            return Ok(getAllUsuario);
        }



        [HttpGet("{id:Guid}")]
        //[Authorize]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var usuario = _ServiceUsuario.GetUsuarioById(id);

            if (usuario == null)
                return NoContent();

            return Ok(usuario);
        }


        [HttpGet("{email}")]
        public ActionResult GetByEmail([FromRoute] string email)
        {

            var usuario = _ServiceUsuario.GetAll().Where(x => x.Email == email);

            if (usuario == null)
                return NoContent();

            return Ok(usuario);
        }



        [HttpPost]
        public ActionResult Usuario([FromBody] CreateUsuario create)
        {

            var usuario = _ServiceUsuario.CreateUsuario(create.Nome, create.Sobrenome, create.Telefone, create.Cpf, create.Cnpj, create.DataNascimento, create.Email, create.Senha, create.TipoUsuario);

            return Created("api/[controller]", usuario);
        }


        [HttpDelete("{id:Guid}")]
        //[Authorize]
        public ActionResult Delete(Guid id)
        {

            _ServiceUsuario.DeleteUsuario(id);

            return NoContent();
        }


        [HttpPut("{id:Guid}")]
        //[Authorize]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Usuario update)
        {
            Usuario usuarioUpdate = update;
            usuarioUpdate.Id = id;
            var updateUsuario = _ServiceUsuario.UpdateUsuario(usuarioUpdate);

            return Ok(updateUsuario);

        }



    }
}
