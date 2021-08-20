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

            var autor = new Autor();
            autor = create;
            autor.Id = NewGuid();
            db.Autor.Add(autor);
            db.SaveChanges();

            return autor;
        }

        public Autor UpdateAutor(Guid id, Autor update)
        {

            var Autor = db.Autor.Find(id);
            Autor.Nome = update.Nome;
            Autor.Sobrenome = update.Sobrenome;
            Autor.Email = update.Email;
            Autor.Senha = update.Senha;
            Autor.UpdatedAt = DateTime.UtcNow;

            return Autor;
        }

        public void DeleteAutor(Guid id)
        {
            var Autor = db.Autor.Find(id);
            db.Autor.Remove(Autor);
            db.SaveChanges();
        }

    }
}
