using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using System.Collections.Generic;

namespace Rodar.Business
{
    public class bllEventoTransportePassageiro
    {
        IEventoTransportePassageiroRepository _eventoTransportePassageiroRepository;

        public bllEventoTransportePassageiro(IEventoTransportePassageiroRepository eventoTransportePassageiroRepository)
        {
            _eventoTransportePassageiroRepository = eventoTransportePassageiroRepository;
        }

        public int Cadastrar(EventoTransportePassageiro eventoTransportePassageiro)
        {
            try
            {
                return _eventoTransportePassageiroRepository.Cadastrar(eventoTransportePassageiro);
            }
            catch
            {
                throw;
            }
        }

        public int Excluir(int idEventoTransporte, int idUsuarioPassageiro)
        {
            try
            {
                return _eventoTransportePassageiroRepository.Excluir(idEventoTransporte, idUsuarioPassageiro);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<EventoTransportePassageiro> BuscarTodos()
        {
            try
            {
                return _eventoTransportePassageiroRepository.BuscarTodos();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<EventoTransportePassageiro> BuscarPorTransporte(int idEventoTransporte = 0)
        {
            try
            {
                return _eventoTransportePassageiroRepository.BuscarPorTransporte(idEventoTransporte);
            }
            catch
            {
                throw;
            }
        }
    }
}
