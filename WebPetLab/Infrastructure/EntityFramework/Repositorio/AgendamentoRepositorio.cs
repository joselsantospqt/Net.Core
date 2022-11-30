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
    public class AgendamentoRepositorio : IAgendamentoRepositorio
    {
        private BancoDeDados _db { get; }

        public AgendamentoRepositorio(BancoDeDados bancoDeDados)
        {
            _db = bancoDeDados;
        }

        public IEnumerable<Agendamento> GetAll()
        {
            return _db.Agendamento.Include(x => x.Pet).Include(x => x.MedicoResponsavel).AsNoTracking().ToList();
        }

        public Agendamento GetById(Guid id)
        {
            return _db.Agendamento.Include(x => x.Pet).Include(x => x.MedicoResponsavel).Where(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var Agendamento = _db.Agendamento.Find(id);
            if (Agendamento != null)
            {
                _db.Agendamento.Remove(Agendamento);
                _db.SaveChanges();
            }
        }
        public void SaveUpdate(Agendamento agendamento)
        {
            if (agendamento.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                _db.Add(agendamento);
            else
                _db.Update(agendamento);

            _db.SaveChanges();

        }

        public IEnumerable<Agendamento> GetAllByIdMedico(Guid id)
        {
            return _db.Agendamento.Include(x => x.Pet).Include(x => x.MedicoResponsavel).Where(x => x.MedicoResponsavel.UsuarioId == id).ToList();
        }
    }
}
