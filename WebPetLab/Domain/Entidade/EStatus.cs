using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public enum EStatus
    {
        [Description("Pendente")]
        Pendente,
        [Description("Aprovado")]
        Aprovado,
        [Description("Recusado")]
        Recusado
    }
}
