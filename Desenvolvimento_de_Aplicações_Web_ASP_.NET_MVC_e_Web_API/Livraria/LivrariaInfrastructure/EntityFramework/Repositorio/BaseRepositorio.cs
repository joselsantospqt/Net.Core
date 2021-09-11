using LivrariaCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaInfrastructure.EntityFramework.Repositorio
{
    public abstract class BaseRepositorio<T> where T : class
    {
        protected BancoDeDados _db;
        private DbSet<T> _tabela;

        public BaseRepositorio(BancoDeDados bancoDedados)
        {
            _db = bancoDedados;
            _tabela = _db.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _tabela.AsNoTracking().ToList();
        }

        public virtual T GetById(Guid id)
        {
            return _tabela.Find(id);
        }


        public void Remove(Guid id)
        {
            var registro = _tabela.Find(id);
            if (registro != null)
            {
                _tabela.Remove(registro);
                _db.SaveChanges();
            }
        }

        public void Add(T registro) 
        {
            _tabela.Add(registro);
            _db.SaveChanges();
        }
        public void Update(T registro)
        {
            _tabela.Update(registro);
            _db.SaveChanges();
        }

    }
}
