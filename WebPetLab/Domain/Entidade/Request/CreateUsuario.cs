using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
    public class CreateUsuario
    {
        public CreateUsuario() { Nao_Preenchido = "---"; }
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
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "CRM")]
        public string Crm { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data Nascimento")]
        public DateTime DataNascimento { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Tipo Usuário")]
        public ETipoUsuario TipoUsuario { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Anexo")]
        public byte[] Anexo { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Tipo Anexo")]
        public string TipoAnexo { get; set; }
        [NotMapped]
        [Display(Name = "Imagem Usuário")]
        public string url_documento { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Nao_Preenchido { get; set; }
    }
}
