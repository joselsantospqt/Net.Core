using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Prontuario
    {
        public Prontuario() { Pet = new ProntuarioPet(); Medico = new ProntuarioUsuario(); Documentos = new List<ProntuarioDocumento>();}
        [Key]
        public Guid Id { get; set; }
        public string Resumo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public ProntuarioPet Pet { get; set; }
        public ProntuarioUsuario Medico { get; set; }
        public IList<ProntuarioDocumento> Documentos { get; set; }
        internal void AddPet(Guid petId)
        {
            Pet = new ProntuarioPet() { PetId = petId, ProntuarioId = this.Id };
        }

        internal void AddMedicoResponsavel(Guid usuarioId)
        {
            Medico = new ProntuarioUsuario() { UsuarioId = usuarioId, ProntuarioId = this.Id };
        }

    }
}
