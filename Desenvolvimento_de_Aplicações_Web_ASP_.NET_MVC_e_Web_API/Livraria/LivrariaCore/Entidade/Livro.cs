using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaCore
{
    public class Livro
    {
        public Livro()
        {
            Autores = new List<AutorLivro>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ISBN { get; set; }
        public int Ano { get; set; }
        public DateTime UpdatedDt { get; set; }
        public IList<AutorLivro> Autores { get; set; }

        internal void AdicionarAutor(List<Guid> pAutorId)
        {
            //foreach (var item in pAutorId)
            //{
            //    Autores.Add(new AutorLivro() { AutorId = item, LivroId = this.Id });
            //}

            pAutorId.Select(a => { Autores.Add(new AutorLivro() { AutorId = a, LivroId = this.Id }); return a; }).ToList();
        }
    }
}
