using Rodar.Domain.Repository;
using System;
using System.Collections.Generic;
using Rodar.Domain.Entity;
using System.Data.SqlClient;
using Rodar.Utilities;
using System.Data;

namespace Rodar.Repository.SqlServer
{
    public class ChatUsuarioEventoCaronaRepository : IChatUsuarioEventoCaronaRepository
    {
        public int Cadastrar(ChatUsuarioEventoCarona chatUsuarioEventoCarona)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"INSERT INTO ChatUsuarioEventoCarona
                                                        (idEventoCarona
                                                        ,dataHoraInclusaoMensagem
                                                        ,idUsuarioOrigem
                                                        ,idUsuarioDestino
                                                        ,Mensagem)
                                                    VALUES
                                                        (@idEventoCarona
                                                        ,GETDATE()
                                                        ,@idUsuarioOrigem
                                                        ,@idUsuarioDestino
                                                        ,@Mensagem);
                                            SET @idChatUsuarioEventoCarona = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);

                ValidaCampos(ref chatUsuarioEventoCarona);

                cmdInserir.Parameters.Add("idChatUsuarioEventoCarona", SqlDbType.Int);
                cmdInserir.Parameters["idChatUsuarioEventoCarona"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("idEventoCarona", SqlDbType.Int).Value = chatUsuarioEventoCarona.idEventoCarona;
                cmdInserir.Parameters.Add("idUsuarioOrigem", SqlDbType.Int).Value = chatUsuarioEventoCarona.idUsuarioOrigem;
                cmdInserir.Parameters.Add("idUsuarioDestino", SqlDbType.Int).Value = chatUsuarioEventoCarona.idUsuarioDestino;
                cmdInserir.Parameters.Add("Mensagem", SqlDbType.VarChar).Value = chatUsuarioEventoCarona.Mensagem;

                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();

                    return (int)cmdInserir.Parameters["idChatUsuarioEventoCarona"].Value;
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

        public List<ChatUsuarioEventoCarona> BuscarTodos()
        {
            List<ChatUsuarioEventoCarona> listaMensagens = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM ChatUsuarioEventoCarona";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        listaMensagens = new List<ChatUsuarioEventoCarona>();

                    while (drSelecao.Read())
                    {
                        var chatUsuarioEventoCarona = new ChatUsuarioEventoCarona();
                        PreencheCampos(drSelecao, ref chatUsuarioEventoCarona);
                        listaMensagens.Add(chatUsuarioEventoCarona);
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

        public List<ChatUsuarioEventoCarona> BuscarCabecalhoMensagensPorUsuario(int idUsuario)
        {
            List<ChatUsuarioEventoCarona> listaMensagens = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT
	                                    DISTINCT idEventoCarona, 
	                                    MAX(dataHoraInclusaoMensagem) AS dataHoraInclusaoMensagem,
	                                    0 AS idChatUsuarioEventoCarona, 
	                                    0 AS idUsuarioOrigem, 
	                                    0 AS idUsuarioDestino,
	                                    '' AS Mensagem
                                    FROM ChatUsuarioEventoCarona
                                    WHERE idUsuarioDestino = @idUsuario
                                    OR	idUsuarioOrigem = @idUsuario
                                    GROUP BY idEventoCarona";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        listaMensagens = new List<ChatUsuarioEventoCarona>();

                    while (drSelecao.Read())
                    {
                        var chatUsuarioEventoCarona = new ChatUsuarioEventoCarona();
                        PreencheCampos(drSelecao, ref chatUsuarioEventoCarona);
                        listaMensagens.Add(chatUsuarioEventoCarona);
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

        private static void PreencheCampos(SqlDataReader drSelecao, ref ChatUsuarioEventoCarona chatUsuarioEventoCarona)
        {
            if (drSelecao["idChatUsuarioEventoCarona"] != DBNull.Value)
                chatUsuarioEventoCarona.idChatUsuarioEventoCarona = Convert.ToInt32(drSelecao["idChatUsuarioEventoCarona"].ToString());

            if (drSelecao["idEventoCarona"] != DBNull.Value)
                chatUsuarioEventoCarona.idEventoCarona = Convert.ToInt32(drSelecao["idEventoCarona"].ToString());

            if (drSelecao["idUsuarioOrigem"] != DBNull.Value)
                chatUsuarioEventoCarona.idUsuarioOrigem = Convert.ToInt32(drSelecao["idUsuarioOrigem"].ToString());

            if (drSelecao["idUsuarioDestino"] != DBNull.Value)
                chatUsuarioEventoCarona.idUsuarioDestino = Convert.ToInt32(drSelecao["idUsuarioDestino"].ToString());

            if (drSelecao["dataHoraInclusaoMensagem"] != DBNull.Value)
                chatUsuarioEventoCarona.dataHoraInclusaoMensagem = Convert.ToDateTime(drSelecao["dataHoraInclusaoMensagem"]);
            else
                chatUsuarioEventoCarona.dataHoraInclusaoMensagem = null;

            if (drSelecao["Mensagem"] != DBNull.Value)
                chatUsuarioEventoCarona.Mensagem = drSelecao["Mensagem"].ToString();
        }

        private static void ValidaCampos(ref ChatUsuarioEventoCarona chatUsuarioEventoCarona)
        {
            if (String.IsNullOrEmpty(chatUsuarioEventoCarona.Mensagem)) { chatUsuarioEventoCarona.Mensagem = String.Empty; }
        }
    }
}
