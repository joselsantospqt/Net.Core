
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
        public IRepositorioAutor RepositorioAutor { get; }

        public AutorService(IRepositorioAutor repositorioAutor)
        {
            RepositorioAutor = repositorioAutor;
        }

        public IEnumerable<Autor> GetAll()
        {
            var Autores = RepositorioAutor.GetAll();

            return Autores;
        }
        public Autor GetById(Guid id)
        {
            var Autor = RepositorioAutor.GetById(id);
            return Autor;
        }

        //public Autor GetByNome(string autor)
        //{
        //    if (IsNullOrWhiteSpace(autor))
        //    {
        //        return db.Autor.Find(autor);
        //    }

        //    return db.Autor.Where(x => x.Nome == autor).FirstOrDefault();

        //}

        public Autor CreateAutor(string Nome, string Sobrenome, DateTime Datanascimento, String Email, String Senha)
        {

            Autor novoAutor = new Autor();
            novoAutor.Id = NewGuid();
            novoAutor.Nome = Nome;
            novoAutor.Sobrenome = Sobrenome;
            novoAutor.Datanascimento = Datanascimento;
            novoAutor.Email = Email;
            novoAutor.Senha = Senha;
            novoAutor.UpdatedAt = new DateTime();

            RepositorioAutor.Save(novoAutor);

            return novoAutor;
        }

        public Autor UpdateAutor(Guid id, string Nome, string Sobrenome, String Email, String Senha)
        {

            Autor Autor = RepositorioAutor.GetById(id);
            if (Nome != null)
                Autor.Nome = Nome;
            if (Sobrenome != null)
                Autor.Sobrenome = Sobrenome;
            if (Email != null)
                Autor.Email = Email;
            if (Senha != null)
                Autor.Senha = Senha;

            Autor.UpdatedAt = DateTime.UtcNow;
            RepositorioAutor.Update(Autor);
            return Autor;
        }

        public void DeleteAutor(Guid id)
        {
            RepositorioAutor.Remove(id);
        }

    }
}
