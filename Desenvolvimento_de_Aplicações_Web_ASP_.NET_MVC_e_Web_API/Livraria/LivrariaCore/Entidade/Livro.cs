﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaCore
{
    public class Livro
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AutorId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ISBN { get; set; }
        public DateTime CreateDt { get; set; }
        public DateTime UpdatedDt { get; set; }
        [ForeignKey("AutorId")]
        [NotMapped]
        public Autor Autor { get; set; }
    }
}
