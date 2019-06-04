using Rodar.Domain.Repository;
using System.Data;
using System.Data.SqlClient;
using Rodar.Domain.Entity;
using Rodar.Utilities;
using System.Collections.Generic;
using System;

namespace Rodar.Repository.SqlServer
{
    public class AvaliacaoTransporteRepository : IAvaliacaoTransporteRepository
    {
        public int Cadastrar(AvaliacaoTransporte avaliacaoTransporte)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"INSERT INTO AvaliacaoTransporte
                                                        (idUsuarioAvaliador
                                                        ,idUsuarioAvaliado
                                                        ,idEventoTransporte
                                                        ,Avaliacao
                                                        ,Mensagem
                                                        ,dataHoraAvaliacao)
                                                    VALUES
                                                        (@idUsuarioAvaliador
                                                        ,@idUsuarioAvaliado
                                                        ,@idEventoTransporte
                                                        ,@Avaliacao
                                                        ,@Mensagem
                                                        ,GETDATE());
                                            SET @idAvaliacaoTransporte = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);

                ValidaCampos(ref avaliacaoTransporte);

                cmdInserir.Parameters.Add("idAvaliacaoTransporte", SqlDbType.Int);
                cmdInserir.Parameters["idAvaliacaoTransporte"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("idUsuarioAvaliador", SqlDbType.Int).Value = avaliacaoTransporte.idUsuarioAvaliador;
                cmdInserir.Parameters.Add("idUsuarioAvaliado", SqlDbType.Int).Value = avaliacaoTransporte.idUsuarioAvaliado;
                cmdInserir.Parameters.Add("idEventoTransporte", SqlDbType.Int).Value = avaliacaoTransporte.idEventoTransporte;
                cmdInserir.Parameters.Add("Avaliacao", SqlDbType.Float).Value = avaliacaoTransporte.Avaliacao;
                cmdInserir.Parameters.Add("Mensagem", SqlDbType.VarChar).Value = avaliacaoTransporte.Mensagem;

                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();

                    return (int)cmdInserir.Parameters["idAvaliacaoTransporte"].Value;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public List<AvaliacaoTransporte> BuscarTodos()
        {
            List<AvaliacaoTransporte> listaAvaliacoes = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM AvaliacaoTransporte";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        listaAvaliacoes = new List<AvaliacaoTransporte>();

                    while (drSelecao.Read())
                    {
                        var avaliacaoTransporte = new AvaliacaoTransporte();
                        PreencheCampos(drSelecao, ref avaliacaoTransporte);
                        listaAvaliacoes.Add(avaliacaoTransporte);
                    }

                    return listaAvaliacoes;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    
        private static void PreencheCampos(SqlDataReader drSelecao, ref AvaliacaoTransporte avaliacaoTransporte)
        {
            if (drSelecao["idAvaliacaoTransporte"] != DBNull.Value)
                avaliacaoTransporte.idAvaliacaoTransporte = Convert.ToInt32(drSelecao["idAvaliacaoTransporte"].ToString());

            if (drSelecao["idUsuarioAvaliador"] != DBNull.Value)
                avaliacaoTransporte.idUsuarioAvaliador = Convert.ToInt32(drSelecao["idUsuarioAvaliador"].ToString());

            if (drSelecao["idUsuarioAvaliado"] != DBNull.Value)
                avaliacaoTransporte.idUsuarioAvaliado = Convert.ToInt32(drSelecao["idUsuarioAvaliado"].ToString());

            if (drSelecao["idEventoTransporte"] != DBNull.Value)
                avaliacaoTransporte.idEventoTransporte = Convert.ToInt32(drSelecao["idEventoTransporte"].ToString());

            if (drSelecao["Avaliacao"] != DBNull.Value)
                avaliacaoTransporte.Avaliacao = Convert.ToSingle(drSelecao["Avaliacao"].ToString());

            if (drSelecao["dataHoraAvaliacao"] != DBNull.Value)
                avaliacaoTransporte.dataHoraAvaliacao = Convert.ToDateTime(drSelecao["dataHoraAvaliacao"]);
            else
                avaliacaoTransporte.dataHoraAvaliacao = null;

            if (drSelecao["Mensagem"] != DBNull.Value)
                avaliacaoTransporte.Mensagem = drSelecao["Mensagem"].ToString();
        }

        private static void ValidaCampos(ref AvaliacaoTransporte avaliacaoTransporte)
        {
            if (String.IsNullOrEmpty(avaliacaoTransporte.Mensagem)) { avaliacaoTransporte.Mensagem = String.Empty; }
        }
    }
}
