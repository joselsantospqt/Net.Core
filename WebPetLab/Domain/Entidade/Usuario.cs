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
        public Usuario() { Pets = new List<UsuarioPet>(); }
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public string Crm { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ImagemUrlusuario { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ETipoUsuario TipoUsuario { get; set; }
        public IList<UsuarioPet> Pets { get; set; }

        internal void AddPet(Guid petId)
        {
            Pets.Add(new UsuarioPet() { PetId = petId, UsuarioId = this.Id });
        }

    }
}
