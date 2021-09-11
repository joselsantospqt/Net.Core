using Microsoft.EntityFrameworkCore;
using MyGroupFriends.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGroupFriends.Infrastructure.EntityFramework
{
    public class BancoDeDados : DbContext
    {
        public BancoDeDados(DbContextOptions options) : base(options){}
        public DbSet<Amigo> Amigo { get; set; } 
    }
}
