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

        public void CadastrarProduto(int id, string nome, string preco, int quantidade)
        {
            Produto produto = new Produto();
            produto.ID = id;
            produto.NM_NOME = nome;
            produto.NR_PRECO = String.Format("R$ {0:C}", preco);
            produto.NR_QUANTIDADE = quantidade;

            db.Produto.Add(produto);
            db.SaveChanges();
        }

        public void AlterarProduto(Produto produto)
        {
            db.Produto.Update(produto);
            db.SaveChanges();
        }

        public void RemoverProduto(int id)
        {
            var produto = db.Produto.Find(id);
            db.Produto.Remove(produto);
            db.SaveChanges();
        }

        public List<Produto> BuscaProdutoNome(string nome)
        {
            List<Produto> ListaProdutos = new List<Produto>();
            ListaProdutos.Add(db.Produto.Find(nome));

            return ListaProdutos;
        }

        public List<Produto> BuscarListaProdutos()
        {
            var ListaProduto = db.Produto.ToList();
            return ListaProduto;
        }

        public Produto BuscarProdutoId(int id)
        {
            var produto = db.Produto.Find(id);
            return produto;
        }
    }
}
