using LojaVirtual.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    public class ProdutosController : Controller
    {

        [HttpGet]
        [Route("produtos/cadastrar")]
        [Route("prod/cadastrar")]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        [Route("produtos/listar")]
        public ActionResult Listar(string pesquisa, string ordenarpelonome)
        {
            if (pesquisa != null)
            {
                var produtosPesquisados = produtos.Where(x => x.Nome == pesquisa).ToList();
                return View(produtosPesquisados);

            }

            if (ordenarpelonome == "asc")
            {
                return View(produtos.OrderBy(produto => produto.Nome).ToList());
            }

            return View(produtos);
        }

        [HttpPost]
        public ActionResult ExecutarCadastroDeProduto(String nome, decimal preco)
        {
            Produto produto = new Produto();
            produto.Nome = nome;
            produto.Preco = preco;
            produto.Id = produtos.Count + 1;
            produtos.Add(produto);

            return Redirect("/produtos/listar");
        }

        static List<Produto> produtos = new List<Produto>();

        //public ActionResult ExecutarCadastroDeProduto(String nome, decimal preco)
        //{

        //    GerenciadoDeProduto gerenciador = new GerenciadoDeProduto();

        //    gerenciador.CadastrarProduto(nome, preco);

        //    return RedirectToAction("ProdutoCadastrado");
        //}

        [HttpGet]
        public ActionResult ProdutoCadastrado()
        {
            return View();
        }


        [HttpGet]
        [Route("produtos/editar")]
        public ActionResult Editar(int id)
        {
            var produto = produtos.First(produto => produto.Id == id);
            return View(produto);
        }

        [HttpPost]
        [Route("produtos/editar")]
        [Route("prod/editar")]
        public ActionResult Editar(int id, string nome, decimal preco)
        {
            Produto produto = produtos.First(produto => produto.Id == id);
            produto.Nome = nome;
            produto.Preco = preco;

            return Redirect("/produtos/listar");
        }

        [HttpGet]
        [Route("produtos/excluir")]
        public ActionResult ExcluirGet(int id)
        {
            var produto = produtos.First(produto => produto.Id == id);
            return View("Excluir", produto);
        }

        [HttpPost]
        [Route("produtos/excluir")]
        public ActionResult ExcluirPost(int id)
        {
            var produto = produtos.First(produto => produto.Id == id);

            produtos.Remove(produto);

            return Redirect("/produtos/listar");
        }
    }

}
