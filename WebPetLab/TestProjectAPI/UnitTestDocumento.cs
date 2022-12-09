using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProjectAPI
{
   public class UnitTestDocumento
    {
        DocumentoMockService _service;

        public UnitTestDocumento()
        {
            _service = new DocumentoMockService();
        }

        [Fact(DisplayName = "Cadastrar Documento com Sucesso")]
        public void Cadastrar_Documento_Sucesso()
        {
            var idPet = new Guid("{12345678-0000-0000-1234-000000000000}");
            var idProntuario = new Guid("{87654321-0000-0000-1234-000000000000}");
            var documento = new Documento();
            documento.Descricao = "Descrição documento";
            documento.Nome = "Nome documento";
            documento.Quantidade = 1;
            documento.TipoDocumento = ETipoDocumento.Documento;
            documento.DataInicio =  DateTime.UtcNow;
            documento.DataFim = DateTime.UtcNow;

            var (documentoCriado, _) = _service.CreateDocumento(idProntuario, idPet, documento.Descricao, documento.Quantidade, documento.Nome, documento.TipoAnexo, documento.Anexo, documento.TipoDocumento, documento.DataInicio, documento.DataFim);


            Assert.Equal(documento.Id, documentoCriado.Id);
            Assert.Equal(documento.Descricao, documentoCriado.Descricao);
            Assert.Equal(documento.Nome, documentoCriado.Nome);
            Assert.Equal(documento.Quantidade, documentoCriado.Quantidade);
            Assert.Equal(documento.TipoDocumento, documentoCriado.TipoDocumento);
            Assert.Equal(documento.DataInicio, documentoCriado.DataInicio);
            Assert.Equal(documento.DataFim, documentoCriado.DataFim);
            Assert.Equal(idPet, documentoCriado.Pet.PetId);
        }

        [Fact(DisplayName = "Cadastrar Documento sem Tutor")]
        public void Cadastrar_sem_Pet()
        {
            var idPet = new Guid();
            var idProntuario = new Guid("{87654321-0000-0000-1234-000000000000}");
            var documento = new Documento();
            documento.Descricao = "Descrição documento";
            documento.Nome = "Nome documento";
            documento.Quantidade = 1;
            documento.TipoDocumento = ETipoDocumento.Documento;
            documento.DataInicio = DateTime.UtcNow;
            documento.DataFim = DateTime.UtcNow;

            var (_, listaMensagemDeErro) = _service.CreateDocumento(idProntuario, idPet, documento.Descricao, documento.Quantidade, documento.Nome, documento.TipoAnexo, documento.Anexo, documento.TipoDocumento, documento.DataInicio, documento.DataFim);

            Assert.Equal("Documento não pode ser cadastrado sem Pet", listaMensagemDeErro[0]);
        }

        [Theory(DisplayName = "Cadastrar Documento sem Descrição, nome, quantidade,Tipo Documento, Datas")]
        [InlineData("")]
        [InlineData(null)]
        public void Cadastrar_sem_Documento_sem_Descricao_nome_quantidade_Tipo_Documento_Datas(string word)
        {
            var idPet = new Guid();
            var idProntuario = new Guid();
            var documento = new Documento();
            documento.Descricao = word;
            documento.Nome = word;
            documento.Quantidade = 0;
            documento.TipoDocumento = new ETipoDocumento();
            documento.DataInicio = new DateTime();
            documento.DataFim = new DateTime();

            var (_, listaMensagemDeErro) = _service.CreateDocumento(idProntuario, idPet, documento.Descricao, documento.Quantidade, documento.Nome, documento.TipoAnexo, documento.Anexo, documento.TipoDocumento, documento.DataInicio, documento.DataFim);

            Assert.Equal(6, listaMensagemDeErro.Length);
            Assert.Equal("Documento não pode ser cadastrado sem Pet", listaMensagemDeErro[0]);
            Assert.Equal("Documento não pode ser cadastrado sem Nome", listaMensagemDeErro[1]);
            Assert.Equal("Documento não pode ser cadastrado sem Descricao", listaMensagemDeErro[2]);
            Assert.Equal("Documento não pode ser cadastrado sem Quantidade", listaMensagemDeErro[3]);
            Assert.Equal("Documento não pode ser cadastrado sem Data de Inicio", listaMensagemDeErro[4]);
            Assert.Equal("Documento não pode ser cadastrado sem um Tipo", listaMensagemDeErro[5]);
        }

        [Theory(DisplayName = "Atualizar Documento sem Descrição, nome, quantidade,Tipo Documento, Data")]
        [InlineData("")]
        [InlineData(null)]
        public void Atualizar_sem_Documento_sem_Descricao_nome_quantidade_Tipo_Documento_Datas(string word)
        {
            var idPet = new Guid("{12345678-0000-0000-1234-000000000000}");
            var idProntuario = new Guid("{87654321-0000-0000-1234-000000000000}");
            var documento = new Documento();
            documento.Descricao = "Descrição documento";
            documento.Nome = "Nome documento";
            documento.Quantidade = 1;
            documento.TipoDocumento = ETipoDocumento.Documento;
            documento.DataInicio = DateTime.UtcNow;
            documento.DataFim = DateTime.UtcNow;

            var (documentoCriado, _) = _service.CreateDocumento(idProntuario, idPet, documento.Descricao, documento.Quantidade, documento.Nome, documento.TipoAnexo, documento.Anexo, documento.TipoDocumento, documento.DataInicio, documento.DataFim);

            documento = documentoCriado;
            documento.Descricao = word;
            documento.Pet.PetId = new Guid();
            documento.Nome = word;
            documento.Quantidade = 0;
            documento.TipoDocumento = new ETipoDocumento();
            documento.DataInicio = new DateTime();
            documento.DataFim = new DateTime();

            var (_, listaMensagemDeErro) = _service.UpdateDocumento(documentoCriado, documento);

            Assert.Equal(6, listaMensagemDeErro.Length);
            Assert.Equal("Documento não pode ser cadastrado sem Pet", listaMensagemDeErro[0]);
            Assert.Equal("Documento não pode ser cadastrado sem Nome", listaMensagemDeErro[1]);
            Assert.Equal("Documento não pode ser cadastrado sem Descricao", listaMensagemDeErro[2]);
            Assert.Equal("Documento não pode ser cadastrado sem Quantidade", listaMensagemDeErro[3]);
            Assert.Equal("Documento não pode ser cadastrado sem Data de Inicio", listaMensagemDeErro[4]);
            Assert.Equal("Documento não pode ser cadastrado sem um Tipo", listaMensagemDeErro[5]);
        }
    }
}
