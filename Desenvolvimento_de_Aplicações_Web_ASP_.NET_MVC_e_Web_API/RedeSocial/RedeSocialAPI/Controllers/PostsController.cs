using Microsoft.AspNetCore.Mvc;
using DTO;
using static System.String;
using static System.Guid;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RedeSocialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        static List<Post> list = new List<Post>() {

            new Post { Id = NewGuid(), Author = "author", CreatedAt = DateTime.UtcNow, Subject = "subjct" },
            new Post { Id = NewGuid(), Author = "author", CreatedAt = DateTime.UtcNow, Subject = "subjct" },
            new Post { Id = NewGuid(), Author = "author", CreatedAt = DateTime.UtcNow, Subject = "subjct" }

        };

        [HttpGet()]
        public List<Post> Get()
        {
            return list;
        }


        [HttpGet("{id}")]
        public Post Get(Guid id)
        {
            var post = list.FirstOrDefault(x => x.Id == id);

            return post;
        }

        [HttpPost("")]
        public ActionResult Post([FromBody] Post post)
        {
            list.Add(post);

            return Created("api/[controller]", post);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var post = list.FirstOrDefault(x => x.Id == id);
            list.Remove(post);

            return NoContent();
        }
    }
}
