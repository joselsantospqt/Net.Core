using MyGroupFriends.Domain.Entidades;
using MyGroupFriends.Domain.IRepositorio;
using MyGroupFriends.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGroupFriends.Infrastructure.EntityFramework.Repositorio
{
    public class RepositorioAmigo : IRepositorioAmigo
    {
        public BancoDeDados _Db;
        public RepositorioAmigo(BancoDeDados bancoDeDados)
        {
            _Db = bancoDeDados;
        }

        public IEnumerable<Amigo> GetAll()
        {
            var retorno = _Db.Amigo.ToList();
            return retorno;
        }

        public Amigo GetById(Guid id)
        {
            var retorno = _Db.Amigo.Find(id);
            return retorno;
        }

        public void Remove(Guid id)
        {
            var retorno = _Db.Amigo.Find(id);
            if (retorno != null)
            {
                _Db.Amigo.Remove(retorno);
                _Db.SaveChanges();
            }

        }

        public void SaveUpdate(Amigo amigo)
        {
            if (amigo.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")) || _Db.Amigo.Find(amigo.Id) == null)
                _Db.Amigo.Add(amigo);
            else
                _Db.Amigo.Update(amigo);

            _Db.SaveChanges();
        }

        public Amigo GetByNome(string nome)
        {
            return _Db.Amigo.Where(x => x.Nome == nome).FirstOrDefault();
        }

      
    }
}
