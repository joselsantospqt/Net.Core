using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{

    public enum ETipoDocumento
    {
        [Description("Exame")]
        Exame,
        [Description("Alimentação")]
        Alimentacao,
        [Description("Medicamento")]
        Medicamento,
        [Description("Documento")]
        Documento,
    }

}
