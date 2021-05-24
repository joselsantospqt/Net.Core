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
        public DbSet<Produto> TabelaProduto { get; set; }
    }
}
