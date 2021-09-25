using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class ReponseAll
    {
        public List<Tarefa> Value { get; set; }
        public IEnumerable<string> Formatters { get; set; }
        public IEnumerable<string> ContentTypes { get; set; }
        public IEnumerable<string> DeclaredType { get; set; }
        public int StatusCode { get; set; }
    }
}
