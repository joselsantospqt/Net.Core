using DTO.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Service
{
    public class PessoaService
    {
        private BancoDeDados db;
        public PessoaService(BancoDeDados bancoDeDados)
        {
            db = bancoDeDados;
        }



    }
}
