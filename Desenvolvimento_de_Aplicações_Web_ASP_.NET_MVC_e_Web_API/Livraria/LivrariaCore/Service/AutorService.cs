using LivrariaCore.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;
using static System.Guid;

namespace LivrariaCore.Service
{
    public class AutorService
    {
        private BancoDeDados db;

        public AutorService(BancoDeDados db)
        {
            this.db = db;
        }

        public List<Autor> GetAll()
        {
            var list = new List<Autor>();
            list.AddRange(db.Autor.ToList());

            return list;
        }
        public Autor GetById(Guid id)
        {
            var Autor = db.Autor.Find(id);
            return Autor;
        }

        public Autor GetByNome(string autor)
        {
            if (IsNullOrWhiteSpace(autor))
            {
                return db.Autor.Find(autor);
            }

            return db.Autor.Where(x => x.Nome == autor).FirstOrDefault();

        }

        public Autor CreateAutor(Autor create)
        {

            var Autor = new Autor();
            Autor = create;
            Autor.Id = NewGuid();
            db.Autor.Add(Autor);
            db.SaveChanges();

            return Autor;
        }

        public Autor UpdateAutor(Guid id, Autor update)
        {

            var Autor = db.Autor.Find(id);
            Autor.Nome = update.Nome;
            Autor.UpdatedAt = DateTime.UtcNow;

            return Autor;
        }

        public List<Livro> GetLivros(Guid AutorId, Guid LivroId)
        {
            var list = new List<Livro>();

            if (LivroId != default)
            {
                var Livro = db.Livro.Find(LivroId);
                list.Add(Livro);
            }
            else
            {
                var Livros = db.Livro.Where(x => x.AutorId == AutorId).ToList();
                list.AddRange(Livros);
            }


            return list;
        }

        public Livro CreateLivro(Guid id, Livro create)
        {
            var Livro = new Livro();
            Livro = create;
            Livro.Id = NewGuid();
            Livro.AutorId = id;
            db.Livro.Add(Livro);
            db.SaveChanges();
            return Livro;
        }

        public void DeleteAutor(Guid id)
        {
            var Autor = db.Autor.Find(id);
            db.Autor.Remove(Autor);
            db.SaveChanges();
        }

    }
}
