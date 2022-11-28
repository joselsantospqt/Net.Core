using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
   public class CreatePet
    {
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data Nascimento")]
        public DateTime DataNascimento { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Espécie")]
        public ETipoEspecie Especie { get; set; }
        public string ImagemUrlPet { get; set; }
    }
}
