using Rodar.Domain.Repository;
using System;
using System.Collections.Generic;
using Rodar.Domain.Entity;
using System.Data.SqlClient;
using Rodar.Utilities;
using System.Data;

namespace Rodar.Repository.SqlServer
{
    public class EventoTransporteRepository : IEventoTransporteRepository
    {
        public int Cadastrar(EventoTransporte eventoTransporte)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
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
                                                ,dataHoraPartida
                                                ,dataHoraPrevisaoChegada
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
                                                ,@dataHoraPartida
                                                ,@dataHoraPrevisaoChegada
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

                if (eventoTransporte.dataHoraPartida == null)
                    cmdInserir.Parameters.Add("dataHoraPartida", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdInserir.Parameters.Add("dataHoraPartida", SqlDbType.DateTime).Value = eventoTransporte.dataHoraPartida;

                if (eventoTransporte.dataHoraPrevisaoChegada == null)
                    cmdInserir.Parameters.Add("dataHoraPrevisaoChegada", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdInserir.Parameters.Add("dataHoraPrevisaoChegada", SqlDbType.DateTime).Value = eventoTransporte.dataHoraPrevisaoChegada;

                cmdInserir.Parameters.Add("Mensagem", SqlDbType.VarChar).Value = eventoTransporte.Mensagem;

                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();

                    return (int)cmdInserir.Parameters["idEventoTransporte"].Value;
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

        public void Atualizar(EventoTransporte eventoTransporte)
        {
            using (var con = Database.GetCurrentDbConnection())
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
                                          ,dataHoraPartida = @dataHoraPartida
                                          ,dataHoraPrevisaoChegada = @dataHoraPrevisaoChegada
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

                if (eventoTransporte.dataHoraPartida == null)
                    cmdAtualizar.Parameters.Add("dataHoraPartida", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdAtualizar.Parameters.Add("dataHoraPartida", SqlDbType.DateTime).Value = eventoTransporte.dataHoraPartida;

                if (eventoTransporte.dataHoraPrevisaoChegada == null)
                    cmdAtualizar.Parameters.Add("dataHoraPrevisaoChegada", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdAtualizar.Parameters.Add("dataHoraPrevisaoChegada", SqlDbType.DateTime).Value = eventoTransporte.dataHoraPrevisaoChegada;

                cmdAtualizar.Parameters.Add("Mensagem", SqlDbType.VarChar).Value = eventoTransporte.Mensagem;

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

        public EventoTransporte Buscar(int idEventoTransporte)
        {
            EventoTransporte eventoTransporte = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM EventoTransporte
                                        WHERE idEventoTransporte = @idEventoTransporte";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idEventoTransporte", SqlDbType.Int).Value = idEventoTransporte;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.Read())
                    {
                        eventoTransporte = new EventoTransporte();
                        PreencheCampos(drSelecao, ref eventoTransporte);
                    }

                    return eventoTransporte;
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

        public IEnumerable<EventoTransporte> BuscarPorEvento(int idEvento = 0)
        {
            if (idEvento == 0)
                return BuscarTodos();

            List<EventoTransporte> eventoTransportes = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT * FROM EventoTransporte WHERE idEvento = @idEvento";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idEvento", SqlDbType.Int).Value = idEvento;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        eventoTransportes = new List<EventoTransporte>();

                    while (drSelecao.Read())
                    {
                        EventoTransporte eventoTransporte = new EventoTransporte();

                        PreencheCampos(drSelecao, ref eventoTransporte);

                        eventoTransportes.Add(eventoTransporte);
                    }

                    return eventoTransportes;
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

        public IEnumerable<EventoTransporte> BuscarTodos()
        {
            List<EventoTransporte> listaTransportes = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                var stringSQL = @"SELECT *
                                        FROM EventoTransporte";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                try
                {
                    con.Open();
                    var drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        listaTransportes = new List<EventoTransporte>();

                    while (drSelecao.Read())
                    {
                        var eventoTransporte = new EventoTransporte();
                        PreencheCampos(drSelecao, ref eventoTransporte);
                        listaTransportes.Add(eventoTransporte);
                    }

                    return listaTransportes;
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

        public int Excluir(int idEventoTransporte)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"DELETE FROM EventoTransporte
                                        WHERE idEventoTransporte = @idEventoTransporte";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, con);

                cmdDeletar.Parameters.Add("idEventoTransporte", SqlDbType.Int).Value = idEventoTransporte;

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

            if (drSelecao["dataHoraPartida"] != DBNull.Value)
                eventoTransporte.dataHoraPartida = Convert.ToDateTime(drSelecao["dataHoraPartida"]);
            else
                eventoTransporte.dataHoraPartida = null;

            if (drSelecao["dataHoraPrevisaoChegada"] != DBNull.Value)
                eventoTransporte.dataHoraPrevisaoChegada = Convert.ToDateTime(drSelecao["dataHoraPrevisaoChegada"]);
            else
                eventoTransporte.dataHoraPrevisaoChegada = null;

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
