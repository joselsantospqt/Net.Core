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

        public Container Container { get;}
        private CosmosClient CosmosClient { get; set; }

        private string ConnectionString = "AccountEndpoint=https://infnet-cosmo-db.documents.azure.com:443/;AccountKey=DTgL7ZvwPJocQBGPrhafURr55Y2qtLhZ4QmmmqdfO6rkqEMGsMaSfWCXXrSvQL5MQyUNBNSHcVaeoLHurHCJ3Q==;";
        private string Database = "infnet-db";
        public BancoDeDados(string container)
        {
            try
            {
                CosmosClient = new CosmosClient(ConnectionString);
                Container = CosmosClient.GetContainer(this.Database, container);
            }
            catch (Exception ex) { }
        }
    }
}

