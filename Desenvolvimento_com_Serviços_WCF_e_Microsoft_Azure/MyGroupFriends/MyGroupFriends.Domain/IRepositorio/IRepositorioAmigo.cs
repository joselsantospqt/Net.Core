using MyGroupFriends.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGroupFriends.Domain.IRepositorio
{
    public interface IRepositorioAmigo
    {
        void SaveUpdate(Amigo autor);
        Amigo GetById(Guid id);
        void Remove(Guid id);
        Amigo GetByNome(string nome);
        IEnumerable<Amigo> GetAll();
    }
}
