using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    public class PessoasController : Controller
    {
        private LojaVirtualDb db { get; set; }

        public PessoasController(LojaVirtualDb bancoDeDados)
        {
            db = bancoDeDados;
        }


        static List<Pessoa> pessoas = new List<Pessoa>();

        [HttpGet]
        [Route("pessoas/cadastrar")]
        [Route("pess/cadastrar")]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PessoaCadastrado()
        {
            return View();
        }

        [HttpGet]
        [Route("pessoas/buscar")]
        [Route("pess/buscar")]
        public ActionResult Buscar()
        {
            return View();
        }

        [HttpGet]
        [Route("pessoas/listar")]
        public ActionResult Listar(string pesquisa, string ordenarpelonome)
        {
            pessoas = db.Pessoa.ToList();

            if (pesquisa != null)
            {
                var pessoasPesquisadas = pessoas.Where(x => x.NM_NOME == pesquisa).ToList();
                return View(pessoasPesquisadas);

            }

            if (ordenarpelonome == "asc")
            {
                return View(pessoas.OrderBy(x => x.NM_NOME).ToList());
            }


            return View(pessoas);
        }

        [HttpGet]
        [Route("pessoas/calendario")]
        public ActionResult Calendario()
        {
            pessoas = db.Pessoa.ToList();
            DateTime DataHoje = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            var obj = pessoas.OrderByDescending(x => x.DT_NASCIMENTO.Day).OrderByDescending(x => x.DT_NASCIMENTO.Month == DataHoje.Month).ToList();

            return View(obj);

        }

        [HttpPost]
        public ActionResult ExecutarCadastroDePessoas(string nome, string sobrenome, DateTime nascimento)
        {
            int id = pessoas.Count();
            var conexao = new PessoaService(db);
            pessoas = db.Pessoa.ToList();
            Pessoa pessoa = new Pessoa();
            if (id != 0)
            {
                pessoa = pessoas.LastOrDefault();
                id = pessoa.ID + 1;
            }
            else
                id = id + 1;

            conexao.CadastrarPessoa(id, nome, sobrenome, nascimento);

            return Redirect("/pessoas/listar");
        }

        [HttpPost]
        [Route("pessoas/BuscarPessoas")]
        public IActionResult BuscarProdutos(string nome)
        {
            var conexao = new PessoaService(db);
            var pessoa = conexao.BuscaPessoaNome(nome);
            return View("Buscar", pessoa);
        }

        [HttpGet]
        [Route("pessoas/editar")]
        public ActionResult Editar(int id)
        {
            var conexao = new PessoaService(db);
            Pessoa pessoa = conexao.BuscarPessoaId(id);
            return View(pessoa);
        }

        [HttpPost]
        [Route("pessoas/editar")]
        [Route("pess/editar")]
        public ActionResult Editar(int id, string nome, string sobrenome, DateTime nascimento)
        {
            var conexao = new PessoaService(db);
            Pessoa pessoa = new Pessoa();
            pessoa.ID = id;
            pessoa.NM_NOME = nome;
            pessoa.NM_SOBRENOME = sobrenome;
            pessoa.DT_NASCIMENTO = Convert.ToDateTime(nascimento);
            conexao.AlterarPessoa(pessoa);

            return Redirect("/pessoas/listar");
        }

        [HttpGet]
        [Route("pessoas/excluir")]
        public ActionResult ExcluirGet(int id)
        {
            var conexao = new PessoaService(db);
            Pessoa pessoa = conexao.BuscarPessoaId(id);
            return View("Excluir", pessoa);
        }

        [HttpPost]
        [Route("pessoas/excluir")]
        public ActionResult ExcluirPost(int id)
        {
            var conexao = new PessoaService(db);

            conexao.RemoverPessoa(id);

            return Redirect("/pessoas/listar");
        }
    }
}
