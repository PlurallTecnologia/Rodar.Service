using Rodar.Business;
using Rodar.Service.Globals;
using Rodar.Service.Models;
using Rodar.Service.Providers;
using System;
using System.Collections.Generic;
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

                evento.idUsuarioCriacao = LoggedUserInformation.getUserId(User.Identity);

                return Request.CreateResponse(HttpStatusCode.OK, appEvento.Cadastrar(Evento.ModelToEntity(evento)));
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

                var idUsuario = (somenteMeusEventos ? LoggedUserInformation.getUserId(User.Identity) : 0);
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

        [HttpPost]
        [ActionName("EnviarFoto")]
        public HttpResponseMessage EnviarFoto(int idEvento)
        {
            Dictionary<string, object> dictionaryErros = new Dictionary<string, object>();

            //var fileLog = HttpContext.Current.Server.MapPath("~/Log/Log.txt");
            //File.AppendAllText(fileLog, $"MediaType: {Request.Content.Headers.ContentType.MediaType}");

            if (!Request.Content.IsMimeMultipartContent("form-data"))
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png", "bmp" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();

                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            var message = string.Format("Por favor envie imagens com o formato .jpg, .jpeg, .gif, .png, .bmp");

                            dictionaryErros.Add("Erro", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dictionaryErros);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {
                            var message = string.Format("Please Upload a file upto 1 mb.");
                            dictionaryErros.Add("Erro", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dictionaryErros);
                        }
                        else
                        {
                            var fileExtension = Path.GetExtension(postedFile.FileName);
                            var urlImagemEvento = $"evento_{idEvento}{fileExtension}";

                            //if (!Directory.Exists(HttpContext.Current.Server.MapPath($"~/EventImages/"))

                            var filePath = HttpContext.Current.Server.MapPath($"~/{urlImagemEvento}");

                            if (File.Exists(filePath))
                                File.Delete(filePath);

                            postedFile.SaveAs(filePath);

                            var appEvento = new bllEvento(DBRepository.GetEventoRepository());
                            var evento = appEvento.Buscar(idEvento);
                            evento.urlImagemCapa = urlImagemEvento;
                            appEvento.Atualizar(evento);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                //File.AppendAllText(fileLog, $"Erro enviar foto evento: {Ex.Message}");
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Erro enviar foto evento: {Ex.Message}");
            }

            return Request.CreateResponse(HttpStatusCode.Created, "Imagens enviadas com sucesso"); ;
        }
    }
}