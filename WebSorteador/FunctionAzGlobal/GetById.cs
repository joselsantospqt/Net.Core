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
            var repositorio = new PessoaRepositorio();
            var okRetorno = req.CreateResponse();
            var logger = executionContext.GetLogger("GetById");
            Pessoa pessoa = new();
            logger.LogInformation("C# HTTP trigger function processed a request.");
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            var value = query["id"];

            if (value != null)
            {
                pessoa = repositorio.GetById(new Guid(value));
                if (pessoa == null)
                    okRetorno = req.CreateResponse(System.Net.HttpStatusCode.NotFound);
                else
                    await okRetorno.WriteAsJsonAsync(new OkObjectResult(pessoa));
            }
            else
                okRetorno = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);


            return okRetorno;
        }
    }
}
