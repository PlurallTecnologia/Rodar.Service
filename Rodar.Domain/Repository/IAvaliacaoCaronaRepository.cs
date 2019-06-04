﻿using Rodar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Repository
{
    public interface IAvaliacaoCaronaRepository
    {
        int Cadastrar(AvaliacaoCarona avaliacaoCarona);
        List<AvaliacaoCarona> BuscarTodos();
    }
}