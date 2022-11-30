using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.View
{
   public class ViewModel
    {
        public ViewModel() { ListaPets = new List<Pet>(); Agendamentos = new List<Agendamento>(); Prontuarios = new List<Prontuario>(); Documentos = new List<Documento>(); ListaMedicos = new List<Usuario>(); }
        [Display(Name = "Pet")]
        public Pet Pet { get; set; }        
        [Display(Name = "Usuário")]
        public Usuario Usuario { get; set; }
        [Display(Name = "Documento")]
        public Documento Documento { get; set; }
        [Display(Name = "Prontuario")]
        public Prontuario Prontuario { get; set; }
        [Display(Name = "Agendamento")]
        public Agendamento Agendamento { get; set; }
        [Display(Name = "Lista Médicos")]
        public IList<Usuario> ListaMedicos { get; set; }
        [Display(Name = "Lista Pets")]
        public IList<Pet> ListaPets { get; set; }
        [Display(Name = "Lista Agendamentos")]
        public IList<Agendamento> Agendamentos { get; set; }
        [Display(Name = "Lista Prontuários")]
        public IList<Prontuario> Prontuarios { get; set; }
        [Display(Name = "Lista Documentos")]
        public IList<Documento> Documentos { get; set; }
    }
}
