using System;
using Rodar.Domain.Repository;
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
    public class ChatUsuarioEventoTransporteRepository : IChatUsuarioEventoTransporteRepository
    {
        public int Cadastrar(ChatUsuarioEventoTransporte chatUsuarioEventoTransporte)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"INSERT INTO ChatUsuarioEventoTransporte
                                                        (idEventoTransporte
                                                        ,dataHoraInclusaoMensagem
                                                        ,idUsuarioOrigem
                                                        ,idUsuarioDestino
                                                        ,Mensagem)
                                                    VALUES
                                                        (@idEventoTransporte
                                                        ,GETDATE()
                                                        ,@idUsuarioOrigem
                                                        ,@idUsuarioDestino
                                                        ,@Mensagem);
                                            SET @idChatUsuarioEventoTransporte = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);

                ValidaCampos(ref chatUsuarioEventoTransporte);

                cmdInserir.Parameters.Add("idChatUsuarioEventoTransporte", SqlDbType.Int);
                cmdInserir.Parameters["idChatUsuarioEventoTransporte"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("idEventoTransporte", SqlDbType.Int).Value = chatUsuarioEventoTransporte.idEventoTransporte;
                cmdInserir.Parameters.Add("idUsuarioOrigem", SqlDbType.Int).Value = chatUsuarioEventoTransporte.idUsuarioOrigem;
                cmdInserir.Parameters.Add("idUsuarioDestino", SqlDbType.Int).Value = chatUsuarioEventoTransporte.idUsuarioDestino;
                cmdInserir.Parameters.Add("Mensagem", SqlDbType.VarChar).Value = chatUsuarioEventoTransporte.Mensagem;

                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();

                    return (int)cmdInserir.Parameters["idChatUsuarioEventoTransporte"].Value;
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

        public List<ChatUsuarioEventoTransporte> BuscarTodos()
        {
            List<ChatUsuarioEventoTransporte> listaMensagens = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM ChatUsuarioEventoTransporte";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        listaMensagens = new List<ChatUsuarioEventoTransporte>();

                    while (drSelecao.Read())
                    {
                        var chatUsuarioEventoTransporte = new ChatUsuarioEventoTransporte();
                        PreencheCampos(drSelecao, ref chatUsuarioEventoTransporte);
                        listaMensagens.Add(chatUsuarioEventoTransporte);
                    }

                    return listaMensagens;
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

        public List<ChatUsuarioEventoTransporte> BuscarCabecalhoMensagensPorUsuario(int idUsuario)
        {
            List<ChatUsuarioEventoTransporte> listaMensagens = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT
	                                    DISTINCT idEventoTransporte, 
	                                    MAX(dataHoraInclusaoMensagem) AS dataHoraInclusaoMensagem,
	                                    0 AS idChatUsuarioEventoTransporte, 
	                                    0 AS idUsuarioOrigem, 
	                                    (SELECT TOP 1 idUsuarioDestino FROM ChatUsuarioEventoTransporte ChatEx WHERE ChatEx.idEventoTransporte = ChatUsuarioEventoTransporte.idEventoTransporte AND (ChatEx.idUsuarioOrigem = @idUsuario OR ChatEx.idUsuarioDestino = @idUsuario) ORDER BY dataHoraInclusaoMensagem ASC) AS idUsuarioDestino,
	                                    '' AS Mensagem
                                    FROM ChatUsuarioEventoTransporte
                                    WHERE idUsuarioDestino = @idUsuario
                                    OR	idUsuarioOrigem = @idUsuario
                                    GROUP BY idEventoTransporte";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        listaMensagens = new List<ChatUsuarioEventoTransporte>();

                    while (drSelecao.Read())
                    {
                        var chatUsuarioEventoTransporte = new ChatUsuarioEventoTransporte();
                        PreencheCampos(drSelecao, ref chatUsuarioEventoTransporte);
                        listaMensagens.Add(chatUsuarioEventoTransporte);
                    }

                    return listaMensagens;
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

        private static void PreencheCampos(SqlDataReader drSelecao, ref ChatUsuarioEventoTransporte chatUsuarioEventoTransporte)
        {
            if (drSelecao["idChatUsuarioEventoTransporte"] != DBNull.Value)
                chatUsuarioEventoTransporte.idChatUsuarioEventoTransporte = Convert.ToInt32(drSelecao["idChatUsuarioEventoTransporte"].ToString());

            if (drSelecao["idEventoTransporte"] != DBNull.Value)
                chatUsuarioEventoTransporte.idEventoTransporte = Convert.ToInt32(drSelecao["idEventoTransporte"].ToString());

            if (drSelecao["idUsuarioOrigem"] != DBNull.Value)
                chatUsuarioEventoTransporte.idUsuarioOrigem = Convert.ToInt32(drSelecao["idUsuarioOrigem"].ToString());

            if (drSelecao["idUsuarioDestino"] != DBNull.Value)
                chatUsuarioEventoTransporte.idUsuarioDestino = Convert.ToInt32(drSelecao["idUsuarioDestino"].ToString());

            if (drSelecao["dataHoraInclusaoMensagem"] != DBNull.Value)
                chatUsuarioEventoTransporte.dataHoraInclusaoMensagem = Convert.ToDateTime(drSelecao["dataHoraInclusaoMensagem"]);
            else
                chatUsuarioEventoTransporte.dataHoraInclusaoMensagem = null;

            if (drSelecao["Mensagem"] != DBNull.Value)
                chatUsuarioEventoTransporte.Mensagem = drSelecao["Mensagem"].ToString();
        }

        private static void ValidaCampos(ref ChatUsuarioEventoTransporte chatUsuarioEventoTransporte)
        {
            if (String.IsNullOrEmpty(chatUsuarioEventoTransporte.Mensagem)) { chatUsuarioEventoTransporte.Mensagem = String.Empty; }
        }
    }
}
