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
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Status")]
        public EStatus Status { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Comentario")]
        public string Comentario { get; set; }
    }
}
