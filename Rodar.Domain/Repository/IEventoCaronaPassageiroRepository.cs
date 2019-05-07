using Rodar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Repository
{
    public interface IEventoCaronaPassageiroRepository
    {
        int Cadastrar(EventoCaronaPassageiro eventoCaronaPassageiro);
        int Excluir(int idEventoCaronaPassageiro);
        int Excluir(int idEventoCarona, int idUsuarioPassageiro);
        IEnumerable<EventoCaronaPassageiro> BuscarTodos();
    }
}
