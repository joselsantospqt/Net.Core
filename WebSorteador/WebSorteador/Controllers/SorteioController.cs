using Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebSorteador.Controllers
{
    public class SorteioController : Controller
    {
        private HttpClient client { get; set; }
        public IConfiguration _configuration { get; }

        public SorteioController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            client = httpClientFactory.CreateClient();
            _configuration = configuration;
        }
        // GET: SorteioController/Criar
        public ActionResult Criar()
        {
            return View();
        }

        // POST: SorteioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(IFormCollection collection)
        {

            var createPost = new Pessoa { Nome = collection["Nome"], PartitionKey = collection["PartitionKey"] };
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/Post";
            var putAsJson = JsonConvert.SerializeObject(createPost);
            var conteudo = new StringContent(putAsJson, System.Text.Encoding.UTF8, "application/json");
            var resultado = await client.PostAsync(urlApi, conteudo);
            var Json = await resultado.Content.ReadAsStringAsync();
            ResponseOne reponseJson = JsonConvert.DeserializeObject<ResponseOne>(Json);
            if (resultado.IsSuccessStatusCode)
            {
                ViewData["status"] = "Criado com Sucesso !";
                return View();

            }
            ViewData["status"] = reponseJson.StatusCode;
            return View();
        }

        public async Task<IActionResult> Listar()
        {
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/GetAll";
            var resultado = await client.GetAsync(urlApi);
            var Json = await resultado.Content.ReadAsStringAsync();
            ResponseAll tarefas = JsonConvert.DeserializeObject<ResponseAll>(Json);
            return View(tarefas);
        }

    }
}
