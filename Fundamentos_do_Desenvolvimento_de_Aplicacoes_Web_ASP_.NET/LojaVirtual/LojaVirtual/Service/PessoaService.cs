using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Service
{
    public class PessoaService : BaseService
    {
        public Pessoa BuscarPorId(int id)
        {
            var pessoa = new Pessoa();
            command = ConnectDataBase();
            command.CommandText = "SELECT  * FROM PESSOA WHERE ID = @ID";
            var vLer = ObterPorID(id);

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

        public int Registrar(string nome, string sobreNome, DateTime aniversario)
        {

            Pessoa pessoa = new Pessoa();
            pessoa.NM_NOME = nome;
            pessoa.NM_SOBRENOME = sobreNome;
            pessoa.DT_NASCIMENTO = aniversario;

            command = ConnectDataBase();

            command.CommandText = "INSERT INTO PESSOA(NM_NOME, NM_SOBRENOME, DT_NASCIMENTO) VALUES(@NOME, @SOBRENOME, @ANIVERSARIO)";
            command.Parameters.AddWithValue("@NOME", nome.ToUpper());
            command.Parameters.AddWithValue("@SOBRENOME", sobreNome.ToUpper());
            command.Parameters.AddWithValue("@ANIVERSARIO", aniversario.ToString("MM/dd/yyyy"));
            return ExecutarQuery();
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
    }
}
