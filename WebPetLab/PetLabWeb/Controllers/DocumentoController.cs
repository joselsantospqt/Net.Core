using Domain.Entidade;
using Domain.Entidade.Request;
using Domain.Entidade.View;
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
    public class DocumentoController : BaseController
    {
        private readonly ILogger<DocumentoController> _logger;
        private IdentityUser _sessionUserSign;
        private TokenCode _sessionToken;
        private ETipoUsuario _sessionTipoUsuario;

        public DocumentoController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<DocumentoController> logger) : base(configuration)
        {
            _logger = logger;
            _sessionUserSign = SessionExtensionsHelp.GetObject<IdentityUser>(httpContextAccessor.HttpContext.Session, "UserSign");
            _sessionToken = SessionExtensionsHelp.GetObject<TokenCode>(httpContextAccessor.HttpContext.Session, "Token");
            _sessionTipoUsuario = SessionExtensionsHelp.GetObject<ETipoUsuario>(httpContextAccessor.HttpContext.Session, "UsuarioPerfil");
        }

        [HttpGet]
        [Route("Documento/Listar/{Id:guid}")]
        public async Task<IActionResult> Listar(Guid id)
        {
            var documentos = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Documento/GetDocumentosAllById");
            return View(documentos);
        }

        [Route("Documento/Criar/{Id:guid}")]
        public async Task<IActionResult> Criar(Guid id)
        {
            try
            {
                var pets = await ApiFindAllById<Pet>(_sessionToken.Token, id, "Pet/GetAllPetsById");
                var usuario = await ApiFindById<Usuario>(_sessionToken.Token, id, "Usuario");
                IList<Prontuario> listaProntuario = new List<Prontuario>();

                foreach (var item in pets)
                {
                    foreach (var prontuario in item.Prontuarios)
                    {
                        listaProntuario.Add(await ApiFindById<Prontuario>(_sessionToken.Token, prontuario.ProntuarioId, "Prontuario"));
                    }
                }
                ViewModel agendamento = new ViewModel()
                {
                    ListaPets = pets,
                    Prontuarios = listaProntuario,
                    Usuario = usuario

                };
                return View(agendamento);
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }
        }


        [HttpPost]
        [Route("Documento/Criar/{Id:guid}")]
        public async Task<IActionResult> Criar(Guid id, IFormCollection collection)
        {
            try
            {
                var existeImagem = false;

                MemoryStream ms = new MemoryStream();
                CreateDocumento documento = new CreateDocumento();
                Guid idProntuario = new Guid();


                foreach (var item in this.Request.Form.Files)
                {
                    existeImagem = true;
                    item.CopyTo(ms);
                    ms.Position = 0;
                    documento.TipoAnexo = item.ContentType;
                }

                if (existeImagem)
                    documento.Anexo = ms.ToArray();

                if (new Guid(collection["Prontuarios"]) == new Guid("{00000000-0000-0000-0000-000000000000}"))
                    idProntuario = new Guid(collection["Prontuarios"]);

                if (new Guid(collection["ListaPets"]) == new Guid("{00000000-0000-0000-0000-000000000000}"))
                {
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "É preciso associar a pelo menos um pet !");
                    return RedirectToAction("Listar", new { id = _sessionUserSign.Id });
                }

                documento.Nome = collection["Nome"];
                documento.Descricao = collection["Descricao"];
                documento.Quantidade = Convert.ToInt32(collection["Quantidade"]);
                documento.TipoDocumento = EnumDescriptionHelp.ParseEnum<ETipoDocumento>(collection["Documento.TipoDocumento"]);
                documento.DataInicio = Convert.ToDateTime(collection["DataInicio"]);
                documento.DataInicio = Convert.ToDateTime(collection["DataFim"]);
                var retorno = await ApiSaveAutorize<Documento>(_sessionToken.Token, documento, $"Documento/{idProntuario}/{collection["ListaPets"]}");

                if (retorno != null)
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Documento criado com sucesso !");
                else
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve uma falhar ao criar o documento !");

                if (_sessionTipoUsuario == ETipoUsuario.Medico)
                    return RedirectToAction("Listar", "Agendamento", new { id = _sessionUserSign.Id });
                else
                    return RedirectToAction("Listar", new { id = _sessionUserSign.Id });
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }

        }

        [HttpGet]
        [Route("Documento/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var documento = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Documento/GetDocumentoDetalhesById");
            return View(documento);
        }

        [HttpPost]
        [Route("Documento/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id, IFormCollection collection)
        {
            try
            {
                if (new Guid(collection["Id"]) != id)
                {
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve Um erro durante a exclusão !");
                    return View(new Guid(collection["Id"]));
                }

                Documento documento = await ApiFindById<Documento>(_sessionToken.Token, id, "Documento");

                var retorno = await ApiRemove(_sessionToken.Token, id, "Documento");

                if (retorno != null)
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Documento excluido com sucesso !");
                else
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve uma falhar ao excluir o documento !");

                if (documento.Prontuario.ProntuarioId != new Guid("{00000000-0000-0000-0000-000000000000}"))
                    return RedirectToAction("Detalhes", "Prontuario", new { id = documento.Prontuario.ProntuarioId });

                return RedirectToAction("Listar", new { id = _sessionUserSign.Id });
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }
        }

        [HttpGet]
        [Route("Documento/Detalhes/{Id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var detalhes = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Documento/GetDocumentoDetalhesById");
            return View(detalhes);
        }

        [HttpGet]
        [Route("Documento/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var documento = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Documento/GetDocumentoDetalhesById");
            return View(documento);
        }

        [HttpPost]
        [Route("Documento/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id, IFormCollection collection)
        {
            try
            {
                var existeImagem = false;

                MemoryStream ms = new MemoryStream();
                Documento documento = await ApiFindById<Documento>(_sessionToken.Token, id, "Documento");

                foreach (var item in this.Request.Form.Files)
                {
                    existeImagem = true;
                    item.CopyTo(ms);
                    ms.Position = 0;
                    documento.TipoAnexo = item.ContentType;
                }

                if (existeImagem)
                    documento.Anexo = ms.ToArray();

                if (documento.Prontuario != null && new Guid(collection["Prontuario"]) != new Guid("{00000000-0000-0000-0000-000000000000}"))
                    documento.Prontuario.ProntuarioId = new Guid(collection["Prontuario"]);

                if (documento.Pet != null && new Guid(collection["Pet"]) != new Guid("{00000000-0000-0000-0000-000000000000}"))
                    documento.Pet.PetId = new Guid(collection["Pet"]);

                documento.Nome = collection["Nome"];
                documento.Descricao = collection["Descricao"];
                documento.Quantidade = Convert.ToInt32(collection["Quantidade"]);
                documento.TipoDocumento = EnumDescriptionHelp.ParseEnum<ETipoDocumento>(collection["Documento.TipoDocumento"]);
                documento.DataInicio = Convert.ToDateTime(collection["DataInicio"]);
                documento.DataFim = Convert.ToDateTime(collection["DataFim"]);

                var retorno = await ApiUpdate<Documento>(_sessionToken.Token, documento.Id, documento, "Documento");

                if (retorno == null)
                {
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve Um erro durante o update !");
                    return RedirectToAction("Listar", new { Id = id });
                }

                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Alterado com Sucesso !");
                return RedirectToAction("Editar", new { Id = id });
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }

        }

        [HttpGet]
        [Route("Documento/ExcluirDocumento/{Id:guid}")]
        public async Task<IActionResult> ExcluirDocumento(Guid id)
        {
            try
            {
                Documento documento = await ApiFindById<Documento>(_sessionToken.Token, id, "Documento");
                documento.Anexo = null;
                documento.TipoAnexo = null;

                var retorno = await ApiUpdate<Documento>(_sessionToken.Token, documento.Id, documento, "Documento");

                if (retorno != null)
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Documento Anexo excluido com sucesso !");
                else
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve uma falhar ao excluir o documento anexo !");

                return RedirectToAction("Editar", new { Id = documento.Id });
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }
        }
    }
}
