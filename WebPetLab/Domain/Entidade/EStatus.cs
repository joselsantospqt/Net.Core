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
        [Description("Aprovado")]
        Aprovado,
        [Description("Recusado")]
        Recusado,
        [Description("Pendente")]
        Pendente
    }
}
