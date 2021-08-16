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

        public string Autor { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
