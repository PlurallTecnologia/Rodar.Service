using FirebaseAdmin.Messaging;
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
using System.Threading.Tasks;
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

                eventoCarona.idUsuarioMotorista = LoggedUserInformation.getUserId(User.Identity);
                eventoCarona.idEventoCarona = appEventoCarona.Cadastrar(EventoCarona.ModelToEntity(eventoCarona));

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
                //var fileLog = HttpContext.Current.Server.MapPath("~/Log/Log.txt");
                //File.AppendAllText(fileLog, $"Entrei excluir Carona");

                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                var topic = $"carona{idEventoCarona}";

                var message = new Message()
                {
                    Notification = new Notification()
                    {
                        Title = "CARONA EXCLUIDA",
                        Body = $"A CARONA {idEventoCarona} FOI EXCLUIDA COM SUCESSO",
                    },
                    Topic = topic,
                };

                var response = Task.Factory.StartNew(() => FirebaseMessaging.DefaultInstance.SendAsync(message)) ;

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
                    .Where(le => le.idUsuarioMotorista == LoggedUserInformation.getUserId(User.Identity))
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
        public HttpResponseMessage BuscarPorEvento(int idEvento,
                                                    string cidadeUfPartida = null,
                                                    string dataPartida = null,
                                                    string valorInicial = null,
                                                    string valorFinal = null,
                                                    string vagasDisponiveis = null)
        {
            try
            {
                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                var listaCaronas = appEventoCarona
                    .BuscarPorEvento(idEvento)?
                    .Select(eventoCarona => EventoCarona.EntityToModel(eventoCarona))
                    .ToList();

                if (!string.IsNullOrWhiteSpace(cidadeUfPartida))
                {
                    var cidadeUf = cidadeUfPartida.Split(',');
                    listaCaronas = listaCaronas.Where(le => le.enderecoPartidaCidade.ToUpper().Contains(cidadeUf[0].ToUpper().Trim()) && le.enderecoPartidaUF.ToUpper().Contains(cidadeUf[1].ToUpper().Trim())).ToList();
                }

                if (!string.IsNullOrWhiteSpace(dataPartida))
                    listaCaronas = listaCaronas.Where(le => le.dataHoraPartida.Value == Convert.ToDateTime(dataPartida.Trim())).ToList();

                if (!string.IsNullOrWhiteSpace(valorInicial))
                    listaCaronas = listaCaronas.Where(le => le.valorParticipacao >= Convert.ToDecimal(valorInicial.Trim())).ToList();

                if (!string.IsNullOrWhiteSpace(valorFinal))
                    listaCaronas = listaCaronas.Where(le => le.valorParticipacao <= Convert.ToDecimal(valorFinal.Trim())).ToList();

                if (!string.IsNullOrWhiteSpace(vagasDisponiveis))
                    listaCaronas = listaCaronas.Where(le => le.quantidadeVagasDisponiveis >= Convert.ToDecimal(vagasDisponiveis.Trim())).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, listaCaronas);
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
                    .Where(le => le.idUsuarioMotorista == LoggedUserInformation.getUserId(User.Identity)
                    || (le.Passageiros != null ? le.Passageiros.Exists(p => p.idUsuario == LoggedUserInformation.getUserId(User.Identity)) : true)
                    && le.dataHoraPrevisaoChegada >= DateTime.Now)
                    .ToList();

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
                    .Where(le => le.idUsuarioMotorista == LoggedUserInformation.getUserId(User.Identity)
                    || (le.Passageiros != null ? le.Passageiros.Exists(p => p.idUsuario == LoggedUserInformation.getUserId(User.Identity)) : true)
                    && le.dataHoraPrevisaoChegada <= DateTime.Now)
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

                eventoCaronaModel.idUsuarioPassageiro = LoggedUserInformation.getUserId(User.Identity);
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

                return Request.CreateResponse(HttpStatusCode.OK, appEventoCaronaPassageiro.Excluir(idEventoCarona, LoggedUserInformation.getUserId(User.Identity)));
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpPost]
        [ActionName("AvaliarCarona")]
        public HttpResponseMessage AvaliarCarona([System.Web.Http.FromBody] AvaliacaoCarona avaliacaoCarona)
        {
            try
            {
                var appAvaliacaoCarona = new bllAvaliacaoCarona(DBRepository.GetAvaliacaoCaronaRepository());
                var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                var avaliacaoCaronaModel = new AvaliacaoCarona();

                avaliacaoCaronaModel.idUsuarioAvaliador = LoggedUserInformation.getUserId(User.Identity);
                avaliacaoCaronaModel.idUsuarioAvaliado = appEventoCarona.Buscar(avaliacaoCarona.idEventoCarona).idUsuarioMotorista;
                avaliacaoCaronaModel.Avaliacao = avaliacaoCarona.Avaliacao;
                avaliacaoCaronaModel.idEventoCarona = avaliacaoCarona.idEventoCarona;
                avaliacaoCaronaModel.Mensagem = avaliacaoCarona.Mensagem;

                appAvaliacaoCarona.Cadastrar(AvaliacaoCarona.ModelToEntity(avaliacaoCaronaModel));

                return Request.CreateResponse(HttpStatusCode.OK, avaliacaoCaronaModel);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpPost]
        [ActionName("EnviarMensagem")]
        public HttpResponseMessage EnviarMensagem([System.Web.Http.FromBody] ChatUsuarioEventoCarona chatUsuarioEventoCarona)
        {
            try
            {
                var appChatUusuarioEventoCarona = new bllChatUsuarioEventoCarona(DBRepository.GetChatUsuarioEventoCaronaRepository());

                chatUsuarioEventoCarona.idUsuarioOrigem = LoggedUserInformation.getUserId(User.Identity);
                appChatUusuarioEventoCarona.Cadastrar(ChatUsuarioEventoCarona.ModelToEntity(chatUsuarioEventoCarona));

                return Request.CreateResponse(HttpStatusCode.OK, chatUsuarioEventoCarona);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("BuscarMensagensUsuario")]
        public HttpResponseMessage BuscarMensagensUsuario(int idEventoCarona)
        {
            try
            {
                var appChatUusuarioEventoCarona = new bllChatUsuarioEventoCarona(DBRepository.GetChatUsuarioEventoCaronaRepository());

                var listaMensagens = appChatUusuarioEventoCarona
                    .BuscarTodos()
                    ?.Where(chat => (chat.idUsuarioOrigem == LoggedUserInformation.getUserId(User.Identity) 
                            || chat.idUsuarioDestino == LoggedUserInformation.getUserId(User.Identity))
                            && chat.idEventoCarona == idEventoCarona)
                    .Select(chatUsuarioEventoCarona => ChatUsuarioEventoCarona.EntityToModel(chatUsuarioEventoCarona));

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
                var appChatUusuarioEventoCarona = new bllChatUsuarioEventoCarona(DBRepository.GetChatUsuarioEventoCaronaRepository());

                var listaMensagens = appChatUusuarioEventoCarona
                    .BuscarCabecalhoMensagensPorUsuario(LoggedUserInformation.getUserId(User.Identity))
                    .Select(chatUsuarioEventoCarona => ChatUsuarioEventoCarona.EntityToModel(chatUsuarioEventoCarona))
                    .ToList();

                return Request.CreateResponse(HttpStatusCode.OK, listaMensagens);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("BuscarListaCidadeUfsExistentesEmEventoCarona")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarListaCidadeUfsExistentesEmEventoCarona()
        {
            try
            {
                bllEventoCarona appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

                var listaCidadesUfs = appEventoCarona
                    .BuscarTodos()
                    .Select(eventoCarona => String.Concat(eventoCarona.enderecoPartidaCidade, ", ", eventoCarona.enderecoPartidaUF))
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