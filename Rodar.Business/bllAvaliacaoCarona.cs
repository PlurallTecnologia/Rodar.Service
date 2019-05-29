using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using System.Collections.Generic;

namespace Rodar.Business
{
    public class bllAvaliacaoCarona
    {
        IAvaliacaoCaronaRepository _avaliacaoCaronaRepository;

        public bllAvaliacaoCarona(IAvaliacaoCaronaRepository avaliacaoCaronaRepository)
        {
            _avaliacaoCaronaRepository = avaliacaoCaronaRepository;
        }

        public int Cadastrar(AvaliacaoCarona avaliacaoCarona)
        {
            try
            {
                return _avaliacaoCaronaRepository.Cadastrar(avaliacaoCarona);
            }
            catch
            {
                throw;
            }
        }

        public List<AvaliacaoCarona> BuscarTodos()
        {
            try
            {
                return _avaliacaoCaronaRepository.BuscarTodos();
            }
            catch
            {
                throw;
            }
        }
    }
}