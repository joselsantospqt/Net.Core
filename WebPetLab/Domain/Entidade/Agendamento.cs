using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Agendamento
    {
        public Agendamento() { Pet = new PetAgendamento(); MedicoResponsavel = new UsuarioAgendamento(); }
        [Key]
        public Guid Id { get; set; }
        public DateTime DataNascimento { get; set; }
        public EStatus Status { get; set; }
        public string Comentario { get; set; }
        public PetAgendamento Pet { get; set; }
        public UsuarioAgendamento MedicoResponsavel { get; set; }


        internal void AddPet(Guid petId)
        {
            Pet = new PetAgendamento() { PetId = petId, AgendamentoId = this.Id };
        }

        internal void AddMedicoResponsavel(Guid usuarioId)
        {
            MedicoResponsavel = new UsuarioAgendamento() { UsuarioId = usuarioId, AgendamentoId = this.Id };
        }
    }
}
