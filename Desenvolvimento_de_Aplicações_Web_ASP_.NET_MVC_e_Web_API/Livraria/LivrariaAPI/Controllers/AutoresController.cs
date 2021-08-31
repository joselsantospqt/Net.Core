using LivrariaAPI.Request;
using LivrariaCore;
using LivrariaCore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private AutorService _Service;

        public AutoresController(AutorService service)
        {
            _Service = service;
        }


        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            var todosPosts = _Service.GetAll();
            return Ok(todosPosts);
        }


        //[HttpGet]
        //public ActionResult GetByAuthor([FromQuery] string nome)
        //{
        //    var conexao = new AutorService(db);
        //    var post = conexao.GetByNome(nome);

        //    return Ok(post);
        //}


        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] Guid id)
        {
            var post = _Service.GetById(id);
            if (post == null)
                return NoContent();

            return Ok(post);
        }

        [HttpPost]
        public ActionResult Autor([FromBody] CriarAutorRequest create)
        {
            _Service.Create(create.Nome, create.Sobrenome, create.Datanascimento, create.Email, create.Senha);
            return Created("api/[controller]", create);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _Service.Delete(id);
            return NoContent();
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, AtualizarAutorRequest update)
        {
            _Service.Update(id, update.Nome, update.Sobrenome, update.Email, update.Senha);
            return Ok(update);

        }

    }
}
