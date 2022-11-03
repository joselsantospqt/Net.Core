﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Agendamento
    {
        public Agendamento() { Pet = new AgendamentoPet(); MedicoResponsavel = new AgendamentoUsuario(); }
        [Key]
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public EStatus Status { get; set; }
        public string Comentario { get; set; }
        public AgendamentoPet Pet { get; set; }
        public AgendamentoUsuario MedicoResponsavel { get; set; }


        internal void AddPet(Guid petId)
        {
            Pet = new AgendamentoPet() { PetId = petId, AgendamentoId = this.Id };
        }

        internal void AddMedicoResponsavel(Guid usuarioId)
        {
            MedicoResponsavel = new AgendamentoUsuario() { UsuarioId = usuarioId, AgendamentoId = this.Id };
        }
    }
}
