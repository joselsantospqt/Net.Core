using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Domain.Entidades;
using Infrastructure.CosmoDb.Repositorio;
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
            var repositorio = new TarefaRepositorio();
            var okRetorno = req.CreateResponse();
            if (obj == null)
                await okRetorno.WriteAsJsonAsync(new BadRequestObjectResult(new { message = "Dados para criação de uma tarefa é obrigatoria" }));
            else
            {
                obj.Id = Guid.NewGuid();
                obj.DtCreate = DateTime.Now;
                await repositorio.Save(obj);
                await okRetorno.WriteAsJsonAsync(new CreatedResult("", obj));
            }

            return okRetorno;
        }
    }
}
