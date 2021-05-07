using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Produto
    {
        public int Id;
        public string Nome;
        public decimal Preco;
        public int Quantidade;

    }

    public class Pessoa
    {
        public string Cpf { get; set; }
    }
}
