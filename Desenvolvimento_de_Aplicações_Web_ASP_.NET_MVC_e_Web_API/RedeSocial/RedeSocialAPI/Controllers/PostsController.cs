using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static System.String;
using static System.Guid;
using System.Linq;
using DTO;
using DTO.Database;
using DTO.Service;

namespace RedeSocialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        //static List<Post> list = new List<Post>() {

        //    new Post { Id = NewGuid(), Author = "author", CreatedAt = DateTime.UtcNow, Subject = "subjct" },
        //    new Post { Id = NewGuid(), Author = "author", CreatedAt = DateTime.UtcNow, Subject = "subjct" },
        //    new Post { Id = NewGuid(), Author = "author", CreatedAt = DateTime.UtcNow, Subject = "subjct" }

        //};


        private BancoDeDados db;

        public PostsController(BancoDeDados bancoDeDados)
        {
            db = bancoDeDados;
        }

        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            var conexao = new PostService(db);
            var todosPosts = conexao.GetAll();

            return Ok(todosPosts);
        }


        [HttpGet]
        public ActionResult GetByAuthor([FromQuery] string author)
        {
            var conexao = new PostService(db);
            var post = conexao.GetAuthor(author);

            return Ok(post);
        }


        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] Guid id)
        {
            var conexao = new PostService(db);
            var post = conexao.GetPost(id);

            if (post == null)
                return NoContent();

            return Ok(post);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Post create)
        {
            var conexao = new PostService(db);
            var post = conexao.CreatePost(create);

            return Created("api/[controller]", post);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var conexao = new PostService(db);
            conexao.DeletePost(id);

            return NoContent();
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, Post update)
        {

            var conexao = new PostService(db);
            var updatePost = conexao.UpdatePost(id, update);

            return Ok(updatePost);

        }


        [HttpPost("{id}/comments")]
        public ActionResult PostComment([FromRoute] Guid id, [FromBody] Comment create)
        {
            var conexao = new PostService(db);
            var comment = conexao.CreateComment(id, create);

            return Created("", comment);
        }

        [HttpGet("{id}/comments")]
        public ActionResult GetComments([FromRoute] Guid id, [FromQuery] Guid commentId)
        {
            var conexao = new PostService(db);
            var comments = conexao.GetComments(id, commentId);

            return Ok(comments);
        }

    }
}
