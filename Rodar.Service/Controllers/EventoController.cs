using Newtonsoft.Json;
using Rodar.Service.Models;
using Rodar.Service.Providers;
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
    public class EventoController : ApiController
    {
        [HttpPost]
        [ActionName("Cadastrar")]
        public HttpResponseMessage Cadastrar([System.Web.Http.FromBody] Evento Evento)
        {
            using (SqlConnection con = DBConnection.GetDBConnection())
            {
                string stringSQL = @"INSERT INTO Evento(dataCriacao
                                                        ,idUsuarioCriacao
                                                        ,enderecoRua
                                                        ,enderecoComplemento
                                                        ,enderecoBairro
                                                        ,enderecoNumero
                                                        ,enderecoCEP
                                                        ,enderecoCidade
                                                        ,enderecoUF
                                                        ,urlImagemCapa
                                                        ,urlImagem1
                                                        ,urlImagem2
                                                        ,urlImagem3
                                                        ,urlImagem4
                                                        ,urlImagem5
                                                        ,dataHoraInicio
                                                        ,dataHoraTermino
                                                        ,descricaoEvento)
                                                    VALUES
                                                        (@dataCriacao
                                                        ,@idUsuarioCriacao
                                                        ,@enderecoRua
                                                        ,@enderecoComplemento
                                                        ,@enderecoBairro
                                                        ,@enderecoNumero
                                                        ,@enderecoCEP
                                                        ,@enderecoCidade
                                                        ,@enderecoUF
                                                        ,@urlImagemCapa
                                                        ,@urlImagem1
                                                        ,@urlImagem2
                                                        ,@urlImagem3
                                                        ,@urlImagem4
                                                        ,@urlImagem5
                                                        ,@dataHoraInicio
                                                        ,@dataHoraTermino
                                                        ,@descricaoEvento);
                                            SET @idEvento = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);

                ValidaCampos(ref Evento);

                cmdInserir.Parameters.Add("idEvento", SqlDbType.Int);
                cmdInserir.Parameters["idEvento"].Direction = ParameterDirection.Output;

                if (Evento.dataCriacao == null)
                    cmdInserir.Parameters.Add("dataCriacao", SqlDbType.VarChar).Value = DBNull.Value;
                else
                    cmdInserir.Parameters.Add("dataCriacao", SqlDbType.VarChar).Value = Evento.dataCriacao;

                cmdInserir.Parameters.Add("idUsuarioCriacao", SqlDbType.Int).Value = Evento.idUsuarioCriacao;
                cmdInserir.Parameters.Add("enderecoRua", SqlDbType.VarChar).Value = Evento.enderecoRua;
                cmdInserir.Parameters.Add("enderecoComplemento", SqlDbType.VarChar).Value = Evento.enderecoComplemento;
                cmdInserir.Parameters.Add("enderecoBairro", SqlDbType.VarChar).Value = Evento.enderecoBairro;
                cmdInserir.Parameters.Add("enderecoNumero", SqlDbType.Int).Value = Evento.enderecoNumero;
                cmdInserir.Parameters.Add("enderecoCEP", SqlDbType.VarChar).Value = Evento.enderecoCEP;
                cmdInserir.Parameters.Add("enderecoCidade", SqlDbType.VarChar).Value = Evento.enderecoCidade;
                cmdInserir.Parameters.Add("enderecoUF", SqlDbType.VarChar).Value = Evento.enderecoUF;
                cmdInserir.Parameters.Add("urlImagemCapa", SqlDbType.VarChar).Value = Evento.urlImagemCapa;
                cmdInserir.Parameters.Add("urlImagem1", SqlDbType.VarChar).Value = Evento.urlImagem1;
                cmdInserir.Parameters.Add("urlImagem2", SqlDbType.VarChar).Value = Evento.urlImagem2;
                cmdInserir.Parameters.Add("urlImagem3", SqlDbType.VarChar).Value = Evento.urlImagem3;
                cmdInserir.Parameters.Add("urlImagem4", SqlDbType.VarChar).Value = Evento.urlImagem4;
                cmdInserir.Parameters.Add("urlImagem5", SqlDbType.VarChar).Value = Evento.urlImagem5;

                if (Evento.dataHoraInicio == null)
                    cmdInserir.Parameters.Add("dataHoraInicio", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdInserir.Parameters.Add("dataHoraInicio", SqlDbType.DateTime).Value = Evento.dataHoraInicio.Value;

                if (Evento.dataHoraTermino == null)
                    cmdInserir.Parameters.Add("dataHoraTermino", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdInserir.Parameters.Add("dataHoraTermino", SqlDbType.DateTime).Value = Evento.dataHoraTermino;

                cmdInserir.Parameters.Add("descricaoEvento", SqlDbType.VarChar).Value = Evento.descricaoEvento;

                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();

                    return Request.CreateResponse(HttpStatusCode.OK, (int)cmdInserir.Parameters["idEvento"].Value);
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
        public HttpResponseMessage Atualizar([System.Web.Http.FromBody] Evento Evento)
        {
            using (SqlConnection con = DBConnection.GetDBConnection())
            {
                string stringSQL = @"UPDATE Evento
                                       SET dataCriacao = @dataCriacao
                                          ,idUsuarioCriacao = @idUsuarioCriacao
                                          ,enderecoRua = @enderecoRua
                                          ,enderecoComplemento = @enderecoComplemento
                                          ,enderecoBairro = @enderecoBairro
                                          ,enderecoNumero = @enderecoNumero
                                          ,enderecoCEP = @enderecoCEP
                                          ,enderecoCidade = @enderecoCidade
                                          ,enderecoUF = @enderecoUF
                                          ,urlImagemCapa = @urlImagemCapa
                                          ,urlImagem1 = @urlImagem1
                                          ,urlImagem2 = @urlImagem2
                                          ,urlImagem3 = @urlImagem3
                                          ,urlImagem4 = @urlImagem4
                                          ,urlImagem5 = @urlImagem5
                                          ,dataHoraInicio = @dataHoraInicio
                                          ,dataHoraTermino = @dataHoraTermino
                                          ,descricaoEvento = @descricaoEvento
                                     WHERE idEvento = @idEvento";

                SqlCommand cmdAtualizar = new SqlCommand(stringSQL, con);

                ValidaCampos(ref Evento);

                cmdAtualizar.Parameters.Add("idEvento", SqlDbType.Int).Value = Evento.idEvento;

                if (Evento.dataCriacao == null)
                    cmdAtualizar.Parameters.Add("dataCriacao", SqlDbType.VarChar).Value = DBNull.Value;
                else
                    cmdAtualizar.Parameters.Add("dataCriacao", SqlDbType.VarChar).Value = Evento.dataCriacao;

                cmdAtualizar.Parameters.Add("idUsuarioCriacao", SqlDbType.Int).Value = Evento.idUsuarioCriacao;
                cmdAtualizar.Parameters.Add("enderecoRua", SqlDbType.VarChar).Value = Evento.enderecoRua;
                cmdAtualizar.Parameters.Add("enderecoComplemento", SqlDbType.VarChar).Value = Evento.enderecoComplemento;
                cmdAtualizar.Parameters.Add("enderecoBairro", SqlDbType.VarChar).Value = Evento.enderecoBairro;
                cmdAtualizar.Parameters.Add("enderecoNumero", SqlDbType.Int).Value = Evento.enderecoNumero;
                cmdAtualizar.Parameters.Add("enderecoCEP", SqlDbType.VarChar).Value = Evento.enderecoCEP;
                cmdAtualizar.Parameters.Add("enderecoCidade", SqlDbType.VarChar).Value = Evento.enderecoCidade;
                cmdAtualizar.Parameters.Add("enderecoUF", SqlDbType.VarChar).Value = Evento.enderecoUF;
                cmdAtualizar.Parameters.Add("urlImagemCapa", SqlDbType.VarChar).Value = Evento.urlImagemCapa;
                cmdAtualizar.Parameters.Add("urlImagem1", SqlDbType.VarChar).Value = Evento.urlImagem1;
                cmdAtualizar.Parameters.Add("urlImagem2", SqlDbType.VarChar).Value = Evento.urlImagem2;
                cmdAtualizar.Parameters.Add("urlImagem3", SqlDbType.VarChar).Value = Evento.urlImagem3;
                cmdAtualizar.Parameters.Add("urlImagem4", SqlDbType.VarChar).Value = Evento.urlImagem4;
                cmdAtualizar.Parameters.Add("urlImagem5", SqlDbType.VarChar).Value = Evento.urlImagem5;

                if (Evento.dataHoraInicio == null)
                    cmdAtualizar.Parameters.Add("dataNascimento", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdAtualizar.Parameters.Add("dataHoraInicio", SqlDbType.DateTime).Value = Evento.dataHoraInicio.Value;

                if (Evento.dataHoraTermino == null)
                    cmdAtualizar.Parameters.Add("dataHoraTermino", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdAtualizar.Parameters.Add("dataHoraTermino", SqlDbType.DateTime).Value = Evento.dataHoraTermino;

                cmdAtualizar.Parameters.Add("descricaoEvento", SqlDbType.VarChar).Value = Evento.descricaoEvento;

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
        public HttpResponseMessage Buscar(int id)
        {
            Evento evento = new Evento();

            using (SqlConnection con = DBConnection.GetDBConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM Evento
                                        WHERE idEvento = @idEvento";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idEvento", SqlDbType.Int).Value = id;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.Read())
                        PreencheCampos(drSelecao, ref evento);
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

            return Request.CreateResponse(HttpStatusCode.OK, evento);
        }

        [HttpDelete]
        [ActionName("Excluir")]
        public HttpResponseMessage Excluir(int id)
        {
            using (SqlConnection con = DBConnection.GetDBConnection())
            {
                string stringSQL = @"DELETE FROM Evento
                                        WHERE idEvento = @idEvento";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, con);

                cmdDeletar.Parameters.Add("idEvento", SqlDbType.Int).Value = id;

                try
                {
                    con.Open();

                    return Request.CreateResponse(HttpStatusCode.OK, (int)cmdDeletar.ExecuteNonQuery());
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
        [ActionName("BuscarTodos")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage BuscarTodos()
        {
            List<Evento> eventos = new List<Evento>();

            using (SqlConnection con = DBConnection.GetDBConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM Evento";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    while (drSelecao.Read())
                    {
                        Evento evento = new Evento();

                        PreencheCampos(drSelecao, ref evento);

                        eventos.Add(evento);
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

            return Request.CreateResponse(HttpStatusCode.OK, eventos);
        }

        private static void PreencheCampos(SqlDataReader drSelecao, ref Evento evento)
        {
            if (drSelecao["idEvento"] != DBNull.Value)
                evento.idEvento = Convert.ToInt32(drSelecao["idEvento"].ToString());

            if (drSelecao["dataCriacao"] != DBNull.Value)
                evento.dataCriacao = Convert.ToDateTime(drSelecao["dataCriacao"]);
            else
                evento.dataCriacao = null;

            if (drSelecao["idUsuarioCriacao"] != DBNull.Value)
                evento.idUsuarioCriacao = Convert.ToInt32(drSelecao["idUsuarioCriacao"].ToString());

            if (drSelecao["enderecoRua"] != DBNull.Value)
                evento.enderecoRua = drSelecao["enderecoRua"].ToString();

            if (drSelecao["enderecoComplemento"] != DBNull.Value)
                evento.enderecoComplemento = drSelecao["enderecoComplemento"].ToString();

            if (drSelecao["enderecoBairro"] != DBNull.Value)
                evento.enderecoBairro = drSelecao["enderecoBairro"].ToString();

            if (drSelecao["enderecoNumero"] != DBNull.Value)
                evento.enderecoNumero = Convert.ToInt32(drSelecao["enderecoNumero"].ToString());

            if (drSelecao["enderecoCEP"] != DBNull.Value)
                evento.enderecoCEP = drSelecao["enderecoCEP"].ToString();

            if (drSelecao["enderecoCidade"] != DBNull.Value)
                evento.enderecoCidade = drSelecao["enderecoCidade"].ToString();

            if (drSelecao["enderecoUF"] != DBNull.Value)
                evento.enderecoUF = drSelecao["enderecoUF"].ToString();

            if (drSelecao["urlImagemCapa"] != DBNull.Value)
                evento.urlImagemCapa = drSelecao["urlImagemCapa"].ToString();

            if (drSelecao["urlImagem1"] != DBNull.Value)
                evento.urlImagem1 = drSelecao["urlImagem1"].ToString();

            if (drSelecao["urlImagem2"] != DBNull.Value)
                evento.urlImagem2 = drSelecao["urlImagem2"].ToString();

            if (drSelecao["urlImagem3"] != DBNull.Value)
                evento.urlImagem3 = drSelecao["urlImagem3"].ToString();

            if (drSelecao["urlImagem4"] != DBNull.Value)
                evento.urlImagem4 = drSelecao["urlImagem4"].ToString();

            if (drSelecao["urlImagem5"] != DBNull.Value)
                evento.urlImagem5 = drSelecao["urlImagem5"].ToString();

            if (drSelecao["dataHoraInicio"] != DBNull.Value)
                evento.dataHoraInicio = Convert.ToDateTime(drSelecao["dataHoraInicio"]);
            else
                evento.dataHoraInicio = null;

            if (drSelecao["dataHoraTermino"] != DBNull.Value)
                evento.dataHoraTermino = Convert.ToDateTime(drSelecao["dataHoraTermino"]);
            else
                evento.dataHoraTermino = null;

            if (drSelecao["descricaoEvento"] != DBNull.Value)
                evento.descricaoEvento = drSelecao["descricaoEvento"].ToString();
        }

        private static void ValidaCampos(ref Evento evento)
        {
            if (String.IsNullOrEmpty(evento.enderecoRua)) { evento.enderecoRua = String.Empty; }
            if (String.IsNullOrEmpty(evento.enderecoComplemento)) { evento.enderecoComplemento = String.Empty; }
            if (String.IsNullOrEmpty(evento.enderecoBairro)) { evento.enderecoBairro = String.Empty; }
            if (String.IsNullOrEmpty(evento.enderecoCEP)) { evento.enderecoCEP = String.Empty; }
            if (String.IsNullOrEmpty(evento.enderecoCidade)) { evento.enderecoCidade = String.Empty; }
            if (String.IsNullOrEmpty(evento.enderecoUF)) { evento.enderecoUF = String.Empty; }
            if (String.IsNullOrEmpty(evento.urlImagemCapa)) { evento.urlImagemCapa = String.Empty; }
            if (String.IsNullOrEmpty(evento.urlImagem1)) { evento.urlImagem1 = String.Empty; }
            if (String.IsNullOrEmpty(evento.urlImagem2)) { evento.urlImagem2 = String.Empty; }
            if (String.IsNullOrEmpty(evento.urlImagem3)) { evento.urlImagem3 = String.Empty; }
            if (String.IsNullOrEmpty(evento.urlImagem4)) { evento.urlImagem4 = String.Empty; }
            if (String.IsNullOrEmpty(evento.urlImagem5)) { evento.urlImagem5 = String.Empty; }
            if (String.IsNullOrEmpty(evento.descricaoEvento)) { evento.descricaoEvento = String.Empty; }
        }
    }
}