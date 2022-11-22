using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
   public class CreatePet
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public ETipoEspecie Especie { get; set; }
        public string ImagemUrlPet { get; set; }
    }
}
