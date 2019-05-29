using Rodar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Repository
{
    public interface IEventoTransportePassageiroRepository
    {
        int Cadastrar(EventoTransportePassageiro eventoTransportePassageiro);
        int Excluir(int idEventoTransportePassageiro);
        int Excluir(int idEventoTransporte, int idUsuarioPassageiro);
        IEnumerable<EventoTransportePassageiro> BuscarTodos();
        IEnumerable<EventoTransportePassageiro> BuscarPorTransporte(int idEventoTransporte);
    }
}
