using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
    public class CreateDocumento
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public ETipoDocumento TipoDocumento { get; set; }
        public byte[] Anexo { get; set; }
        public string TipoAnexo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string url_documento { get; set; }
    }
}
