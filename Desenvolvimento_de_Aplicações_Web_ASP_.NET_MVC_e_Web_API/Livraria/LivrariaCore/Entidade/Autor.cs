using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaCore
{
    public class Autor
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime Datanascimento { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IEnumerable<Livro> Livros{get; set;}

}
}
