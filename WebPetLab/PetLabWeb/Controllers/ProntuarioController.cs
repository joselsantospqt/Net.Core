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
using System.Linq;
using System.Threading.Tasks;

namespace PetLabWeb.Controllers
{
    public class ProntuarioController : BaseController
    {
        private readonly ILogger<ProntuarioController> _logger;
        private IdentityUser _sessionUserSign;
        private TokenCode _sessionToken;

        public ProntuarioController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<ProntuarioController> logger) : base(configuration)
        {
            _logger = logger;
            _sessionUserSign = SessionExtensionsHelp.GetObject<IdentityUser>(httpContextAccessor.HttpContext.Session, "UserSign");
            _sessionToken = SessionExtensionsHelp.GetObject<TokenCode>(httpContextAccessor.HttpContext.Session, "Token");
        }

        [HttpGet]
        [Route("Prontuario/Listar/{Id:guid}")]
        public async Task<IActionResult> Listar(Guid id)
        {
            var prontuarios = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Prontuario/GetAllProntuariosById");
            return View(prontuarios);
        }

        [Route("Prontuario/Criar/{Id:guid}")]
        public async Task<IActionResult> Criar(Guid id)
        {
            var pets = await ApiFindAllById<Pet>(_sessionToken.Token, id, "Pet/GetAllProtuariosById");
            IList<Prontuario> listaProntuario = new List<Prontuario>();

            foreach (var item in pets)
            {
                foreach (var prontuario in item.Prontuarios)
                {
                    //listaProntuario.Add();
                }
            }
            //CreateProntuario agendamento = new CreateProntuario()
            //{
            //    Pets = pets,
            //    Prontuarios = listaProntuario,
            //    Tutor = id

            //};

            return View(listaProntuario);
        }


        [HttpPost]
        [Route("Prontuario/Criar/{Id:guid}")]
        public async Task<IActionResult> Criar(Guid id, IFormCollection collection)
        {
            CreateProntuario documento = new CreateProntuario();
            Guid idProntuario = new Guid();

            //documento.Nome = collection["Nome"];
            //documento.Descricao = collection["Descricao"];
            //documento.Quantidade = Convert.ToInt32(collection["Quantidade"]);
            //documento.TipoProntuario = EnumDescriptionHelp.ParseEnum<ETipoProntuario>(collection["TipoProntuario"]);
            //documento.DataInicio = Convert.ToDateTime(collection["DataInicio"]);
            //documento.DataInicio = Convert.ToDateTime(collection["DataFim"]);
            var retorno = await ApiSaveAutorize<Prontuario>(_sessionToken.Token, documento, $"Prontuario/{idProntuario}/{collection["Pets"]}");

            if (retorno == null)
            {
                ViewData["MensagemRetorno"] = "Houve Um erro ao criar novo Pet !";
            }

            return RedirectToAction("Listar", new { id = _sessionUserSign.Id });

        }


        [HttpGet]
        [Route("Prontuario/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var prontuario = await ApiFindById<Prontuario>(_sessionToken.Token, id, "Prontuario/GetProntuarioDetalhesById");
            return View(prontuario);
        }

        [HttpPost]
        [Route("Prontuario/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id, IFormCollection collection)
        {
            if (new Guid(collection["Id"]) != id)
            {
                ViewData["messenger"] = "Houve Um erro durante a exclusão !";
                return View(new Guid(collection["Id"]));
            }
            var retorno = await ApiRemove(_sessionToken.Token, id, "Prontuario");
            return RedirectToAction("Listar", new { id = _sessionUserSign.Id });
        }

        [HttpGet]
        [Route("Prontuario/Detalhes/{Id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var detalhes = await ApiFindById<Prontuario>(_sessionToken.Token, id, "Prontuario/GetProntuarioDetalhesById");
            return View(detalhes);
        }

        [HttpGet]
        [Route("Prontuario/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var documento = await ApiFindById<Prontuario>(_sessionToken.Token, id, "Prontuario/GetProntuarioDetalhesById");
            return View(documento);
        }

        [HttpPost]
        [Route("Prontuario/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id, IFormCollection collection)
        {
            Prontuario prontuario = await ApiFindById<Prontuario>(_sessionToken.Token, id, "Prontuario");

            //documento.Nome = collection["Nome"];
            //documento.DataNascimento = Convert.ToDateTime(collection["DataNascimento"]);
            //documento.Especie = EnumDescriptionHelp.ParseEnum<ETipoEspecie>(collection["Especie"]);

            var retorno = await ApiUpdate<Prontuario>(_sessionToken.Token, prontuario.Id, prontuario, "Prontuario");

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
