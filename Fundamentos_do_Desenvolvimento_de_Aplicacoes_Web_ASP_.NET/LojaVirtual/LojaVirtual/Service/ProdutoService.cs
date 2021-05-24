using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Service
{
    public class ProdutoService : BaseService
    {
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

            command.Connection.Close();

            return produto;
        }
    }
}
