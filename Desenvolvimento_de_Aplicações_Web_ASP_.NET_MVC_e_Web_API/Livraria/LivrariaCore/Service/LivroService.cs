using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;
using static System.Guid;
using LivrariaCore.Repositorio;

namespace LivrariaCore.Service
{
    public class LivroService
    {
        public IRepositorioLivro RepositorioLivro { get; }

        public LivroService(IRepositorioLivro repositorioLivro)
        {
            RepositorioLivro = repositorioLivro;
        }


        public IEnumerable<Livro> GetAll()
        {
            var livros = RepositorioLivro.GetAll();
            return livros;
        }

        public Livro GetById(Guid id)
        {
            var Livro = RepositorioLivro.GetById(id);
            return Livro;
        }

        //public Livro GetByNome(string pTitulo)
        //{
        //    if (IsNullOrWhiteSpace(pTitulo))
        //    {
        //        return db.Livro.Find(pTitulo);
        //    }

        //    return db.Livro.Where(x => x.Titulo == pTitulo).FirstOrDefault();

        //}

        public Livro UpdateLivro(Guid id, string Titulo, string Descricao, string ISBN)
        {

            Livro livro = RepositorioLivro.GetById(id);
            if (Titulo != null)
                livro.Titulo = Titulo;
            if (Descricao != null)
                livro.Descricao = Descricao;
            if (ISBN != null)
                livro.ISBN = ISBN;
            livro.UpdatedDt = DateTime.UtcNow;
            RepositorioLivro.Update(livro);
            return livro;
        }

        public void DeleteLivro(Guid id)
        {
            RepositorioLivro.Remove(id);
        }

        public Livro CreateLivro(Guid AutorId, string Titulo, string Descricao, string ISBN)
        {
            Livro livro = new Livro();
            livro.Id = NewGuid();
            livro.AutorId = AutorId;
            livro.Titulo = Titulo;
            livro.Descricao = Descricao;
            livro.ISBN = ISBN;
            livro.CreateDt = DateTime.UtcNow;
            livro.UpdatedDt = new DateTime();
            return livro;
        }
    }
}
