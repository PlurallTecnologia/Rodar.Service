using Rodar.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rodar.Domain.Entity;
using System.Data.SqlClient;
using Rodar.Utilities;
using System.Data;

namespace Rodar.Repository.SqlServer
{
    public class EventoCaronaPassageiroRepository : IEventoCaronaPassageiroRepository
    {
        public int Cadastrar(EventoCaronaPassageiro eventoCaronaPassageiro)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"INSERT INTO EventoCaronaPassageiro
                                                    (idEventoCarona
                                                    ,idUsuarioPassageiro)
                                                VALUES
                                                    (@idEventoCarona
                                                    ,@idUsuarioPassageiro);
                                                    SET @idEventoCaronaPassageiro = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);

                cmdInserir.Parameters.Add("idEventoCaronaPassageiro", SqlDbType.Int);
                cmdInserir.Parameters["idEventoCaronaPassageiro"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("idEventoCarona", SqlDbType.Int).Value = eventoCaronaPassageiro.idEventoCarona;
                cmdInserir.Parameters.Add("idUsuarioPassageiro", SqlDbType.Int).Value = eventoCaronaPassageiro.idUsuarioPassageiro;

                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();

                    return (int)cmdInserir.Parameters["idEventoCaronaPassageiro"].Value;
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

        public int Excluir(int idEventoCaronaPassageiro)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"DELETE FROM EventoCaronaPassageiro
                                        WHERE idEventoCaronaPassageiro = @idEventoCaronaPassageiro";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, con);

                cmdDeletar.Parameters.Add("idEventoCaronaPassageiro", SqlDbType.Int).Value = idEventoCaronaPassageiro;

                try
                {
                    con.Open();

                    return (int)cmdDeletar.ExecuteNonQuery();
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

        public int Excluir(int idEventoCarona, int idUsuarioPassageiro)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"DELETE FROM EventoCaronaPassageiro
                                        WHERE idEventoCarona = @idEventoCarona
                                        AND idUsuarioPassageiro = @idUsuarioPassageiro";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, con);

                cmdDeletar.Parameters.Add("idEventoCarona", SqlDbType.Int).Value = idEventoCarona;
                cmdDeletar.Parameters.Add("idUsuarioPassageiro", SqlDbType.Int).Value = idUsuarioPassageiro;

                try
                {
                    con.Open();

                    return (int)cmdDeletar.ExecuteNonQuery();
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

        public IEnumerable<EventoCaronaPassageiro> BuscarTodos()
        {
            List<EventoCaronaPassageiro> eventoCaronasPassageiros = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM EventoCaronaPassageiro";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        eventoCaronasPassageiros = new List<EventoCaronaPassageiro>();

                    while (drSelecao.Read())
                    {
                        var eventoCaronaPassageiro = new EventoCaronaPassageiro();

                        PreencheCampos(drSelecao, ref eventoCaronaPassageiro);

                        eventoCaronasPassageiros.Add(eventoCaronaPassageiro);
                    }

                    return eventoCaronasPassageiros;
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

        private static void PreencheCampos(SqlDataReader drSelecao, ref EventoCaronaPassageiro eventoCaronaPassageiro)
        {
            if (drSelecao["idEventoCaronaPassageiro"] != DBNull.Value)
                eventoCaronaPassageiro.idEventoCaronaPassageiro = Convert.ToInt32(drSelecao["idEventoCaronaPassageiro"].ToString());

            if (drSelecao["idEventoCarona"] != DBNull.Value)
                eventoCaronaPassageiro.idEventoCarona = Convert.ToInt32(drSelecao["idEventoTransporte"].ToString());

            if (drSelecao["idUsuarioPassageiro"] != DBNull.Value)
                eventoCaronaPassageiro.idUsuarioPassageiro = Convert.ToInt32(drSelecao["idUsuarioPassageiro"].ToString());
        }

    }
}