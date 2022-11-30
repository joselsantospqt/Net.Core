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
    }
}
