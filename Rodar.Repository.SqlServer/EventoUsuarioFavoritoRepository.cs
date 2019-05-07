using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using Rodar.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Rodar.Repository.SqlServer
{
    public class EventoUsuarioFavoritoRepository : IEventoUsuarioFavoritoRepository
    {   
        public void AdicionarFavorito(int idEvento, int idUsuario)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"INSERT INTO EventoUsuarioFavorito(idEvento
                                                        ,idUsuario
                                                        ,dataHoraFavoritado)
                                                    VALUES
                                                        (@idEvento
                                                        ,@idUsuario
                                                        ,getdate());";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);
                
                cmdInserir.Parameters.Add("idEvento", SqlDbType.Int).Value = idEvento;
                cmdInserir.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;

                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();
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

        public void RemoverFavorito(int idEvento, int idUsuario)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"DELETE FROM EventoUsuarioFavorito
                                        WHERE idEvento = @idEvento 
                                        AND idUsuario = @idUsuario";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, con);

                cmdDeletar.Parameters.Add("idEvento", SqlDbType.Int).Value = idEvento;
                cmdDeletar.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;

                try
                {
                    con.Open();
                    cmdDeletar.ExecuteNonQuery();
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

        public EventoUsuarioFavorito BuscarFavorito(int idEvento, int idUsuario)
        {
            EventoUsuarioFavorito eventoUsuarioFavorito = new EventoUsuarioFavorito();

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM EventoUsuarioFavorito
                                        WHERE idEvento = @idEvento
                                        AND idUsuario = @idUsuario";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idEvento", SqlDbType.Int).Value = idEvento;
                cmdSelecionar.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.Read())
                        PreencheCampos(drSelecao, ref eventoUsuarioFavorito);

                    return eventoUsuarioFavorito;
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

        public bool ExisteFavorito(int idEvento, int idUsuario)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT 1
                                FROM EventoUsuarioFavorito
                                WHERE idEvento = @idEvento AND idUsuario = @idUsuario";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idEvento", SqlDbType.Int).Value = idEvento;
                cmdSelecionar.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;

                try
                {
                    con.Open();

                    return (cmdSelecionar.ExecuteScalar() != null && cmdSelecionar.ExecuteScalar().ToString() == "1");
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

        private static void PreencheCampos(SqlDataReader drSelecao, ref EventoUsuarioFavorito eventoUsuarioFavorito)
        {
            if (drSelecao["idUsuario"] != DBNull.Value)
                eventoUsuarioFavorito.idUsuario = Convert.ToInt32(drSelecao["idUsuario"].ToString());

            if (drSelecao["idEvento"] != DBNull.Value)
                eventoUsuarioFavorito.idEvento = Convert.ToInt32(drSelecao["idEvento"].ToString());

            if (drSelecao["dataHoraFavoritado"] != DBNull.Value)
                eventoUsuarioFavorito.dataHoraFavoritado = Convert.ToDateTime(drSelecao["dataHoraFavoritado"]);
            else
                eventoUsuarioFavorito.dataHoraFavoritado = null;
        }
    }
}
