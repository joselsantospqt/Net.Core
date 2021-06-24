using LojaVirtual.DataBase;
using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Service
{
    public class PessoaService : BaseService
    {

        private LojaVirtualDb db;
        public PessoaService(LojaVirtualDb bancoDeDados)
        {
            db = bancoDeDados;
        }


        public Pessoa BuscarPessoaId(int id)
        {
            var pessoa = db.Pessoa.Find(id);
            return pessoa;
        }

        public void CadastrarPessoa(int id, string nome, string sobreNome, DateTime aniversario)
        {

            Pessoa pessoa = new Pessoa();
            pessoa.ID = id;
            pessoa.NM_NOME = nome;
            pessoa.NM_SOBRENOME = sobreNome;
            pessoa.DT_NASCIMENTO = aniversario;

            db.Pessoa.Add(pessoa);
            db.SaveChanges();
        }

        public List<Pessoa> BuscaPessoaNome(string nome)
        {
           
            List<Pessoa> ListaPessoa = new List<Pessoa>();
            ListaPessoa.Add(db.Pessoa.Find(nome));

            return ListaPessoa;
        }

        public List<Pessoa> BuscarListaPessoas()
        {
            var ListaPessoa = db.Pessoa.ToList();
            return ListaPessoa;
        }

        public void RemoverPessoa(int id)
        {
            var pessoa = db.Pessoa.Find(id);
            db.Pessoa.Remove(pessoa);
            db.SaveChanges();

        }

        public void AlterarPessoa(Pessoa pessoa)
        {
            db.Pessoa.Update(pessoa);
            db.SaveChanges();
        }
    }
}
