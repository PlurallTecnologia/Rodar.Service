using Newtonsoft.Json;
using Rodar.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Services;

namespace Rodar.Service.Controllers
{
    public class UsuarioController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["rodarDB"].ToString());

        [HttpPost]
        [ActionName("Cadastrar")]
        public HttpResponseMessage Cadastrar([System.Web.Http.FromBody] Usuario Usuario)
        {
            using (con)
            {
                string stringSQL = @"INSERT INTO Usuario(nomeCompleto, RG, CPF, urlImagemSelfie, Genero, Descricao, dataNascimento, Email, numeroTelefone, Senha, facebookId, googleId, urlImagemRGFrente, urlImagemRGTras, urlImagemCPF)
                                            VALUES(@nomeCompleto, @RG, @CPF, @urlImagemSelfie, @Genero, @Descricao, @dataNascimento, @Email, @numeroTelefone, @Senha, @facebookId, @googleId, @urlImagemRGFrente, @urlImagemRGTras, @urlImagemCPF);
                                            SET @idUsuario = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);

                ValidaCampos(ref Usuario);

                cmdInserir.Parameters.Add("idUsuario", SqlDbType.Int);
                cmdInserir.Parameters["idUsuario"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("nomeCompleto", SqlDbType.VarChar).Value = Usuario.nomeCompleto;
                cmdInserir.Parameters.Add("RG", SqlDbType.VarChar).Value = Usuario.RG;
                cmdInserir.Parameters.Add("CPF", SqlDbType.VarChar).Value = Usuario.CPF;
                cmdInserir.Parameters.Add("urlImagemSelfie", SqlDbType.VarChar).Value = Usuario.urlImagemSelfie;
                cmdInserir.Parameters.Add("Genero", SqlDbType.VarChar).Value = Usuario.Genero;
                cmdInserir.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Usuario.Descricao;

                if (Usuario.dataNascimento == null)
                    cmdInserir.Parameters.Add("dataNascimento", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdInserir.Parameters.Add("dataNascimento", SqlDbType.DateTime).Value = Usuario.dataNascimento.Value;

                cmdInserir.Parameters.Add("Email", SqlDbType.VarChar).Value = Usuario.Email;
                cmdInserir.Parameters.Add("numeroTelefone", SqlDbType.VarChar).Value = Usuario.numeroTelefone;
                cmdInserir.Parameters.Add("Senha", SqlDbType.VarChar).Value = Usuario.Senha;
                cmdInserir.Parameters.Add("facebookId", SqlDbType.VarChar).Value = Usuario.facebookId;
                cmdInserir.Parameters.Add("googleId", SqlDbType.VarChar).Value = Usuario.googleId;
                cmdInserir.Parameters.Add("urlImagemRGTras", SqlDbType.VarChar).Value = Usuario.urlImagemRGTras;
                cmdInserir.Parameters.Add("urlImagemRGFrente", SqlDbType.VarChar).Value = Usuario.urlImagemRGFrente;
                cmdInserir.Parameters.Add("urlImagemCPF", SqlDbType.VarChar).Value = Usuario.urlImagemCPF;

                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();

                    return Request.CreateResponse(HttpStatusCode.OK, (int)cmdInserir.Parameters["idUsuario"].Value);
                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        [HttpPost]
        [ActionName("Atualizar")]
        public HttpResponseMessage Atualizar([System.Web.Http.FromBody] Usuario Usuario)
        {
            using (con)
            {
                string stringSQL = @"UPDATE Usuario
                                   SET nomeCompleto = @nomeCompleto
                                      ,RG = @RG
                                      ,CPF = @CPF
                                      ,urlImagemSelfie = @urlImagemSelfie
                                      ,Genero = @Genero
                                      ,Descricao = @Descricao
                                      ,dataNascimento = @dataNascimento
                                      ,Email = @Email
                                      ,numeroTelefone = @numeroTelefone
                                      ,Senha = @Senha
                                      ,facebookId = @facebookId
                                      ,googleId = @googleId
                                      ,urlImagemRGFrente = @urlImagemRGFrente
                                      ,urlImagemRGTras = @urlImagemRGTras
                                      ,urlImagemCPF = @urlImagemCPF
                                 WHERE Email = @Email ";

                SqlCommand cmdAtualizar = new SqlCommand(stringSQL, con);

                ValidaCampos(ref Usuario);

                cmdAtualizar.Parameters.Add("idUsuario", SqlDbType.Int).Value = Usuario.idUsuario;

                cmdAtualizar.Parameters.Add("nomeCompleto", SqlDbType.VarChar).Value = Usuario.nomeCompleto;
                cmdAtualizar.Parameters.Add("RG", SqlDbType.VarChar).Value = Usuario.RG;
                cmdAtualizar.Parameters.Add("CPF", SqlDbType.VarChar).Value = Usuario.CPF;
                cmdAtualizar.Parameters.Add("urlImagemSelfie", SqlDbType.VarChar).Value = Usuario.urlImagemSelfie;
                cmdAtualizar.Parameters.Add("Genero", SqlDbType.VarChar).Value = Usuario.Genero;
                cmdAtualizar.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Usuario.Descricao;

                if (Usuario.dataNascimento == null)
                    cmdAtualizar.Parameters.Add("dataNascimento", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdAtualizar.Parameters.Add("dataNascimento", SqlDbType.DateTime).Value = Usuario.dataNascimento.Value; cmdAtualizar.Parameters.Add("Email", SqlDbType.VarChar).Value = Usuario.Email;

                cmdAtualizar.Parameters.Add("numeroTelefone", SqlDbType.VarChar).Value = Usuario.numeroTelefone;
                cmdAtualizar.Parameters.Add("Senha", SqlDbType.VarChar).Value = Usuario.Senha;
                cmdAtualizar.Parameters.Add("facebookId", SqlDbType.VarChar).Value = Usuario.facebookId;
                cmdAtualizar.Parameters.Add("googleId", SqlDbType.VarChar).Value = Usuario.googleId;
                cmdAtualizar.Parameters.Add("urlImagemRGTras", SqlDbType.VarChar).Value = Usuario.urlImagemRGTras;
                cmdAtualizar.Parameters.Add("urlImagemRGFrente", SqlDbType.VarChar).Value = Usuario.urlImagemRGFrente;
                cmdAtualizar.Parameters.Add("urlImagemCPF", SqlDbType.VarChar).Value = Usuario.urlImagemCPF;

                try
                {
                    con.Open();
                    cmdAtualizar.ExecuteNonQuery();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        [HttpGet]
        [ActionName("Buscar")]
        public HttpResponseMessage Get(int id)
        {
            Usuario usuario = new Usuario();

            using (con)
            {
                string stringSQL = @"SELECT *
                                    FROM Usuario
                                    WHERE idUsuario = @idUsuario";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idUsuario", SqlDbType.Int).Value = id;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.Read())
                        PreencheCampos(drSelecao, ref usuario);
                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        [HttpGet]
        [ActionName("BuscarTodos")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (con)
            {
                string stringSQL = @"SELECT *
                                    FROM Usuario";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    while (drSelecao.Read())
                    {
                        Usuario usuario = new Usuario();

                        PreencheCampos(drSelecao, ref usuario);

                        usuarios.Add(usuario);
                    }
                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, usuarios);
        }

        private static void PreencheCampos(SqlDataReader drSelecao, ref Usuario usuario)
        {
            if (drSelecao["idUsuario"] != DBNull.Value)
                usuario.idUsuario = Convert.ToInt32(drSelecao["idUsuario"].ToString());

            if (drSelecao["dataNascimento"] != DBNull.Value)
                usuario.dataNascimento = Convert.ToDateTime(drSelecao["dataNascimento"]);
            else
                usuario.dataNascimento = null;

            if (drSelecao["RG"] != DBNull.Value)
                usuario.RG = drSelecao["RG"].ToString();

            if (drSelecao["CPF"] != DBNull.Value)
                usuario.CPF = drSelecao["CPF"].ToString();

            if (drSelecao["urlImagemSelfie"] != DBNull.Value)
                usuario.urlImagemSelfie = drSelecao["urlImagemSelfie"].ToString();

            if (drSelecao["Genero"] != DBNull.Value)
                usuario.Genero = drSelecao["Genero"].ToString();

            if (drSelecao["Descricao"] != DBNull.Value)
                usuario.Descricao = drSelecao["Descricao"].ToString();

            if (drSelecao["Email"] != DBNull.Value)
                usuario.Email = drSelecao["Email"].ToString();

            if (drSelecao["numeroTelefone"] != DBNull.Value)
                usuario.numeroTelefone = drSelecao["numeroTelefone"].ToString();

            if (drSelecao["Senha"] != DBNull.Value)
                usuario.Senha = drSelecao["Senha"].ToString();

            if (drSelecao["facebookId"] != DBNull.Value)
                usuario.facebookId = drSelecao["facebookId"].ToString();

            if (drSelecao["googleId"] != DBNull.Value)
                usuario.googleId = drSelecao["googleId"].ToString();

            if (drSelecao["urlImagemRGFrente"] != DBNull.Value)
                usuario.urlImagemRGFrente = drSelecao["urlImagemRGFrente"].ToString();

            if (drSelecao["urlImagemRGTras"] != DBNull.Value)
                usuario.urlImagemRGTras = drSelecao["urlImagemRGTras"].ToString();

            if (drSelecao["urlImagemCPF"] != DBNull.Value)
                usuario.urlImagemCPF = drSelecao["urlImagemCPF"].ToString();
        }

        private static void ValidaCampos(ref Usuario usuario)
        {
            if (String.IsNullOrEmpty(usuario.CPF)) { usuario.CPF = String.Empty; }
            if (String.IsNullOrEmpty(usuario.Descricao)) { usuario.Descricao = String.Empty; }
            if (String.IsNullOrEmpty(usuario.Email)) { usuario.Email = String.Empty; }
            if (String.IsNullOrEmpty(usuario.facebookId)) { usuario.facebookId = String.Empty; }
            if (String.IsNullOrEmpty(usuario.Genero)) { usuario.Genero = String.Empty; }
            if (String.IsNullOrEmpty(usuario.googleId)) { usuario.googleId = String.Empty; }
            if (String.IsNullOrEmpty(usuario.nomeCompleto)) { usuario.nomeCompleto = String.Empty; }
            if (String.IsNullOrEmpty(usuario.numeroTelefone)) { usuario.numeroTelefone = String.Empty; }
            if (String.IsNullOrEmpty(usuario.RG)) { usuario.RG = String.Empty; }
            if (String.IsNullOrEmpty(usuario.Senha)) { usuario.Senha = String.Empty; }
            if (String.IsNullOrEmpty(usuario.urlImagemCPF)) { usuario.urlImagemCPF = String.Empty; }
            if (String.IsNullOrEmpty(usuario.urlImagemRGFrente)) { usuario.urlImagemRGFrente = String.Empty; }
            if (String.IsNullOrEmpty(usuario.urlImagemRGTras)) { usuario.urlImagemRGTras = String.Empty; }
            if (String.IsNullOrEmpty(usuario.urlImagemSelfie)) { usuario.urlImagemSelfie = String.Empty; }
        }
    }
}
