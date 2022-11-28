using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
    public class CreateAgendamento
    {
        public CreateAgendamento() { Pets = new List<Pet>(); }
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Status")]
        public EStatus Status { get; set; }
        [Display(Name = "Comentários")]
        public string Comentario { get; set; }
        public IList<Pet> Pets { get; set; }
        public Usuario MedicoResponsavel { get; set; }
        public Guid Tutor { get; set; }
    }
}
