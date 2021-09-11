using MyGroupFriends.Domain.Entidades;
using MyGroupFriends.Domain.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGroupFriends.Domain.Services
{
    public class AmigoService
    {
        public IRepositorioAmigo _RepositorioAmigo { get; }

        public AmigoService(IRepositorioAmigo repositorioAmigo)
        {
            _RepositorioAmigo = repositorioAmigo;
        }

        public IEnumerable<Amigo> GetAll()
        {
            var retorno = _RepositorioAmigo.GetAll();

            return retorno;
        }
        public Amigo GetById(Guid pId)
        {
            return _RepositorioAmigo.GetById(pId);
        }

        public Amigo GetByNome(string autor)
        {
            return _RepositorioAmigo.GetByNome(autor);

        }

        public Amigo Create(string pNome, string pSobrenome, DateTime pDatanascimento, string pEmail, string pTelefone, string pUrlFoto)
        {

            Amigo novoAmigo = new Amigo();
            novoAmigo.Nome = pNome;
            novoAmigo.Sobrenome = pSobrenome;
            novoAmigo.DtAniversario = pDatanascimento;
            novoAmigo.Email = pEmail;
            novoAmigo.Telefone = pTelefone;
            novoAmigo.Urlfoto = pUrlFoto;

            _RepositorioAmigo.SaveUpdate(novoAmigo);

            return novoAmigo;
        }

        public Amigo Update(Guid pId, string pNome, string pSobrenome, DateTime pDatanascimento, string pEmail, string pTelefone, string pUrlFoto)
        {

            Amigo Amigo = _RepositorioAmigo.GetById(pId);
            if (pNome != null)
                Amigo.Nome = pNome;
            if (pSobrenome != null)
                Amigo.Sobrenome = pSobrenome;
            if (pEmail != null)
                Amigo.Email = pEmail;
            if (pTelefone != null)
                Amigo.Telefone = pTelefone;
            if (pUrlFoto != null)
                Amigo.Urlfoto = pUrlFoto;

            _RepositorioAmigo.SaveUpdate(Amigo);
            return Amigo;
        }

        public void Delete(Guid pId)
        {
            _RepositorioAmigo.Remove(pId);
        }

    }
}
