using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using System.Collections.Generic;

namespace Rodar.Business
{
    public class bllAvaliacaoTransporte
    {
        IAvaliacaoTransporteRepository _avaliacaoTransporteRepository;

        public bllAvaliacaoTransporte(IAvaliacaoTransporteRepository avaliacaoTransporteRepository)
        {
            _avaliacaoTransporteRepository = avaliacaoTransporteRepository;
        }

        public int Cadastrar(AvaliacaoTransporte avaliacaoTransporte)
        {
            try
            {
                return _avaliacaoTransporteRepository.Cadastrar(avaliacaoTransporte);
            }
            catch
            {
                throw;
            }
        }

        public List<AvaliacaoTransporte> BuscarTodos()
        {
            try
            {
                return _avaliacaoTransporteRepository.BuscarTodos();
            }
            catch
            {
                throw;
            }
        }
    }
}