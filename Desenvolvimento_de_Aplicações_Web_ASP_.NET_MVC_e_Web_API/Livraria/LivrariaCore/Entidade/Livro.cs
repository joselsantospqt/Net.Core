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
            Autores = new List<Autor>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ISBN { get; set; }
        public int Ano { get; set; }
        public DateTime UpdatedDt { get; set; }
        public IList<Autor> Autores { get; set; }


    }
}
