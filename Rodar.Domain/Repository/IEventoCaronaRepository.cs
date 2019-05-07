using Rodar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Repository
{
    public interface IEventoCaronaRepository
    {
        int Cadastrar(EventoCarona eventoCarona);

        void Atualizar(EventoCarona eventoCarona);

        EventoCarona Buscar(int idEventoCarona);

        int Excluir(int idEventoCarona);

        IEnumerable<EventoCarona> BuscarTodos();

        IEnumerable<EventoCarona> BuscarPorEvento(int idEvento = 0);
    }
}
