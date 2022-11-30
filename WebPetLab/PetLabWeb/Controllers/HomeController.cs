using Domain.Entidade;
using Domain.Entidade.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetLabWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetLabWeb.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private IdentityUser _sessionExtensions;

        public HomeController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<HomeController> logger) : base(configuration)
        {
            _logger = logger;
            _HttpContextAccessor = httpContextAccessor;
            _sessionExtensions = SessionExtensionsHelp.GetObject<IdentityUser>(httpContextAccessor.HttpContext.Session, "UserSign");
        }

        public IActionResult Home()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            bool PrimeiroAcesso = false;
            CredenciaisUsuario Autorizicao = new()
            {
                idUsuario = Guid.Parse(_sessionExtensions.Id),
                PasswordHash = _sessionExtensions.PasswordHash
            };

            var value = await ApiToken(Autorizicao);
            if (value.Token == null)
            {
                var createUsuario = new CreateUsuario
                {
                    Id = Guid.Parse(_sessionExtensions.Id),
                    Email = _sessionExtensions.Email,
                    Nome = _sessionExtensions.UserName.Substring(0, _sessionExtensions.UserName.IndexOf('@')),
                    Senha = _sessionExtensions.PasswordHash,
                    Telefone = _sessionExtensions.PhoneNumber,
                    DataNascimento = DateTime.UtcNow,
                    TipoUsuario = ETipoUsuario.Tutor
                };
                createUsuario.Sobrenome = createUsuario.Nao_Preenchido;
                createUsuario.Cpf = createUsuario.Nao_Preenchido;
                createUsuario.Cnpj = createUsuario.Nao_Preenchido;
                createUsuario.Crm = createUsuario.Nao_Preenchido;
                createUsuario.url_documento = createUsuario.Nao_Preenchido;

                var retorno = await ApiSave(createUsuario, "Usuario");
                if (retorno.StatusCode != HttpStatusCode.OK)
                {
                    SessionExtensionsHelp.RemoveObject(_HttpContextAccessor.HttpContext.Session, "UserSign");
                    SessionExtensionsHelp.RemoveObject(_HttpContextAccessor.HttpContext.Session, "Token");
                    SessionExtensionsHelp.RemoveObject(_HttpContextAccessor.HttpContext.Session, "UsuarioPerfil");

                    ViewData["Status"] = retorno.Content.ToString();
                    return RedirectToAction("Login", "Autenticacao");
                }

                PrimeiroAcesso = true;
                value = await ApiToken(Autorizicao);
            }

            if (value.Token != null)
            {

                var pessoa = await ApiFindById<Usuario>(value.Token, _sessionExtensions.Email, "Usuario");

                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "UsuarioPerfil", pessoa.TipoUsuario);

                if (value.Token != null && pessoa != null)
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Token", value);
                else
                {
                    ModelState.AddModelError(string.Empty, "Falhar ao realizar login !");
                    return RedirectToAction("Autenticacao", "Login");
                }

                if (PrimeiroAcesso)
                    return RedirectToAction("Editar", "Perfil", pessoa);

                return RedirectToAction("Home", "Home");



            }

            ModelState.AddModelError(string.Empty, "Houve uma Falha, Contate o Administrador.");
            return RedirectToAction("Autenticacao", "Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
