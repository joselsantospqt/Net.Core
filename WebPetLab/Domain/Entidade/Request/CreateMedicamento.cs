using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
    public class CreateMedicamento
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Fim { get; set; }
    }
}
