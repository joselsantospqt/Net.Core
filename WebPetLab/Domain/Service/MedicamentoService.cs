using Domain.Entidade;
using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class MedicamentoService
    {
        private IMedicamentoRepositorio RepositorioMedicamento { get; }

        public MedicamentoService(IMedicamentoRepositorio repositorioMedicamento)
        {
            RepositorioMedicamento = repositorioMedicamento;
        }

        public Medicamento GetMedicamentoById(Guid id)
        {
            return RepositorioMedicamento.GetById(id);
        }

        public IEnumerable<Medicamento> GetAll()
        {
            return RepositorioMedicamento.GetAll();
        }

        public Medicamento CreateMedicamento(
            Guid idProntuario,
            string codigo,
            string nome,
            int quantidade,
            DateTime data_Inicio,
            DateTime data_Fim
           )
        {
            var medicamento = new Medicamento();
            medicamento.Codigo = codigo;
            medicamento.Nome = nome;
            medicamento.Quantidade = quantidade;
            medicamento.Data_Inicio = data_Inicio;
            medicamento.Data_Fim = data_Fim;
            medicamento.AddProntuario(idProntuario);

            RepositorioMedicamento.SaveUpdate(medicamento);

            return medicamento;
        }

        public Medicamento UpdateMedicamento(Medicamento MedicamentoUpdate)
        {

            var medicamento = RepositorioMedicamento.GetById(MedicamentoUpdate.Id);
            if (MedicamentoUpdate.Codigo != medicamento.Codigo)
                medicamento.Codigo = MedicamentoUpdate.Codigo;
            if (MedicamentoUpdate.Nome != medicamento.Nome)
                medicamento.Nome = MedicamentoUpdate.Nome;
            if (MedicamentoUpdate.Quantidade != medicamento.Quantidade)
                medicamento.Quantidade = MedicamentoUpdate.Quantidade;
            if (MedicamentoUpdate.Data_Inicio != medicamento.Data_Inicio)
                medicamento.Data_Inicio = MedicamentoUpdate.Data_Inicio;
            if (MedicamentoUpdate.Data_Fim != medicamento.Data_Fim)
                medicamento.Data_Fim = MedicamentoUpdate.Data_Fim;

            RepositorioMedicamento.SaveUpdate(medicamento);

            return medicamento;
        }

        public void DeleteMedicamento(Guid id)
        {
            RepositorioMedicamento.Remove(id);
        }
    }
}
