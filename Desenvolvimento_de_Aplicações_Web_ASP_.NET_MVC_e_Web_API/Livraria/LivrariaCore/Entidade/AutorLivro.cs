using System;

namespace LivrariaCore
{
    public class AutorLivro
    {
        public int Id { get; set; }
        public Guid AutorId { get; set; }
        public Guid LivroId { get; set; }
    }
}
