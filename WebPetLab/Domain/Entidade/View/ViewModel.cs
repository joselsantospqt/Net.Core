using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.View
{
   public class ViewModel
    {
        public ViewModel() { ListaPets = new List<Pet>(); Agendamentos = new List<Agendamento>(); Prontuarios = new List<Prontuario>(); Documentos = new List<Documento>(); ListaMedicos = new List<Usuario>(); }
        public Pet Pet { get; set; }
        public Usuario Usuario { get; set; }
        public IList<Usuario> ListaMedicos { get; set; }
        public IList<Pet> ListaPets { get; set; }
        public IList<Agendamento> Agendamentos { get; set; }
        public IList<Prontuario> Prontuarios { get; set; }
        public IList<Documento> Documentos { get; set; }
    }
}
