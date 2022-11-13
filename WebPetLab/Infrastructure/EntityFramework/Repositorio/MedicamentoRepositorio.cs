using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entidade;

namespace Infrastructure.EntityFramework.Repositorio
{
    public class MedicamentoRepositorio: IMedicamentoRepositorio
    {
        private BancoDeDados _db { get; }

        public MedicamentoRepositorio(BancoDeDados bancoDeDados)
        {
            _db = bancoDeDados;
        }

        public IEnumerable<Medicamento> GetAll()
        {
            return _db.Medicamento.Include(x => x.Prontuario).AsNoTracking().ToList();
        }

        public Medicamento GetById(Guid id)
        {
            return _db.Medicamento.Include(x => x.Prontuario).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var Medicamento = _db.Medicamento.Find(id);
            if (Medicamento != null)
            {
                _db.Medicamento.Remove(Medicamento);
                _db.SaveChanges();
            }
        }
        public void SaveUpdate(Medicamento medicamento)
        {
            if (medicamento.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                _db.Add(medicamento);
            else
                _db.Update(medicamento);

            _db.SaveChanges();

        }
    }
}
