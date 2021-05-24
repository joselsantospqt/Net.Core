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
            return Registrar();
        }
    }
}
