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
    public class RepositorioAutor : IRepositorioAutor
    {
        private BancoDeDados db;

        public RepositorioAutor(BancoDeDados bancoDeDados)
        {
            db = bancoDeDados;
        }

        public IEnumerable<Autor> GetAll()
        {
            var pessoas = db.Autor.AsNoTracking().ToList();
            return pessoas;
        }

        public Autor GetById(Guid id)
        {
            var Autor = db.Autor.Find(id);
            return Autor;
        }

        public void Remove(Guid id)
        {
            var Autor = db.Autor.Find(id);
            if (Autor != null)
            {
                db.Autor.Remove(Autor);
                db.SaveChanges();
            }

        }

        public void Save(Autor autor)
        {
            db.Autor.Add(autor);
            db.SaveChanges();
        }

        public void Update(Autor autor)
        {
            db.Autor.Update(autor);
            db.SaveChanges();
        }
    }
}
