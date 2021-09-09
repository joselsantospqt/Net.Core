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
        private ILivroRepositorio _RepositorioLivro { get; }

        public LivroService(ILivroRepositorio repositorioLivro)
        {
            _RepositorioLivro = repositorioLivro;
        }


        public IEnumerable<Livro> GetAll()
        {
            return _RepositorioLivro.GetAll();
        }

        public Livro GetById(Guid pId)
        {
            return _RepositorioLivro.GetById(pId);

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
            livro.AdicionarAutor(pAutorId);
            livro.UpdatedDt = new DateTime();
            _RepositorioLivro.SaveUpdate(livro);
            return livro;
        }
    }
}
