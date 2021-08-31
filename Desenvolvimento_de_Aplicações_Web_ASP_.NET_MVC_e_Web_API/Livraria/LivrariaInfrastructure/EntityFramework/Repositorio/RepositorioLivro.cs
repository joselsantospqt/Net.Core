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
        private BancoDeDados _db;

        public RepositorioLivro(BancoDeDados bancoDeDados)
        {
            _db = bancoDeDados;
        }

        public IEnumerable<Livro> GetAll()
        {
            var livros = _db.Livro.AsNoTracking().ToList();
            return livros;
        }

        public Livro GetById(Guid id)
        {

            var livro = _db.Livro.Find(id);
            return livro;
        }

        public IEnumerable<Livro> GetLivrosById(Guid id)
        {
            IEnumerable<Livro> Livros = _db.Livro.Where(x => x.AutorId == id).ToList();
            return Livros;
        }

        public void Remove(Guid id)
        {
            var livro = _db.Livro.Find(id);
            if (livro != null)
            {
                _db.Livro.Remove(livro);
                _db.SaveChanges();
            }
        }

        public void Save(Livro livro)
        {
            _db.Livro.Add(livro);
            _db.SaveChanges();
        }

        public void Update(Livro livro)
        {
            _db.Livro.Update(livro);
            _db.SaveChanges();
        }
    }
}
