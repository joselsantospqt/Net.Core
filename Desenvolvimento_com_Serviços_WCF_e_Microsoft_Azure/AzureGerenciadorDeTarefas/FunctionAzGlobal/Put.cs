using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
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
    public static class Put
    {
        [Function("Put")]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var repositorio = new TarefaRepositorio();
            var okRetorno = req.CreateResponse();
            var logger = executionContext.GetLogger("Put");
            logger.LogInformation("C# HTTP trigger function processed a request.");
            Tarefa tarefa = new();
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Tarefa update = JsonConvert.DeserializeObject<Tarefa>(requestBody);
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            var value = query["id"];
            if (value != null)
            {
                tarefa = repositorio.GetById(new Guid(value));
                if (tarefa != null)
                {
                    if (update.Titulo != null)
                        tarefa.Titulo = update.Titulo;
                    if (update.Descricao != null)
                        tarefa.Descricao = update.Descricao;
                    if (update.Status != null)
                        tarefa.Status = update.Status;
                    if (update.Responsavel != null)
                        tarefa.Responsavel = update.Responsavel;
                    tarefa.DtUpdate = DateTime.UtcNow;
                    await repositorio.Update(tarefa);
                    await okRetorno.WriteAsJsonAsync(new OkObjectResult(tarefa));
                }
                else
                    okRetorno = req.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }
            else
                okRetorno = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);

            return okRetorno;
        }
    }
}
