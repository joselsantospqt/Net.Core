﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaCore.Repositorio
{
    public interface IAutorRepositorio
    {
        void SaveUpdate(Autor autor);
        Autor GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Autor> GetAll();

    }
}
