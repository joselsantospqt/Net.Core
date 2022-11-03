using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class ProntuarioExame
    {
        public int Id { get; set; }
        public Guid ProntuarioId { get; set; }
        public Guid ExameId { get; set; }
    }
}
