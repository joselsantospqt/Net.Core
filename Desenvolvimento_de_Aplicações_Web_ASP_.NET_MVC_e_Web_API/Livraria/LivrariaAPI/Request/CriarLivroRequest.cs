using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.Request
{
    public class CriarLivroRequest
    {
        public List<Guid> AutorId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ISBN { get; set; }
        public int Ano { get; set; }


    }
}
