using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class ResponseAll
    {
        public List<Pessoa> Value { get; set; }
        public IEnumerable<string> Formatters { get; set; }
        public IEnumerable<string> ContentTypes { get; set; }
        public IEnumerable<string> DeclaredType { get; set; }
        public int StatusCode { get; set; }
    }
}
