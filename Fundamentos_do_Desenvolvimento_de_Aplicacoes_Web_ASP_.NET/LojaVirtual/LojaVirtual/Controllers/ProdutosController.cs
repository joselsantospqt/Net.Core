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
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Listar()
        {
            GerenciadoDeProduto gerenciador = new GerenciadoDeProduto();

            List<Produto> listaDeProdutos = gerenciador.ListarProdutos();

            return View(listaDeProdutos);
        }

        [HttpPost]
        public ActionResult ExecutarCadastroDeProduto(String nome, decimal preco)
        {

            GerenciadoDeProduto gerenciador = new GerenciadoDeProduto();

            gerenciador.CadastrarProduto(nome, preco);

            return RedirectToAction("ProdutoCadastrado");
        }

        [HttpGet]
        public ActionResult ProdutoCadastrado()
        {
            return View();
        }

    }


    public class GerenciadoDeProduto
    {
        static List<Produto> listaDeProdutos = new List<Produto>();

        public string CadastrarProduto(string nome, decimal preco)
        {
            //var vDados= (Produto)Session["Produtos"];

            Produto produto = new Produto();
            produto.Nome = nome;
            produto.Preco = preco;

            listaDeProdutos.Add(produto);

            return "Produto Cadastrado";

        }

        public List<Produto> ListarProdutos()
        {

            return listaDeProdutos;

        }

        void AlterarProduto() { }

        void ExcluirProduto() { }


    }
    public class ProdutoCadastrado { }

    public class Produto { public string Nome; public decimal Preco; public int Quantidade; }

    public class Contato { public string Nome; public int Telefone; }

    public class Cliente { }


}
