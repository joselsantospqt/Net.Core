using Domain.Entidade;
using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class ExameService
    {
        private IExameRepositorio RepositorioExame { get; }

        public ExameService(IExameRepositorio repositorioExame)
        {
            RepositorioExame = repositorioExame;
        }

        public Exame GetExameById(Guid id)
        {
            return RepositorioExame.GetById(id);
        }

        public IEnumerable<Exame> GetAll()
        {
            return RepositorioExame.GetAll();
        }

        public Exame CreateExame(
            Guid idProntuario,
            string descricao,
            byte[] documento
           )
        {
            var exame = new Exame();
            exame.Descricao = descricao;
            exame.Data = DateTime.UtcNow;
            exame.AddProntuario(idProntuario);
            exame.Documento = documento;

            RepositorioExame.SaveUpdate(exame);

            return exame;
        }

        public Exame UpdateExame(Exame ExameUpdate)
        {

            var exame = RepositorioExame.GetById(ExameUpdate.Id);
            if (ExameUpdate.Descricao.Length > 0)
                exame.Descricao = ExameUpdate.Descricao;
            if (ExameUpdate.Data != exame.Data)
                exame.Data = ExameUpdate.Data;
            if (ExameUpdate.Documento != null)
                exame.Documento = ExameUpdate.Documento;

            RepositorioExame.SaveUpdate(exame);

            return exame;
        }

        public void DeleteExame(Guid id)
        {
            RepositorioExame.Remove(id);
        }
    }
}
