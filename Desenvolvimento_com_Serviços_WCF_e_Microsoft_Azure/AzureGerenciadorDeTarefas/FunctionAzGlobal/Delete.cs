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
    public static class Delete
    {
        [Function("Delete")]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Delete");
            logger.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Tarefa obj = JsonConvert.DeserializeObject<Tarefa>(requestBody);
            var repositorio = new TarefaRepositorio();
            var okRetorno = req.CreateResponse();
            var tarefa = repositorio.GetById(obj.Id);
            if (tarefa != null)
            {
                await repositorio.Remove(tarefa);
                await okRetorno.WriteAsJsonAsync(new OkResult());
            }
            else
                okRetorno = req.CreateResponse(System.Net.HttpStatusCode.NotFound);


            return okRetorno;
        }
    }
}
