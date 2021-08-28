using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaCore
{
   public class Livro
    {
        public Guid Id { get; set; }
        public Guid AutorId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ISBN { get; set; }
        public DateTime CreateDt { get; set; }
        public DateTime UpdatedDt { get; set; }
    }
}
