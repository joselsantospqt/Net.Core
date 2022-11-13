using Domain.Entidade;
using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositorio
{
    public class ExameRepositorio : IExameRepositorio
    {
        private BancoDeDados _db { get; }

        public ExameRepositorio(BancoDeDados bancoDeDados)
        {
            _db = bancoDeDados;
        }

        public IEnumerable<Exame> GetAll()
        {
            return _db.Exame.Include(x => x.Prontuario).AsNoTracking().ToList();
        }

        public Exame GetById(Guid id)
        {
            return _db.Exame.Include(x => x.Prontuario).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var Exame = _db.Exame.Find(id);
            if (Exame != null)
            {
                _db.Exame.Remove(Exame);
                _db.SaveChanges();
            }
        }
        public void SaveUpdate(Exame exame)
        {
            if (exame.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                _db.Add(exame);
            else
                _db.Update(exame);

            _db.SaveChanges();

        }
    }
}
