using Rodar.Business;
using Rodar.Service.Globals;
using Rodar.Service.Models;
using Rodar.Service.Providers;
using System;
using System.Data;
using System.IO;
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
        public HttpResponseMessage BuscarTodos(bool somenteMeusEventos = false, bool somenteMeusFavoritos = false, 
            string nomeEvento = null, string cidadeUfEvento = null, string dataEvento = null)
        {
            try
            {
                var fileLog = HttpContext.Current.Server.MapPath("~/Log/Log.txt");
                File.AppendAllText(fileLog, $"NomeEvento: {nomeEvento} CidadeEvento:{cidadeUfEvento} DataEvento:{dataEvento}");
                File.AppendAllText(fileLog, System.Environment.NewLine);

                var appEvento = new bllEvento(DBRepository.GetEventoRepository());
                var appEventoFavorito = new bllEventoUsuarioFavorito(DBRepository.GetEventoUsuarioFavoritoRepository());

                var idUsuario = (somenteMeusEventos ? Globals.LoggedUserInformation.userId : 0);
                var listaEventos = appEvento
                    .BuscarPorUsuario(idUsuario)
                    ?.Select(evento => Evento.EntityToModel(evento))
                    .ToList();

                if (!string.IsNullOrWhiteSpace(nomeEvento))
                    listaEventos = listaEventos.Where(le => le.nomeEvento.ToUpper().Contains(nomeEvento.ToUpper().Trim())).ToList();

                if (!string.IsNullOrWhiteSpace(cidadeUfEvento))
                {
                    var cidadeUf = cidadeUfEvento.Split(',');
                    listaEventos = listaEventos.Where(le => le.enderecoCidade.ToUpper().Contains(cidadeUf[0].ToUpper().Trim()) && le.enderecoUF.ToUpper().Contains(cidadeUf[1].ToUpper().Trim())).ToList();
                }

                if (!string.IsNullOrWhiteSpace(dataEvento))
                    listaEventos = listaEventos.Where(le => le.dataHoraInicio.Value.ToString("dd/MM/yyyy") == Convert.ToDateTime(dataEvento.Trim()).ToString("dd/MM/yyyy")).ToList();
                
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
                    .Select(evento => String.Concat(evento.enderecoCidade, ", ", evento.enderecoUF))
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