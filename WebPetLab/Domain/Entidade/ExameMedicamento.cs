using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class ExameMedicamento
    {
        public int Id { get; set; }
        public Guid MedicamentoId { get; set; }
        public Guid ExameId { get; set; }
    }
}
