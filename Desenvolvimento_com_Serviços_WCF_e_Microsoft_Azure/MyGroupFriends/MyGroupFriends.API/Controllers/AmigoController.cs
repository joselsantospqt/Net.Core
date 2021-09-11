using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGroupFriends.Domain.Entidades;
using MyGroupFriends.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroupFriends.API.Controllers
{
    public class AmigoController : Controller
    {
        private AmigoService _Service;

        public AmigoController(AmigoService service)
        {
            _Service = service;
        }


        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            var todosAmigos = _Service.GetAll();
            return Ok(todosAmigos);
        }


        [HttpGet("")]
        public ActionResult GetByAuthor([FromQuery] string nome)
        {
            var retorno = _Service.GetByNome(nome);
            if (retorno != null)
                return Ok(retorno);
            return NoContent();
        }


        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] Guid id)
        {
            var amigo = _Service.GetById(id);
            if (amigo == null)
                return NoContent();

            return Ok(amigo);
        }

        [HttpPost("")]
        public ActionResult Autor([FromBody] AmigoRequest create)
        {
            _Service.Create(create.Nome, create.Sobrenome, create.DtAniversario, create.Email, create.Telefone, create.Urlfoto);
            return Created("api/[controller]", create);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _Service.Delete(id);
            return NoContent();
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id,[FromBody] AmigoRequest update)
        {
            _Service.Update(id, update.Nome, update.Sobrenome, update.DtAniversario, update.Email, update.Telefone, update.Urlfoto);
            return Ok(update);

        }
    }
}
