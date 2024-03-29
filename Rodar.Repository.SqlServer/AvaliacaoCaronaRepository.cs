﻿using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using Rodar.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Rodar.Repository.SqlServer
{
    public class AvaliacaoCaronaRepository : IAvaliacaoCaronaRepository
    {
        public int Cadastrar(AvaliacaoCarona avaliacaoCarona)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"INSERT INTO AvaliacaoCarona
                                                        (idUsuarioAvaliador
                                                        ,idUsuarioAvaliado
                                                        ,idEventoCarona
                                                        ,Avaliacao
                                                        ,Mensagem
                                                        ,dataHoraAvaliacao)
                                                    VALUES
                                                        (@idUsuarioAvaliador
                                                        ,@idUsuarioAvaliado
                                                        ,@idEventoCarona
                                                        ,@Avaliacao
                                                        ,@Mensagem
                                                        ,GETDATE());
                                            SET @idAvaliacaoCarona = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);

                ValidaCampos(ref avaliacaoCarona);

                cmdInserir.Parameters.Add("idAvaliacaoCarona", SqlDbType.Int);
                cmdInserir.Parameters["idAvaliacaoCarona"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("idUsuarioAvaliador", SqlDbType.Int).Value = avaliacaoCarona.idUsuarioAvaliador;
                cmdInserir.Parameters.Add("idUsuarioAvaliado", SqlDbType.Int).Value = avaliacaoCarona.idUsuarioAvaliado;
                cmdInserir.Parameters.Add("idEventoCarona", SqlDbType.Int).Value = avaliacaoCarona.idEventoCarona;
                cmdInserir.Parameters.Add("Avaliacao", SqlDbType.Float).Value = avaliacaoCarona.Avaliacao;
                cmdInserir.Parameters.Add("Mensagem", SqlDbType.VarChar).Value = avaliacaoCarona.Mensagem;

                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();

                    return (int)cmdInserir.Parameters["idAvaliacaoCarona"].Value;
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

        public List<AvaliacaoCarona> BuscarTodos()
        {
            List<AvaliacaoCarona> listaAvaliacoes = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM AvaliacaoCarona";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        listaAvaliacoes = new List<AvaliacaoCarona>();

                    while (drSelecao.Read())
                    {
                        var avaliacaoCarona = new AvaliacaoCarona();
                        PreencheCampos(drSelecao, ref avaliacaoCarona);
                        listaAvaliacoes.Add(avaliacaoCarona);
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

        private static void PreencheCampos(SqlDataReader drSelecao, ref AvaliacaoCarona avaliacaoCarona)
        {
            if (drSelecao["idAvaliacaoCarona"] != DBNull.Value)
                avaliacaoCarona.idAvaliacaoCarona = Convert.ToInt32(drSelecao["idAvaliacaoCarona"].ToString());

            if (drSelecao["idUsuarioAvaliador"] != DBNull.Value)
                avaliacaoCarona.idUsuarioAvaliador = Convert.ToInt32(drSelecao["idUsuarioAvaliador"].ToString());

            if (drSelecao["idUsuarioAvaliado"] != DBNull.Value)
                avaliacaoCarona.idUsuarioAvaliado = Convert.ToInt32(drSelecao["idUsuarioAvaliado"].ToString());

            if (drSelecao["idEventoCarona"] != DBNull.Value)
                avaliacaoCarona.idEventoCarona = Convert.ToInt32(drSelecao["idEventoCarona"].ToString());

            if (drSelecao["Avaliacao"] != DBNull.Value)
                avaliacaoCarona.Avaliacao = Convert.ToSingle(drSelecao["Avaliacao"].ToString());

            if (drSelecao["dataHoraAvaliacao"] != DBNull.Value)
                avaliacaoCarona.dataHoraAvaliacao = Convert.ToDateTime(drSelecao["dataHoraAvaliacao"]);
            else
                avaliacaoCarona.dataHoraAvaliacao = null;

            if (drSelecao["Mensagem"] != DBNull.Value)
                avaliacaoCarona.Mensagem = drSelecao["Mensagem"].ToString();
        }

        private static void ValidaCampos(ref AvaliacaoCarona avaliacaoCarona)
        {
            if (String.IsNullOrEmpty(avaliacaoCarona.Mensagem)) { avaliacaoCarona.Mensagem = String.Empty; }
        }
    }
}
