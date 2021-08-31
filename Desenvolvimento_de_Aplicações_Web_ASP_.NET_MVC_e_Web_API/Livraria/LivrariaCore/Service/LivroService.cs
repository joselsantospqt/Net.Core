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

        public Livro GetById(Guid pId)
        {
            var Livro = RepositorioLivro.GetById(pId);
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

        public Livro Update(Guid pId, string pTitulo, string pDescricao, string pISBN)
        {

            Livro livro = RepositorioLivro.GetById(pId);
            if (pTitulo != null)
                livro.Titulo = pTitulo;
            if (pDescricao != null)
                livro.Descricao = pDescricao;
            if (pISBN != null)
                livro.ISBN = pISBN;
            livro.UpdatedDt = DateTime.UtcNow;
            RepositorioLivro.Update(livro);
            return livro;
        }

        public void Delete(Guid id)
        {
            RepositorioLivro.Remove(id);
        }

        public Livro Create(Guid pAutorId, string pTitulo, string pDescricao, string pISBN)
        {
            Livro livro = new Livro();
            livro.Id = NewGuid();
            livro.AutorId = pAutorId;
            livro.Titulo = pTitulo;
            livro.Descricao = pDescricao;
            livro.ISBN = pISBN;
            livro.CreateDt = DateTime.UtcNow;
            livro.UpdatedDt = new DateTime();
            RepositorioLivro.Save(livro);
            return livro;
        }
    }
}
