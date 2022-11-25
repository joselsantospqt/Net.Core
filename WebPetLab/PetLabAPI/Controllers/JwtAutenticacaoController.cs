using Domain.Entidade;
using Domain.Entidade.Request;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PetLabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtAutenticacaoController : ControllerBase
    {
        private IConfiguration _config;
        private UsuarioService _Service;
        public JwtAutenticacaoController(IConfiguration Configuration, UsuarioService usuarioService)
        {
            _config = Configuration;
            _Service = usuarioService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] CredenciaisUsuario Login)
        {
            bool resultado = ValidarUsuario(Login);
            if (resultado)
            {
                var usuario = _Service.GetValidaUsuarioLogin(Login);
                TokenCode token = GerarTokenJWT(usuario);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
        private TokenCode GerarTokenJWT(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>();

            claims.Add(new Claim("id", usuario.Id.ToString()));
            claims.Add(new Claim("password", usuario.Senha.ToString()));

            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"])), SecurityAlgorithms.HmacSha256),
                Audience = _config["Jwt:Audience"],
                Issuer = _config["Jwt:Issuer"]
            };

            var securityToken = tokenHandler.CreateToken(token);
            var stringToken = tokenHandler.WriteToken(securityToken);

            return new TokenCode()
            {
                IdUser = usuario.Id.ToString(),
                Token = stringToken,
                Expires = token.Expires.Value
            };
        }

        private bool ValidarUsuario(CredenciaisUsuario Login)
        {
            var pessoa = _Service.GetValidaUsuarioLogin(Login);

            if (pessoa != null)
                return true;
            else
                return false;
        }
    }
}
