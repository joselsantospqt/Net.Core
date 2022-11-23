using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
    public class CredenciaisUsuario
    {
        public Guid idUsuario { get; set; }
        public string PasswordHash { get; set; }
    }
}
