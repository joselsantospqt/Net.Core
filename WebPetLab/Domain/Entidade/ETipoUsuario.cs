using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public enum ETipoUsuario
    {
        [Description("Médico")]
        Medico,
        [Description("Tutor")]
        Tutor
    }
}
