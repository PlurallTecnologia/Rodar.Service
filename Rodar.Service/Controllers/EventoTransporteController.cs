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
    public class EventoTransporteController : ApiController
    {
        [HttpPost]
        [ActionName("Cadastrar")]
        public HttpResponseMessage Cadastrar([System.Web.Http.FromBody] EventoTransporte eventoTransporte)
        {
            using (SqlConnection con = DBConnection.GetDBConnection())
            {
                string stringSQL = @"INSERT INTO EventoTransporte
                                                (idEvento
                                                ,idUsuarioTransportador
                                                ,quantidadeVagas
                                                ,enderecoPartidaRua
                                                ,enderecoPartidaComplemento
                                                ,enderecoPartidaBairro
                                                ,enderecoPartidaNumero
                                                ,enderecoPartidaCEP
                                                ,enderecoPartidaCidade
                                                ,enderecoPartidaUF
                                                ,valorParticipacao
                                                ,Mensagem)
                                            VALUES
                                                (@idEvento
                                                ,@idUsuarioTransportador
                                                ,@quantidadeVagas
                                                ,@enderecoPartidaRua
                                                ,@enderecoPartidaComplemento
                                                ,@enderecoPartidaBairro
                                                ,@enderecoPartidaNumero
                                                ,@enderecoPartidaCEP
                                                ,@enderecoPartidaCidade
                                                ,@enderecoPartidaUF
                                                ,@valorParticipacao
                                                ,@Mensagem);
                                                SET @idEventoTransporte = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);

                ValidaCampos(ref eventoTransporte);

                cmdInserir.Parameters.Add("idEventoTransporte", SqlDbType.Int);
                cmdInserir.Parameters["idEventoTransporte"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("idEvento", SqlDbType.Int).Value = eventoTransporte.idEvento;
                cmdInserir.Parameters.Add("idUsuarioTransportador", SqlDbType.Int).Value = eventoTransporte.idUsuarioTransportador;

                cmdInserir.Parameters.Add("quantidadeVagas", SqlDbType.Int).Value = eventoTransporte.quantidadeVagas;

                cmdInserir.Parameters.Add("enderecoPartidaRua", SqlDbType.VarChar).Value = eventoTransporte.enderecoPartidaRua;
                cmdInserir.Parameters.Add("enderecoPartidaComplemento", SqlDbType.VarChar).Value = eventoTransporte.enderecoPartidaComplemento;
                cmdInserir.Parameters.Add("enderecoPartidaBairro", SqlDbType.VarChar).Value = eventoTransporte.enderecoPartidaBairro;
                cmdInserir.Parameters.Add("enderecoPartidaNumero", SqlDbType.Int).Value = eventoTransporte.enderecoPartidaNumero;
                cmdInserir.Parameters.Add("enderecoPartidaCEP", SqlDbType.VarChar).Value = eventoTransporte.enderecoPartidaCEP;
                cmdInserir.Parameters.Add("enderecoPartidaCidade", SqlDbType.VarChar).Value = eventoTransporte.enderecoPartidaCidade;
                cmdInserir.Parameters.Add("enderecoPartidaUF", SqlDbType.VarChar).Value = eventoTransporte.enderecoPartidaUF;
                cmdInserir.Parameters.Add("valorParticipacao", SqlDbType.Decimal).Value = eventoTransporte.valorParticipacao;
                cmdInserir.Parameters.Add("Mensagem", SqlDbType.VarChar).Value = eventoTransporte.Mensagem;

                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();

                    return Request.CreateResponse(HttpStatusCode.OK, (int)cmdInserir.Parameters["idEventoTransporte"].Value);
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
        public HttpResponseMessage Atualizar([System.Web.Http.FromBody] EventoTransporte eventoTransporte)
        {
            using (SqlConnection con = DBConnection.GetDBConnection())
            {
                string stringSQL = @"UPDATE EventoTransporte
                                       SET idEvento = idEvento
                                          ,idUsuarioMotorista = idUsuarioMotorista
                                          ,quantidadeVagas = quantidadeVagas
                                          ,enderecoPartidaRua = enderecoPartidaRua
                                          ,enderecoPartidaComplemento = enderecoPartidaComplemento
                                          ,enderecoPartidaBairro = enderecoPartidaBairro
                                          ,enderecoPartidaNumero = enderecoPartidaNumero
                                          ,enderecoPartidaCEP = enderecoPartidaCEP
                                          ,enderecoPartidaCidade = enderecoPartidaCidade
                                          ,enderecoPartidaUF = enderecoPartidaUF
                                          ,valorParticipacao = valorParticipacao
                                          ,Mensagem = Mensagem
                                     WHERE idEvento = @idEvento";

                SqlCommand cmdAtualizar = new SqlCommand(stringSQL, con);

                ValidaCampos(ref eventoTransporte);

                cmdAtualizar.Parameters.Add("idEventoTransporte", SqlDbType.Int).Value = eventoTransporte.idEventoTransporte;

                cmdAtualizar.Parameters.Add("idEvento", SqlDbType.Int).Value = eventoTransporte.idEvento;
                cmdAtualizar.Parameters.Add("idUsuarioTransportador", SqlDbType.Int).Value = eventoTransporte.idUsuarioTransportador;

                cmdAtualizar.Parameters.Add("quantidadeVagas", SqlDbType.Int).Value = eventoTransporte.quantidadeVagas;

                cmdAtualizar.Parameters.Add("enderecoPartidaRua", SqlDbType.VarChar).Value = eventoTransporte.enderecoPartidaRua;
                cmdAtualizar.Parameters.Add("enderecoPartidaComplemento", SqlDbType.VarChar).Value = eventoTransporte.enderecoPartidaComplemento;
                cmdAtualizar.Parameters.Add("enderecoPartidaBairro", SqlDbType.VarChar).Value = eventoTransporte.enderecoPartidaBairro;
                cmdAtualizar.Parameters.Add("enderecoPartidaNumero", SqlDbType.Int).Value = eventoTransporte.enderecoPartidaNumero;
                cmdAtualizar.Parameters.Add("enderecoPartidaCEP", SqlDbType.VarChar).Value = eventoTransporte.enderecoPartidaCEP;
                cmdAtualizar.Parameters.Add("enderecoPartidaCidade", SqlDbType.VarChar).Value = eventoTransporte.enderecoPartidaCidade;
                cmdAtualizar.Parameters.Add("enderecoPartidaUF", SqlDbType.VarChar).Value = eventoTransporte.enderecoPartidaUF;
                cmdAtualizar.Parameters.Add("valorParticipacao", SqlDbType.Decimal).Value = eventoTransporte.valorParticipacao;
                cmdAtualizar.Parameters.Add("Mensagem", SqlDbType.VarChar).Value = eventoTransporte.Mensagem;

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
            EventoTransporte eventoTransporte = new EventoTransporte();

            using (SqlConnection con = DBConnection.GetDBConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM EventoTransporte
                                        WHERE idEventoTransporte = @idEventoTransporte";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idEventoTransporte", SqlDbType.Int).Value = id;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.Read())
                        PreencheCampos(drSelecao, ref eventoTransporte);
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

            return Request.CreateResponse(HttpStatusCode.OK, eventoTransporte);
        }

        [HttpDelete]
        [ActionName("Excluir")]
        public HttpResponseMessage Excluir(int id)
        {
            using (SqlConnection con = DBConnection.GetDBConnection())
            {
                string stringSQL = @"DELETE FROM EventoTransporte
                                        WHERE idEventoTransporte = @idEventoTransporte";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, con);

                cmdDeletar.Parameters.Add("idEventoTransporte", SqlDbType.Int).Value = id;

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
            List<EventoTransporte> eventoTransportes = new List<EventoTransporte>();

            using (SqlConnection con = DBConnection.GetDBConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM EventoTransporte";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    while (drSelecao.Read())
                    {
                        EventoTransporte eventoTransporte = new EventoTransporte();

                        PreencheCampos(drSelecao, ref eventoTransporte);

                        eventoTransportes.Add(eventoTransporte);
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

            return Request.CreateResponse(HttpStatusCode.OK, eventoTransportes);
        }

        private static void PreencheCampos(SqlDataReader drSelecao, ref EventoTransporte eventoTransporte)
        {
            if (drSelecao["idEventoTransporte"] != DBNull.Value)
                eventoTransporte.idEventoTransporte = Convert.ToInt32(drSelecao["idEventoTransporte"].ToString());

            if (drSelecao["idEvento"] != DBNull.Value)
                eventoTransporte.idEvento = Convert.ToInt32(drSelecao["idEvento"].ToString());

            if (drSelecao["idUsuarioTransportador"] != DBNull.Value)
                eventoTransporte.idUsuarioTransportador = Convert.ToInt32(drSelecao["idUsuarioTransportador"].ToString());

            if (drSelecao["quantidadeVagas"] != DBNull.Value)
                eventoTransporte.quantidadeVagas = Convert.ToInt32(drSelecao["quantidadeVagas"].ToString());

            if (drSelecao["enderecoPartidaRua"] != DBNull.Value)
                eventoTransporte.enderecoPartidaRua = drSelecao["enderecoPartidaRua"].ToString();

            if (drSelecao["enderecoPartidaComplemento"] != DBNull.Value)
                eventoTransporte.enderecoPartidaComplemento = drSelecao["enderecoPartidaComplemento"].ToString();

            if (drSelecao["enderecoPartidaBairro"] != DBNull.Value)
                eventoTransporte.enderecoPartidaBairro = drSelecao["enderecoPartidaBairro"].ToString();

            if (drSelecao["enderecoPartidaNumero"] != DBNull.Value)
                eventoTransporte.enderecoPartidaNumero = Convert.ToInt32(drSelecao["enderecoPartidaNumero"].ToString());

            if (drSelecao["enderecoPartidaCEP"] != DBNull.Value)
                eventoTransporte.enderecoPartidaCEP = drSelecao["enderecoPartidaCEP"].ToString();

            if (drSelecao["enderecoPartidaCidade"] != DBNull.Value)
                eventoTransporte.enderecoPartidaCidade = drSelecao["enderecoPartidaCidade"].ToString();

            if (drSelecao["enderecoPartidaUF"] != DBNull.Value)
                eventoTransporte.enderecoPartidaUF = drSelecao["enderecoPartidaUF"].ToString();

            if (drSelecao["valorParticipacao"] != DBNull.Value)
                eventoTransporte.valorParticipacao = Convert.ToDecimal(drSelecao["valorParticipacao"].ToString());

            if (drSelecao["Mensagem"] != DBNull.Value)
                eventoTransporte.Mensagem = drSelecao["Mensagem"].ToString();
        }

        private static void ValidaCampos(ref EventoTransporte eventoTransporte)
        {
            if (String.IsNullOrEmpty(eventoTransporte.enderecoPartidaRua)) { eventoTransporte.enderecoPartidaRua = String.Empty; }
            if (String.IsNullOrEmpty(eventoTransporte.enderecoPartidaComplemento)) { eventoTransporte.enderecoPartidaComplemento = String.Empty; }
            if (String.IsNullOrEmpty(eventoTransporte.enderecoPartidaBairro)) { eventoTransporte.enderecoPartidaBairro = String.Empty; }
            if (String.IsNullOrEmpty(eventoTransporte.enderecoPartidaCEP)) { eventoTransporte.enderecoPartidaCEP = String.Empty; }
            if (String.IsNullOrEmpty(eventoTransporte.enderecoPartidaCidade)) { eventoTransporte.enderecoPartidaCidade = String.Empty; }
            if (String.IsNullOrEmpty(eventoTransporte.enderecoPartidaUF)) { eventoTransporte.enderecoPartidaUF = String.Empty; }
            if (String.IsNullOrEmpty(eventoTransporte.Mensagem)) { eventoTransporte.Mensagem = String.Empty; }
        }
    }
}