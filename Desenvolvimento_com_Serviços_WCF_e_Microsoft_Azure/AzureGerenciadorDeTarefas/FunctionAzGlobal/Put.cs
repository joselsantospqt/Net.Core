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
    public static class Put
    {
        [Function("Put")]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Put");
            logger.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Tarefa obj = JsonConvert.DeserializeObject<Tarefa>(requestBody);
            var repositorio = new TarefaRepositorio();
            var okRetorno = req.CreateResponse();
            var tarefa = repositorio.GetById(obj.Id);
            if (tarefa != null)
            {
                if (obj.Titulo != null)
                    tarefa.Titulo = obj.Titulo;
                if (obj.Descricao != null)
                    tarefa.Descricao = obj.Descricao;
                tarefa.DtUpdate = DateTime.UtcNow;
                await repositorio.Update(tarefa);
                await okRetorno.WriteAsJsonAsync(new OkObjectResult(tarefa));
            }
            else
                okRetorno = req.CreateResponse(System.Net.HttpStatusCode.NotFound);


            return okRetorno;
        }
    }
}
