using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Domain.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionAzGlobal
{
    public static class GetAll
    {
        [Function("GetAll")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {


            var logger = executionContext.GetLogger("GetAll");
            logger.LogInformation("C# HTTP trigger function processed a request.");
            string ConnectString = Environment.GetEnvironmentVariable("ConnectionString", EnvironmentVariableTarget.Process);
            string DataBase = Environment.GetEnvironmentVariable("Database", EnvironmentVariableTarget.Process);
            var repositorio = new TarefaRepositorio(ConnectString, DataBase);
            var tarefas = repositorio.GetAll();
            var okRetorno = req.CreateResponse();
            if (tarefas == null)
                okRetorno = req.CreateResponse(System.Net.HttpStatusCode.NoContent);
            else
                okRetorno.WriteAsJsonAsync(new OkObjectResult(tarefas));

            return okRetorno;
        }
    }
}
