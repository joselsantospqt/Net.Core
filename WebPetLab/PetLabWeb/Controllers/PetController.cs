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
            _sessionUserSign = SessionExtensionsModel.GetObject<IdentityUser>(httpContextAccessor.HttpContext.Session, "UserSign");
            _sessionToken = SessionExtensionsModel.GetObject<TokenCode>(httpContextAccessor.HttpContext.Session, "Token");
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
            var pets = await ApiFindAllById<Pet>(_sessionToken.Token, Id, "Pet/GetAllPetsByEmail");

            return RedirectToAction("Listar", "Pet", _sessionUserSign.Id);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Route("Pet/Criar")]
        public async Task<IActionResult> Criar(IFormCollection collection)
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

            CreatePet pet = new CreatePet();

            //if (existeImagem)
            pet.Nome = collection["Nome"];
            pet.DataNascimento = Convert.ToDateTime(collection["DataNascimento"]);
            pet.Especie = (ETipoEspecie)Enum.Parse(typeof(ETipoEspecie), collection["Especie"], true);

            var retorno = await ApiSaveAutorize<Pet>(_sessionToken.Token, pet, $"Pet/{_sessionUserSign.Id}");

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
        [Route("Pet/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var pet = await ApiFindById<Pet>(_sessionToken.Token, id, "Pet");
            return View(pet);
        }

        [HttpPost]
        [Route("Pet/DeletarPet/{Id:guid}")]
        public async Task<IActionResult> DeletarPet(Guid id)
        {
            var pet = await ApiRemove(_sessionToken.Token, id, "Pet");
            return RedirectToAction("Listar", new { id = _sessionUserSign.Id });
        }


    }
}
