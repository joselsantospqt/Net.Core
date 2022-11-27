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
    public class ProntuarioRepositorio : IProntuarioRepositorio
    {
        private BancoDeDados _db { get; }

        public ProntuarioRepositorio(BancoDeDados bancoDeDados)
        {
            _db = bancoDeDados;
        }

        public IEnumerable<Prontuario> GetAll()
        {
            return _db.Prontuario.Include(x => x.Pet).Include(x => x.Medico).Include(x => x.Documentos).AsNoTracking().ToList();
        }

        public Prontuario GetById(Guid id)
        {
            return _db.Prontuario.Include(x => x.Pet).Include(x => x.Medico).Include(x => x.Documentos).Where(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var Prontuario = _db.Prontuario.Find(id);
            if (Prontuario != null)
            {
                _db.Prontuario.Remove(Prontuario);
                _db.SaveChanges();
            }
        }
        public void SaveUpdate(Prontuario prontuario)
        {
            if (prontuario.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                _db.Add(prontuario);
            else
                _db.Update(prontuario);

            _db.SaveChanges();

        }
    }
}
