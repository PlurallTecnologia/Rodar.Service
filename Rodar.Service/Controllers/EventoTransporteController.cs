using FirebaseAdmin.Messaging;
using Newtonsoft.Json;
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
using System.Threading.Tasks;
using System.Web;
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

                eventoTransporte.idUsuarioTransportador = LoggedUserInformation.getUserId(User.Identity);
                eventoTransporte.idEventoTransporte = appEventoTransporte.Cadastrar(EventoTransporte.ModelToEntity(eventoTransporte));

                return Request.CreateResponse(HttpStatusCode.OK, eventoTransporte);
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
                eventoTransporteEntity.idUsuarioTransportador = LoggedUserInformation.getUserId(User.Identity);

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

                var fileLog = HttpContext.Current.Server.MapPath("~/Log/Log.txt");
                File.AppendAllText(fileLog, $"Entrei excluir Carona");

                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                var topic = $"transporte{idEventoTransporte}";

                var message = new Message()
                {
                    Notification = new Notification()
                    {
                        Title = "TRANSPORTE EXCLUIDO",
                        Body = $"O TRANSPORTE {idEventoTransporte} FOI EXCLUIDA COM SUCESSO",
                    },
                    Topic = topic,
                };
                var response = Task.Factory.StartNew(() => FirebaseMessaging.DefaultInstance.SendAsync(message));

                return Request.CreateResponse(HttpStatusCode.OK, appEventoTransporte.Excluir(idEventoTransporte));
                //return Request.CreateResponse(HttpStatusCode.OK);
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
                .Where(le => le.idUsuarioTransportador == LoggedUserInformation.getUserId(User.Identity))
                .Select(eventoTransporte => EventoTransporte.EntityToModel(eventoTransporte))
                .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, listaEventos);
        }

        [HttpGet]
        [ActionName("BuscarPorEvento")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarPorEvento(int idEvento, 
                                                    string nomeTransporte = null,
                                                    string cidadeUfPartida = null,
                                                    string dataPartida = null,
                                                    string valorInicial = null,
                                                    string valorFinal = null,
                                                    string vagasDisponiveis = null)
        {
            var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());

            var listaTransporte = appEventoTransporte
                .BuscarPorEvento(idEvento)
                ?.Select(eventoTransporte => EventoTransporte.EntityToModel(eventoTransporte))
                .ToList();

            if (!string.IsNullOrWhiteSpace(nomeTransporte))
                listaTransporte = listaTransporte.Where(le => le.nomeTransporte.ToUpper().Contains(nomeTransporte.ToUpper().Trim())).ToList();

            if (!string.IsNullOrWhiteSpace(cidadeUfPartida))
            {
                var cidadeUf = cidadeUfPartida.Split(',');
                listaTransporte = listaTransporte.Where(le => le.enderecoPartidaCidade.ToUpper().Contains(cidadeUf[0].ToUpper().Trim()) && le.enderecoPartidaUF.ToUpper().Contains(cidadeUf[1].ToUpper().Trim())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(dataPartida))
                listaTransporte = listaTransporte.Where(le => le.dataHoraPartida.Value == Convert.ToDateTime(dataPartida.Trim())).ToList();

            if (!string.IsNullOrWhiteSpace(valorInicial))
                listaTransporte = listaTransporte.Where(le => le.valorParticipacao >= Convert.ToDecimal(valorInicial.Trim())).ToList();

            if (!string.IsNullOrWhiteSpace(valorFinal))
                listaTransporte = listaTransporte.Where(le => le.valorParticipacao <= Convert.ToDecimal(valorFinal.Trim())).ToList();

            if (!string.IsNullOrWhiteSpace(vagasDisponiveis))
                listaTransporte = listaTransporte.Where(le => le.quantidadeVagasDisponiveis >= Convert.ToDecimal(vagasDisponiveis.Trim())).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, listaTransporte);
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
                .Where(le => le.idUsuarioTransportador == LoggedUserInformation.getUserId(User.Identity)
                || (le.Passageiros != null ? le.Passageiros.Exists(p => p.idUsuario == LoggedUserInformation.getUserId(User.Identity)) : true)
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
                .Where(le => le.idUsuarioTransportador == LoggedUserInformation.getUserId(User.Identity)
                || (le.Passageiros != null ? le.Passageiros.Exists(p => p.idUsuario == LoggedUserInformation.getUserId(User.Identity)) : true)
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

                eventoTransporteModel.idUsuarioPassageiro = LoggedUserInformation.getUserId(User.Identity);
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

                return Request.CreateResponse(HttpStatusCode.OK, appEventoTransportePassageiro.Excluir(idEventoTransporte, LoggedUserInformation.getUserId(User.Identity)));
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

                avaliacaoTransporteModel.idUsuarioAvaliador = LoggedUserInformation.getUserId(User.Identity);
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
        [ActionName("EnviarMensagem")]
        public HttpResponseMessage EnviarMensagem([System.Web.Http.FromBody] ChatUsuarioEventoTransporte chatUsuarioEventoTransporte)
        {
            try
            {
                var appChatUusuarioEventoTransporte = new bllChatUsuarioEventoTransporte(DBRepository.GetChatUsuarioEventoTransporteRepository());

                chatUsuarioEventoTransporte.idUsuarioOrigem = LoggedUserInformation.getUserId(User.Identity);
                appChatUusuarioEventoTransporte.Cadastrar(ChatUsuarioEventoTransporte.ModelToEntity(chatUsuarioEventoTransporte));

                return Request.CreateResponse(HttpStatusCode.OK, chatUsuarioEventoTransporte);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("BuscarMensagensUsuario")]
        public HttpResponseMessage BuscarMensagensUsuario(int idEventoTransporte)
        {
            try
            {
                var appChatUusuarioEventoTransporte = new bllChatUsuarioEventoTransporte(DBRepository.GetChatUsuarioEventoTransporteRepository());

                var listaMensagens = appChatUusuarioEventoTransporte
                    .BuscarTodos()
                    ?.Where(chat => (chat.idUsuarioOrigem == LoggedUserInformation.getUserId(User.Identity)
                            || chat.idUsuarioDestino == LoggedUserInformation.getUserId(User.Identity))
                            && chat.idEventoTransporte == idEventoTransporte)
                    .Select(chatUsuarioEventoTransporte => ChatUsuarioEventoTransporte.EntityToModel(chatUsuarioEventoTransporte));

                return Request.CreateResponse(HttpStatusCode.OK, listaMensagens);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("BuscarCabecalhoMensagensUsuario")]
        public HttpResponseMessage BuscarCabecalhoMensagensEnviadasUsuario()
        {
            try
            {
                var appChatUusuarioEventoTransporte = new bllChatUsuarioEventoTransporte(DBRepository.GetChatUsuarioEventoTransporteRepository());

                var listaMensagens = appChatUusuarioEventoTransporte
                    .BuscarCabecalhoMensagensPorUsuario(LoggedUserInformation.getUserId(User.Identity))
                    ?.Select(chatUsuarioEventoTransporte => ChatUsuarioEventoTransporte.EntityToModel(chatUsuarioEventoTransporte))
                    .ToList();

                return Request.CreateResponse(HttpStatusCode.OK, listaMensagens);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("BuscarListaCidadeUfsExistentesEmEventoTransporte")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarListaCidadeUfsExistentesEmEventoTransporte()
        {
            try
            {
                bllEventoTransporte appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());

                var listaCidadesUfs = appEventoTransporte
                    .BuscarTodos()
                    .Select(eventoTransporte => String.Concat(eventoTransporte.enderecoPartidaCidade, ", ", eventoTransporte.enderecoPartidaUF))
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