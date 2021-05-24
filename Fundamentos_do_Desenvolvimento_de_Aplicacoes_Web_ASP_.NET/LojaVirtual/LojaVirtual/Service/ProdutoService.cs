using LojaVirtual.DataBase;
using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace LojaVirtual.Service
{
    public class ProdutoService : BaseService
    {
        private LojaVirtualDb db;

        public ProdutoService(LojaVirtualDb bancoDeDados)
        {
            db = bancoDeDados;
        }

        public void CadastrarProduto(string nome, string preco, int quantidade)
        {
            Produto produto = new Produto();
            produto.NM_NOME = nome;
            produto.NR_PRECO = String.Format("R$ {0:C}", preco);
            produto.NR_QUANTIDADE = quantidade;

            db.TabelaProduto.Add(produto);
            db.SaveChanges();
        }

        public void AlterarProduto(Produto produto)
        {
            db.TabelaProduto.Update(produto);
            db.SaveChanges();
        }

        public void RemoverProduto(int id)
        {
            var produto = db.TabelaProduto.Find(id);
            db.TabelaProduto.Remove(produto);
            db.SaveChanges();
        }

        public List<Produto> BuscaProdutoNome(string nome)
        {
            List<Produto> ListaProdutos = new List<Produto>();
            ListaProdutos.Add(db.TabelaProduto.Find(nome));

            return ListaProdutos;
        }

        public List<Produto> BuscarListaProdutos()
        {
            var ListaProduto = db.TabelaProduto.ToList();
            return ListaProduto;
        }

        public Produto BuscarProdutoId(int id)
        {
            var produto = db.TabelaProduto.Find(id);
            return produto;
        }
    }
}
