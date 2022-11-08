using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Exame
    {
        public Exame() { Prontuario = new ProntuarioExame();}
        [Key]
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public byte Documento { get; set; }
        public DateTime Data { get; set; }
        public ProntuarioExame Prontuario { get; set; }

        internal void AddProntuario(Guid prontuarioId)
        {
            Prontuario = new ProntuarioExame() { ProntuarioId = prontuarioId,  ExameId = this.Id };
        }

    }
}
