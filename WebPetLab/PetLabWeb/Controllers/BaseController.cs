using Domain.Entidade;
using Domain.Entidade.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using RestSharp.Authenticators;

namespace PetLabWeb.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
            _API_URL = $"{_configuration.GetSection("Logging").GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/";
            _httpClient = new RestClient(_API_URL);

        }

        public IConfiguration _configuration { get; }

        public RestClient _httpClient;

        public string _API_URL;

        public async Task<TokenCode> ApiToken(Object objeto)
        {
            try
            {
                var path = "JwtAutenticacao";
                var request = new RestRequest(path, Method.Post);
                request.AddJsonBody(objeto);
                return await _httpClient.PostAsync<TokenCode>(request);
            }
            catch (Exception ex)
            {
                return new TokenCode() { Exception = ex};
            }
        }

        public async Task<HttpResponseMessage> ApiSave(Object objeto, string path)
        {
            var request = new RestRequest(path, Method.Post);
            request.AddJsonBody(objeto);
            var response = await _httpClient.PostAsync<Usuario>(request);
            return response != null ? new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Cadastro Realizado") } : new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Falha ao Cadastrar Usuario.") };
        }

        public async Task<HttpResponseMessage> ApiRemove(string jwt, Object id, string path)
        {
            var request = new RestRequest($"{path}/{id}", Method.Delete);
            _httpClient.Authenticator = new JwtAuthenticator(jwt);
            var response= await _httpClient.DeleteAsync(request);
            return response != null ? new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Deletado com sucesso!") } : new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Falha ao Deletar.") };
        }

        public async Task<TipoRetorno> ApiFindById<TipoRetorno>(string jwt, Object id, string path)
        {
            var request = new RestRequest($"{path}/{id}", Method.Get);
            _httpClient.Authenticator = new JwtAuthenticator(jwt);
            return await _httpClient.GetAsync<TipoRetorno>(request);
        }

        public async Task<List<TipoRetorno>> ApiFindAll<TipoRetorno>(string jwt, string path)
        {
            var request = new RestRequest($"{path}", Method.Get);
            _httpClient.Authenticator = new JwtAuthenticator(jwt);
            return await _httpClient.GetAsync<List<TipoRetorno>>(request);
        }

        public async Task<List<TipoRetorno>> ApiFindAllById<TipoRetorno>(string jwt, Object id, string path)
        {
            var request = new RestRequest($"{path}/{id}", Method.Get);
            _httpClient.Authenticator = new JwtAuthenticator(jwt);
            return await _httpClient.GetAsync<List<TipoRetorno>>(request);
        }

        public async Task<TipoRetorno> ApiUpdate<TipoRetorno>(string jwt, Object id, Object objeto, string path)
        {
            var request = new RestRequest($"{path}/{id}", Method.Put);
            request.AddJsonBody(objeto);
            _httpClient.Authenticator = new JwtAuthenticator(jwt);
            return await _httpClient.PutAsync<TipoRetorno>(request);
        }

        public async Task<TipoRetorno> ApiSaveAutorize<TipoRetorno>(string jwt, Object objeto, string path)
        {
            var request = new RestRequest($"{path}", Method.Post);
            request.AddJsonBody(objeto);
            _httpClient.Authenticator = new JwtAuthenticator(jwt);
            return await _httpClient.PostAsync<TipoRetorno>(request);
        }


    }
}
