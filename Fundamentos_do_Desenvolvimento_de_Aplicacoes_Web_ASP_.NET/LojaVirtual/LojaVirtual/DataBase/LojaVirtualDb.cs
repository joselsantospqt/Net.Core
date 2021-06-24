using LojaVirtual.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace LojaVirtual.DataBase
{
    public class LojaVirtualDb : DbContext
    {

        public LojaVirtualDb(DbContextOptions options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        @"Data Source=RICO-PC;Initial Catalog=LojaVirtual;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Produto>(builder =>
        //    {
        //        builder.ToTable("PRODUTO");
        //    });
        //}

        public DbSet<Produto> Produto { get; set; }

        public DbSet<Pessoa> Pessoa { get; set; }

    }
}
