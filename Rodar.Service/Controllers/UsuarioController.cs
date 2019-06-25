using Rodar.Business;
using Rodar.Service.Globals;
using Rodar.Service.Models;
using Rodar.Service.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Rodar.Service.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        [HttpPost]
        [ActionName("Cadastrar")]
        [AllowAnonymous]
        public HttpResponseMessage Cadastrar([System.Web.Http.FromBody] Usuario usuario)
        {
            try
            {
                bllUsuario appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());

                appUsuario.Cadastrar(Usuario.ModelToEntity(usuario));

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpPost]
        [ActionName("Atualizar")]
        public HttpResponseMessage Atualizar([System.Web.Http.FromBody] Usuario usuario)
        {
            try
            {
                bllUsuario appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());

                appUsuario.Atualizar(Usuario.ModelToEntity(usuario));

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpPost]
        [ActionName("PromoverParaTransportador")]
        public HttpResponseMessage PromoverParaTransportador()
        {
            try
            {
                bllUsuario appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());

                appUsuario.PromoverParaTransportador(LoggedUserInformation.getUserId(User.Identity));

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpPost]
        [ActionName("PromoverParaOrganizadorEvento")]
        public HttpResponseMessage PromoverParaOrganizadorEvento()
        {
            try
            {
                bllUsuario appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());

                appUsuario.PromoverParaOrganizadorEvento(LoggedUserInformation.getUserId(User.Identity));

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpGet]
        [ActionName("Buscar")]
        public HttpResponseMessage Buscar()
        {
            try
            {
                bllUsuario appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());

                var identity = (ClaimsIdentity)User.Identity;
                var teste = identity.Name;

                //return Request.CreateResponse(HttpStatusCode.OK, Usuario.EntityToModel(appUsuario.bus BuscarPorEmail(LoggedUserInformation.userEmail)));
                return Request.CreateResponse(HttpStatusCode.OK, Usuario.EntityToModel(appUsuario.BuscarPorId(LoggedUserInformation.getUserId(User.Identity))));
            }
            catch (Exception Ex)
            {
                var fileLog = HttpContext.Current.Server.MapPath("~/Log/Log.txt");
                File.AppendAllText(fileLog, Ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        [HttpPost]
        [ActionName("EnviarSelfie")]
        public HttpResponseMessage EnviarSelfie()
        {
            Dictionary<string, object> dictionaryErros = new Dictionary<string, object>();
            var urlImagemSelfie = string.Empty;
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
                            urlImagemSelfie = $"selfie_{LoggedUserInformation.getUserId(User.Identity)}{fileExtension}";

                            var filePath = HttpContext.Current.Server.MapPath($"~/UserImages/{urlImagemSelfie}");

                            if (File.Exists(filePath))
                                File.Delete(filePath);

                            postedFile.SaveAs(filePath);

                            var appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());
                            var usuario = appUsuario.BuscarPorId(LoggedUserInformation.getUserId(User.Identity));
                            usuario.urlImagemSelfie = urlImagemSelfie;
                            appUsuario.Atualizar(usuario);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                //File.AppendAllText(fileLog, $"Erro enviar foto do usuário: {Ex.Message}");
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Erro enviar foto evento: {Ex.Message}");
            }

            return Request.CreateResponse(HttpStatusCode.Created, urlImagemSelfie); ;
        }

        [HttpPost]
        [ActionName("AtualizarTokenNotificacao")]
        public HttpResponseMessage AtualizarTokenNotificacao(string novoTokenNotificacao)
        {
            try
            {
                bllUsuario appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());

                appUsuario.AtualizarTokenNotificacao(LoggedUserInformation.getUserId(User.Identity), novoTokenNotificacao);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        //public async Task<HttpResponseMessage> EnviarSelfie()
        //{
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    try
        //    {
        //        var httpRequest = HttpContext.Current.Request;

        //        foreach (string file in httpRequest.Files)
        //        {
        //            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

        //            var postedFile = httpRequest.Files[file];
        //            if (postedFile != null && postedFile.ContentLength > 0)
        //            {

        //                int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

        //                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
        //                var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
        //                var extension = ext.ToLower();
        //                if (!AllowedFileExtensions.Contains(extension))
        //                {
        //                    var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

        //                    dict.Add("error", message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
        //                }
        //                else if (postedFile.ContentLength > MaxContentLength)
        //                {

        //                    var message = string.Format("Please Upload a file upto 1 mb.");

        //                    dict.Add("error", message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
        //                }
        //                else
        //                {
        //                    var filePath = HttpContext.Current.Server.MapPath("~/Userimages/" + postedFile.FileName + extension);
        //                    postedFile.SaveAs(filePath);
        //                }
        //            }

        //            var message1 = string.Format("Image Updated Successfully.");

        //            return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
        //        }

        //        var res = string.Format("Please Upload a image.");
        //        dict.Add("error", res);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        //    }
        //    catch (Exception ex)
        //    {
        //        var res = string.Format("some Message");
        //        dict.Add("error", res);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        //    }
        //}

    }
}
