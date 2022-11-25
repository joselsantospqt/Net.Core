﻿using Domain.Entidade;
using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private BancoDeDados _db { get; }

        public UsuarioRepositorio(BancoDeDados bancoDeDados)
        {
            _db = bancoDeDados;
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _db.Usuario.Include(x => x.Pets).AsNoTracking().ToList();
        }

        public Usuario GetById(Guid id)
        {
            return _db.Usuario.Include(x => x.Pets).Where(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var Usuario = _db.Usuario.Find(id);
            if (Usuario != null)
            {
                _db.Usuario.Remove(Usuario);
                _db.SaveChanges();
            }
        }
        public void SaveUpdate(Usuario usuario)
        {
            if (usuario.Cpf == null && usuario.Cnpj == null && usuario.UpdatedAt == new DateTime())
                _db.Add(usuario);
            else
                _db.Update(usuario);

            _db.SaveChanges();

        }

        public Usuario GetByEmail(string email)
        {
            return _db.Usuario.Include(x => x.Pets).Where(x => x.Email == email).FirstOrDefault();
        }
    }
}
