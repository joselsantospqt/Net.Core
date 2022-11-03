using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class AgendamentoUsuario
    {
        public int Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid AgendamentoId { get; set; }
    }
}
