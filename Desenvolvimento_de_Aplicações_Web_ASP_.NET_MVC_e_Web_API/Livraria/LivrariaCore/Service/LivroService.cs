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
    public class LivroService
    {
        private BancoDeDados db;

        public LivroService(BancoDeDados db)
        {
            this.db = db;
        }

        public List<Livro> GetAll()
        {
            var list = new List<Livro>();
            list.AddRange(db.Livro.ToList());

            return list;
        }

        public Livro GetById(Guid id)
        {
            var Livro = db.Livro.Find(id);
            return Livro;
        }

        public Livro GetByNome(string pTitulo)
        {
            if (IsNullOrWhiteSpace(pTitulo))
            {
                return db.Livro.Find(pTitulo);
            }

            return db.Livro.Where(x => x.Titulo == pTitulo).FirstOrDefault();

        }

        public Livro UpdateLivro(Guid id, Livro update)
        {

            var livro = db.Livro.Find(id);
            livro.Titulo = update.Titulo;
            livro.Descricao = update.Descricao;
            livro.UpdatedDt = DateTime.UtcNow;
            db.Livro.Update(livro);
            db.SaveChanges();
            return livro;
        }

        public void DeleteLivro(Guid id)
        {
            var Livro = db.Livro.Find(id);
            db.Livro.Remove(Livro);
            db.SaveChanges();
        }

        public Livro CreateLivro(Livro create)
        {
            var livro = new Livro();
            livro = create;
            livro.Id = NewGuid();
            livro.AutorId = create.AutorId;
            db.Livro.Add(livro);
            db.SaveChanges();
            return livro;
        }
    }
}
