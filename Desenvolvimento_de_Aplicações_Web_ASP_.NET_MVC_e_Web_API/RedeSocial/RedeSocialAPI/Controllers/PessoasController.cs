using Microsoft.AspNetCore.Mvc;
using System;

namespace RedeSocialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult GetPessoa(Guid id)
        {

            return Ok();
        }
    }
}
