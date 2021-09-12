using Domain.Entidades;
using Domain.IRepositorio;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CosmoDb.Repositorio
{
    public class TarefaRepositorio : BancoDeDados, ITarefaRepositorio
    {
        private Container _Db { get; }
        private static string name_container = "tarefas-container";

        public TarefaRepositorio() : base(name_container)
        {
            _Db = Container;
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
