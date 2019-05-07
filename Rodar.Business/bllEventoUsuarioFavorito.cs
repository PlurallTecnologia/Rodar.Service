using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Business
{
    public class bllEventoUsuarioFavorito
    {
        IEventoUsuarioFavoritoRepository _eventoUsuarioFavoritoRepository;

        public bllEventoUsuarioFavorito(IEventoUsuarioFavoritoRepository eventoUsuarioFavoritoRepository)
        {
            _eventoUsuarioFavoritoRepository = eventoUsuarioFavoritoRepository;
        }

        public void AdicionarFavorito(int idEvento, int idUsuario)
        {
            try
            {
                _eventoUsuarioFavoritoRepository.AdicionarFavorito(idEvento, idUsuario);
            }
            catch
            {
                throw;
            }
        }

        public void RemoverFavorito(int idEvento, int idUsuario)
        {
            try
            {
                _eventoUsuarioFavoritoRepository.RemoverFavorito(idEvento, idUsuario);
            }
            catch
            {
                throw;
            }
        }

        public EventoUsuarioFavorito BuscarFavorito(int idEvento, int idUsuario)
        {
            return _eventoUsuarioFavoritoRepository.BuscarFavorito(idEvento, idUsuario);
        }

        public bool ExisteFavorito(int idEvento, int idUsuario)
        {
            return _eventoUsuarioFavoritoRepository.ExisteFavorito(idEvento, idUsuario);
        }
    }
}
