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
    public class UnitTestUsuario
    {
        UsuarioMockService _service;
        public UnitTestUsuario()
        {
            _service = new UsuarioMockService();
        }

        [Fact(DisplayName = "Cadastrar Usuário com Sucesso")]
        public void Cadastrar_Usuario_Sucesso()
        {

            var usuario = new Usuario();
            usuario.Nome = "José";
            usuario.Sobrenome = "Teste";
            usuario.Telefone = "123456798";
            usuario.Cpf = "123456798";
            usuario.Cnpj = "123456798";
            usuario.DataNascimento = DateTime.UtcNow;
            usuario.Email = "teste@petshop.com";
            usuario.Senha = "12346477";
            usuario.CreatedAt = DateTime.UtcNow;
            usuario.UpdatedAt = new DateTime();

            var (usuarioCriado, _) = _service.CreateUsuario(usuario.Id, usuario.Nome, usuario.Sobrenome, usuario.Telefone, usuario.Cpf, usuario.Cnpj, usuario.Crm, usuario.DataNascimento, usuario.Email, usuario.Senha, usuario.TipoUsuario);

            Assert.Equal(usuario.Id, usuarioCriado.Id);
            Assert.True(usuario.Nome == usuarioCriado.Nome);
            Assert.True(usuario.Sobrenome == usuarioCriado.Sobrenome);
            Assert.True(usuario.Telefone == usuarioCriado.Telefone);
            Assert.True(usuario.Cpf == usuarioCriado.Cpf);
            Assert.True(usuario.Cnpj == usuarioCriado.Cnpj);
            Assert.True(usuario.DataNascimento == usuarioCriado.DataNascimento);
            Assert.True(usuario.Email == usuarioCriado.Email);
            Assert.True(usuario.Senha == usuarioCriado.Senha);
        }

        [Theory(DisplayName = "Cadastrar sem Email, Senha e Telefone")]
        [InlineData("")]
        [InlineData(null)]
        public void Cadastrar_sem_Email_Senha_Telefone(string word)
        {

            var usuario = new Usuario();
            usuario.Nome = "José";
            usuario.Sobrenome = "Teste";
            usuario.Telefone = word;
            usuario.Cpf = "";
            usuario.Cnpj = "";
            usuario.Email = word;
            usuario.Senha = word;
            usuario.DataNascimento = DateTime.UtcNow;
            usuario.CreatedAt = DateTime.UtcNow;
            usuario.UpdatedAt = new DateTime();

            var (_,listaMensagemDeErro) = _service.CreateUsuario(usuario.Id, usuario.Nome, usuario.Sobrenome, usuario.Telefone, usuario.Cpf, usuario.Cnpj, usuario.Crm, usuario.DataNascimento, usuario.Email, usuario.Senha, usuario.TipoUsuario);

            Assert.Equal(3, listaMensagemDeErro.Length);
            Assert.Equal("Usuário não pode ser cadastrado sem Email", listaMensagemDeErro[0]);
            Assert.Equal("Usuário não pode ser cadastrado sem Senha", listaMensagemDeErro[1]);
            Assert.Equal("Usuário não pode ser cadastrado sem Telefone", listaMensagemDeErro[2]);

        }

        [Theory(DisplayName = "Atualizar Telefone, Data Nascimento, Nome para em Branco")]
        [InlineData("")]
        [InlineData(null)]
        public void Atualizar_sem_Telefone_DtNascimento_Nome(string word)
        {
            var usuario = new Usuario();
            usuario.Nome = "José";
            usuario.Sobrenome = "Teste";
            usuario.Telefone = "123456798";
            usuario.Cpf = "123456798";
            usuario.Cnpj = "123456798";
            usuario.DataNascimento = DateTime.UtcNow;
            usuario.Email = "teste@petshop.com";
            usuario.Senha = "12346477";
            usuario.CreatedAt = DateTime.UtcNow;
            usuario.UpdatedAt = new DateTime();

            var (usuarioCriado, _) = _service.CreateUsuario(usuario.Id, usuario.Nome, usuario.Sobrenome, usuario.Telefone, usuario.Cpf, usuario.Cnpj, usuario.Crm, usuario.DataNascimento, usuario.Email, usuario.Senha, usuario.TipoUsuario);

            usuario.Telefone = word;
            usuario.DataNascimento = new DateTime();
            usuario.Nome = word;

            var (_, listaMensagemDeErro) = _service.UpdateUsuario(usuarioCriado, usuario);

            Assert.Equal(3, listaMensagemDeErro.Length);
            Assert.Equal("Usuário não pode ser cadastrado sem Nome", listaMensagemDeErro[0]);
            Assert.Equal("Usuário não pode ser cadastrado sem Telefone", listaMensagemDeErro[1]);
            Assert.Equal("Usuário não pode ser cadastrado sem Data Nascimento", listaMensagemDeErro[2]);
        }
    }
}
