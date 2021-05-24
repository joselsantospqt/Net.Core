using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendasDeProdutos.DataBase
{
    public class BancoDedados : DbContext
    {
        DbSet<Produto> TabelaProduto { get; set; }
    }


    public class Produto
    {
        public int ID;
        public string NM_NOME;
        public string NR_PRECO;
        public int NR_QUANTIDADE;
    }

}

