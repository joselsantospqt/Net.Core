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

        public async Task<IActionResult> Listar()
        {
            ResponseAll tarefas = await BuscarTodasPessoas();
            return View(tarefas);
        }
        [HttpPost]
        public async Task<IActionResult> Sortear(IFormCollection collection)
        {
            var vListGuid = collection["Value"].ToString().Length < 1 ? null : collection["Value"].ToString().Split(',').Select(Guid.Parse).ToList();

            if (vListGuid == null || vListGuid.Count() < 2)
            {
                ViewData["message"] = "Selecione Pessoas";
                return RedirectToAction("Listar");
            }

            Guid sorteado = vListGuid[RandomNumber(vListGuid.Count())];
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/GetById?id={sorteado}";
            var resultado = await client.GetAsync(urlApi);
            var Json = await resultado.Content.ReadAsStringAsync();
            ResponseOne reponseJson = JsonConvert.DeserializeObject<ResponseOne>(Json);
            Pessoa ganhador = reponseJson.Value;
            ganhador.Vitorias += 1;
            urlApi = $"{urlApi.Substring(0, urlApi.IndexOf("/api/") + 5)}Put?id={sorteado}";
            var putAsJson = JsonConvert.SerializeObject(ganhador);
            var conteudo = new StringContent(putAsJson, System.Text.Encoding.UTF8, "application/json");
            resultado = await client.PutAsync(urlApi, conteudo);
            Json = await resultado.Content.ReadAsStringAsync();
            reponseJson = JsonConvert.DeserializeObject<ResponseOne>(Json);

            if (resultado.IsSuccessStatusCode)
            {
                ViewData["status"] = resultado.StatusCode.ToString();
                ViewData["message"] = "Sorteado com Sucesso !";
                return View(reponseJson.Value);
            }

            ViewData["status"] = resultado.StatusCode;
            return RedirectToAction("Listar");
        }
        public ActionResult Criar()
        {
            return View();
        }
        // POST: SorteioController/Criar
        [HttpPost]
        public async Task<IActionResult> Criar(IFormCollection collection)
        {

            var createPost = new Pessoa { Nome = collection["Nome"], Vitorias = 0, PartitionKey = collection["PartitionKey"] };
            if (createPost.Nome.Length < 1)
            {
                ViewData["message"] = "Preencha o campo Nome";
                return View();
            }

            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/Post";
            var putAsJson = JsonConvert.SerializeObject(createPost);
            var conteudo = new StringContent(putAsJson, System.Text.Encoding.UTF8, "application/json");
            var resultado = await client.PostAsync(urlApi, conteudo);
            var Json = await resultado.Content.ReadAsStringAsync();
            ResponseOne reponseJson = JsonConvert.DeserializeObject<ResponseOne>(Json);
            if (resultado.IsSuccessStatusCode)
            {
                ViewData["status"] = resultado.StatusCode.ToString();
                ViewData["message"] = "Criado com Sucesso !";
                return View();

            }
            ViewData["status"] = resultado.StatusCode;
            return View();
        }
        [HttpGet]
        [Route("Sorteio/Editar/{Id:guid}")]
        // GET: SorteioController/Editar/id
        public async Task<IActionResult> Editar(Guid id)
        {
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/GetById?id={id}";
            var resultado = await client.GetAsync(urlApi);
            var Json = await resultado.Content.ReadAsStringAsync();
            ResponseOne reponseJson = JsonConvert.DeserializeObject<ResponseOne>(Json);
            return View(reponseJson.Value);
        }
        // POST: TarefaController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Editar(IFormCollection collection)
        {

            var createPost = new Pessoa
            {
                Id = new Guid(collection["Id"]),
                Nome = collection["Nome"],
                PartitionKey = collection["PartitionKey"]
            };
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/Put?id={createPost.Id}";
            var putAsJson = JsonConvert.SerializeObject(createPost);
            var conteudo = new StringContent(putAsJson, System.Text.Encoding.UTF8, "application/json");
            var resultado = await client.PutAsync(urlApi, conteudo);
            var Json = await resultado.Content.ReadAsStringAsync();
            ResponseOne reponseJson = JsonConvert.DeserializeObject<ResponseOne>(Json);
            if (resultado.IsSuccessStatusCode)
            {
                ViewData["status"] = resultado.StatusCode.ToString();
                ViewData["message"] = "Atualizado com Sucesso !";
                return View(reponseJson.Value);
            }
            ViewData["status"] = resultado.StatusCode;
            return View();
        }
        [HttpGet]
        [Route("Tarefa/Deletar/{Id:guid}")]
        // GET: SorteioController/Deletar/Id
        public async Task<IActionResult> Deletar(Guid id)
        {
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/Delete?id={id}";
            var resultado = await client.DeleteAsync(urlApi);
            var Json = await resultado.Content.ReadAsStringAsync();
            ResponseOne reponseJson = JsonConvert.DeserializeObject<ResponseOne>(Json);
            if (resultado.IsSuccessStatusCode)
            {
                ViewData["status"] = resultado.StatusCode.ToString();
                ViewData["message"] = "Deletado com Sucesso !";
                return View("Listar", await BuscarTodasPessoas());
            }
            ViewData["status"] = resultado.StatusCode;
            return View("Listar");
        }
        private int RandomNumber(int final)
        {
            Random generator = new Random();
            return generator.Next(0, final);
        }


        public async Task<ResponseAll> BuscarTodasPessoas()
        {
            string urlApi = $"{_configuration.GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/GetAll";
            var resultado = await client.GetAsync(urlApi);
            var Json = await resultado.Content.ReadAsStringAsync();
            ResponseAll tarefas = JsonConvert.DeserializeObject<ResponseAll>(Json);
            return tarefas;
        }
    }
}
