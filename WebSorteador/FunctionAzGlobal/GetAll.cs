using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Domain.Entidades;
using Infrastructure.CosmoDb;
using Infrastructure.CosmoDb.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
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
            var repositorio = new PessoaRepositorio();
           IEnumerable<Pessoa> pessoas = repositorio.GetAll();
            var okRetorno = req.CreateResponse();
            if (pessoas == null)
                okRetorno = req.CreateResponse(System.Net.HttpStatusCode.NoContent);
            else
                okRetorno.WriteAsJsonAsync(new OkObjectResult(pessoas));

            return okRetorno;
        }
    }
}
