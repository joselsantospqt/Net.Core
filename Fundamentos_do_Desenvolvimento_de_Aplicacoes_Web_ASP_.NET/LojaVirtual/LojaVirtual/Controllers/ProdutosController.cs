using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    public class ProdutosController : Controller
    {
        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Listar()
        {
            return View();
        }


    }

    class Calculadora
    {
        int Somar(int numeroA, int numeroB)
        {
        }
    }

    public class GerenciadoDeProduto
    {
        ProdutoCadastrado CadastrarProduto(string nome, decimal preco)
        {
            Produto produto = new Produto();
            produto.Nome = nome;
            produto.Preco = preco;

        }
    }
    public class ProdutoCadastrado { }

    public class Produto { public string Nome; public decimal Preco; }

    public class Contato { public string Nome; public int Telefone; }

    public class Cliente { }


}
