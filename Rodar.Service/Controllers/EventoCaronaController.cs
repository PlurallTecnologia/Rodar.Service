using Newtonsoft.Json;
using Rodar.Business;
using Rodar.Service.Globals;
using Rodar.Service.Models;
using Rodar.Service.Providers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    public class EventoCaronaController : ApiController
    {
        [HttpPost]
        [ActionName("Cadastrar")]
        public HttpResponseMessage Cadastrar([System.Web.Http.FromBody] EventoCarona eventoCarona)
        {
            try
            {
                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                eventoCarona.idUsuarioMotorista = LoggedUserInformation.userId;

                appEventoCarona.Cadastrar(EventoCarona.ModelToEntity(eventoCarona));

                return Request.CreateResponse(HttpStatusCode.OK, eventoCarona);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpPost]
        [ActionName("Atualizar")]
        public HttpResponseMessage Atualizar([System.Web.Http.FromBody] EventoCarona eventoCarona)
        {
            try
            {
                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                appEventoCarona.Atualizar(EventoCarona.ModelToEntity(eventoCarona));

                return Request.CreateResponse(HttpStatusCode.OK, eventoCarona);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("Buscar")]
        public HttpResponseMessage Buscar(int idEventoCarona)
        {
            try
            {
                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                return Request.CreateResponse(HttpStatusCode.OK, EventoCarona.EntityToModel(appEventoCarona.Buscar(idEventoCarona)));
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpDelete]
        [ActionName("Excluir")]
        public HttpResponseMessage Excluir(int idEventoCarona)
        {
            try
            {
                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                return Request.CreateResponse(HttpStatusCode.OK, appEventoCarona.Excluir(idEventoCarona));
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("BuscarTodos")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarTodos()
        {
            try
            {
                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                var listaEventos = appEventoCarona
                    .BuscarTodos()
                    .Where(le => le.idUsuarioMotorista == LoggedUserInformation.userId)
                    .Select(eventoCarona => EventoCarona.EntityToModel(eventoCarona))
                    .ToList();

                return Request.CreateResponse(HttpStatusCode.OK, listaEventos);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("BuscarPorEvento")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarPorEvento(int idEvento)
        {
            try
            {
                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                var listaEventos = appEventoCarona
                    .BuscarPorEvento(idEvento)?
                    .Select(eventoCarona => EventoCarona.EntityToModel(eventoCarona))
                    .ToList();

                return Request.CreateResponse(HttpStatusCode.OK, listaEventos);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("BuscarAtivos")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarAtivos()
        {
            try
            {
                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                var listaEventos = appEventoCarona
                    .BuscarTodos()
                    .Select(eventoCarona => EventoCarona.EntityToModel(eventoCarona))
                    .Where(le => le.idUsuarioMotorista == LoggedUserInformation.userId
                    || (le.Passageiros != null ? le.Passageiros.Exists(p => p.idUsuario == LoggedUserInformation.userId) : true)
                    && le.Evento.dataHoraInicio >= DateTime.Now)
                    .ToList();

                var fileLog = HttpContext.Current.Server.MapPath("~/Log/Log.txt");
                File.AppendAllText(fileLog, $"Buscar Ativos: {listaEventos.Count.ToString()}");

                return Request.CreateResponse(HttpStatusCode.OK, listaEventos);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("BuscarHistorico")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarHistorico()
        {
            try
            {
                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                var listaEventos = appEventoCarona
                    .BuscarTodos()
                    .Select(eventoCarona => EventoCarona.EntityToModel(eventoCarona))
                    .Where(le => le.idUsuarioMotorista == LoggedUserInformation.userId
                    || (le.Passageiros != null ? le.Passageiros.Exists(p => p.idUsuario == LoggedUserInformation.userId) : true)
                    && le.Evento.dataHoraInicio <= DateTime.Now)
                    .ToList();

                return Request.CreateResponse(HttpStatusCode.OK, listaEventos);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }
        
        [HttpPost]
        [ActionName("AdicionarParticipacaoCarona")]
        public HttpResponseMessage AdicionarParticipacaoCarona(int idEventoCarona)
        {
            try
            {
                var appEventoCaronaPassageiro = new bllEventoCaronaPassageiro(DBRepository.GetEventoCaronaPassageiroRepository());
                var eventoCaronaModel = new EventoCaronaPassageiro();

                eventoCaronaModel.idUsuarioPassageiro = LoggedUserInformation.userId;
                eventoCaronaModel.idEventoCarona = idEventoCarona;

                appEventoCaronaPassageiro.Cadastrar(EventoCaronaPassageiro.ModelToEntity(eventoCaronaModel));

                return Request.CreateResponse(HttpStatusCode.OK, eventoCaronaModel);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpDelete]
        [ActionName("RemoverParticipacaoCarona")]
        public HttpResponseMessage RemoverParticipacaoCarona(int idEventoCarona)
        {
            try
            {
                var appEventoCaronaPassageiro = new bllEventoCaronaPassageiro(DBRepository.GetEventoCaronaPassageiroRepository());

                return Request.CreateResponse(HttpStatusCode.OK, appEventoCaronaPassageiro.Excluir(idEventoCarona, LoggedUserInformation.userId));
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpPost]
        [ActionName("AvaliarCarona")]
        public HttpResponseMessage AvaliarCarona(int idEventoCarona, int avaliacao)
        {
            try
            {
                var appAvaliacaoCarona = new bllAvaliacaoCarona(DBRepository.GetAvaliacaoCaronaRepository());
                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                var avaliacaoCaronaModel = new AvaliacaoCarona();

                avaliacaoCaronaModel.idUsuarioAvaliador = LoggedUserInformation.userId;
                avaliacaoCaronaModel.idUsuarioAvaliado = appEventoCarona.Buscar(idEventoCarona).idUsuarioMotorista;
                avaliacaoCaronaModel.Avaliacao = avaliacao;
                avaliacaoCaronaModel.idEventoCarona = idEventoCarona;

                return Request.CreateResponse(HttpStatusCode.OK, avaliacaoCaronaModel);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }
    }
}