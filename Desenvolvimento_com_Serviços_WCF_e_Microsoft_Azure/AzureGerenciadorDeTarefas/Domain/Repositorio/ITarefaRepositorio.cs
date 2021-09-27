using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface ITarefaRepositorio
    {
        Task Update(Tarefa tarefa);
        Task Save(Tarefa tarefa);
        Tarefa GetById(Guid id);
        Task Remove(Tarefa tarefa);
        IEnumerable<Tarefa> GetAll();

    }
}
