using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGroupFriends.Domain.Entidades
{
    public class AmigoRequest
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Urlfoto { get; set; }
        public DateTime DtAniversario { get; set; }
    }
}
