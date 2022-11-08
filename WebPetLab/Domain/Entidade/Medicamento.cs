using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Medicamento
    {
        public Medicamento() { Prontuario = new ProntuarioMedicamento(); }

        [Key]
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Fim { get; set; }
        public ProntuarioMedicamento Prontuario { get; set; }

        internal void AddProntuario(Guid prontuarioId)
        {
            Prontuario = new ProntuarioMedicamento() { ProntuarioId = prontuarioId, MedicamentoId = this.Id };
        }
    }
}
