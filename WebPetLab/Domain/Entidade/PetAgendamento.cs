using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class PetAgendamento
    {
        public int Id { get; set; }
        public Guid AgendamentoId { get; set; }
        public Guid PetId { get; set; }
    }
}
