using Rodar.Domain.Entity;
using System.Collections.Generic;

namespace Rodar.Domain.Repository
{
    public interface IAvaliacaoTransporteRepository
    {
        int Cadastrar(AvaliacaoTransporte avaliacaoTransporte);
        List<AvaliacaoTransporte> BuscarTodos();
    }
}