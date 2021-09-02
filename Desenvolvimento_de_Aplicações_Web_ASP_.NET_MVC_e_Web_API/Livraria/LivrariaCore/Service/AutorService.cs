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
    public class AutorService
    {
        public IRepositorioAutor _RepositorioAutor { get; }

        private IRepositorioLivro _RepositorioLivro { get; }

        public AutorService(IRepositorioAutor repositorioAutor, IRepositorioLivro repositorioLivro)
        {
            _RepositorioAutor = repositorioAutor;
            _RepositorioLivro = repositorioLivro;
        }

        public IEnumerable<Autor> GetAll()
        {
            var Autores = _RepositorioAutor.GetAll();

            return Autores;
        }
        public Autor GetById(Guid pId)
        {
            return _RepositorioAutor.GetById(pId);
        }

        //public Autor GetByNome(string autor)
        //{
        //    if (IsNullOrWhiteSpace(autor))
        //    {
        //        return db.Autor.Find(autor);
        //    }

        //    return db.Autor.Where(x => x.Nome == autor).FirstOrDefault();

        //}

        public Autor Create(string pNome, string pSobrenome, DateTime pDatanascimento, String pEmail, String pSenha)
        {

            Autor novoAutor = new Autor();
            novoAutor.Nome = pNome;
            novoAutor.Sobrenome = pSobrenome;
            novoAutor.Datanascimento = pDatanascimento;
            novoAutor.Email = pEmail;
            novoAutor.Senha = pSenha;
            novoAutor.UpdatedAt = new DateTime();

            _RepositorioAutor.SaveUpdate(novoAutor);

            return novoAutor;
        }

        public Autor Update(Guid pId, string pNome, string pSobrenome, String pEmail, String pSenha)
        {

            Autor Autor = _RepositorioAutor.GetById(pId);
            if (pNome != null)
                Autor.Nome = pNome;
            if (pSobrenome != null)
                Autor.Sobrenome = pSobrenome;
            if (pEmail != null)
                Autor.Email = pEmail;
            if (pSenha != null)
                Autor.Senha = pSenha;

            Autor.UpdatedAt = DateTime.UtcNow;
            _RepositorioAutor.SaveUpdate(Autor);
            return Autor;
        }

        public void Delete(Guid pId)
        {
            _RepositorioAutor.Remove(pId);
        }

    }
}
