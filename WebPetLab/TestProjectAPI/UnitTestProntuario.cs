using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProjectAPI
{
    public class UnitTestProntuario
    {
        ProntuarioMockService _service;

        public UnitTestProntuario()
        {
            _service = new ProntuarioMockService();
        }

        [Fact(DisplayName = "Cadastrar Prontuario com Sucesso")]
        public void Cadastrar()
        {
            var idPet = new Guid("{12345678-0000-0000-1234-000000000000}");
            var idMedico = new Guid("{87654321-0000-0000-1234-000000000000}");
            var prontuario = new Prontuario();
            prontuario.Resumo = "Consulta 01";
            prontuario.Descricao = "Emergencia";

            var (prontuarioCriado, _) = _service.CreateProntuario(idMedico, idPet, prontuario.Resumo, prontuario.Descricao);


            Assert.Equal(prontuario.Id, prontuarioCriado.Id);
            Assert.Equal(prontuario.Id, prontuarioCriado.Medico.ProntuarioId);
            Assert.Equal(prontuario.Id, prontuarioCriado.Pet.ProntuarioId);
            Assert.Equal(idMedico, prontuarioCriado.Medico.UsuarioId);
            Assert.Equal(idPet, prontuarioCriado.Pet.PetId);
            Assert.Equal(prontuario.Resumo, prontuarioCriado.Resumo);
            Assert.Equal(prontuario.Descricao, prontuarioCriado.Descricao);

        }

        [Theory(DisplayName = "Cadastrar Prontuario sem Resumo, Descrição")]
        [InlineData("")]
        [InlineData(null)]
        public void Cadastrar_sem_Resumo_Descricao(string word)
        {
            var idPet = new Guid("{12345678-0000-0000-1234-000000000000}");
            var idMedico = new Guid("{87654321-0000-0000-1234-000000000000}");
            var prontuario = new Prontuario();
            prontuario.Resumo = word;
            prontuario.Descricao = word;

            var (_, listaMensagemDeErro) = _service.CreateProntuario(idMedico, idPet, prontuario.Resumo, prontuario.Descricao);

            Assert.Equal(2, listaMensagemDeErro.Length);
            Assert.Equal("Prontuario não pode ser cadastrado sem Resumo", listaMensagemDeErro[0]);
            Assert.Equal("Prontuario não pode ser cadastrado sem Descrição", listaMensagemDeErro[1]);
        }

        [Fact(DisplayName = "Cadastrar Prontuario sem Pet, Medico")]
        public void Cadastrar_sem_Pet_Medico()
        {
            var idPet = new Guid();
            var idMedico = new Guid();
            var prontuario = new Prontuario();
            prontuario.Resumo = "Consulta 01";
            prontuario.Descricao = "Emergencia";

            var (_, listaMensagemDeErro) = _service.CreateProntuario(idMedico, idPet, prontuario.Resumo, prontuario.Descricao);

            Assert.Equal(2, listaMensagemDeErro.Length);
            Assert.Equal("Prontuario não pode ser cadastrado sem Médico", listaMensagemDeErro[0]);
            Assert.Equal("Prontuario não pode ser cadastrado sem Pet", listaMensagemDeErro[1]);
        }

        [Theory(DisplayName = "Atualizar Nome, Data Nascimento para em Branco")]
        [InlineData("")]
        [InlineData(null)]
        public void Atualizar_sem_Resumo_Idade_Pet_Medico(string word)
        {
            var idPet = new Guid("{12345678-0000-0000-1234-000000000000}");
            var idMedico = new Guid("{87654321-0000-0000-1234-000000000000}");
            var prontuario = new Prontuario();
            prontuario.Resumo = "Consulta 01";
            prontuario.Descricao = "Emergencia";

            var (prontuarioCriado, _) = _service.CreateProntuario(idMedico, idPet, prontuario.Resumo, prontuario.Descricao);

            prontuario = prontuarioCriado;
            prontuario.Medico.UsuarioId = new Guid();
            prontuario.Pet.PetId = new Guid();
            prontuario.Descricao = word;
            prontuario.Resumo = word;


            var (_, listaMensagemDeErro) = _service.UpdateProntuario(prontuarioCriado, prontuario);

            Assert.Equal(4, listaMensagemDeErro.Length);
            Assert.Equal("Prontuario não pode ser cadastrado sem Resumo", listaMensagemDeErro[0]);
            Assert.Equal("Prontuario não pode ser cadastrado sem Descrição", listaMensagemDeErro[1]);
            Assert.Equal("Prontuario não pode ser cadastrado sem Médico", listaMensagemDeErro[2]);
            Assert.Equal("Prontuario não pode ser cadastrado sem Pet", listaMensagemDeErro[3]);
        }

    }
}
