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
            ETipoEspecie especie,
            byte[] anexo,
            string tipoAnexo
           )
        {

            var pet = new Pet();
            pet.Nome = nome;
            pet.DataNascimento = dataDeNascimento;
            pet.Especie = especie;
            pet.AddTutor(idTutor);
            pet.TipoAnexo = tipoAnexo;
            pet.Anexo = anexo;
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
            if (PetUpdate.Agendamentos.Count() > 0)
                pet.Agendamentos = PetUpdate.Agendamentos;
            if (PetUpdate.Prontuarios.Count() > 0)
                pet.Prontuarios = PetUpdate.Prontuarios;
            if (PetUpdate.TipoAnexo != pet.TipoAnexo)
                pet.TipoAnexo = PetUpdate.TipoAnexo;
            if (PetUpdate.Anexo != pet.Anexo)
                pet.Anexo = PetUpdate.Anexo;

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
