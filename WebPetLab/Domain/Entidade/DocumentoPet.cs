using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class DocumentoPet
    {
        public int Id { get; set; }
        public Guid DocumentoId { get; set; }
        public Guid PetId { get; set; }
    }
}
