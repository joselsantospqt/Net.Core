using Domain.Entidade;
using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class ProntuarioService
    {
        private IProntuarioRepositorio RepositorioProntuario { get; }

        public ProntuarioService(IProntuarioRepositorio repositorioProntuario)
        {
            RepositorioProntuario = repositorioProntuario;
        }

        public Prontuario GetProntuarioById(Guid id)
        {
            return RepositorioProntuario.GetById(id);
        }

        public IEnumerable<Prontuario> GetAll()
        {
            return RepositorioProntuario.GetAll();
        }

        public Prontuario CreateProntuario(
            Guid idMedico,
            Guid idPet,
            string resumo,
            string descricao
           )
        {
            var prontuario = new Prontuario();
            prontuario.Resumo = resumo;
            prontuario.Descricao = descricao;
            prontuario.Data = DateTime.UtcNow;
            prontuario.AddPet(idPet);
            prontuario.AddMedicoResponsavel(idMedico);

            RepositorioProntuario.SaveUpdate(prontuario);

            return prontuario;
        }

        public Prontuario UpdateProntuario(Prontuario ProntuarioUpdate)
        {

            var prontuario = RepositorioProntuario.GetById(ProntuarioUpdate.Id);
            if (ProntuarioUpdate.Resumo.Length > 0)
                prontuario.Resumo = ProntuarioUpdate.Resumo;
            if (ProntuarioUpdate.Descricao.Length > 0)
                prontuario.Descricao = ProntuarioUpdate.Descricao;
            if (ProntuarioUpdate.Data != prontuario.Data)
                prontuario.Data = ProntuarioUpdate.Data;
            if (ProntuarioUpdate.Documentos.Count > 0)
                prontuario.Documentos = ProntuarioUpdate.Documentos;
            if (ProntuarioUpdate.Pet.PetId != prontuario.Pet.PetId)
                prontuario.Pet.PetId = ProntuarioUpdate.Pet.PetId;
        

            RepositorioProntuario.SaveUpdate(prontuario);

            return prontuario;
        }

        public void DeleteProntuario(Guid id)
        {
            RepositorioProntuario.Remove(id);
        }
    }
}
