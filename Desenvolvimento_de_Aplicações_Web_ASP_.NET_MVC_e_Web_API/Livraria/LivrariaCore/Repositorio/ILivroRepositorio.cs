using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaCore.Repositorio
{
    public interface ILivroRepositorio
    {
        void SaveUpdate(Livro livro);
        Livro GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Livro> GetAll();
    }
}
