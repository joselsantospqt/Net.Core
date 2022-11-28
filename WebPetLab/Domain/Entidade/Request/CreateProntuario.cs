using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
    public class CreateProntuario
    {
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Resumo")]
        public string Resumo { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }
    }
}
