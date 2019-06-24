using Rodar.Business;
using Rodar.Service.Models;
using Rodar.Service.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Rodar.Service.Globals
{
    public class LoggedUserInformation
    {
        public static int getUserId(IIdentity identity)
        {
            identity = (ClaimsIdentity)identity;

            bllUsuario appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());
            var usuario = Usuario.EntityToModel(appUsuario.BuscarPorEmail(identity.Name));

            return usuario.idUsuario;
        }

        public string getUserEmail(IIdentity identity)
        {
            identity = (ClaimsIdentity)identity;
            return identity.Name;
        }

        public static string userEmail = string.Empty;
        public static int userId = 0;
    }
}