using Domain.Entidade;
using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class PetService
    {
        private IPetRepositorio RepositorioPet { get; }

        public PetService(IPetRepositorio repositorioPet)
        {
            RepositorioPet = repositorioPet;
        }

        public Pet GetPetById(Guid id)
        {
            return RepositorioPet.GetById(id);
        }

        public IEnumerable<Pet> GetAll()
        {
            return RepositorioPet.GetAll();
        }

        public Pet CreatePet(
            Guid idTutor,
            string nome,
            DateTime dataDeNascimento,
            ETipoEspecie especie
           )
        {

            var pet = new Pet();
            pet.Nome = nome;
            pet.DataNascimento = dataDeNascimento;
            pet.Especie = especie;
            pet.AddTutor(idTutor);
            pet.ImagemUrlPet = "Perfil_default.png";
            pet.CreatedAt = DateTime.UtcNow;
            pet.UpdatedAt = new DateTime();

            RepositorioPet.SaveUpdate(pet);

            return pet;
        }

        public Pet UpdatePet(Pet PetUpdate)
        {

            var pet = RepositorioPet.GetById(PetUpdate.Id);
            if (PetUpdate.Nome != null)
                pet.Nome = PetUpdate.Nome;
            if (PetUpdate.DataNascimento != pet.DataNascimento)
                pet.DataNascimento = PetUpdate.DataNascimento;
            if (PetUpdate.Especie != pet.Especie)
                pet.Especie = PetUpdate.Especie;
            if (PetUpdate.ImagemUrlPet != null && PetUpdate.ImagemUrlPet.Length == 0)
                pet.ImagemUrlPet = PetUpdate.ImagemUrlPet;
            if (PetUpdate.Agendamentos.Count() > 0)
                pet.Agendamentos = PetUpdate.Agendamentos;
            if (PetUpdate.Prontuarios.Count() > 0)
                pet.Prontuarios = PetUpdate.Prontuarios;
            pet.UpdatedAt = DateTime.UtcNow;

            RepositorioPet.SaveUpdate(pet);

            return pet;
        }


        public void DeletePet(Guid id)
        {
            RepositorioPet.Remove(id);
        }
    }
}
