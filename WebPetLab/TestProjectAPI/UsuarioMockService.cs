using Domain.Entidade;
using System;

namespace TestProjectAPI
{
    public class UsuarioMockService
    {
        public Usuario CreateUsuario(
        Guid id,
        string nome,
        string sobrenome,
        string telefone,
        string cpf,
        string cnpj,
        DateTime dataDeNascimento,
        string email,
        string senha,
        ETipoUsuario tipoUsuario
       )
        {

            var usuario = new Usuario();
            usuario.Id = id;
            usuario.Nome = nome;
            usuario.Sobrenome = sobrenome;
            usuario.Telefone = telefone;
            usuario.Cpf = cpf;
            usuario.Cnpj = cnpj;
            usuario.DataNascimento = dataDeNascimento;
            usuario.Email = email;
            usuario.Senha = senha;
            usuario.ImagemUrlusuario = "Perfil_default.png";
            usuario.CreatedAt = DateTime.UtcNow;
            usuario.UpdatedAt = new DateTime();

            return usuario;
        }
    }
}
