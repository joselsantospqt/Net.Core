using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface IExameRepositorio
    {
        Exame GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Exame> GetAll();
        void SaveUpdate(Exame exame);
    }
}
