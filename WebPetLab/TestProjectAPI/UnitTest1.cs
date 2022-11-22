using Domain.Entidade;
using Domain.Repositorio;
using Domain.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProjectAPI
{
    public class UnitTest1 : IClassFixture<UsuarioService>
    {

        UsuarioService _Service;

        public UnitTest1(UsuarioService usuarioService)
        {
            _Service = usuarioService;
        }

        [Fact(DisplayName = "Cadastrar Usu�rio")]
        public void Teste1()
        {
            var lusuario = new List<Usuario>();

            var usuario = new Usuario();
            usuario.Nome = "Jos�";
            usuario.Sobrenome = "Teste";
            usuario.Telefone = 1234567489;
            usuario.Cpf = 1234444;
            usuario.Cnpj = 0;
            usuario.DataNascimento = DateTime.UtcNow;
            usuario.Email = "teste@petshop.com";
            usuario.Senha = "12346477";
            usuario.ImagemUrlusuario = "Perfil_default.png";
            usuario.CreatedAt = DateTime.UtcNow;
            usuario.UpdatedAt = new DateTime();

            var usuarioCriado = _Service.CreateUsuario(usuario.Nome, usuario.Sobrenome, usuario.Telefone, usuario.Cpf, usuario.Cnpj, usuario.DataNascimento, usuario.Email, usuario.Senha, usuario.TipoUsuario);

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
            Assert.True(usuario.CreatedAt == usuarioCriado.CreatedAt);
        }


    }
}
