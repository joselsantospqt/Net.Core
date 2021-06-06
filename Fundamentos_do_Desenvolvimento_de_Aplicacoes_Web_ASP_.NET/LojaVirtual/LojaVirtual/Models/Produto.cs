using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Produto
    {
        public int ID { get; set; }
        public string NM_NOME { get; set; }
        public string NR_PRECO { get; set; }
        public int NR_QUANTIDADE { get; set; }
    }
}
