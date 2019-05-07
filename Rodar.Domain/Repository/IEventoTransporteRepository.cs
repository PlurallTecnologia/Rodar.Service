using Rodar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Repository
{
    public interface IEventoTransporteRepository
    {
        int Cadastrar(EventoTransporte eventoTransporte);

        void Atualizar(EventoTransporte eventoTransporte);

        EventoTransporte Buscar(int idEventoTransporte);

        int Excluir(int idEventoCarona);

        IEnumerable<EventoTransporte> BuscarTodos();

        IEnumerable<EventoTransporte> BuscarPorEvento(int idEvento = 0);
    }
}
