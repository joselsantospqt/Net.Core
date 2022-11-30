using Domain.Entidade;
using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class DocumentoService
    {
        private IDocumentoRepositorio RepositorioDocumento { get; }

        public DocumentoService(IDocumentoRepositorio repositorioDocumento)
        {
            RepositorioDocumento = repositorioDocumento;
        }

        public Documento GetDocumentoById(Guid id)
        {
            return RepositorioDocumento.GetById(id);
        }

        public IEnumerable<Documento> GetAll()
        {
            return RepositorioDocumento.GetAll();
        }

        public Documento CreateDocumento(
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
            documento.AddPet(idPet);
            if (!idProntuario.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                documento.AddProntuario(idProntuario);
            else
                documento.Prontuario = null;

            RepositorioDocumento.SaveUpdate(documento);

            return documento;
        }

        public Documento UpdateDocumento(Documento DocumentoUpdate)
        {

            var documento = RepositorioDocumento.GetById(DocumentoUpdate.Id);
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

            RepositorioDocumento.SaveUpdate(documento);

            return documento;
        }

        public void DeleteDocumento(Guid id)
        {
            RepositorioDocumento.Remove(id);
        }
    }
}
