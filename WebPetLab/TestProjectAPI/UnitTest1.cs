using Domain.Entidade;
using Domain.Repositorio;
using Domain.Service;
using Infrastructure.EntityFramework.Repositorio;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProjectAPI
{
    public class UnitTest1
    {
        UsuarioMockService _service;
        public UnitTest1()
        {
            _service = new UsuarioMockService();
        }

        [Fact(DisplayName = "Cadastrar Usuário")]
        public void Teste1()
        {
            var lusuario = new List<Usuario>();

            var usuario = new Usuario();
            usuario.Nome = "José";
            usuario.Sobrenome = "Teste";
            usuario.Telefone = "";
            usuario.Cpf = "";
            usuario.Cnpj = "";
            usuario.DataNascimento = DateTime.UtcNow;
            usuario.Email = "teste@petshop.com";
            usuario.Senha = "12346477";
            usuario.ImagemUrlusuario = "Perfil_default.png";
            usuario.CreatedAt = DateTime.UtcNow;
            usuario.UpdatedAt = new DateTime();

            var usuarioCriado = _service.CreateUsuario(usuario.Id, usuario.Nome, usuario.Sobrenome, usuario.Telefone, usuario.Cpf, usuario.Cnpj, usuario.DataNascimento, usuario.Email, usuario.Senha, usuario.TipoUsuario);

            Assert.Equal(usuario.Id, usuarioCriado.Id);
            Assert.True(usuario.Nome == usuarioCriado.Nome);
            Assert.True(usuario.Sobrenome == usuarioCriado.Sobrenome);
            Assert.True(usuario.Telefone == usuarioCriado.Telefone);
            Assert.True(usuario.Cpf == usuarioCriado.Cpf);
            Assert.True(usuario.Cnpj == usuarioCriado.Cnpj);
            Assert.True(usuario.DataNascimento == usuarioCriado.DataNascimento);
            Assert.True(usuario.Email == usuarioCriado.Email);
            Assert.True(usuario.Senha == usuarioCriado.Senha);
            Assert.True(usuario.ImagemUrlusuario == usuarioCriado.ImagemUrlusuario);
        }


    }
}
