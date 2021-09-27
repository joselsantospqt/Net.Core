using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Tarefa
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id{ get; set; }
        [Required]
        [JsonProperty(PropertyName = "titulo")]
        public string Titulo{ get; set; }
        [Required]
        [JsonProperty(PropertyName = "descricao")]
        public string Descricao{ get; set; }
        [Required]
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [Required]  
        [JsonProperty(PropertyName = "responsavel")]
        public string Responsavel { get; set; }
        [JsonProperty(PropertyName = "dtCreate")]
        public DateTime DtCreate{ get; set; }
        [JsonProperty(PropertyName = "dtUpdate")]
        public DateTime DtUpdate { get; set; }
        [JsonProperty(PropertyName = "pk")]
        public string PartitionKey { get; set; } = "tarefa";

        //COLOCAR STATUS + DONO DA TAREFA
    }
}
