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
    public class AgendamentoController : BaseController
    {
        private readonly ILogger<AgendamentoController> _logger;
        private IdentityUser _sessionUserSign;
        private TokenCode _sessionToken;

        public AgendamentoController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<AgendamentoController> logger) : base(configuration)
        {
            _logger = logger;
            _sessionUserSign = SessionExtensionsHelp.GetObject<IdentityUser>(httpContextAccessor.HttpContext.Session, "UserSign");
            _sessionToken = SessionExtensionsHelp.GetObject<TokenCode>(httpContextAccessor.HttpContext.Session, "Token");
        }

        [HttpGet]
        [Route("Agendamento/Listar/{Id:guid}")]
        public async Task<IActionResult> Listar(Guid id)
        {
            var retorno = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Agendamento/GetAllAgendamentoById");

            return View(retorno);
        }

        [Route("Agendamento/Criar/{Id:guid}/{IdTutor:guid}")]
        public async Task<IActionResult> Criar(Guid id, Guid idTutor)
        {
            var pets = await ApiFindAllById<Pet>(_sessionToken.Token, idTutor, "Pet/GetAllPetsById");
            var Medico = await ApiFindById<Usuario>(_sessionToken.Token, id, "Usuario");
            var Tutor = await ApiFindById<Usuario>(_sessionToken.Token, idTutor, "Usuario");
            ViewModel agendamento = new ViewModel()
            {
                ListaPets = pets,
                Usuario = Tutor
            };
            agendamento.ListaMedicos.Add(Medico);

            return View(agendamento);
        }


        [HttpPost]
        [Route("Agendamento/Criar/{Id:guid}/{IdTutor:guid}")]
        public async Task<IActionResult> Criar(Guid id, Guid idTutor, IFormCollection collection)
        {

            var pet = await ApiFindById<Pet>(_sessionToken.Token, collection["ListaPets"], "Pet");
            var Medico = await ApiFindById<Usuario>(_sessionToken.Token, collection["MedicoResponsavel"], "Usuario");

            if (pet == null)
            {
                ViewData["MensagemRetorno"] = "Pet não encontrado";
                return RedirectToAction("Criar", new { id = Medico.Id, idTutor });
            }

            CreateAgendamento agendamento = new CreateAgendamento()
            {
                Data = Convert.ToDateTime(collection["Data"])
            };

            var retorno = await ApiSaveAutorize<Agendamento>(_sessionToken.Token, agendamento, $"Agendamento/{Medico.Id}/{pet.Id}");

            if (retorno == null)
            {
                ViewData["MensagemRetorno"] = "Houve Um erro ao criar o agendamento !";
                return RedirectToAction("Criar", new { id = Medico.Id, idTutor });
            }

            return RedirectToAction("Listar", new { id = _sessionUserSign.Id });

        }


        [HttpGet]
        [Route("Agendamento/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var agendamento = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Agendamento/GetAgendamentoDetalhes");
            return View(agendamento);
        }

        [HttpPost]
        [Route("Agendamento/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id, IFormCollection collection)
        {
            if (new Guid(collection["Id"]) != id)
            {
                ViewData["messenger"] = "Houve Um erro durante a exclusão !";
                return View(new Guid(collection["Id"]));
            }
            var agendamento = await ApiRemove(_sessionToken.Token, id, "Agendamento");
            return RedirectToAction("Listar", new { id = _sessionUserSign.Id });
        }

        [HttpGet]
        [Route("Agendamento/Detalhes/{Id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var detalhes = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Agendamento/GetAgendamentoDetalhes");
            return View(detalhes);
        }

        [HttpGet]
        [Route("Agendamento/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var agendamento = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Agendamento/GetAgendamentoDetalhes");
            return View(agendamento);
        }

        [HttpPost]
        [Route("Agendamento/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id, IFormCollection collection)
        {
            var usuario = await ApiFindById<Usuario>(_sessionToken.Token, _sessionUserSign.Id, "Usuario");
            Agendamento agendamento = await ApiFindById<Agendamento>(_sessionToken.Token, id, "Agendamento");

            agendamento.Data = Convert.ToDateTime(collection["Data"]);

            if (usuario.TipoUsuario == ETipoUsuario.Medico)
            {
                agendamento.Comentario = collection["Comentario"];
                agendamento.Status = EnumDescriptionHelp.ParseEnum<EStatus>(collection["Agendamento.Status"]);
            }

            if (agendamento.Pet != null && new Guid(collection["Pet"]) != new Guid("{00000000-0000-0000-0000-000000000000}"))
                agendamento.Pet.PetId = new Guid(collection["Pet"]);

            var retorno = await ApiUpdate<Agendamento>(_sessionToken.Token, agendamento.Id, agendamento, "Agendamento");

            if (retorno == null)
            {
                ViewData["messenger"] = "Houve Um erro durante o update !";
                return RedirectToAction("Editar", new { Id = agendamento.Id });
            }
            ViewData["messenger"] = "Alterado com Sucesso !";

            return RedirectToAction("Editar", new { Id = agendamento.Id });

        }

    }
}
