using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaCore.Repositorio
{
    public interface IRepositorioLivro
    {
        void Save(Livro livro);
        Livro GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Livro> GetAll();
        void Update(Livro livro);
    }
}
