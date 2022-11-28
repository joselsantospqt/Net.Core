using Domain.Entidade;
using Domain.Entidade.Request;
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
        public Usuario GetUsuarioByEmail(string email)
        {
            return RepositorioUsuario.GetByEmail(email);
        }

        public Usuario GetUsuarioById(Guid id)
        {
            return RepositorioUsuario.GetById(id);
        }

        public Usuario GetValidaUsuarioLogin(CredenciaisUsuario credenciaisUsuario)
        {

            Usuario user = RepositorioUsuario.GetById(credenciaisUsuario.idUsuario);
            if (user != null && user.Senha == credenciaisUsuario.PasswordHash)
                return user;
            else
                return null;
        }

        public IEnumerable<Usuario> GetAll()
        {
            return RepositorioUsuario.GetAll();
        }

        public Usuario CreateUsuario(
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
            ETipoUsuario tipoUsuario,
            string ImagemUrlusuario
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
            usuario.ImagemUrlusuario = "Perfil_default.png";
            usuario.CreatedAt = new DateTime();
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
            if (UsuarioUpdate.Crm != usuario.Crm)
                usuario.Crm = UsuarioUpdate.Crm;
            if (UsuarioUpdate.DataNascimento != usuario.DataNascimento)
                usuario.DataNascimento = UsuarioUpdate.DataNascimento;
            if (UsuarioUpdate.TipoUsuario != usuario.TipoUsuario)
                usuario.TipoUsuario = UsuarioUpdate.TipoUsuario;
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
