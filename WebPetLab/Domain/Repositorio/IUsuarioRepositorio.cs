using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Usuario GetByEmail(string email);
        Usuario GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Usuario> GetAll();
        void SaveUpdate(Usuario usuario);
    }
}
