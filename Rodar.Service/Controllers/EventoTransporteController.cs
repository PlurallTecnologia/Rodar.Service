using Newtonsoft.Json;
using Rodar.Business;
using Rodar.Service.Globals;
using Rodar.Service.Models;
using Rodar.Service.Providers;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Services;

namespace Rodar.Service.Controllers
{
    [Authorize]
    public class EventoTransporteController : ApiController
    {
        [HttpPost]
        [ActionName("Cadastrar")]
        public HttpResponseMessage Cadastrar([System.Web.Http.FromBody] EventoTransporte eventoTransporte)
        {
            try
            {
                var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());
                var eventoTransporteEntity = EventoTransporte.ModelToEntity(eventoTransporte);
                eventoTransporteEntity.idUsuarioTransportador = LoggedUserInformation.userId;

                appEventoTransporte.Cadastrar(eventoTransporteEntity);

                return Request.CreateResponse(HttpStatusCode.OK, eventoTransporteEntity);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpPost]
        [ActionName("Atualizar")]
        public HttpResponseMessage Atualizar([System.Web.Http.FromBody] EventoTransporte eventoTransporte)
        {
            try
            {
                var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());
                var eventoTransporteEntity = EventoTransporte.ModelToEntity(eventoTransporte);
                eventoTransporteEntity.idUsuarioTransportador = LoggedUserInformation.userId;

                appEventoTransporte.Atualizar(eventoTransporteEntity);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("Buscar")]
        public HttpResponseMessage Buscar(int idEventoTransporte)
        {
            try
            {
                var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());

                return Request.CreateResponse(HttpStatusCode.OK, EventoTransporte.EntityToModel(appEventoTransporte.Buscar(idEventoTransporte)));
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpDelete]
        [ActionName("Excluir")]
        public HttpResponseMessage Excluir(int idEventoTransporte)
        {
            try
            {
                var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());

                return Request.CreateResponse(HttpStatusCode.OK, appEventoTransporte.Excluir(idEventoTransporte));
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
            var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());

