using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
    public class CreateDocumento
    {
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Tipo Documento")]
        public ETipoDocumento TipoDocumento { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Anexo")]
        public byte[] Anexo { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Tipo Anexo")]
        public string TipoAnexo { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data Inicio")]
        public DateTime DataInicio { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data Fim")]
        public DateTime DataFim { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Anexo Documento")]
        public string url_documento { get; set; }
    }
}
