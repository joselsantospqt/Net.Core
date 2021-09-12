using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Tarefa
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id{ get; set; }
        [JsonProperty(PropertyName = "titulo")]
        public string Titulo{ get; set; }
        [JsonProperty(PropertyName = "descricao")]
        public string Descricao{ get; set; }
        [JsonProperty(PropertyName = "dtCreate")]
        public DateTime DtCreate{ get; set; }
        [JsonProperty(PropertyName = "dtUpdate")]
        public DateTime DtUpdate { get; set; }
        [JsonProperty(PropertyName = "pk")]
        public string PartitionKey { get; set; } = "tarefa";
    }
}
