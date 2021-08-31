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
        private BancoDeDados _db;

        public RepositorioAutor(BancoDeDados bancoDeDados)
        {
            _db = bancoDeDados;
        }

        public IEnumerable<Autor> GetAll()
        {
            var pessoas = _db.Autor.AsNoTracking().ToList();
            return pessoas;
        }

        public Autor GetById(Guid id)
        {
            var autor = _db.Autor.Find(id);
            return autor;
        }

        public void Remove(Guid id)
        {
            var Autor = _db.Autor.Find(id);
            if (Autor != null)
            {
                _db.Autor.Remove(Autor);
                _db.SaveChanges();
            }

        }

        public void Save(Autor autor)
        {
            _db.Autor.Add(autor);
            _db.SaveChanges();
        }

        public void Update(Autor autor)
        {
            _db.Autor.Update(autor);
            _db.SaveChanges();
        }
    }
}
