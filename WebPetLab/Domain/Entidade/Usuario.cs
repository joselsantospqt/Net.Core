using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Usuario
    {
        public Usuario() { Pets = new List<UsuarioPet>();}
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Telefone { get; set; }
        public int Cpf { get; set; }
        public int Cnpj { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ImagemUrlPessoa { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ETipoUsuario TipoUsuario { get; set; }
        public IList<UsuarioPet> Pets { get; set; }

    }
}
