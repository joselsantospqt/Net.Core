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
    public static class GetById
    {
        [Function("GetById")]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("GetById");
            logger.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Tarefa obj = JsonConvert.DeserializeObject<Tarefa>(requestBody);
            var repositorio = new TarefaRepositorio();
            var tarefa = repositorio.GetById(obj.Id);
            var okRetorno = req.CreateResponse();
            if (tarefa == null)
                okRetorno = req.CreateResponse(System.Net.HttpStatusCode.NotFound);
            else

                await okRetorno.WriteAsJsonAsync(new OkObjectResult(tarefa));

            return okRetorno;
        }
    }
}
