using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface IMedicamentoRepositorio
    {
        Medicamento GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Medicamento> GetAll();
        void SaveUpdate(Medicamento medicamento);
    }
}
