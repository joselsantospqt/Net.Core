using LivrariaCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaInfrastructure.EntityFramework
{
    public class BancoDeDados : DbContext
    {
        public BancoDeDados(DbContextOptions options) : base(options) { }

        public DbSet<Autor> Autor { get; set; }
        public DbSet<Livro> Livro { get; set; }

        //TODO: Ajustar os relacionamentos via modelo - https://docs.microsoft.com/pt-br/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>()
                .HasMany(a => a.Livros).WithOne();

            modelBuilder.Entity<Livro>()
                .HasMany(l => l.Autores).WithOne();
        }
    }
}
