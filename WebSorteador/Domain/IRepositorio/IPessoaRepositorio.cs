using System;
using Domain.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositorio
{
    public interface IPessoaRepositorio
    {
        Task Update(Pessoa tarefa);
        Task Save(Pessoa tarefa);
        Pessoa GetById(Guid id);
        Task Remove(Pessoa tarefa);
        IEnumerable<Pessoa> GetAll();
    }
}
