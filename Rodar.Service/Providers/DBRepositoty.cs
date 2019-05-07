using Rodar.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rodar.Service.Providers
{
    public class DBRepository
    {
        public static IUsuarioRepository GetUsuarioRepository()
        {
            return new Repository.SqlServer.UsuarioRepository();
        }

        public static IEventoRepository GetEventoRepository()
        {
            return new Repository.SqlServer.EventoRepository();
        }

        public static IEventoUsuarioFavoritoRepository GetEventoUsuarioFavoritoRepository()
        {
            return new Repository.SqlServer.EventoUsuarioFavoritoRepository();
        }

        public static IEventoCaronaRepository GetEventoCaronaRepository()
        {
            return new Repository.SqlServer.EventoCaronaRepository();
        }

        public static IEventoTransporteRepository GetEventoTransporteRepository()
        {
            return new Repository.SqlServer.EventoTransporteRepository();
        }

        public static IEventoTransportePassageiroRepository GetEventoTransportePassageiroRepository()
        {
            return new Repository.SqlServer.EventoTransportePassageiroRepository();
        }

        public static IEventoCaronaPassageiroRepository GetEventoCaronaPassageiroRepository()
        {
            return new Repository.SqlServer.EventoCaronaPassageiroRepository();
        }
    }
}