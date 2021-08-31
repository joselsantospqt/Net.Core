using LivrariaAPI.Request;
using LivrariaCore;
using LivrariaCore.Repositorio;
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
    public class LivrosController : ControllerBase
    {
        private LivroService _Service { get; }

        public LivrosController(LivroService livroService)
        {
            _Service = livroService;
        }

        [HttpGet("getAll")]
        public ActionResult GetAll()
        {

            var todosLivros = _Service.GetAll();
            return Ok(todosLivros);
        }


        //[HttpGet]
        //public ActionResult GetByTitulo([FromQuery] string pTitulo)
        //{

        //    var livro = _Service.GetByNome(pTitulo);

        //    return Ok(livro);
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
        public ActionResult Livro([FromBody] CriarLivroRequest create)
        {
            var post = _Service.Create(create.AutorId, create.Titulo, create.Descricao, create.ISBN);
            return Created("api/[controller]", post);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _Service.Delete(id);
            return NoContent();
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, AtualizarLivroRequest update)
        {
            var updateLivro = _Service.Update(id, update.Titulo, update.Descricao, update.ISBN);
            return Ok(updateLivro);
        }

    }
}
