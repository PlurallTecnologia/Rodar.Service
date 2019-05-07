using Rodar.Business;
using Rodar.Service.Globals;
using Rodar.Service.Providers;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rodar.Service.Controllers
{
    [Authorize]
    public class EventoUsuarioFavoritoController : ApiController
    {
        [HttpPost]
        [ActionName("AdicionarFavorito")]
        public HttpResponseMessage AdicionarFavorito(int idEvento)
        {
            try
            {
                var appFavorito = new bllEventoUsuarioFavorito(DBRepository.GetEventoUsuarioFavoritoRepository());
                appFavorito.AdicionarFavorito(idEvento, LoggedUserInformation.userId);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpDelete]
        [ActionName("RemoverFavorito")]
        public HttpResponseMessage RemoverFavorito(int idEvento)
        {
            try
            {
                var appFavorito = new bllEventoUsuarioFavorito(DBRepository.GetEventoUsuarioFavoritoRepository());
                appFavorito.RemoverFavorito(idEvento, LoggedUserInformation.userId);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }
    }
}