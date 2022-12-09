using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectAPI
{
    public class PetMockService
    {
        private string[] Validar(Pet obj)
        {
            var erros = new List<string>();

            if (string.IsNullOrEmpty(obj.Nome))
                erros.Add("Pet não pode ser cadastrado sem Nome");

            if (obj.Tutor.UsuarioId.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                erros.Add("Pet não pode ser cadastrado sem Tutor");

            if (obj.DataNascimento == new DateTime() || obj.DataNascimento < new DateTime(1900, 1, 1))
                erros.Add("Pet não pode ser cadastrado sem Data Nascimento");

            if (obj.Especie == new ETipoEspecie())
                erros.Add("Pet não pode ser cadastrado sem uma Especie");
            else
            {

                int n;
                var especie = obj.Especie.GetDescription();

                var result = Int32.TryParse(especie, out n);
                if (result)
                {
                    erros.Add("Pet não pode ser cadastrado sem uma Especie");
                }
            }

            if (erros.Count() > 0)
                return erros.ToArray();

            return Array.Empty<string>();
        }

        public (Pet, string[] mensagemDeErro) CreatePet(
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
            pet.Tutor = new UsuarioPet() { Id = 0, PetId = pet.Id, UsuarioId = idTutor };
            pet.CreatedAt = DateTime.UtcNow;
            pet.UpdatedAt = new DateTime();

            var erros = Validar(pet);

            if (erros.Count() > 0)
                return (null, erros);

            return (pet, Array.Empty<string>());
        }

        public (Pet, string[] mensagemDeErro) UpdatePet(Pet _BancoDeDados, Pet PetUpdate)
        {
            var erros = Validar(PetUpdate);
            if (erros.Count() > 0)
                return (null, erros);

            var pet = _BancoDeDados;
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

            return (pet, Array.Empty<string>());
        }
    }
}
