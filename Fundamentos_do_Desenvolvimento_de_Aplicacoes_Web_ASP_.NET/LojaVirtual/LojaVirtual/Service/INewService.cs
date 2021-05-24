using LojaVirtual.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Service
{
    public interface INewService : TrataErro
    {
        #region CONEXÃO GERAL 
        public SqlCommand ConnectDataBase()
        {
            var dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=localhost;Initial Catalog=LojaVirtualDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            dbConnection.Open();
            var command = dbConnection.CreateCommand();

            return (command);
        }
        #endregion

        #region TODAS AS FUNÇÕES PARA PESSOA 

        public Pessoa buscaPeloId(int id)
        {
            var pessoa = new Pessoa();
            var command = ConnectDataBase();
            command.CommandText = "SELECT  * FROM PESSOA WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            var vLer = command.ExecuteReader();

            while (vLer.Read())
            {
                var idPessoa = vLer["ID"];
                var nome = vLer["NM_NOME"];
                var sobreNome = vLer["NM_SOBRENOME"];
                var aniversario = vLer["DT_NASCIMENTO"];

                pessoa.ID = Convert.ToInt32(idPessoa);
                pessoa.NM_NOME = Convert.ToString(nome);
                pessoa.NM_SOBRENOME = Convert.ToString(sobreNome);
                pessoa.DT_NASCIMENTO = Convert.ToDateTime(aniversario);
            }

            command.Connection.Close();

            return pessoa;
        }

        public Pessoa buscaPeloNome(string nome)
        {
            var pessoa = new Pessoa();
            var command = ConnectDataBase();
            command.CommandText = "SELECT  * FROM PESSOA WHERE NOME = @NOME";
            command.Parameters.AddWithValue("@NOME", nome.ToUpper());
            var vLer = command.ExecuteReader();

            while (vLer.Read())
            {
                var idPessoa = Convert.ToInt32(vLer["ID"]);
                var nome_pessoa = vLer["NM_NOME"].ToString();
                var sobreNome = vLer["NM_SOBRENOME"].ToString();
                var aniversario = Convert.ToDateTime(vLer["DT_NASCIMENTO"]);

                if (idPessoa != 0)
                {
                    pessoa.ID = Convert.ToInt32(idPessoa);
                    pessoa.NM_NOME = Convert.ToString(nome_pessoa);
                    pessoa.NM_SOBRENOME = Convert.ToString(sobreNome);
                    pessoa.DT_NASCIMENTO = Convert.ToDateTime(aniversario);
                }
            }

            command.Connection.Close();

            return pessoa;
        }

        public List<Pessoa> PegaListaPessoas()
        {
            var ListaPessoas = new List<Pessoa>();
            var command = ConnectDataBase();

            command.CommandText = "SELECT  * FROM pessoa";
            var vLer = command.ExecuteReader();

            while (vLer.Read())
            {
                var id = vLer["ID"];
                var nome = vLer["NM_NOME"];
                var sobreNome = vLer["NM_SOBRENOME"];
                var aniversario = vLer["DT_NASCIMENTO"];

                var pessoa = new Pessoa();
                pessoa.ID = Convert.ToInt32(id);
                pessoa.NM_NOME = Convert.ToString(nome);
                pessoa.NM_SOBRENOME = Convert.ToString(sobreNome);
                pessoa.DT_NASCIMENTO = Convert.ToDateTime(aniversario);
                ListaPessoas.Add(pessoa);
            }
            command.Connection.Close();

            return ListaPessoas;
        }

        public void RemoverPessoa(int id)
        {
            var command = ConnectDataBase();
            command.CommandText = "DELETE FROM PESSOA WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();
            command.Connection.Close();

        }

        public void SalvarAlteracao(Pessoa pessoa)
        {
            var command = ConnectDataBase();

            command.CommandText = "UPDATE PESSOA SET NM_NOME = @NOME, NM_SOBRENOME = @SOBRENOME, DT_NASCIMENTO = @ANIVERSARIO WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", pessoa.ID);
            command.Parameters.AddWithValue("@NOME", pessoa.NM_NOME.ToUpper());
            command.Parameters.AddWithValue("@SOBRENOME", pessoa.NM_SOBRENOME.ToUpper());
            command.Parameters.AddWithValue("@ANIVERSARIO", pessoa.DT_NASCIMENTO.ToString("MM/dd/yyyy"));
            command.ExecuteNonQuery();

            command.Connection.Close();
        }

        public void RegistraPessoaBanco(string nome, string sobreNome, DateTime aniversario)
        {
            Pessoa pessoa = new Pessoa();
            pessoa.NM_NOME = nome;
            pessoa.NM_SOBRENOME = sobreNome;
            pessoa.DT_NASCIMENTO = aniversario;

            var command = ConnectDataBase();

            command.CommandText = "INSERT INTO PESSOA(NM_NOME, NM_SOBRENOME, DT_NASCIMENTO) VALUES(@NOME, @SOBRENOME, @ANIVERSARIO)";
            command.Parameters.AddWithValue("@NOME", nome.ToUpper());
            command.Parameters.AddWithValue("@SOBRENOME", sobreNome.ToUpper());
            command.Parameters.AddWithValue("@ANIVERSARIO", aniversario.ToString("MM/dd/yyyy"));
            command.ExecuteNonQuery();

            command.Connection.Close();
        }
        #endregion

        #region TODAS AS FUNÇÕES PARA PRODUTO 
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
            command.ExecuteNonQuery();

            command.Connection.Close();
        }

        public void RemoverProduto(int id)
        {
            var command = ConnectDataBase();
            command.CommandText = "DELETE FROM PRODUTO WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();
            command.Connection.Close();

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

        public Produto BuscarProdutoId(int id)
        {
            var produto = new Produto();
            var command = ConnectDataBase();
            command.CommandText = "SELECT  * FROM PRODUTO WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            var vLer = command.ExecuteReader();

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

            command.Connection.Close();

            return produto;
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
            command.Connection.Close();

            return ListaProduto;
        }
        #endregion
    }
}
