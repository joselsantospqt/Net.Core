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
    public class LivrosController : ControllerBase
    {

        //    private BancoDeDados db;

        //    public LivrosController(BancoDeDados bancoDeDados)
        //    {
        //        db = bancoDeDados;
        //    }


        //    [HttpGet("getAll")]
        //    public ActionResult GetAll()
        //    {
        //        var conexao = new LivroService(db);
        //        var todosLivros = conexao.GetAll();

        //        return Ok(todosLivros);
        //    }


        //    [HttpGet]
        //    public ActionResult GetByTitulo([FromQuery] string pTitulo)
        //    {
        //        var conexao = new LivroService(db);
        //        var livro = conexao.GetByNome(pTitulo);

        //        return Ok(livro);
        //    }


        //    [HttpGet("{id}")]
        //    public ActionResult GetById([FromRoute] Guid id)
        //    {
        //        var conexao = new LivroService(db);
        //        var post = conexao.GetById(id);

        //        if (post == null)
        //            return NoContent();

        //        return Ok(post);
        //    }

        //    [HttpPost]
        //    public ActionResult Livro([FromBody] CriarLivroRequest create)
        //    {
        //        var conexao = new LivroService(db);

        //        var criarLivro = new Livro();
        //        criarLivro.AutorId = create.AutorId;
        //        criarLivro.Titulo = create.Titulo;
        //        criarLivro.Descricao = create.Descricao;
        //        criarLivro.ISBN = create.ISBN;
        //        criarLivro.CreateDt = DateTime.UtcNow;

        //        var post = conexao.CreateLivro(criarLivro);

        //        return Created("api/[controller]", post);
        //    }


        //    [HttpDelete("{id}")]
        //    public ActionResult Delete(Guid id)
        //    {
        //        var conexao = new LivroService(db);
        //        conexao.DeleteLivro(id);

        //        return NoContent();
        //    }


        //    [HttpPut("{id}")]
        //    public ActionResult Put([FromRoute] Guid id, AtualizarLivroRequest update)
        //    {

        //        var conexao = new LivroService(db);
        //        var atualizarLivro = new Livro();
        //        atualizarLivro.Titulo = update.Titulo;
        //        atualizarLivro.Descricao = update.Descricao;
        //        atualizarLivro.ISBN = update.ISBN;

        //        var updateLivro = conexao.UpdateLivro(id, atualizarLivro);

        //        return Ok(updateLivro);

        //    }

    }
}
