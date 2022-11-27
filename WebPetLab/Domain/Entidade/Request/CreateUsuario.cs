using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
    public class CreateUsuario
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }
        [Required]
        [StringLength(12, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} Digitos contando com o DDD.", MinimumLength = 10)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Cpf")]
        public string Cpf { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Cnpj")]
        public string Cnpj { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Crm")]
        public string Crm { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "DataNascimento")]
        public DateTime DataNascimento { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ImagemUrlusuario")]
        public string ImagemUrlusuario { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "TipoUsuario")]
        public ETipoUsuario TipoUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
