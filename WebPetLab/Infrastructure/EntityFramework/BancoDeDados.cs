using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entidade;

namespace Infrastructure.EntityFramework
{
    public class BancoDeDados : DbContext
    {
        public BancoDeDados(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Pet> Pet { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<Prontuario> Prontuario { get; set; }
        public DbSet<Documento> Documento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasMany(p => p.Pets).WithOne();

            modelBuilder.Entity<Pet>()
                .HasMany(p => p.Agendamentos).WithOne();

            modelBuilder.Entity<Pet>()
                .HasMany(p => p.Prontuarios).WithOne();

            modelBuilder.Entity<Pet>()
             .HasMany(p => p.Documentos).WithOne();

            modelBuilder.Entity<Pet>()
                .HasOne(b => b.Tutor).WithOne();

            modelBuilder.Entity<Agendamento>()
                .HasOne(p => p.Pet).WithOne();

            modelBuilder.Entity<Agendamento>()
                .HasOne(p => p.MedicoResponsavel).WithOne();

            modelBuilder.Entity<Prontuario>()
                .HasOne(b => b.Pet).WithOne();

            modelBuilder.Entity<Prontuario>()
                .HasOne(b => b.Medico).WithOne();

            modelBuilder.Entity<Prontuario>()
               .HasMany(p => p.Documentos).WithOne();

            modelBuilder.Entity<Documento>()
              .HasOne(p => p.Prontuario).WithOne();    
            
            modelBuilder.Entity<Documento>()
              .HasOne(p => p.Pet).WithOne();

        }
    }
}
