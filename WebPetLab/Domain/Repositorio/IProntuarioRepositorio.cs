using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface IProntuarioRepositorio
    {
        Prontuario GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Prontuario> GetAll();
        void SaveUpdate(Prontuario prontuario);
    }
}
