﻿// <auto-generated />
using System;
using LojaVirtual.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LojaVirtual.Migrations
{
    [DbContext(typeof(LojaVirtualDb))]
    partial class LojaVirtualDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LojaVirtual.Models.Pessoa", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DT_NASCIMENTO")
                        .HasColumnType("datetime2");

                    b.Property<string>("NM_NOME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NM_SOBRENOME")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("LojaVirtual.Models.Produto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NM_NOME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NR_PRECO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NR_QUANTIDADE")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Produto");
                });
#pragma warning restore 612, 618
        }
    }
}
