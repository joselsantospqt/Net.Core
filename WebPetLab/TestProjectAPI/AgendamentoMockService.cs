using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectAPI
{
    public class AgendamentoMockService
    {
        private string[] Validar(Agendamento obj)
        {
            var erros = new List<string>();

            if (obj.MedicoResponsavel.UsuarioId.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                erros.Add("Agendamento não pode ser cadastrado sem Médico");

            if (obj.Pet.PetId.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                erros.Add("Agendamento não pode ser cadastrado sem Pet");

            if (obj.Data == new DateTime() || obj.Data < new DateTime(1900, 1, 1))
                erros.Add("Agendamento não pode ser cadastrado sem Data");

            int n;
            var status = obj.Status.GetDescription();

            var result = Int32.TryParse(status, out n);
            if (result)
            {
                erros.Add("Agendamento não pode ser cadastrado sem um Status");
            }

            if (obj.Status == EStatus.Recusado && string.IsNullOrEmpty(obj.Comentario))
                erros.Add("Agendamento não pode ser recusado sem Comentário");

            if (erros.Count() > 0)
                return erros.ToArray();

            return Array.Empty<string>();
        }

        public (Agendamento, string[] mensagemDeErro) CreateAgendamento(
           Guid idMedico,
           Guid idPet,
           DateTime data
          )
        {

            var agendamento = new Agendamento();
            agendamento.Data = data;
            agendamento.Pet = new AgendamentoPet() { Id = 0, PetId = idPet, AgendamentoId = agendamento.Id };
            agendamento.MedicoResponsavel = new AgendamentoUsuario() { Id = 0, UsuarioId = idMedico, AgendamentoId = agendamento.Id };
            agendamento.Status = EStatus.Pendente;
            agendamento.Comentario = null;

            var erros = Validar(agendamento);

            if (erros.Count() > 0)
                return (null, erros);

            return (agendamento, Array.Empty<string>());
        }

        public (Agendamento, string[] mensagemDeErro) UpdateAgendamento(Agendamento _BancoDeDados, Agendamento AgendamentoUpdate)
        {

            var erros = Validar(AgendamentoUpdate);
            if (erros.Count() > 0)
                return (null, erros);

            var agendamento = _BancoDeDados;

            if (AgendamentoUpdate.Data != agendamento.Data)
                agendamento.Data = AgendamentoUpdate.Data;
            if (AgendamentoUpdate.Status != agendamento.Status)
                agendamento.Status = AgendamentoUpdate.Status;
            if (AgendamentoUpdate.Comentario != agendamento.Comentario)
                agendamento.Comentario = AgendamentoUpdate.Comentario;
            if (AgendamentoUpdate.Pet.PetId != agendamento.Pet.PetId)
                agendamento.Pet.PetId = AgendamentoUpdate.Pet.PetId;

            return (agendamento, Array.Empty<string>());
        }
    }
}
