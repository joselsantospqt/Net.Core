using LivrariaCore;
using LivrariaCore.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaInfrastructure.EntityFramework.Repositorio
{
    public class LivroRepositorio : BaseRepositorio<Livro>, ILivroRepositorio
    {

        public LivroRepositorio(BancoDeDados bancoDeDados) : base(bancoDeDados)
        {
        }

        public override IEnumerable<Livro> GetAll()
        {
            return _db.Livro.Include(x => x.Autores).AsNoTracking().ToList();
        }

        public override Livro GetById(Guid id)
        {
            Livro Livro = _db.Livro.Include(x => x.Autores).FirstOrDefault();
            return Livro;
        }

        
        public void SaveUpdate(Livro livro)
        {
            if (livro.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                Add(livro);
            else
                Update(livro);
        }
    }
}
