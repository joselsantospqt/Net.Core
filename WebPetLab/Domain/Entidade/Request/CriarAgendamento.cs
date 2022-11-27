using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
    public class CriarAgendamento
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public EStatus Status { get; set; }
        public string Comentario { get; set; }
        public Pet Pet { get; set; }
        public Usuario MedicoResponsavel { get; set; }

    }
}
