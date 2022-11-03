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
        public Pet() { Tutor = new UsuarioPet(); Agendamentos = new List<PetAgendamento>(); }
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public ETipoEspecie Especie { get; set; }
        public string ImagemUrlPet { get; set; }
        public UsuarioPet Tutor { get; set; }
        public IList<PetAgendamento> Agendamentos { get; set; }

        internal void AddTutor(Guid usuarioId)
        {
            Tutor = new UsuarioPet() { UsuarioId = usuarioId, PetId = this.Id };
        }
    }
}
