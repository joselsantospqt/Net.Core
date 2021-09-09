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
    public class AutorRepositorio : BaseRepositorio<Autor>, IAutorRepositorio
    {

        public AutorRepositorio(BancoDeDados bancoDeDados) : base(bancoDeDados)
        {
        }
        public override IEnumerable<Autor> GetAll()
        {
            return _db.Autor.Include(x => x.Livros).AsNoTracking().ToList();
        }

        public override Autor GetById(Guid id)
        {
            Autor autores = _db.Autor.Include(x => x.Livros).FirstOrDefault();
            return autores;
        }

        public void SaveUpdate(Autor autor)
        {

            if (autor.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
               Add(autor);
            else
               Update(autor);
        }
    }
}
