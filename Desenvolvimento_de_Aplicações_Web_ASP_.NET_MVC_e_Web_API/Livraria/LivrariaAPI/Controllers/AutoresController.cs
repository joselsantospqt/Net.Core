using LivrariaAPI.Request;
using LivrariaCore;
using LivrariaCore.Database;
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

        private BancoDeDados db;

        public AutoresController(BancoDeDados bancoDeDados)
        {
            db = bancoDeDados;
        }

        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            var conexao = new AutorService(db);
            var todosPosts = conexao.GetAll();

            return Ok(todosPosts);
        }


        [HttpGet]
        public ActionResult GetByAuthor([FromQuery] string nome)
        {
            var conexao = new AutorService(db);
            var post = conexao.GetByNome(nome);

            return Ok(post);
        }


        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] Guid id)
        {
            var conexao = new AutorService(db);
            var post = conexao.GetById(id);

            if (post == null)
                return NoContent();

            return Ok(post);
        }

        [HttpPost]
        public ActionResult Autor([FromBody] CriarAutorRequest create)
        {
            var conexao = new AutorService(db);

            var criarPost = new Autor();
            criarPost.Nome = create.Nome;
            criarPost.Sobrenome = create.Sobrenome;
            criarPost.Datanascimento = create.Datanascimento;
            criarPost.Email = create.Email;
            criarPost.Senha = create.Senha;

            var post = conexao.CreateAutor(criarPost);

            return Created("api/[controller]", post);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var conexao = new AutorService(db);
            conexao.DeleteAutor(id);

            return NoContent();
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, AtualizarAutorRequest update)
        {

            var conexao = new AutorService(db);
            var atualizarAutor = new Autor();
            atualizarAutor.Nome = update.Nome;
            atualizarAutor.Sobrenome = update.Sobrenome;
            atualizarAutor.Email = update.Email;
            atualizarAutor.Senha = update.Senha;

            var updateAutor = conexao.UpdateAutor(id, atualizarAutor);

            return Ok(updateAutor);

        }


        //[HttpPost("{id}/livros")]
        //public ActionResult PostLivro([FromRoute] Guid id, [FromBody] Livro create)
        //{
        //    var conexao = new AutorService(db);
        //    var comment = conexao.CreateLivro(id, create);

        //    return Created("", comment);
        //}

        //[HttpGet("{id}/livros")]
        //public ActionResult GetLivros([FromRoute] Guid id, [FromQuery] Guid LivroId)
        //{
        //    var conexao = new AutorService(db);
        //    var comments = conexao.GetLivros(id, LivroId);

        //    return Ok(comments);
        //}
    }
}
