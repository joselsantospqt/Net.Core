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
    public class PetController : BaseController
    {
        private readonly ILogger<PetController> _logger;
        private IdentityUser _sessionUserSign;
        private TokenCode _sessionToken;

        public PetController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<PetController> logger) : base(configuration)
        {
            _logger = logger;
            _sessionUserSign = SessionExtensionsHelp.GetObject<IdentityUser>(httpContextAccessor.HttpContext.Session, "UserSign");
            _sessionToken = SessionExtensionsHelp.GetObject<TokenCode>(httpContextAccessor.HttpContext.Session, "Token");
        }

        [HttpGet]
        [Route("Pet/Listar/{Id:guid}")]
        public async Task<IActionResult> Listar(Guid id)
        {
            var pets = await ApiFindAllById<Pet>(_sessionToken.Token, id, "Pet/GetAllPetsById");
            return View(pets);
        }

        [HttpGet]
        [Route("Pet/BuscarEmail/{Id}")]
        public async Task<IActionResult> BuscarEmail(string Id)
        {
            var pessoa = await ApiFindById<Usuario>(_sessionToken.Token, _sessionUserSign.Email, "Usuario");
            if (pessoa == null)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Participante não encontrado !");
                return RedirectToAction("Listar", "Agendamento", new { id = _sessionUserSign.Id });
            }
            if (pessoa.TipoUsuario == ETipoUsuario.Medico)
            {
                var pets = await ApiFindAllById<Pet>(_sessionToken.Token, Id, "Pet/GetAllPetsByEmail");
                return RedirectToAction("Listar", "Pet", _sessionUserSign.Id);
            }

            return RedirectToAction("Autenticacao", "AccessDenied");

        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Route("Pet/Criar")]
        public async Task<IActionResult> Criar(IFormCollection collection)
        {
            try
            {
                var existeImagem = false;
                CreatePet pet = new CreatePet();
                MemoryStream ms = new MemoryStream();
                foreach (var item in this.Request.Form.Files)
                {
                    existeImagem = true;
                    item.CopyTo(ms);
                    ms.Position = 0;
                    pet.TipoAnexo = item.ContentType;
                }

                if (existeImagem)
                    pet.Anexo = ms.ToArray();

                pet.Nome = collection["Nome"];
                pet.DataNascimento = Convert.ToDateTime(collection["DataNascimento"]);
                pet.Especie = EnumDescriptionHelp.ParseEnum<ETipoEspecie>(collection["Especie"]);


                var retorno = await ApiSaveAutorize<Pet>(_sessionToken.Token, pet, $"Pet/{_sessionUserSign.Id}");

                if (retorno != null)
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Pet Criado com sucesso !");
                else
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve uma falhar ao criar Pet !");

                return RedirectToAction("Listar", new { id = _sessionUserSign.Id });
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }

        }


        [HttpGet]
        [Route("Pet/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var pet = await ApiFindById<Pet>(_sessionToken.Token, id, "Pet");
            return View(pet);
        }

        [HttpPost]
        [Route("Pet/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id, IFormCollection collection)
        {
            try
            {
                if (new Guid(collection["Id"]) != id)
                {
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve Um erro durante a exclusão !");
                    return View(new Guid(collection["Id"]));
                }
                var detalhes = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Pet/GetPetsDetalhes");

                foreach (var item in detalhes.Agendamentos)
                {
                    await ApiRemove(_sessionToken.Token, item.Id, "Agendamento");
                }
                foreach (var item in detalhes.Documentos)
                {
                    await ApiRemove(_sessionToken.Token, item.Id, "Documentos");
                }
                foreach (var item in detalhes.Prontuarios)
                {
                    await ApiRemove(_sessionToken.Token, item.Id, "Prontuario");
                }

                var pet = await ApiRemove(_sessionToken.Token, id, "Pet");

                if (pet != null)
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Pet excluido com sucesso !");
                else
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve uma falhar ao deletar Pet !");

                return RedirectToAction("Listar", new { id = _sessionUserSign.Id });
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }
        }

        [HttpGet]
        [Route("Pet/Detalhes/{Id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var detalhes = await ApiFindById<ViewModel>(_sessionToken.Token, id, "Pet/GetPetsDetalhes");
            return View(detalhes);
        }

        [HttpGet]
        [Route("Pet/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var pet = await ApiFindById<Pet>(_sessionToken.Token, id, "Pet");
            return View(pet);
        }

        [HttpPost]
        [Route("Pet/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id, IFormCollection collection)
        {
            try
            {
                var existeImagem = false;
                Pet pet = await ApiFindById<Pet>(_sessionToken.Token, id, "Pet");
                MemoryStream ms = new MemoryStream();
                foreach (var item in this.Request.Form.Files)
                {
                    existeImagem = true;
                    item.CopyTo(ms);
                    ms.Position = 0;
                    pet.TipoAnexo = item.ContentType;
                }

                if (existeImagem)
                    pet.Anexo = ms.ToArray();

                pet.Nome = collection["Nome"];
                pet.DataNascimento = Convert.ToDateTime(collection["DataNascimento"]);
                pet.Especie = EnumDescriptionHelp.ParseEnum<ETipoEspecie>(collection["Especie"]);

                var retorno = await ApiUpdate<Pet>(_sessionToken.Token, pet.Id, pet, "Pet");

                if (retorno == null)
                {
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve Um erro durante o update !");
                }
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Alterado com Sucesso !");

                return View("Editar", retorno);

            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }
        }

        [HttpGet]
        [Route("Pet/ExcluirImagem/{Id:guid}")]
        public async Task<IActionResult> ExcluirImagem(Guid id)
        {
            try
            {
                Pet pet = await ApiFindById<Pet>(_sessionToken.Token, id, "Pet");
                pet.Anexo = null;
                pet.TipoAnexo = null;

                var retorno = await ApiUpdate<Pet>(_sessionToken.Token, pet.Id, pet, "Pet");

                if (retorno != null)
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Foto excluida com sucesso !");
                else
                    SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", "Houve uma falhar ao excluir a foto do Pet !");

                return RedirectToAction("Editar", new { Id = pet.Id });
            }
            catch (Exception ex)
            {
                SessionExtensionsHelp.SetObject(this.HttpContext.Session, "Mensagem", ex.ToString());
                return View("Home", "Home");
            }
        }
    }
}
