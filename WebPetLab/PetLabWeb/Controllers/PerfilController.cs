﻿using Domain.Entidade;
using Domain.Entidade.Request;
using Domain.Entidade.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetLabWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetLabWeb.Controllers
{
    [Authorize]
    public class PerfilController : BaseController
    {
        private readonly ILogger<PerfilController> _logger;

        private IdentityUser _sessionUserSign;
        private TokenCode _sessionToken;

        public PerfilController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<PerfilController> logger) : base(configuration)
        {
            _logger = logger;
            _sessionUserSign = SessionExtensionsHelp.GetObject<IdentityUser>(httpContextAccessor.HttpContext.Session, "UserSign");
            _sessionToken = SessionExtensionsHelp.GetObject<TokenCode>(httpContextAccessor.HttpContext.Session, "Token");
        }

        [HttpGet]
        [Route("Perfil/Medicos")]
        public async Task<IActionResult> Medicos()
        {
            var medicos = await ApiFindAll<Usuario>(_sessionToken.Token, "Usuario/GetAllMedico");
            return View(medicos);
        }


        public IActionResult Editar(Usuario usuario)
        {
            return View(usuario);
        }

        [HttpGet]
        [Route("Perfil/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var usuario = await ApiFindById<Usuario>(_sessionToken.Token, id, "Usuario");

            return View(usuario);
        }

        [HttpPost]
        [Route("Perfil/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id, IFormCollection collection)
        {
            var existeImagem = false;

            MemoryStream ms = new MemoryStream();
            var fileName = $"Perfil_{id}{RandomNumber()}_.png";


            foreach (var item in this.Request.Form.Files)
            {
                existeImagem = true;

                item.CopyTo(ms);

                ms.Position = 0;
            }

            Usuario usuario = await ApiFindById<Usuario>(_sessionToken.Token, _sessionUserSign.Id, "Usuario");

            //if (existeImagem)
            usuario.Nome = collection["Nome"];
            usuario.Sobrenome = collection["Sobrenome"];
            usuario.Telefone = collection["Telefone"];
            usuario.Cpf = collection["Cpf"];
            usuario.Crm = collection["Crm"];
            usuario.Cnpj = collection["Cnpj"];
            usuario.DataNascimento = Convert.ToDateTime(collection["DataNascimento"]);
            usuario.TipoUsuario = EnumDescriptionHelp.ParseEnum<ETipoUsuario>(collection["TipoUsuario"]);

            var retorno = await ApiUpdate<Usuario>(_sessionToken.Token, _sessionUserSign.Id, usuario, "Usuario");

            if (retorno == null)
            {
                ViewData["messenger"] = "Houve Um erro durante o update !";
            }
            ViewData["messenger"] = "Alterado com Sucesso !";
            //else
            //    await _blobstorage.SaveUpdate(fileName, ms);

            return View("Editar", retorno);

        }
        private string RandomNumber()
        {
            Random generator = new Random();
            return generator.Next(0, 1000000).ToString("D6");
        }

        [HttpGet]
        [Route("Perfil/Excluir/{Id:guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var retorno = await ApiRemove(_sessionToken.Token, _sessionUserSign.Id, "Usuario");
            if (retorno.IsSuccessStatusCode)
                return RedirectToAction("Delete", "Autenticacao");

            return View();
        }

        [HttpGet]
        [Route("Perfil/PainelAdm")]
        public IActionResult PainelAdm()
        {
            return RedirectToAction("PainelAdm", "Autenticacao");
        }


        [HttpGet]
        [Route("Perfil/ExcluirImagem/{Id:guid}")]
        public async Task<IActionResult> ExcluirImagem(Guid id)
        {
            Usuario usuario = await ApiFindById<Usuario>(_sessionToken.Token, _sessionUserSign.Id, "Usuario");
            usuario.ImagemUrlusuario = "Perfil_default.png";
            var retorno = await ApiUpdate<Usuario>(_sessionToken.Token, _sessionUserSign.Id, usuario, "Usuario");

            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        [Route("Perfil/Detalhes/{Id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            ViewModel usuario = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Usuario/GetUsuarioDetalhes");

            return View(usuario);
        }


        [HttpGet]
        [Route("Perfil/DetalhesMedico/{Id:guid}")]
        public async Task<IActionResult> DetalhesMedico(Guid id)
        {
            ViewModel usuario = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Usuario/GetUsuarioDetalhes");

            return View(usuario);
        }
    }
}