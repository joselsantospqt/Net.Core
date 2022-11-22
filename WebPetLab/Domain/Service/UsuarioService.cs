using Domain.Entidade;
using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
   public class UsuarioService
    {
        private IUsuarioRepositorio RepositorioUsuario { get; }

        public UsuarioService(IUsuarioRepositorio repositorioUsuario)
        {
            RepositorioUsuario = repositorioUsuario;
        }

        public Usuario GetUsuarioById(Guid id)
        {
            return RepositorioUsuario.GetById(id);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return RepositorioUsuario.GetAll();
        }

        public Usuario CreateUsuario(
            string nome,
            string sobrenome,
            int telefone,
            int cpf,
            int cnpj,
            DateTime dataDeNascimento,
            string email,
            string senha,
            ETipoUsuario tipoUsuario
           )
        {

            var usuario = new Usuario();
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

            RepositorioUsuario.SaveUpdate(usuario);

            return usuario;
        }

        public Usuario UpdateUsuario(Usuario UsuarioUpdate)
        {

            var usuario = RepositorioUsuario.GetById(UsuarioUpdate.Id);
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
            if (UsuarioUpdate.ImagemUrlusuario != null && UsuarioUpdate.ImagemUrlusuario.Length == 0)
                usuario.ImagemUrlusuario = UsuarioUpdate.ImagemUrlusuario;
            if (UsuarioUpdate.Pets.Count() > 0)
                usuario.Pets = UsuarioUpdate.Pets;
            usuario.UpdatedAt = DateTime.UtcNow;

            RepositorioUsuario.SaveUpdate(usuario);

            //CRIAR UM RETORNO PARA SAVEUPDATE PARA VERIFICAR SE FOI PERSISTIDO A IMAGEM
            //DEPOIS INCLUIR O REPOSITORIO DA UPLOAD DE IMAGEM AQUI

            return usuario;
        }


        public void DeleteUsuario(Guid id)
        {
            RepositorioUsuario.Remove(id);
        }

    }
}
