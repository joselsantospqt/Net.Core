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
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public ETipoDocumento TipoDocumento { get; set; }
        public byte[] Anexo { get; set; }
        public string TipoAnexo { get; set; }
        public DateTime DataInicio { get; set; }
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
