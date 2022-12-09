using Domain.Entidade;
using Domain.Entidade.Request;
using Domain.Repositorio;
using Domain.Service;
using Infrastructure.EntityFramework.Repositorio;
using Moq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProjectAPI
{
    public class UnitTestPet
    {
        PetMockService _service;

        public UnitTestPet()
        {
            _service = new PetMockService();
        }

        [Fact(DisplayName = "Cadastrar Pet com Sucesso")]
        public void Cadastrar_Pet_Sucesso()
        {
            var idTutor = new Guid("{12345678-0000-0000-1234-000000000000}");
            var pet = new Pet();
            pet.Nome = "Jordan";
            pet.DataNascimento = DateTime.UtcNow;
            pet.Especie = ETipoEspecie.Invertebrados;
            var (PetCriado, _) = _service.CreatePet(idTutor, pet.Nome, pet.DataNascimento, pet.Especie);


            Assert.Equal(pet.Id, PetCriado.Id);
            Assert.Equal(pet.Id, PetCriado.Tutor.PetId);
            Assert.True(pet.Nome == PetCriado.Nome);
            Assert.True(pet.DataNascimento == PetCriado.DataNascimento);
            Assert.True(pet.Especie == PetCriado.Especie);
            Assert.True(idTutor == PetCriado.Tutor.UsuarioId);
        }

        [Fact(DisplayName = "Cadastrar Pet sem Tutor")]
        public void Cadastrar_sem_Tutor()
        {
            var idTutor = new Guid();
            var pet = new Pet();
            pet.Nome = "Jordan";
            pet.DataNascimento = DateTime.UtcNow;
            pet.Especie = ETipoEspecie.Invertebrados;
            var (_, listaMensagemDeErro) = _service.CreatePet(idTutor, pet.Nome, pet.DataNascimento, pet.Especie);


            Assert.Equal("Pet não pode ser cadastrado sem Tutor", listaMensagemDeErro[0]);
        }

        [Theory(DisplayName = "Cadastrar sem Nome, Idade")]
        [InlineData("")]
        [InlineData(null)]
        public void Cadastrar_sem_Nome_Idade(string word)
        {
            var idTutor = new Guid("{12345678-0000-0000-1234-000000000000}");
            var pet = new Pet();
            pet.Nome = word;
            pet.DataNascimento = new DateTime();
            pet.Especie = ETipoEspecie.Invertebrados;

            var (_, listaMensagemDeErro) = _service.CreatePet(idTutor, pet.Nome, pet.DataNascimento, pet.Especie);

            Assert.Equal(2, listaMensagemDeErro.Length);
            Assert.Equal("Pet não pode ser cadastrado sem Nome", listaMensagemDeErro[0]);
            Assert.Equal("Pet não pode ser cadastrado sem Data Nascimento", listaMensagemDeErro[1]);
        }

        [Fact(DisplayName = "Cadastrar sem Especie")]
        public void Cadastrar_sem_Especie()
        {
            var idTutor = new Guid("{12345678-0000-0000-1234-000000000000}");
            var pet = new Pet();
            pet.Nome = "Jordan";
            pet.DataNascimento = DateTime.UtcNow;

            var (_, listaMensagemDeErro) = _service.CreatePet(idTutor, pet.Nome, pet.DataNascimento, pet.Especie);

            Assert.Equal("Pet não pode ser cadastrado sem uma Especie", listaMensagemDeErro[0]);
        }

        [Theory(DisplayName = "Atualizar Nome, Data Nascimento para em Branco")]
        [InlineData("")]
        [InlineData(null)]
        public void Atualizar_sem_Nome_Idade(string word)
        {
            var idTutor = new Guid("{12345678-0000-0000-1234-000000000000}");
            var pet = new Pet();
            pet.Nome = "Jordan";
            pet.DataNascimento = DateTime.UtcNow;
            pet.Especie = ETipoEspecie.Invertebrados;
            var (PetCriado, _) = _service.CreatePet(idTutor, pet.Nome, pet.DataNascimento, pet.Especie);

            pet = PetCriado;
            pet.Nome = word;
            pet.DataNascimento = new DateTime();

            var (_, listaMensagemDeErro) = _service.UpdatePet(PetCriado, pet);

            Assert.Equal(2, listaMensagemDeErro.Length);
            Assert.Equal("Pet não pode ser cadastrado sem Nome", listaMensagemDeErro[0]);
            Assert.Equal("Pet não pode ser cadastrado sem Data Nascimento", listaMensagemDeErro[1]);
        }


    }
}
