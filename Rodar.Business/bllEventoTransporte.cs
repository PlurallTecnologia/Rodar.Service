using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using System.Collections.Generic;

namespace Rodar.Business
{
    public class bllEventoTransporte
    {
        IEventoTransporteRepository _eventoTransporteRepository;

        public bllEventoTransporte(IEventoTransporteRepository eventoTransporteRepository)
        {
            _eventoTransporteRepository = eventoTransporteRepository;
        }

        public int Cadastrar(EventoTransporte eventoTransporte)
        {
            try
            {
                return _eventoTransporteRepository.Cadastrar(eventoTransporte);
            }
            catch
            {
                throw;
            }
        }

        public void Atualizar(EventoTransporte eventoTransporte)
        {
            try
            {
                _eventoTransporteRepository.Atualizar(eventoTransporte);
            }
            catch
            {
                throw;
            }
        }

        public EventoTransporte Buscar(int idEventoTransporte)
        {
            try
            {
                return _eventoTransporteRepository.Buscar(idEventoTransporte);
            }
            catch
            {
                throw;
            }
        }

        public int Excluir(int idEventoTransporte)
        {
            try
            {
                return _eventoTransporteRepository.Excluir(idEventoTransporte);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<EventoTransporte> BuscarPorEvento(int idEvento = 0)
        {
            try
            {
                return _eventoTransporteRepository.BuscarPorEvento(idEvento);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<EventoTransporte> BuscarTodos()
        {
            try
            {
                return _eventoTransporteRepository.BuscarTodos();
            }
            catch
            {
                throw;
            }
        }
    }
}