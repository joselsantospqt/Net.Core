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
        public IRepositorioLivro _RepositorioLivro { get; }

        private IRepositorioAutor _RepositorioAutor { get; }

        public LivroService(IRepositorioLivro repositorioLivro, IRepositorioAutor repositorioAutor)
        {
            _RepositorioLivro = repositorioLivro;
            _RepositorioAutor = repositorioAutor;
        }


        public IEnumerable<Livro> GetAll()
        {
            var livros = _RepositorioLivro.GetAll();
            return livros;
        }

        public Livro GetById(Guid pId)
        {
            var Livro = _RepositorioLivro.GetById(pId);
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

        public Livro Update(Guid pId, string pTitulo, string pDescricao, string pISBN, int pAno)
        {

            Livro livro = _RepositorioLivro.GetById(pId);
            if (pTitulo != null)
                livro.Titulo = pTitulo;
            if (pDescricao != null)
                livro.Descricao = pDescricao;
            if (pISBN != null)
                livro.ISBN = pISBN;
            if (pAno != 0)
                livro.Ano = pAno;
            livro.UpdatedDt = DateTime.UtcNow;
            _RepositorioLivro.SaveUpdate(livro);
            return livro;
        }

        public void Delete(Guid id)
        {
            _RepositorioLivro.Remove(id);
        }

        public Livro Create(List<Guid> pAutorId, string pTitulo, string pDescricao, string pISBN, int pAno)
        {
            Livro livro = new Livro();
            livro.Titulo = pTitulo;
            livro.Descricao = pDescricao;
            livro.ISBN = pISBN;
            livro.Ano = pAno;
            livro.UpdatedDt = new DateTime();
            livro.Autores = _RepositorioAutor.GetAll().Where(x => pAutorId.Contains(x.Id)).ToList();
            _RepositorioLivro.SaveUpdate(livro);
            return livro;
        }
    }
}
