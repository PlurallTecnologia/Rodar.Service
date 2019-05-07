using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using System.Collections.Generic;

namespace Rodar.Business
{
    public class bllEventoCarona
    {
        IEventoCaronaRepository _eventoCaronaRepository;

        public bllEventoCarona(IEventoCaronaRepository eventoCaronaRepository)
        {
            _eventoCaronaRepository = eventoCaronaRepository;
        }

        public int Cadastrar(EventoCarona eventoCarona)
        {
            try
            {
                return _eventoCaronaRepository.Cadastrar(eventoCarona);
            }
            catch
            {
                throw;
            }
        }

        public void Atualizar(EventoCarona eventoCarona)
        {
            try
            {
                _eventoCaronaRepository.Atualizar(eventoCarona);
            }
            catch
            {
                throw;
            }
        }

        public EventoCarona Buscar(int idEventoCarona)
        {
            try
            {
                return _eventoCaronaRepository.Buscar(idEventoCarona);
            }
            catch
            {
                throw;
            }
        }

        public int Excluir(int idEventoCarona)
        {
            try
            {
                return _eventoCaronaRepository.Excluir(idEventoCarona);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<EventoCarona> BuscarPorEvento(int idEvento = 0)
        {
            try
            {
                return _eventoCaronaRepository.BuscarPorEvento(idEvento);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<EventoCarona> BuscarTodos()
        {
            try
            {
                return _eventoCaronaRepository.BuscarTodos();
            }
            catch
            {
                throw;
            }
        }
    }
}
