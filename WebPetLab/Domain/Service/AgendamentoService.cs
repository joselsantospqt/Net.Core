﻿using Domain.Entidade;
using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class AgendamentoService
    {
        private IAgendamentoRepositorio RepositorioAgendamento { get; }

        public AgendamentoService(IAgendamentoRepositorio repositorioAgendamento)
        {
            RepositorioAgendamento = repositorioAgendamento;
        }

        public Agendamento GetAgendamento(Guid id)
        {
            return RepositorioAgendamento.GetById(id);
        }

        public IEnumerable<Agendamento> GetAll()
        {
            return RepositorioAgendamento.GetAll();
        }

        public Agendamento CreateAgendamento(
            Guid idMedico,
            Guid idPet
           )
        {

            var agendamento = new Agendamento();
            agendamento.Data = DateTime.UtcNow;
            agendamento.AddPet(idPet);
            agendamento.AddMedicoResponsavel(idMedico);
            agendamento.Status = EStatus.Pendente;
            agendamento.Comentario = null;
            RepositorioAgendamento.SaveUpdate(agendamento);

            return agendamento;
        }

        public Agendamento UpdateAgendamento(Agendamento AgendamentoUpdate)
        {

            var agendamento = RepositorioAgendamento.GetById(AgendamentoUpdate.Id);
        
            if (AgendamentoUpdate.Data != agendamento.Data)
                agendamento.Data = AgendamentoUpdate.Data;
            if (AgendamentoUpdate.Status != agendamento.Status)
                agendamento.Status = AgendamentoUpdate.Status;
            if (AgendamentoUpdate.Comentario.Length > 0)
                agendamento.Comentario = AgendamentoUpdate.Comentario;

            RepositorioAgendamento.SaveUpdate(agendamento);

            return agendamento;
        }


        public void DeleteAgendamento(Guid id)
        {
            RepositorioAgendamento.Remove(id);
        }
    }
}