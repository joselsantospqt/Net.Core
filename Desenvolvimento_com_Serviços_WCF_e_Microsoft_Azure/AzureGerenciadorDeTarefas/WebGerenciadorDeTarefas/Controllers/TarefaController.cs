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

namespace WebGerenciadorDeTarefas.Controllers
{
    public class TarefaController : Controller
    {
        private HttpClient client { get; set; }
        public IConfiguration _configuration { get; }

        public TarefaController(IHttpClientFactory httpClientFactory,IConfiguration configuration)
        {
            client = httpClientFactory.CreateClient();
            _configuration = configuration;
        }
        public ActionResult Buscar()
        {
            return View();
        }

        public ActionResult Detalhes(Tarefa tarefa)
        {
            return View(tarefa);
        }

        [HttpGet]
        [Route("Tarefa/Detalhes/{Id:guid}")]
        // GET: TarefaController/Edit/5
        public async Task<IActionResult> Detalhes(Guid id)
        {
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/GetById?id={id}";
            var resultado = await client.GetAsync(urlApi);
            var Json = await resultado.Content.ReadAsStringAsync();
            ReponseOne reponseJson = JsonConvert.DeserializeObject<ReponseOne>(Json);
            return View(reponseJson.Value);
        }

        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(IFormCollection collection)
        {

            var createPost = new Tarefa { Titulo = collection["Titulo"], Descricao = collection["Descricao"], Status = collection["Status"], Responsavel = collection["Responsavel"], PartitionKey = collection["PartitionKey"] };
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/Post";
            var putAsJson = JsonConvert.SerializeObject(createPost);
            var conteudo = new StringContent(putAsJson, System.Text.Encoding.UTF8, "application/json");
            var resultado = await client.PostAsync(urlApi, conteudo);
            var Json = await resultado.Content.ReadAsStringAsync();
            ReponseOne reponseJson = JsonConvert.DeserializeObject<ReponseOne>(Json);
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
            ReponseAll tarefas = JsonConvert.DeserializeObject<ReponseAll>(Json);
            return View(tarefas);
        }


        public ActionResult Editar(Tarefa tarefa)
        {
            return View(tarefa);
        }

        [HttpGet]
        [Route("Tarefa/Editar/{Id:guid}")]
        // GET: TarefaController/Edit/5
        public async Task<IActionResult> Editar(Guid id)
        {
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/GetById?id={id}";
            var resultado = await client.GetAsync(urlApi);
            var Json = await resultado.Content.ReadAsStringAsync();
            ReponseOne reponseJson = JsonConvert.DeserializeObject<ReponseOne>(Json);
            return View(reponseJson.Value);
        }


        [HttpPost]
        [Route("Tarefa/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(IFormCollection collection)
        {

            var createPost = new Tarefa { 
                Id = new Guid(collection["Id"]),
                Titulo = collection["Titulo"],
                Descricao = collection["Descricao"],
                Status = collection["Status"],
                Responsavel = collection["Responsavel"],
                PartitionKey = collection["PartitionKey"] 
            };
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/Put?id={createPost.Id}";
            var putAsJson = JsonConvert.SerializeObject(createPost);
            var conteudo = new StringContent(putAsJson, System.Text.Encoding.UTF8, "application/json");
            var resultado = await client.PutAsync(urlApi, conteudo);
            var Json = await resultado.Content.ReadAsStringAsync();
            ReponseOne reponseJson = JsonConvert.DeserializeObject<ReponseOne>(Json);
            if (resultado.IsSuccessStatusCode)
            {
                ViewData["status"] = "Atualizado com Sucesso !";
                return View(reponseJson.Value);
            }
            ViewData["status"] = reponseJson.StatusCode;
            return View();
        }


        public ActionResult Deletar()
        {
            return View();
        }

        [HttpGet]
        [Route("Tarefa/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/GetById?id={id}";
            var resultado = await client.GetAsync(urlApi);
            var Json = await resultado.Content.ReadAsStringAsync();
            ReponseOne reponseJson = JsonConvert.DeserializeObject<ReponseOne>(Json);
            return View(reponseJson.Value);
        }

        // POST: TarefaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tarefa/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(IFormCollection collection)
        {
            var id = new Guid(collection["Id"]);
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/Delete?id={id}";
            var resultado = await client.DeleteAsync(urlApi);
            var Json = await resultado.Content.ReadAsStringAsync();
            ReponseOne reponseJson = JsonConvert.DeserializeObject<ReponseOne>(Json);
            if (resultado.IsSuccessStatusCode)
            {
                ViewData["status"] = "Deletado com Sucesso !";
                return View();
            }
            ViewData["status"] = reponseJson.StatusCode;
            return View();
        }

        [HttpPost]
        [Route("Tarefa/Buscar")]
        // GET: TarefaController/Edit/5
        public async Task<IActionResult> Buscar(IFormCollection collection)
        {
            var id = new Guid(collection["Id"]);
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/GetById?id={id}";
            var resultado = await client.GetAsync(urlApi);
            var Json = await resultado.Content.ReadAsStringAsync();
            ReponseOne reponseJson = JsonConvert.DeserializeObject<ReponseOne>(Json);
            return View(reponseJson.Value);
        }


    }
}
