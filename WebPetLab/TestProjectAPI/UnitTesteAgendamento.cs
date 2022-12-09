using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProjectAPI
{
    public class UnitTesteAgendamento
    {
        AgendamentoMockService _service;

        public UnitTesteAgendamento()
        {
            _service = new AgendamentoMockService();
        }

        [Fact(DisplayName = "Cadastrar Agendamento com Sucesso")]
        public void Cadastrar()
        {
            var idPet = new Guid("{12345678-0000-0000-1234-000000000000}");
            var idMedico = new Guid("{87654321-0000-0000-1234-000000000000}");
            var agendamento = new Agendamento();
            agendamento.Data = DateTime.UtcNow;
            agendamento.Comentario = "teste";

            var (agendamentoCriado, _) = _service.CreateAgendamento(idMedico, idPet, agendamento.Data);

            Assert.Equal(agendamento.Id, agendamentoCriado.Id);
            Assert.Equal(agendamento.Id, agendamentoCriado.Pet.AgendamentoId);
            Assert.Equal(agendamento.Id, agendamentoCriado.MedicoResponsavel.AgendamentoId);
            Assert.Equal(agendamento.Data, agendamentoCriado.Data);
            Assert.Equal(idPet, agendamentoCriado.Pet.PetId);
            Assert.Equal(idMedico, agendamentoCriado.MedicoResponsavel.UsuarioId);


        }

        [Fact(DisplayName = "Cadastrar Agendamento sem Pet, Médico, Data")]

        public void Cadastrar_sem_Pet_Medico_Data()
        {
            var idPet = new Guid();
            var idMedico = new Guid();
            var agendamento = new Agendamento();
            agendamento.Data = new DateTime();

            var (_, listaMensagemDeErro) = _service.CreateAgendamento(idMedico, idPet, agendamento.Data);

            Assert.Equal(3, listaMensagemDeErro.Length);
            Assert.Equal("Agendamento não pode ser cadastrado sem Médico", listaMensagemDeErro[0]);
            Assert.Equal("Agendamento não pode ser cadastrado sem Pet", listaMensagemDeErro[1]);
            Assert.Equal("Agendamento não pode ser cadastrado sem Data", listaMensagemDeErro[2]);
        }

        [Fact(DisplayName = "Atualizar Sem Nome, Data Nascimento para em Branco")]
        public void Atualizar_sem_Data_Pet_Medico()
        {
            var idPet = new Guid("{12345678-0000-0000-1234-000000000000}");
            var idMedico = new Guid("{87654321-0000-0000-1234-000000000000}");
            var agendamento = new Agendamento();
            agendamento.Data = DateTime.UtcNow;
            agendamento.Comentario = "teste";

            var (agendamentoCriado, _) = _service.CreateAgendamento(idMedico, idPet, agendamento.Data);


            agendamento = agendamentoCriado;
            agendamento.MedicoResponsavel.UsuarioId = new Guid();
            agendamento.Pet.PetId = new Guid();
            agendamento.Data = new DateTime();

            var (_, listaMensagemDeErro) = _service.UpdateAgendamento(agendamentoCriado, agendamento);

            Assert.Equal(3, listaMensagemDeErro.Length);
            Assert.Equal("Agendamento não pode ser cadastrado sem Médico", listaMensagemDeErro[0]);
            Assert.Equal("Agendamento não pode ser cadastrado sem Pet", listaMensagemDeErro[1]);
            Assert.Equal("Agendamento não pode ser cadastrado sem Data", listaMensagemDeErro[2]);
        }

        [Theory(DisplayName = "Recusar agendamento sem por comentário")]
        [InlineData("")]
        [InlineData(null)]
        public void Atualizar_recusa_sem_comentario(string word)
        {
            var idPet = new Guid("{12345678-0000-0000-1234-000000000000}");
            var idMedico = new Guid("{87654321-0000-0000-1234-000000000000}");
            var agendamento = new Agendamento();
            agendamento.Data = DateTime.UtcNow;

            var (agendamentoCriado, _) = _service.CreateAgendamento(idMedico, idPet, agendamento.Data);

            agendamento = agendamentoCriado;
            agendamento.Status = EStatus.Recusado;
            agendamento.Comentario = word;

            var (_, listaMensagemDeErro) = _service.UpdateAgendamento(agendamentoCriado, agendamento);

            Assert.Equal("Agendamento não pode ser recusado sem Comentário", listaMensagemDeErro[0]);
        }
    }
}
