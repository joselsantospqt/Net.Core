using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Domain.Entidades;
using Domain.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionAzGlobal
{
    public static class Post
    {
        [Function("Post")]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Post");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Tarefa obj = JsonConvert.DeserializeObject<Tarefa>(requestBody);
            string ConnectString = Environment.GetEnvironmentVariable("ConnectionString", EnvironmentVariableTarget.Process);
            string DataBase = Environment.GetEnvironmentVariable("Database", EnvironmentVariableTarget.Process);
            var repositorio = new TarefaRepositorio(ConnectString, DataBase); var okRetorno = req.CreateResponse();
            if (obj == null)
                await okRetorno.WriteAsJsonAsync(new BadRequestObjectResult(new { message = "Dados para criação de uma tarefa é obrigatoria" }));
            else
            {
                obj.Id = Guid.NewGuid();
                obj.DtCreate = DateTime.UtcNow;
                await repositorio.Save(obj);
                await okRetorno.WriteAsJsonAsync(new CreatedResult("", obj));
            }

            return okRetorno;
        }
    }
}
