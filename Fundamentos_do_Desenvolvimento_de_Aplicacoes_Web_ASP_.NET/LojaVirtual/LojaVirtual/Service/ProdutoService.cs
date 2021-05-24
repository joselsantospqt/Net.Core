using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Service
{
    public class ProdutoService : BaseService
    {
        public void CadastrarProduto(string nome, string preco, int quantidade)
        {
            Produto produto = new Produto();
            produto.NM_NOME = nome;
            produto.NR_PRECO = String.Format("R$ {0:C}", preco);
            produto.NR_QUANTIDADE = quantidade;

            var command = ConnectDataBase();

            command.CommandText = "INSERT INTO PRODUTO (NM_NOME, NR_PRECO, NR_QUANTIDADE) VALUES(@NOME, @PRECO, @QUANTIDADE)";
            command.Parameters.AddWithValue("@NOME", nome.ToUpper());
            command.Parameters.AddWithValue("@PRECO", String.Format("R$ {0:C}", preco));
            command.Parameters.AddWithValue("@QUANTIDADE", quantidade);
            command.ExecuteNonQuery();

            command.Connection.Close();
        }

        public void AlterarProduto(Produto produto)
        {
            var command = ConnectDataBase();

            command.CommandText = "UPDATE PRODUTO SET NM_NOME = @NOME, NR_PRECO = @PRECO, NR_QUANTIDADE = @QUANTIDADE WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", produto.ID);
            command.Parameters.AddWithValue("@NOME", produto.NM_NOME.ToUpper());
            command.Parameters.AddWithValue("@PRECO", produto.NR_PRECO);
            command.Parameters.AddWithValue("@QUANTIDADE", produto.NR_QUANTIDADE);
            ExecutarQuery();
            FecharQuery();
        }

        public void RemoverProduto(int id)
        {
            var command = ConnectDataBase();
            command.CommandText = "DELETE FROM PRODUTO WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            ExecutarQuery();
            FecharQuery();

        }

        public List<Produto> BuscaProdutoNome(string nome)
        {
            List<Produto> ListaProdutos = new List<Produto>();
            var command = ConnectDataBase();
            command.CommandText = "SELECT  * FROM PRODUTO WHERE NM_NOME = @NOME";
            command.Parameters.AddWithValue("@NOME", nome.ToUpper());
            var vLer = command.ExecuteReader();

            while (vLer.Read())
            {
                var idProduto = Convert.ToInt32(vLer["ID"]);
                var nome_produto = vLer["NM_NOME"].ToString();
                var preco = vLer["NR_PRECO"].ToString();
                var quantidade = Convert.ToInt32(vLer["NR_QUANTIDADE"]);

                if (idProduto != 0)
                {
                    var produto = new Produto();
                    produto.ID = Convert.ToInt32(idProduto);
                    produto.NM_NOME = nome_produto.ToString();
                    produto.NR_PRECO = preco.ToString();
                    produto.NR_QUANTIDADE = Convert.ToInt32(quantidade);
                    ListaProdutos.Add(produto);
                }
            }

            command.Connection.Close();

            return ListaProdutos;
        }

        public List<Produto> BuscarListaProdutos()
        {
            var ListaProduto = new List<Produto>();
            var command = ConnectDataBase();

            command.CommandText = "SELECT  * FROM PRODUTO";
            var vLer = command.ExecuteReader();

            while (vLer.Read())
            {
                var id = Convert.ToInt32(vLer["ID"]);
                var nome = vLer["NM_NOME"].ToString();
                var preco = vLer["NR_PRECO"].ToString();
                var quantidade = Convert.ToInt32(vLer["NR_QUANTIDADE"]);

                var produto = new Produto();
                produto.ID = id;
                produto.NM_NOME = nome;
                produto.NR_PRECO = preco;
                produto.NR_QUANTIDADE = quantidade;
                ListaProduto.Add(produto);
            }
            FecharQuery();
            return ListaProduto;
        }

        public Produto BuscarProdutoId(int id)
        {
            var produto = new Produto();
            command = ConnectDataBase();
            command.CommandText = "SELECT  * FROM PRODUTO WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            var vLer = ObterPorID(id);

            while (vLer.Read())
            {
                var nm_nome = vLer["NM_NOME"].ToString();
                var nr_preco = vLer["NR_PRECO"].ToString();
                var nr_quantidade = Convert.ToInt32(vLer["NR_QUANTIDADE"]);

                produto.ID = id;
                produto.NM_NOME = nm_nome;
                produto.NR_PRECO = nr_preco;
                produto.NR_QUANTIDADE = nr_quantidade;

            }
            FecharQuery();
            return produto;
        }
    }
}
