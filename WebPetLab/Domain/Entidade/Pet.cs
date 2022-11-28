using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Pet
    {
        public Pet() { Tutor = new UsuarioPet(); Agendamentos = new List<AgendamentoPet>(); Prontuarios = new List<ProntuarioPet>(); Documentos = new List<DocumentoPet>(); }
        [Key]
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
        [DataType(DataType.Text)]
        [Display(Name = "Imagem do Pet")]
        public string ImagemUrlPet { get; set; }
        public UsuarioPet Tutor { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IList<AgendamentoPet> Agendamentos { get; set; }
        public IList<ProntuarioPet> Prontuarios { get; set; }
        public IList<DocumentoPet> Documentos { get; set; }

        internal void AddTutor(Guid usuarioId)
        {
            Tutor = new UsuarioPet() { UsuarioId = usuarioId, PetId = this.Id };
        }
    }
}
