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
            var documentos = await ApiFindAllById<Documento>(_sessionToken.Token, id, "Documento/GetDocumentosAllById");
            return View(documentos);
        }

        [Route("Documento/Criar/{Id:guid}")]
        public async Task<IActionResult> Criar(Guid id)
        {
            var pets = await ApiFindAllById<Pet>(_sessionToken.Token, id, "Pet/GetAllPetsById");
            IList<Prontuario> listaProntuario = new List<Prontuario>();

            foreach (var item in pets)
            {
                foreach (var prontuario in item.Prontuarios)
                {
                    //listaProntuario.Add();
                }
            }
            CreateDocumento agendamento = new CreateDocumento()
            {
                Pets = pets,
                Prontuarios = listaProntuario,
                Tutor = id

            };

            return View(agendamento);
        }


        [HttpPost]
        [Route("Documento/Criar/{Id:guid}")]
        public async Task<IActionResult> Criar(Guid id, IFormCollection collection)
        {
            var existeImagem = false;

            MemoryStream ms = new MemoryStream();
            var fileName = $"Perfil_{_sessionUserSign.Id}{RandomNumber()}_.png";


            foreach (var item in this.Request.Form.Files)
            {
                existeImagem = true;

                item.CopyTo(ms);

                ms.Position = 0;
            }

            CreateDocumento documento = new CreateDocumento();

            //if (existeImagem)
            documento.Nome = collection["Nome"];
            documento.Descricao = collection["Descricao"];
            documento.Quantidade = Convert.ToInt32(collection["Quantidade"]);
            documento.TipoDocumento = EnumDescriptionHelp.ParseEnum<ETipoDocumento>(collection["TipoDocumento"]);
            documento.DataInicio = Convert.ToDateTime(collection["DataInicio"]);
            documento.DataInicio = Convert.ToDateTime(collection["DataFim"]);
            var retorno = await ApiSaveAutorize<Documento>(_sessionToken.Token, documento, $"Documento/{collection["Prontuarios"]}/{collection["Pets"]}");

            if (retorno == null)
            {
                ViewData["MensagemRetorno"] = "Houve Um erro ao criar novo Pet !";
            }
            //else
            //    await _blobstorage.SaveUpdate(fileName, ms);

            return RedirectToAction("Listar", new { id = _sessionUserSign.Id });

        }

        private string RandomNumber()
        {
            Random generator = new Random();
            return generator.Next(0, 1000000).ToString("D6");
        }

        [HttpGet]
        [Route("Documento/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var documento = await ApiFindById<Documento>(_sessionToken.Token, id, "Documento");
            return View(documento);
        }

        [HttpPost]
        [Route("Documento/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id, IFormCollection collection)
        {
            var documento = await ApiRemove(_sessionToken.Token, id, "Documento");
            return RedirectToAction("Listar", new { id = _sessionUserSign.Id });
        }

        [HttpGet]
        [Route("Documento/Detalhes/{Id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var detalhes = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Documento/GetDetalhesById");
            return View(detalhes);
        }

        [HttpGet]
        [Route("Documento/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var documento = await ApiFindById<Documento>(_sessionToken.Token, id, "Documento");
            return View(documento);
        }

        [HttpPost]
        [Route("Documento/Editar/{Id:guid}")]
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

            Documento documento = await ApiFindById<Documento>(_sessionToken.Token, id, "Documento");

            //documento.Nome = collection["Nome"];
            //documento.DataNascimento = Convert.ToDateTime(collection["DataNascimento"]);
            //documento.Especie = EnumDescriptionHelp.ParseEnum<ETipoEspecie>(collection["Especie"]);

            var retorno = await ApiUpdate<Documento>(_sessionToken.Token, documento.Id, documento, "Documento");

            if (retorno == null)
            {
                ViewData["messenger"] = "Houve Um erro durante o update !";
            }
            ViewData["messenger"] = "Alterado com Sucesso !";
            //else
            //    await _blobstorage.SaveUpdate(fileName, ms);

            return View("Editar", retorno);

        }
    }
}
