using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using System.Collections.Generic;

namespace Rodar.Business
{
    public class bllEventoCaronaPassageiro
    {
        IEventoCaronaPassageiroRepository _eventoCaronaPassageiroRepository;

        public bllEventoCaronaPassageiro(IEventoCaronaPassageiroRepository eventoCaronaPassageiroRepository)
        {
            _eventoCaronaPassageiroRepository = eventoCaronaPassageiroRepository;
        }

        public int Cadastrar(EventoCaronaPassageiro eventoCaronaPassageiro)
        {
            try
            {
                return _eventoCaronaPassageiroRepository.Cadastrar(eventoCaronaPassageiro);
            }
            catch
            {
                throw;
            }
        }

        public int Excluir(int idEventoCarona, int idUsuarioPassageiro)
        {
            try
            {
                return _eventoCaronaPassageiroRepository.Excluir(idEventoCarona, idUsuarioPassageiro);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<EventoCaronaPassageiro> BuscarTodos()
        {
            try
            {
                return _eventoCaronaPassageiroRepository.BuscarTodos();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<EventoCaronaPassageiro> BuscarPorCarona(int idEventoCarona = 0)
        {
            try
            {
                return _eventoCaronaPassageiroRepository.BuscarPorCarona(idEventoCarona);
            }
            catch
            {
                throw;
            }
        }
    }
}