            var listaEventos = appEventoTransporte
                .BuscarTodos()
                .Where(le => le.idUsuarioTransportador == LoggedUserInformation.userId)
                .Select(eventoTransporte => EventoTransporte.EntityToModel(eventoTransporte))
                .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, listaEventos);
        }

        [HttpGet]
        [ActionName("BuscarPorEvento")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarPorEvento(int idEvento)
        {
            var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());

            var listaEventos = appEventoTransporte
                .BuscarPorEvento(idEvento)?
                .Select(eventoTransporte => EventoTransporte.EntityToModel(eventoTransporte))
                .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, listaEventos);
        }

        [HttpGet]
        [ActionName("BuscarAtivos")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarAtivos()
        {
            var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());

            var listaEventos = appEventoTransporte
                .BuscarTodos()
                .Select(eventoTransporte => EventoTransporte.EntityToModel(eventoTransporte))
                .Where(le => le.idUsuarioTransportador == LoggedUserInformation.userId
                || (le.Passageiros != null ? le.Passageiros.Exists(p => p.idUsuario == LoggedUserInformation.userId) : true)
                && le.dataHoraPrevisaoChegada >= DateTime.Now)
                .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, listaEventos);
        }

        [HttpGet]
        [ActionName("BuscarHistorico")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarHistorico()
        {
            var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());

            var listaEventos = appEventoTransporte
                .BuscarTodos()
                .Select(eventoTransporte => EventoTransporte.EntityToModel(eventoTransporte))
                .Where(le => le.idUsuarioTransportador == LoggedUserInformation.userId
                || (le.Passageiros != null ? le.Passageiros.Exists(p => p.idUsuario == LoggedUserInformation.userId) : true)
                && le.dataHoraPrevisaoChegada <= DateTime.Now)
                .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, listaEventos);
        }

        [HttpPost]
        [ActionName("AdicionarParticipacaoTransporte")]
        public HttpResponseMessage AdicionarParticipacaoTransporte(int idEventoTransporte)
        {
            try
            {
                var appEventoTransportePassageiro = new bllEventoTransportePassageiro(DBRepository.GetEventoTransportePassageiroRepository());
                var eventoTransporteModel = new EventoTransportePassageiro();

                eventoTransporteModel.idUsuarioPassageiro = LoggedUserInformation.userId;
                eventoTransporteModel.idEventoTransporte = idEventoTransporte;

                appEventoTransportePassageiro.Cadastrar(EventoTransportePassageiro.ModelToEntity(eventoTransporteModel));

                return Request.CreateResponse(HttpStatusCode.OK, eventoTransporteModel);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpDelete]
        [ActionName("RemoverParticipacaoTransporte")]
        public HttpResponseMessage RemoverParticipacaoTransporte(int idEventoTransporte)
        {
            try
            {
                var appEventoTransportePassageiro = new bllEventoTransportePassageiro(DBRepository.GetEventoTransportePassageiroRepository());

                return Request.CreateResponse(HttpStatusCode.OK, appEventoTransportePassageiro.Excluir(idEventoTransporte, LoggedUserInformation.userId));
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpPost]
        [ActionName("AvaliarTransporte")]
        public HttpResponseMessage AvaliarTransporte([System.Web.Http.FromBody] AvaliacaoTransporte avaliacaoTransporte)
        {
            try
            {
                var appAvaliacaoTransporte = new bllAvaliacaoTransporte(DBRepository.GetAvaliacaoTransporteRepository());
                var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());

                var avaliacaoTransporteModel = new AvaliacaoTransporte();

                avaliacaoTransporteModel.idUsuarioAvaliador = LoggedUserInformation.userId;
                avaliacaoTransporteModel.idUsuarioAvaliado = appEventoTransporte.Buscar(avaliacaoTransporte.idEventoTransporte).idUsuarioTransportador;
                avaliacaoTransporteModel.Avaliacao = avaliacaoTransporte.Avaliacao;
                avaliacaoTransporteModel.idEventoTransporte = avaliacaoTransporte.idEventoTransporte;
                avaliacaoTransporteModel.Mensagem = avaliacaoTransporte.Mensagem;

                appAvaliacaoTransporte.Cadastrar(AvaliacaoTransporte.ModelToEntity(avaliacaoTransporteModel));

                return Request.CreateResponse(HttpStatusCode.OK, avaliacaoTransporteModel);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpPost]
        [ActionName("EnviarMensagemUsuarioTansporte")]
        public HttpResponseMessage EnviarMensagemUsuarioTansporte([System.Web.Http.FromBody] ChatUsuarioEventoTransporte chatUsuarioEventoTransporte)
        {
            try
            {
                var appChatUsuarioEventoTransporte = new bllChatUsuarioEventoTransporte(DBRepository.GetChatUsuarioEventoTransporteRepository());

                chatUsuarioEventoTransporte.idUsuarioOrigem = LoggedUserInformation.userId;

                appChatUsuarioEventoTransporte.Cadastrar(ChatUsuarioEventoTransporte.ModelToEntity(chatUsuarioEventoTransporte));

                return Request.CreateResponse(HttpStatusCode.OK, chatUsuarioEventoTransporte);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("BuscarMensagensEnviadasUsuario")]
        public HttpResponseMessage BuscarMensagensUsuario()
        {
            try
            {
                var appChatUusuarioEventoTransporte = new bllChatUsuarioEventoTransporte(DBRepository.GetChatUsuarioEventoTransporteRepository());

                var listaMensagens = appChatUusuarioEventoTransporte
                    .BuscarTodos()
                    .Where(chat => chat.idUsuarioOrigem == LoggedUserInformation.userId)
                    .Select(chatUsuarioEventoTransporte => ChatUsuarioEventoTransporte.EntityToModel(chatUsuarioEventoTransporte));

                return Request.CreateResponse(HttpStatusCode.OK, listaMensagens);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }
    }
}