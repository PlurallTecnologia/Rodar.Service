using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using Rodar.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Repository.SqlServer
{
    public class EventoTransportePassageiroRepository : IEventoTransportePassageiroRepository
    {
        public int Cadastrar(EventoTransportePassageiro eventoTransportePassageiro)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"INSERT INTO EventoTransportePassageiro
                                                    (idEventoTransporte
                                                    ,idUsuarioPassageiro)
                                                VALUES
                                                    (@idEventoTransporte
                                                    ,@idUsuarioPassageiro);
                                                    SET @idEventoTransportePassageiro = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);

                cmdInserir.Parameters.Add("idEventoTransportePassageiro", SqlDbType.Int);
                cmdInserir.Parameters["idEventoTransportePassageiro"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("idEventoTransporte", SqlDbType.Int).Value = eventoTransportePassageiro.idEventoTransporte;
                cmdInserir.Parameters.Add("idUsuarioPassageiro", SqlDbType.Int).Value = eventoTransportePassageiro.idUsuarioPassageiro;

                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();

                    return (int)cmdInserir.Parameters["idEventoTransportePassageiro"].Value;
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

        public int Excluir(int idEventoTransportePassageiro)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"DELETE FROM EventoTransportePassageiro
                                        WHERE idEventoTransportePassageiro = @idEventoTransportePassageiro";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, con);

                cmdDeletar.Parameters.Add("idEventoTransportePassageiro", SqlDbType.Int).Value = idEventoTransportePassageiro;

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

        public int Excluir(int idEventoTransporte, int idUsuarioPassageiro)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"DELETE FROM EventoTransportePassageiro
                                        WHERE idEventoTransporte = @idEventoTransporte
                                        AND idUsuarioPassageiro = @idUsuarioPassageiro";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, con);

                cmdDeletar.Parameters.Add("idEventoTransporte", SqlDbType.Int).Value = idEventoTransporte;
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
        
        public IEnumerable<EventoTransportePassageiro> BuscarTodos()
        {
            List<EventoTransportePassageiro> eventoTransportesPassageiros = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM EventoTransportePassageiro";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        eventoTransportesPassageiros = new List<EventoTransportePassageiro>();

                    while (drSelecao.Read())
                    {
                        var eventoTransportePassageiro = new EventoTransportePassageiro();

                        PreencheCampos(drSelecao, ref eventoTransportePassageiro);

                        eventoTransportesPassageiros.Add(eventoTransportePassageiro);
                    }

                    return eventoTransportesPassageiros;
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

        private static void PreencheCampos(SqlDataReader drSelecao, ref EventoTransportePassageiro eventoTransportePassageiro)
        {
            if (drSelecao["idEventoTransportePassageiro"] != DBNull.Value)
                eventoTransportePassageiro.idEventoTransportePassageiro = Convert.ToInt32(drSelecao["idEventoTransportePassageiro"].ToString());

            if (drSelecao["idEventoCarona"] != DBNull.Value)
                eventoTransportePassageiro.idEventoTransporte = Convert.ToInt32(drSelecao["idEventoTransporte"].ToString());

            if (drSelecao["idUsuarioPassageiro"] != DBNull.Value)
                eventoTransportePassageiro.idUsuarioPassageiro = Convert.ToInt32(drSelecao["idUsuarioPassageiro"].ToString());
        }
    }
}
