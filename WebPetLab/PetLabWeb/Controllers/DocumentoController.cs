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

        public DocumentoController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<DocumentoController> logger) : base(configuration)
        {
            _logger = logger;
            _sessionUserSign = SessionExtensionsHelp.GetObject<IdentityUser>(httpContextAccessor.HttpContext.Session, "UserSign");
            _sessionToken = SessionExtensionsHelp.GetObject<TokenCode>(httpContextAccessor.HttpContext.Session, "Token");
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
            var pets = await ApiFindAllById<Pet>(_sessionToken.Token, id, "Pet/GetAllPetsById");
            var usuario = await ApiFindById<Usuario>(_sessionToken.Token, id, "Usuario");
            IList<Prontuario> listaProntuario = new List<Prontuario>();

            foreach (var item in pets)
            {
                foreach (var prontuario in item.Prontuarios)
                {
                    listaProntuario = await ApiFindAllById<Prontuario>(_sessionToken.Token, prontuario.ProntuarioId, "Prontuario/GetAllProntuariosByPetId");
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


        [HttpPost]
        [Route("Documento/Criar/{Id:guid}")]
        public async Task<IActionResult> Criar(Guid id, IFormCollection collection)
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

            if (!string.IsNullOrEmpty(collection["Prontuarios"]))
                idProntuario = new Guid(collection["Prontuarios"]);

            documento.Nome = collection["Nome"];
            documento.Descricao = collection["Descricao"];
            documento.Quantidade = Convert.ToInt32(collection["Quantidade"]);
            documento.TipoDocumento = EnumDescriptionHelp.ParseEnum<ETipoDocumento>(collection["Documento.TipoDocumento"]);
            documento.DataInicio = Convert.ToDateTime(collection["DataInicio"]);
            documento.DataInicio = Convert.ToDateTime(collection["DataFim"]);
            var retorno = await ApiSaveAutorize<Documento>(_sessionToken.Token, documento, $"Documento/{idProntuario}/{collection["ListaPets"]}");

            if (retorno == null)
            {
                ViewData["MensagemRetorno"] = "Houve Um erro ao criar novo Pet !";
            }

            return RedirectToAction("Listar", new { id = _sessionUserSign.Id });

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
            if (new Guid(collection["Id"]) != id)
            {
                ViewData["messenger"] = "Houve Um erro durante a exclusão !";
                return View(new Guid(collection["Id"]));
            }

            var documento = await ApiRemove(_sessionToken.Token, id, "Documento");
            return RedirectToAction("Listar", new { id = _sessionUserSign.Id });
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
                ViewData["messenger"] = "Houve Um erro durante o update !";
                return RedirectToAction("Listar", new { Id = id });
            }
            ViewData["messenger"] = "Alterado com Sucesso !";


            return RedirectToAction("Editar", new { Id = id });

        }

        [HttpGet]
        [Route("Documento/ExcluirDocumento/{Id:guid}")]
        public async Task<IActionResult> ExcluirDocumento(Guid id)
        {
            Documento documento = await ApiFindById<Documento>(_sessionToken.Token, id, "Documento");
            documento.Anexo = null;
            documento.TipoAnexo = null;

            var retorno = await ApiUpdate<Documento>(_sessionToken.Token, documento.Id, documento, "Documento");

            return RedirectToAction("Editar", new { Id = documento.Id });
        }
    }
}
