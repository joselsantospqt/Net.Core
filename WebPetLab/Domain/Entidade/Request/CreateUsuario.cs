using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
    public class CreateUsuario
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Telefone { get; set; }
        public int Cpf { get; set; }
        public int Cnpj { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ImagemUrlUsuario { get; set; }
        public ETipoUsuario TipoUsuario { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
