using Domain.Entidade;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProjectAPI
{
    public class UsuarioMockService
    {
        private string[] Validar(Usuario obj)
        {
            var erros = new List<string>();

            if (string.IsNullOrEmpty(obj.Nome))
                erros.Add("Usuário não pode ser cadastrado sem Nome");

            if (string.IsNullOrEmpty(obj.Email))
                erros.Add("Usuário não pode ser cadastrado sem Email");

            if (string.IsNullOrEmpty(obj.Senha))
                erros.Add("Usuário não pode ser cadastrado sem Senha");

            if (string.IsNullOrEmpty(obj.Telefone))
                erros.Add("Usuário não pode ser cadastrado sem Telefone");

            if (obj.DataNascimento == new DateTime() || obj.DataNascimento < new DateTime(1900,1,1))
                erros.Add("Usuário não pode ser cadastrado sem Data Nascimento");

            if (erros.Count() > 0)
                return erros.ToArray();

            return Array.Empty<string>();
        }

        public (Usuario, string[] mensagemDeErro) CreateUsuario(
          Guid id,
          string nome,
          string sobrenome,
          string telefone,
          string cpf,
          string cnpj,
          string crm,
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
            usuario.Crm = crm;
            usuario.DataNascimento = dataDeNascimento;
            usuario.Email = email;
            usuario.Senha = senha;
            usuario.TipoUsuario = tipoUsuario;
            usuario.CreatedAt = new DateTime();
            usuario.UpdatedAt = new DateTime();

            var erros = Validar(usuario);

            if (erros.Count() > 0)
                return (null, erros);

            return (usuario, Array.Empty<string>());
        }


        public (Usuario, string[] mensagemDeErro) UpdateUsuario(Usuario _BancoDeDados ,Usuario UsuarioUpdate)
        {
            var erros = Validar(UsuarioUpdate);
            if (erros.Count() > 0)
                return (null, erros);

            var usuario = _BancoDeDados;
            if (UsuarioUpdate.Nome != null)
                usuario.Nome = UsuarioUpdate.Nome;
            if (UsuarioUpdate.Sobrenome != null)
                usuario.Sobrenome = UsuarioUpdate.Sobrenome;
            if (UsuarioUpdate.Telefone != usuario.Telefone)
                usuario.Telefone = UsuarioUpdate.Telefone;
            if (UsuarioUpdate.Cpf != usuario.Cpf)
                usuario.Cpf = UsuarioUpdate.Cpf;
            if (UsuarioUpdate.Cnpj != usuario.Cnpj)
                usuario.Cnpj = UsuarioUpdate.Cnpj;
            if (UsuarioUpdate.Crm != usuario.Crm)
                usuario.Crm = UsuarioUpdate.Crm;
            if (UsuarioUpdate.DataNascimento != usuario.DataNascimento)
                usuario.DataNascimento = UsuarioUpdate.DataNascimento;
            if (UsuarioUpdate.TipoUsuario != usuario.TipoUsuario)
                usuario.TipoUsuario = UsuarioUpdate.TipoUsuario;
            if (UsuarioUpdate.Pets.Count() > 0)
                usuario.Pets = UsuarioUpdate.Pets;
            if (UsuarioUpdate.TipoAnexo != usuario.TipoAnexo)
                usuario.TipoAnexo = UsuarioUpdate.TipoAnexo;
            if (UsuarioUpdate.Anexo != usuario.Anexo)
                usuario.Anexo = UsuarioUpdate.Anexo;

            usuario.UpdatedAt = DateTime.UtcNow;

            return (usuario, Array.Empty<string>());
        }
    }
}
