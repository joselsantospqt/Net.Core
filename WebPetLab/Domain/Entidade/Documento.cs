using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Documento
    {
        public Documento() { Prontuario = new ProntuarioDocumento(); Pet = new DocumentoPet(); }
        [Key]
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
        [Display(Name = "Tipo do Documento")]
        public ETipoDocumento TipoDocumento { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Anexo")]
        public byte[] Anexo { get; set; }
        [Required]
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
        public ProntuarioDocumento Prontuario { get; set; }
        public DocumentoPet Pet { get; set; }


        internal void AddProntuario(Guid prontuarioId)
        {
            Prontuario = new ProntuarioDocumento() { ProntuarioId = prontuarioId,  DocumentoId = this.Id };
        } 
        internal void AddPet(Guid petId)
        {
            Pet = new DocumentoPet() { PetId = petId,  DocumentoId = this.Id };
        }

    }
}
