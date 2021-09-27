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
    public static class Delete
    {
        [Function("Delete")]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete")] HttpRequestData req,
            FunctionContext executionContext)
        {
            string ConnectString = Environment.GetEnvironmentVariable("ConnectionString", EnvironmentVariableTarget.Process);
            string DataBase = Environment.GetEnvironmentVariable("Database", EnvironmentVariableTarget.Process);
            var repositorio = new TarefaRepositorio();
            var okRetorno = req.CreateResponse();
            var logger = executionContext.GetLogger("Delete");
            logger.LogInformation("C# HTTP trigger function processed a request.");
            Tarefa tarefa = new();
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            var value = query["id"];

            if (value != null)
            {
                tarefa = repositorio.GetById(new Guid(value));
                if (tarefa != null)
                {
                    await repositorio.Remove(tarefa);
                    await okRetorno.WriteAsJsonAsync(new OkResult());
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
