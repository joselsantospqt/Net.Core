using Domain.Entidade;
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
        private ETipoUsuario _sessionPerfilUsuario;

        public PerfilController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<PerfilController> logger) : base(configuration)
        {
            _logger = logger;
            _sessionUserSign = SessionExtensionsHelp.GetObject<IdentityUser>(httpContextAccessor.HttpContext.Session, "UserSign");
            _sessionToken = SessionExtensionsHelp.GetObject<TokenCode>(httpContextAccessor.HttpContext.Session, "Token");
            _sessionPerfilUsuario = SessionExtensionsHelp.GetObject<ETipoUsuario>(httpContextAccessor.HttpContext.Session, "UsuarioPerfil");
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
            try
            {
                var existeImagem = false;
                Usuario usuario = await ApiFindById<Usuario>(_sessionToken.Token, _sessionUserSign.Id, "Usuario");
                MemoryStream ms = new MemoryStream();
                foreach (var item in this.Request.Form.Files)
                {
                    existeImagem = true;
                    item.CopyTo(ms);
                    ms.Position = 0;
                    usuario.TipoAnexo = item.ContentType;
                }

                if (existeImagem)
                    usuario.Anexo = ms.ToArray();

                usuario.Nome = collection["Nome"];
                usuario.Sobrenome = collection["Sobrenome"];
                usuario.Telefone = collection["Telefone"];
                usuario.Cpf = collection["Cpf"];
                usuario.DataNascimento = Convert.ToDateTime(collection["DataNascimento"]);
                if (usuario.TipoUsuario == ETipoUsuario.Medico)
                {
                    usuario.Crm = collection["Crm"];
                    usuario.Cnpj = collection["Cnpj"];
                }
                else
                    usuario.TipoUsuario = EnumDescriptionHelp.ParseEnum<ETipoUsuario>(collection["TipoUsuario"]);

                var retorno = await ApiUpdate<Usuario>(_sessionToken.Token, _sessionUserSign.Id, usuario, "Usuario");


                if (retorno == null)
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve Um erro durante o update !");
                else
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Alterado com Sucesso !");

                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "UsuarioPerfil", retorno.TipoUsuario);

                return View("Editar", retorno);
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }
        }

        [HttpGet]
        [Route("Perfil/Excluir/{Id:guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                if (_sessionPerfilUsuario != ETipoUsuario.Medico)
                {

                    ViewModel usuario = await ApiFindById<ViewModel>(_sessionToken.Token, _sessionUserSign.Id, "Usuario/GetUsuarioDetalhes");

                    foreach (var item in usuario.Agendamentos)
                    {
                        await ApiRemove(_sessionToken.Token, item.Id, "Agendamento");
                    }
                    foreach (var item in usuario.Documentos)
                    {
                        await ApiRemove(_sessionToken.Token, item.Id, "Documentos");
                    }
                    foreach (var item in usuario.Prontuarios)
                    {
                        await ApiRemove(_sessionToken.Token, item.Id, "Prontuario");
                    }
                    foreach (var item in usuario.ListaPets)
                    {
                        await ApiRemove(_sessionToken.Token, item.Id, "Pet");
                    }

                    var retorno = await ApiRemove(_sessionToken.Token, _sessionUserSign.Id, "Usuario");
                    if (retorno.IsSuccessStatusCode)
                        return RedirectToAction("Delete", "Autenticacao");
                }
                else
                {
                    Usuario Usuario = await ApiFindById<Usuario>(_sessionToken.Token, id, "Usuario");
                    Usuario.Telefone = "00000000000";
                    Usuario.Nome = "Usuário Desativado";
                    Usuario.Sobrenome = "Usuário Desativado";
                    Usuario.Anexo = null;
                    Usuario.TipoAnexo = null;
                    Usuario.TipoUsuario = ETipoUsuario.Tutor;
                    var retorno = await ApiUpdate<Usuario>(_sessionToken.Token, _sessionUserSign.Id, Usuario, "Usuario");
                    if (retorno != null)
                    {
                        SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Excluido com Sucesso !");
                        return RedirectToAction("Delete", "Autenticacao");
                    }
                    else
                    {
                        SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve Um erro durante o update !");
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }
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
            try
            {
                Usuario usuario = await ApiFindById<Usuario>(_sessionToken.Token, id, "Usuario");
                usuario.Anexo = null;
                usuario.TipoAnexo = null;

                var retorno = await ApiUpdate<Usuario>(_sessionToken.Token, usuario.Id, usuario, "Usuario");

                if (retorno != null)
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Foto do excluida com sucesso !");
                else
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve uma falhar ao excluir o Foto do usuário!");

                return RedirectToAction("Editar", new { Id = usuario.Id });
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }
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
