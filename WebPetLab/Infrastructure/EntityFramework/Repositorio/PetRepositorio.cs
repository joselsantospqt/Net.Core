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
    public class PetRepositorio : IPetRepositorio
    {
        private BancoDeDados _db { get; }

        public PetRepositorio(BancoDeDados bancoDeDados)
        {
            _db = bancoDeDados;
        }

        public IEnumerable<Pet> GetAll()
        {
            return _db.Pet.Include(x => x.Tutor).Include(x => x.Agendamentos).Include(x => x.Prontuarios).Include(x => x.Documentos).AsNoTracking().ToList();
        }

        public Pet GetById(Guid id)
        {
            return _db.Pet.Include(x => x.Tutor).Include(x => x.Agendamentos).Include(x => x.Prontuarios).Include(x => x.Documentos).Where(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var Pet = _db.Pet.Find(id);
            if (Pet != null)
            {
                _db.Pet.Remove(Pet);
                _db.SaveChanges();
            }
        }
        public void SaveUpdate(Pet pet)
        {
            if (pet.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                _db.Add(pet);
            else
                _db.Update(pet);

            _db.SaveChanges();

        }
    }
}
