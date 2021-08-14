using Microsoft.AspNetCore.Mvc;
using System;

namespace RedeSocialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult GetComments(Guid id)
        {

            return Ok();
        }
    }
}
