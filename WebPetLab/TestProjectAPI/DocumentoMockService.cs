using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectAPI
{
    public class DocumentoMockService
    {

        private string[] Validar(Documento obj)
        {
            var erros = new List<string>();

            if (obj.Pet.PetId.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                erros.Add("Documento não pode ser cadastrado sem Pet");

            if (string.IsNullOrEmpty(obj.Nome))
                erros.Add("Documento não pode ser cadastrado sem Nome");

            if (string.IsNullOrEmpty(obj.Descricao))
                erros.Add("Documento não pode ser cadastrado sem Descricao");

            if (obj.Quantidade == 0)
                erros.Add("Documento não pode ser cadastrado sem Quantidade");

            if (obj.DataInicio == new DateTime() || obj.DataInicio < new DateTime(1900, 1, 1))
                erros.Add("Documento não pode ser cadastrado sem Data de Inicio");

            if (obj.TipoDocumento == new ETipoDocumento())
                erros.Add("Documento não pode ser cadastrado sem um Tipo");
            else
            {
                int n;
                var tipoDocumento = obj.TipoDocumento.GetDescription();

                var result = Int32.TryParse(tipoDocumento, out n);
                if (result)
                {
                    erros.Add("Documento não pode ser cadastrado sem um Tipo");
                }
            }

            if (erros.Count() > 0)
                return erros.ToArray();

            return Array.Empty<string>();
        }

        public (Documento, string[] mensagemDeErro) CreateDocumento(
      Guid idProntuario,
      Guid idPet,
      string descricao,
      int quantidade,
      string nome,
      string tipoAnexo,
      byte[] anexo,
      ETipoDocumento tipoDocumento,
      DateTime dataInicio,
      DateTime dataFim

     )
        {
            var documento = new Documento();
            documento.Nome = nome;
            documento.Descricao = descricao;
            documento.Quantidade = quantidade;
            documento.DataInicio = dataInicio;
            documento.DataFim = dataFim;
            documento.TipoDocumento = tipoDocumento;
            documento.TipoAnexo = tipoAnexo;
            documento.Anexo = anexo;
            documento.Pet = new DocumentoPet() { Id = 0, PetId = idPet, DocumentoId = documento.Id };
            if (!idProntuario.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                documento.Prontuario = new ProntuarioDocumento() { Id = 0, ProntuarioId = idProntuario, DocumentoId = documento.Id };
            else
                documento.Prontuario = null;

            var erros = Validar(documento);

            if (erros.Count() > 0)
                return (null, erros);

            return (documento, Array.Empty<string>());
        }

        public (Documento, string[] mensagemDeErro) UpdateDocumento(Documento _BancoDeDados, Documento DocumentoUpdate)
        {
            var erros = Validar(DocumentoUpdate);
            if (erros.Count() > 0)
                return (null, erros);

            var documento = _BancoDeDados;
            if (DocumentoUpdate.Nome.Length > 0)
                documento.Nome = DocumentoUpdate.Nome;

            if (DocumentoUpdate.Descricao.Length > 0)
                documento.Descricao = DocumentoUpdate.Descricao;

            if (DocumentoUpdate.Quantidade != documento.Quantidade)
                documento.Quantidade = DocumentoUpdate.Quantidade;

            if (DocumentoUpdate.DataInicio != documento.DataInicio)
                documento.DataInicio = DocumentoUpdate.DataInicio;

            if (DocumentoUpdate.DataFim != documento.DataFim)
                documento.DataFim = DocumentoUpdate.DataFim;

            if (DocumentoUpdate.TipoDocumento != documento.TipoDocumento)
                documento.TipoDocumento = DocumentoUpdate.TipoDocumento;

            if (DocumentoUpdate.TipoAnexo != documento.TipoAnexo)
                documento.TipoAnexo = DocumentoUpdate.TipoAnexo;

            if (DocumentoUpdate.Anexo != documento.Anexo)
                documento.Anexo = DocumentoUpdate.Anexo;

            if (DocumentoUpdate.Prontuario.ProntuarioId != documento.Prontuario.ProntuarioId)
                documento.Prontuario.ProntuarioId = DocumentoUpdate.Prontuario.ProntuarioId;

            if (DocumentoUpdate.Pet.PetId != documento.Pet.PetId)
                documento.Pet.PetId = DocumentoUpdate.Pet.PetId;

            return (documento, Array.Empty<string>());
        }

    }
}
