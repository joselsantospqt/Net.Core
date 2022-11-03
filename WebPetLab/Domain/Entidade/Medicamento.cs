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
        public Medicamento() { Exame = new ExameMedicamento();}

        [Key]
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Fim { get; set; }
        public ExameMedicamento Exame { get; set; }

        internal void AddExame(Guid exameId)
        {
            Exame = new ExameMedicamento() { ExameId = exameId, MedicamentoId = this.Id };
        }

    }
}
