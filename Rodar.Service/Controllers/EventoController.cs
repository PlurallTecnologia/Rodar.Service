using Rodar.Business;
using Rodar.Service.Globals;
using Rodar.Service.Models;
using Rodar.Service.Providers;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Services;

namespace Rodar.Service.Controllers
{
    [Authorize]
    public class EventoController : ApiController
    {
        [HttpPost]
        [ActionName("Cadastrar")]
        public HttpResponseMessage Cadastrar([System.Web.Http.FromBody] Evento evento)
        {
            try
            {
                var appEvento = new bllEvento(DBRepository.GetEventoRepository());
                evento.idUsuarioCriacao = LoggedUserInformation.userId;

                appEvento.Cadastrar(Evento.ModelToEntity(evento));

                return Request.CreateResponse(HttpStatusCode.OK, evento);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpPost]
        [ActionName("Atualizar")]
        public HttpResponseMessage Atualizar([System.Web.Http.FromBody] Evento evento)
        {
            try
            {
                var appEvento = new bllEvento(DBRepository.GetEventoRepository());

                appEvento.Atualizar(Evento.ModelToEntity(evento));

                return Request.CreateResponse(HttpStatusCode.OK, evento);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("Buscar")]
        public HttpResponseMessage Buscar(int idEvento)
        {
            try
            {
                var appEvento = new bllEvento(DBRepository.GetEventoRepository());
                
                return Request.CreateResponse(HttpStatusCode.OK, Evento.EntityToModel(appEvento.Buscar(idEvento)));
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpDelete]
        [ActionName("Excluir")]
        public HttpResponseMessage Excluir(int idEvento)
        {
            try
            {
                var appEvento = new bllEvento(DBRepository.GetEventoRepository());
                return Request.CreateResponse(HttpStatusCode.OK, appEvento.Excluir(idEvento));
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("BuscarTodos")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarTodos(bool somenteMeusEventos = false, bool somenteMeusFavoritos = false)
        {
            try
            {
                var appEvento = new bllEvento(DBRepository.GetEventoRepository());
                var appEventoFavorito = new bllEventoUsuarioFavorito(DBRepository.GetEventoUsuarioFavoritoRepository());

                var idUsuario = (somenteMeusEventos ? Globals.LoggedUserInformation.userId : 0);
                var listaEventos = appEvento
                    .BuscarPorUsuario(idUsuario)
                    .Select(evento => Evento.EntityToModel(evento))
                    .ToList();

                if (somenteMeusFavoritos)
                    listaEventos = listaEventos.Where(evento => evento.Favorito).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, listaEventos);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("BuscarListaCidadeUfsExistentesEmEventos")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarListaCidadeUfsExistentesEmEventos()
        {
            try
            {
                bllEvento appEvento = new bllEvento(DBRepository.GetEventoRepository());

                var listaCidadesUfs = appEvento
                    .BuscarTodos()
                    .Select(evento => new { evento.enderecoCidade, evento.enderecoUF })
                    .Distinct()
                    .ToList();

                return Request.CreateResponse(HttpStatusCode.OK, listaCidadesUfs);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }
    }
}