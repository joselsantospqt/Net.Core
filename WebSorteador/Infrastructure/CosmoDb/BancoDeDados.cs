using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CosmoDb
{
    public class BancoDeDados
    {
        public Container Container { get; }
        private CosmosClient CosmosClient { get; set; }

        private string ConnectionString = "AccountEndpoint=https://sinqiasorteador-db.documents.azure.com:443/;AccountKey=ESG9cvLIry4Nj9Wstc5YRJruXFsFoBZbx4Hy3QMugSHl7lxcsrCxiLfvodcwP14joDWFqt6NtHkl57rzVjBlxQ==;";
        private string Database = "sinqia-db";
        //private string ConnectionString = "";
        //private string Database = "";
        public BancoDeDados(string container)
        {
            try
            {
                CosmosClient = new CosmosClient(ConnectionString);
                Container = CosmosClient.GetContainer(this.Database, container);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um problema no acesso ao Cosmo DB - Erro: {ex}", ex);
            }
        }
    }
}
