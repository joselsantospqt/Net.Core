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
    public class RepositorioLivro : IRepositorioLivro
    {
        private BancoDeDados db;

        public RepositorioLivro(BancoDeDados bancoDeDados)
        {
            db = bancoDeDados;
        }

        public IEnumerable<Livro> GetAll()
        {
            var livros = db.Livro.AsNoTracking().ToList();
            return livros;
        }

        public Livro GetById(Guid id)
        {

            var livro = db.Livro.Find(id);
            return livro;
        }

        public void Remove(Guid id)
        {
            var livro = db.Livro.Find(id);
            if (livro != null)
            {
                db.Livro.Remove(livro);
                db.SaveChanges();
            }
        }

        public void Save(Livro livro)
        {
            db.Livro.Add(livro);
            db.SaveChanges();
        }

        public void Update(Livro livro)
        {
            db.Livro.Update(livro);
            db.SaveChanges();
        }
    }
}
