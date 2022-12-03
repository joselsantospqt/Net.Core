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

        [Route("Prontuario/Criar/{Id:guid}/{IdMedico:guid}")]
        public async Task<IActionResult> Criar(Guid id, Guid IdMedico)
        {
            try
            {
                var pet = await ApiFindById<Pet>(_sessionToken.Token, id, "Pet");

                if (pet == null)
                {
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Pet não encontrado");
                    return RedirectToAction("Listar", new { id = IdMedico });
                }

                var medico = await ApiFindById<Usuario>(_sessionToken.Token, IdMedico, "Usuario");
                var tutor = await ApiFindById<Usuario>(_sessionToken.Token, pet.Tutor.UsuarioId, "Usuario");
                var listaPet = await ApiFindAllById<Pet>(_sessionToken.Token, tutor.Id, "Pet/GetAllPetsById");


                ViewModel prontuario = new ViewModel()
                {
                    Pet = pet,
                    Usuario = tutor,
                    ListaPets = listaPet
                };
                prontuario.ListaMedicos.Add(medico);


                return View(prontuario);
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }

        }


        [HttpPost]
        [Route("Prontuario/Criar/{Id:guid}/{IdMedico:guid}")]
        public async Task<IActionResult> Criar(Guid id, Guid IdMedico, IFormCollection collection)
        {
            try
            {
                CreateProntuario documento = new CreateProntuario();

                documento.Resumo = collection["Resumo"];
                documento.Descricao = collection["Descricao"];

                var pet = await ApiFindById<Pet>(_sessionToken.Token, id, "Pet");
                var medico = await ApiFindById<Usuario>(_sessionToken.Token, IdMedico, "Usuario");

                if (pet == null)
                {
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Pet não encontrado");
                    return RedirectToAction("Listar", new { id = id, IdMedico = IdMedico });
                }

                if (medico == null)
                {
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Usuário médico não encontrado");
                    return RedirectToAction("Listar", new { id = id, IdMedico = IdMedico });
                }

                var retorno = await ApiSaveAutorize<Prontuario>(_sessionToken.Token, documento, $"Prontuario/{medico.Id}/{pet.Id}");

                if (retorno != null)
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Prontuário criado com sucesso !");
                else
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve uma falhar ao criar o Prontuário!");

                return RedirectToAction("Detalhes", "Pet", new { id = pet.Id });
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }
        }


        [HttpGet]
        [Route("Prontuario/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var prontuario = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Prontuario/GetProntuarioDetalhesById");
            return View(prontuario);
        }

        [HttpPost]
        [Route("Prontuario/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id, IFormCollection collection)
        {
            try
            {
                if (new Guid(collection["Id"]) != id)
                {
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve Um erro durante a exclusão !");
                    return View(new Guid(collection["Id"]));
                }
                var prontuarios = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Prontuario/GetProntuarioDetalhesById");

                foreach (var item in prontuarios.Documentos)
                {
                    await ApiRemove(_sessionToken.Token, item.Id, "Documento");
                }
                var retorno = await ApiRemove(_sessionToken.Token, id, "Prontuario");

                if (retorno != null)
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Prontuário deletado com sucesso !");
                else
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve uma falhar ao excluir o Prontuário!");

                return RedirectToAction("Listar", new { id = _sessionUserSign.Id });
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }
        }

        [HttpGet]
        [Route("Prontuario/Detalhes/{Id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var detalhes = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Prontuario/GetProntuarioDetalhesById");
            return View(detalhes);
        }

        [HttpGet]
        [Route("Prontuario/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var documento = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Prontuario/GetProntuarioDetalhesById");
            return View(documento);
        }

        [HttpPost]
        [Route("Prontuario/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id, IFormCollection collection)
        {
            try
            {
                Prontuario prontuario = await ApiFindById<Prontuario>(_sessionToken.Token, id, "Prontuario");

                prontuario.Resumo = collection["Resumo"];
                prontuario.Descricao = collection["Descricao"];

                if (prontuario.Pet != null && new Guid(collection["Pet"]) != new Guid("{00000000-0000-0000-0000-000000000000}"))
                    prontuario.Pet.PetId = new Guid(collection["Pet"]);

                var retorno = await ApiUpdate<Prontuario>(_sessionToken.Token, prontuario.Id, prontuario, "Prontuario");

                if (retorno == null)
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve Um erro durante a exclusão !");
                else
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Alterado com Sucesso !");

                return RedirectToAction("Detalhes", "Pet", new { id = retorno.Pet.PetId });
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }

        }
    }
}
