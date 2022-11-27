using Domain.Entidade;
using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositorio
{
    public class DocumentoRepositorio : IDocumentoRepositorio
    {
        private BancoDeDados _db { get; }

        public DocumentoRepositorio(BancoDeDados bancoDeDados)
        {
            _db = bancoDeDados;
        }

        public IEnumerable<Documento> GetAll()
        {
            return _db.Documento.Include(x => x.Prontuario).Include(x => x.Pet).AsNoTracking().ToList();
        }

        public Documento GetById(Guid id)
        {
            return _db.Documento.Include(x => x.Prontuario).Include(x => x.Pet).Where(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var Documento = _db.Documento.Find(id);
            if (Documento != null)
            {
                _db.Documento.Remove(Documento);
                _db.SaveChanges();
            }
        }
        public void SaveUpdate(Documento documento)
        {
            if (documento.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                _db.Add(documento);
            else
                _db.Update(documento);

            _db.SaveChanges();

        }
    }
}
