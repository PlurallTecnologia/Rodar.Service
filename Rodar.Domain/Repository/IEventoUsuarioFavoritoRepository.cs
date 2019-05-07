using Rodar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Repository
{
    public interface IEventoUsuarioFavoritoRepository
    {
        void AdicionarFavorito(int idEvento, int idUsuario);
        void RemoverFavorito(int idEvento, int idUsuario);
        EventoUsuarioFavorito BuscarFavorito(int idEvento, int idUsuario);
        bool ExisteFavorito(int idEvento, int idUsuario);
    }
}
