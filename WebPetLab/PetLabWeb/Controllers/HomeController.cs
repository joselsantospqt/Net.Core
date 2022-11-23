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
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        private IdentityUser _sessionExtensions;

        public HomeController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<HomeController> logger) : base(configuration)
        {
            _logger = logger;
            _sessionExtensions = SessionExtensionsModel.GetObject<IdentityUser>(httpContextAccessor.HttpContext.Session, "UserSign");
        }


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {


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
                    Telefone = _sessionExtensions.PhoneNumber
                };


                var retorno = await ApiSave(createUsuario, "Usuario");
                if (retorno.StatusCode != HttpStatusCode.OK)
                {
                    ModelState.AddModelError(string.Empty, retorno.Content.ToString());
                    return RedirectToAction("Autenticacao", "Login");
                }

                value = await ApiToken(Autorizicao);
            }

            if (value.Token != null)
            {
                SessionExtensionsModel.SetObject(this.HttpContext.Session, "Token", value);
                var pessoa = await ApiFindById<Usuario>(value.Token, _sessionExtensions.Email, "Usuario");
                if (pessoa != null)
                    return RedirectToAction("Index", "Feed", pessoa);
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
