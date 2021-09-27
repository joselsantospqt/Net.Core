using Domain.Entidades;
using Domain.Repositorio;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private CosmosClient _CosmosClient { get; set; }
        private Container _Db { get; }
        private static string name_container = "tarefas-container";
        private string ConnectionString = "AccountEndpoint=https://infnetatazuredb.documents.azure.com:443/;AccountKey=JVjmBYkLoit96YUIsZb7eCrRPMqi3rfX0Tl2v2mDoWZMGucBYRqXNf7LYjGHkOm4yO76pzPhSO2kqBSysPZEeA==;";
        private string Database = "AT-Infnet-Db";

        public TarefaRepositorio()
        {
            _CosmosClient = new CosmosClient(ConnectionString);
            _Db = _CosmosClient.GetContainer(Database, name_container);
        }

        public IEnumerable<Tarefa> GetAll()
        {
            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c");

            var result = new List<Tarefa>();

            var queryResult = _Db.GetItemQueryIterator<Tarefa>(queryDefinition);

            while (queryResult.HasMoreResults)
            {
                FeedResponse<Tarefa> currentResultSet = queryResult.ReadNextAsync().Result;
                result.AddRange(currentResultSet.Resource);
            }

            return result;

        }

        public Tarefa GetById(Guid id)
        {
            QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c where c.id = '{id}'");

            var queryResult = _Db.GetItemQueryIterator<Tarefa>(queryDefinition);

            Tarefa item = new();

            while (queryResult.HasMoreResults)
            {
                FeedResponse<Tarefa> currentResultSet = queryResult.ReadNextAsync().Result;
                item = currentResultSet.Resource.FirstOrDefault();
            }

            return item;
        }

        public async Task Remove(Tarefa tarefa)
        {
            await _Db.DeleteItemAsync<Tarefa>(tarefa.Id.ToString(), new PartitionKey(tarefa.PartitionKey));
        }

        public async Task Save(Tarefa tarefa)
        {
            await _Db.CreateItemAsync<Tarefa>(tarefa, new PartitionKey(tarefa.PartitionKey));
        }

        public async Task Update(Tarefa tarefa)
        {
            await _Db.ReplaceItemAsync<Tarefa>(tarefa, tarefa.Id.ToString(), new PartitionKey(tarefa.PartitionKey));
        }
    }
}
