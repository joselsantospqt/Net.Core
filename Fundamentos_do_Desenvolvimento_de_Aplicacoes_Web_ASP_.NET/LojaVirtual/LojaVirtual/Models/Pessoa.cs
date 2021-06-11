using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Pessoa
    {
        public int ID { get; set; }
        public string NM_NOME { get; set; }
        public string NM_SOBRENOME { get; set; }
        public DateTime DT_NASCIMENTO { get; set; }
    }
}
