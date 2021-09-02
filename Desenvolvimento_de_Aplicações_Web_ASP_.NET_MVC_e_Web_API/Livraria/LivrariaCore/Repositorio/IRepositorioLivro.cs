using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaCore.Repositorio
{
    public interface IRepositorioLivro
    {
        void SaveUpdate(Livro livro);
        Livro GetById(Guid id);
        IEnumerable<Livro> GetLivrosByAutorId(Guid id);
        void Remove(Guid id);
        IEnumerable<Livro> GetAll();
    }
}
