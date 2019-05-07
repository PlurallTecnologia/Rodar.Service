using Rodar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Repository
{
    public interface IEventoRepository
    {
        int Cadastrar(Evento evento);

        void Atualizar(Evento evento);

        Evento Buscar(int idEvento);

        int Excluir(int idEvento);

        IEnumerable<Evento> BuscarTodos();

        IEnumerable<Evento> BuscarPorUsuario(int idUsuario = 0);
    }
}
