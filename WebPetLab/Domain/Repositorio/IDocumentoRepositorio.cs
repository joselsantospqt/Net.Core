using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface IDocumentoRepositorio
    {
        Documento GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Documento> GetAll();
        void SaveUpdate(Documento documento);
    }
}
