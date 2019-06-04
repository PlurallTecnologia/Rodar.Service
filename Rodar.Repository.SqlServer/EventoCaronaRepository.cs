using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using Rodar.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Rodar.Repository.SqlServer
{
    public class EventoCaronaRepository : IEventoCaronaRepository
    {
        public int Cadastrar(EventoCarona eventoCarona)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"INSERT INTO EventoCarona
                                                    (idEvento
                                                    ,idUsuarioMotorista
                                                    ,quantidadeVagas
                                                    ,enderecoPartidaRua
                                                    ,enderecoPartidaComplemento
                                                    ,enderecoPartidaBairro
                                                    ,enderecoPartidaNumero
                                                    ,enderecoPartidaCEP
                                                    ,enderecoPartidaCidade
                                                    ,enderecoPartidaUF
                                                    ,valorParticipacao
                                                    ,dataHoraPartida
                                                    ,dataHoraPrevisaoChegada
                                                    ,Mensagem)
                                                VALUES
                                                    (@idEvento
                                                    ,@idUsuarioMotorista
                                                    ,@quantidadeVagas
                                                    ,@enderecoPartidaRua
                                                    ,@enderecoPartidaComplemento
                                                    ,@enderecoPartidaBairro
                                                    ,@enderecoPartidaNumero
                                                    ,@enderecoPartidaCEP
                                                    ,@enderecoPartidaCidade
                                                    ,@enderecoPartidaUF
                                                    ,@valorParticipacao
                                                    ,@dataHoraPartida
                                                    ,@dataHoraPrevisaoChegada
                                                    ,@Mensagem);
                                                    SET @idEventoCarona = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);

                ValidaCampos(ref eventoCarona);

                cmdInserir.Parameters.Add("idEventoCarona", SqlDbType.Int);
                cmdInserir.Parameters["idEventoCarona"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("idEvento", SqlDbType.Int).Value = eventoCarona.idEvento;
                cmdInserir.Parameters.Add("idUsuarioMotorista", SqlDbType.Int).Value = eventoCarona.idUsuarioMotorista;

                cmdInserir.Parameters.Add("quantidadeVagas", SqlDbType.Int).Value = eventoCarona.quantidadeVagas;

                cmdInserir.Parameters.Add("enderecoPartidaRua", SqlDbType.VarChar).Value = eventoCarona.enderecoPartidaRua;
                cmdInserir.Parameters.Add("enderecoPartidaComplemento", SqlDbType.VarChar).Value = eventoCarona.enderecoPartidaComplemento;
                cmdInserir.Parameters.Add("enderecoPartidaBairro", SqlDbType.VarChar).Value = eventoCarona.enderecoPartidaBairro;
                cmdInserir.Parameters.Add("enderecoPartidaNumero", SqlDbType.Int).Value = eventoCarona.enderecoPartidaNumero;
                cmdInserir.Parameters.Add("enderecoPartidaCEP", SqlDbType.VarChar).Value = eventoCarona.enderecoPartidaCEP;
                cmdInserir.Parameters.Add("enderecoPartidaCidade", SqlDbType.VarChar).Value = eventoCarona.enderecoPartidaCidade;
                cmdInserir.Parameters.Add("enderecoPartidaUF", SqlDbType.VarChar).Value = eventoCarona.enderecoPartidaUF;
                cmdInserir.Parameters.Add("valorParticipacao", SqlDbType.Decimal).Value = eventoCarona.valorParticipacao;

                if (eventoCarona.dataHoraPartida == null)
                    cmdInserir.Parameters.Add("dataHoraPartida", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdInserir.Parameters.Add("dataHoraPartida", SqlDbType.DateTime).Value = eventoCarona.dataHoraPartida;

                if (eventoCarona.dataHoraPrevisaoChegada == null)
                    cmdInserir.Parameters.Add("dataHoraPrevisaoChegada", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdInserir.Parameters.Add("dataHoraPrevisaoChegada", SqlDbType.DateTime).Value = eventoCarona.dataHoraPrevisaoChegada;

                cmdInserir.Parameters.Add("Mensagem", SqlDbType.VarChar).Value = eventoCarona.Mensagem;
                
                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();

                    return (int)cmdInserir.Parameters["idEventoCarona"].Value;
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
        
        public void Atualizar(EventoCarona eventoCarona)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"UPDATE EventoCarona
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
                                          ,dataHoraPartida = @dataHoraPartida
                                          ,dataHoraPrevisaoChegada = @dataHoraPrevisaoChegada
                                          ,Mensagem = Mensagem
                                     WHERE idEvento = @idEvento";

                SqlCommand cmdAtualizar = new SqlCommand(stringSQL, con);

                ValidaCampos(ref eventoCarona);

                cmdAtualizar.Parameters.Add("idEventoCarona", SqlDbType.Int).Value = eventoCarona.idEventoCarona;

                cmdAtualizar.Parameters.Add("idEvento", SqlDbType.Int).Value = eventoCarona.idEvento;
                cmdAtualizar.Parameters.Add("idUsuarioMotorista", SqlDbType.Int).Value = eventoCarona.idUsuarioMotorista;

                cmdAtualizar.Parameters.Add("quantidadeVagas", SqlDbType.Int).Value = eventoCarona.quantidadeVagas;

                cmdAtualizar.Parameters.Add("enderecoPartidaRua", SqlDbType.VarChar).Value = eventoCarona.enderecoPartidaRua;
                cmdAtualizar.Parameters.Add("enderecoPartidaComplemento", SqlDbType.VarChar).Value = eventoCarona.enderecoPartidaComplemento;
                cmdAtualizar.Parameters.Add("enderecoPartidaBairro", SqlDbType.VarChar).Value = eventoCarona.enderecoPartidaBairro;
                cmdAtualizar.Parameters.Add("enderecoPartidaNumero", SqlDbType.Int).Value = eventoCarona.enderecoPartidaNumero;
                cmdAtualizar.Parameters.Add("enderecoPartidaCEP", SqlDbType.VarChar).Value = eventoCarona.enderecoPartidaCEP;
                cmdAtualizar.Parameters.Add("enderecoPartidaCidade", SqlDbType.VarChar).Value = eventoCarona.enderecoPartidaCidade;
                cmdAtualizar.Parameters.Add("enderecoPartidaUF", SqlDbType.VarChar).Value = eventoCarona.enderecoPartidaUF;
                cmdAtualizar.Parameters.Add("valorParticipacao", SqlDbType.Decimal).Value = eventoCarona.valorParticipacao;

                if (eventoCarona.dataHoraPartida == null)
                    cmdAtualizar.Parameters.Add("dataHoraPartida", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdAtualizar.Parameters.Add("dataHoraPartida", SqlDbType.DateTime).Value = eventoCarona.dataHoraPartida;

                if (eventoCarona.dataHoraPrevisaoChegada == null)
                    cmdAtualizar.Parameters.Add("dataHoraPrevisaoChegada", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdAtualizar.Parameters.Add("dataHoraPrevisaoChegada", SqlDbType.DateTime).Value = eventoCarona.dataHoraPrevisaoChegada;

                cmdAtualizar.Parameters.Add("Mensagem", SqlDbType.VarChar).Value = eventoCarona.Mensagem;

                try
                {
                    con.Open();
                    cmdAtualizar.ExecuteNonQuery();
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

        public EventoCarona Buscar(int idEventoCarona)
        {
            EventoCarona eventoCarona = null; ;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM EventoCarona
                                        WHERE idEventoCarona = @idEventoCarona";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idEventoCarona", SqlDbType.Int).Value = idEventoCarona;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.Read())
                    {
                        eventoCarona = new EventoCarona();
                        PreencheCampos(drSelecao, ref eventoCarona);
                    }

                    return eventoCarona;
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

        public IEnumerable<EventoCarona> BuscarPorEvento(int idEvento = 0)
        {
            if (idEvento == 0)
                return BuscarTodos();

            List<EventoCarona> eventoCaronas = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT * FROM EventoCarona WHERE idEvento = @idEvento";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idEvento", SqlDbType.Int).Value = idEvento;
                
                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        eventoCaronas = new List<EventoCarona>();

                    while (drSelecao.Read())
                    {
                        var eventoCarona = new EventoCarona();
                        PreencheCampos(drSelecao, ref eventoCarona);
                        eventoCaronas.Add(eventoCarona);
                    }

                    return eventoCaronas;
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

        public IEnumerable<EventoCarona> BuscarTodos()
        {
            List<EventoCarona> eventoCaronas = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM EventoCarona";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        eventoCaronas = new List<EventoCarona>();

                    while (drSelecao.Read())
                    {
                        var eventoCarona = new EventoCarona();
                        PreencheCampos(drSelecao, ref eventoCarona);
                        eventoCaronas.Add(eventoCarona);
                    }

                    return eventoCaronas;
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

        public int Excluir(int idEventoCarona)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"DELETE FROM EventoCarona
                                        WHERE idEventoCarona = @idEventoCarona";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, con);

                cmdDeletar.Parameters.Add("idEventoCarona", SqlDbType.Int).Value = idEventoCarona;

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
        
        private static void PreencheCampos(SqlDataReader drSelecao, ref EventoCarona eventoCarona)
        {
            if (drSelecao["idEventoCarona"] != DBNull.Value)
                eventoCarona.idEventoCarona = Convert.ToInt32(drSelecao["idEventoCarona"].ToString());

            if (drSelecao["idEvento"] != DBNull.Value)
                eventoCarona.idEvento = Convert.ToInt32(drSelecao["idEvento"].ToString());

            if (drSelecao["idUsuarioMotorista"] != DBNull.Value)
                eventoCarona.idUsuarioMotorista = Convert.ToInt32(drSelecao["idUsuarioMotorista"].ToString());

            if (drSelecao["quantidadeVagas"] != DBNull.Value)
                eventoCarona.quantidadeVagas = Convert.ToInt32(drSelecao["quantidadeVagas"].ToString());

            if (drSelecao["enderecoPartidaRua"] != DBNull.Value)
                eventoCarona.enderecoPartidaRua = drSelecao["enderecoPartidaRua"].ToString();

            if (drSelecao["enderecoPartidaComplemento"] != DBNull.Value)
                eventoCarona.enderecoPartidaComplemento = drSelecao["enderecoPartidaComplemento"].ToString();

            if (drSelecao["enderecoPartidaBairro"] != DBNull.Value)
                eventoCarona.enderecoPartidaBairro = drSelecao["enderecoPartidaBairro"].ToString();

            if (drSelecao["enderecoPartidaNumero"] != DBNull.Value)
                eventoCarona.enderecoPartidaNumero = Convert.ToInt32(drSelecao["enderecoPartidaNumero"].ToString());

            if (drSelecao["enderecoPartidaCEP"] != DBNull.Value)
                eventoCarona.enderecoPartidaCEP = drSelecao["enderecoPartidaCEP"].ToString();

            if (drSelecao["enderecoPartidaCidade"] != DBNull.Value)
                eventoCarona.enderecoPartidaCidade = drSelecao["enderecoPartidaCidade"].ToString();

            if (drSelecao["enderecoPartidaUF"] != DBNull.Value)
                eventoCarona.enderecoPartidaUF = drSelecao["enderecoPartidaUF"].ToString();

            if (drSelecao["valorParticipacao"] != DBNull.Value)
                eventoCarona.valorParticipacao = Convert.ToDecimal(drSelecao["valorParticipacao"].ToString());

            if (drSelecao["dataHoraPartida"] != DBNull.Value)
                eventoCarona.dataHoraPartida = Convert.ToDateTime(drSelecao["dataHoraPartida"]);
            else
                eventoCarona.dataHoraPartida = null;

            if (drSelecao["dataHoraPrevisaoChegada"] != DBNull.Value)
                eventoCarona.dataHoraPrevisaoChegada = Convert.ToDateTime(drSelecao["dataHoraPrevisaoChegada"]);
            else
                eventoCarona.dataHoraPrevisaoChegada = null;

            if (drSelecao["Mensagem"] != DBNull.Value)
                eventoCarona.Mensagem = drSelecao["Mensagem"].ToString();
        }

        private static void ValidaCampos(ref EventoCarona eventoCarona)
        {
            if (String.IsNullOrEmpty(eventoCarona.enderecoPartidaRua)) { eventoCarona.enderecoPartidaRua = String.Empty; }
            if (String.IsNullOrEmpty(eventoCarona.enderecoPartidaComplemento)) { eventoCarona.enderecoPartidaComplemento = String.Empty; }
            if (String.IsNullOrEmpty(eventoCarona.enderecoPartidaBairro)) { eventoCarona.enderecoPartidaBairro = String.Empty; }
            if (String.IsNullOrEmpty(eventoCarona.enderecoPartidaCEP)) { eventoCarona.enderecoPartidaCEP = String.Empty; }
            if (String.IsNullOrEmpty(eventoCarona.enderecoPartidaCidade)) { eventoCarona.enderecoPartidaCidade = String.Empty; }
            if (String.IsNullOrEmpty(eventoCarona.enderecoPartidaUF)) { eventoCarona.enderecoPartidaUF = String.Empty; }
            if (String.IsNullOrEmpty(eventoCarona.Mensagem)) { eventoCarona.Mensagem = String.Empty; }
        }
    }
}
