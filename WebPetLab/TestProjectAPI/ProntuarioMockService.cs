using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectAPI
{
    public class ProntuarioMockService
    {

        private string[] Validar(Prontuario obj)
        {
            var erros = new List<string>();

            if (string.IsNullOrEmpty(obj.Resumo))
                erros.Add("Prontuario não pode ser cadastrado sem Resumo");

            if (string.IsNullOrEmpty(obj.Descricao))
                erros.Add("Prontuario não pode ser cadastrado sem Descrição");

            if (obj.Medico.UsuarioId.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                erros.Add("Prontuario não pode ser cadastrado sem Médico");

            if (obj.Pet.PetId.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                erros.Add("Prontuario não pode ser cadastrado sem Pet");

            if (erros.Count() > 0)
                return erros.ToArray();

            return Array.Empty<string>();
        }

        public (Prontuario, string[] mensagemDeErro) CreateProntuario(
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
            prontuario.Pet = new ProntuarioPet() { Id = 0, PetId = idPet, ProntuarioId = prontuario.Id };
            prontuario.Medico = new ProntuarioUsuario() { Id = 0, UsuarioId = idMedico, ProntuarioId = prontuario.Id };

            var erros = Validar(prontuario);

            if (erros.Count() > 0)
                return (null, erros);

            return (prontuario, Array.Empty<string>());
        }

        public (Prontuario, string[] mensagemDeErro) UpdateProntuario(Prontuario _BancoDeDados, Prontuario ProntuarioUpdate)
        {
            var erros = Validar(ProntuarioUpdate);
            if (erros.Count() > 0)
                return (null, erros);

            var prontuario = _BancoDeDados;

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


            return (prontuario, Array.Empty<string>());
        }
    }
}
