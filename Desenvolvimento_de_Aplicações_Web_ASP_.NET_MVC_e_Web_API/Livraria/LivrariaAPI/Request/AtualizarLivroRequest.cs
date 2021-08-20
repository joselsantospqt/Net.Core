using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.Request
{
    public class AtualizarLivroRequest
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ISBN { get; set; }
    }
}
