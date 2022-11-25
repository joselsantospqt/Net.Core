using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public enum ETipoEspecie
    {
        [Description("Mamíferos")]
        Mamiferos,
        [Description("Aves")]
        Aves,
        [Description("Peixes")]
        Peixes,
        [Description("Répteis")]
        Repteis,
        [Description("Anfíbios")]
        Anfibios,
        [Description("Vermiforme")]
        Vermiforme,
        [Description("Não Vermiforme")]
        Nao_Vermiforme,
        [Description("Invertebrados Com 3 Pares ou Mais Pernas")]
        Invertebrados
    }
}
