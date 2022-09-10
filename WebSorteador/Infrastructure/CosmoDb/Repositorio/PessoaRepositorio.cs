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
    public class PessoaRepositorio : BancoDeDados, IPessoaRepositorio
    {

        private Container _Db { get; }
        private static string name_container = "pessoa";

        public PessoaRepositorio() : base(name_container)
        {
            _Db = Container;
        }

        public IEnumerable<Pessoa> GetAll()
        {
            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c");

            var result = new List<Pessoa>();

            var queryResult = _Db.GetItemQueryIterator<Pessoa>(queryDefinition);

            while (queryResult.HasMoreResults)
            {
                FeedResponse<Pessoa> currentResultSet = queryResult.ReadNextAsync().Result;
                result.AddRange(currentResultSet.Resource);
            }

            return result;

        }

        public Pessoa GetById(Guid id)
        {
            QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c where c.id = '{id}'");

            var queryResult = _Db.GetItemQueryIterator<Pessoa>(queryDefinition);

            Pessoa item = new();

            while (queryResult.HasMoreResults)
            {
                FeedResponse<Pessoa> currentResultSet = queryResult.ReadNextAsync().Result;
                item = currentResultSet.Resource.FirstOrDefault();
            }

            return item;
        }

        public async Task Remove(Pessoa pessoa)
        {
            await _Db.DeleteItemAsync<Pessoa>(pessoa.Id.ToString(), new PartitionKey(pessoa.PartitionKey));
        }

        public async Task Save(Pessoa pessoa)
        {
            await _Db.CreateItemAsync<Pessoa>(pessoa, new PartitionKey(pessoa.PartitionKey));
        }

        public async Task Update(Pessoa pessoa)
        {
            await _Db.ReplaceItemAsync<Pessoa>(pessoa, pessoa.Id.ToString(), new PartitionKey(pessoa.PartitionKey));
        }
    }
}
