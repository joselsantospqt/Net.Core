using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface IPetRepositorio
    {
        Pet GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Pet> GetAll();
        void SaveUpdate(Pet pet);
    }
}
