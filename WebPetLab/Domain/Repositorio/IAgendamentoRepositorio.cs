using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface IAgendamentoRepositorio
    {
        Agendamento GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Agendamento> GetAll();
        void SaveUpdate(Agendamento agendamento);
    }
}
