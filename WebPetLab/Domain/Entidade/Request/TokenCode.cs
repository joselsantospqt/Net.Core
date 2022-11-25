using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
   public class TokenCode
    {
        public string IdUser { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public Exception Exception { get; set; }
    }
}
