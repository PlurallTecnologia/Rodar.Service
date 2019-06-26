using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using Rodar.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Rodar.Repository.SqlServer
{
    public class EventoRepository: IEventoRepository
    {
        public int Cadastrar(Evento evento)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"INSERT INTO Evento(dataCriacao
                                                        ,idUsuarioCriacao
                                                        ,nomeEvento
                                                        ,enderecoRua
                                                        ,enderecoComplemento
                                                        ,enderecoBairro
                                                        ,enderecoNumero
                                                        ,enderecoCEP
                                                        ,enderecoCidade
                                                        ,enderecoUF
                                                        ,urlImagemCapa
                                                        ,urlImagem1
                                                        ,urlImagem2
                                                        ,urlImagem3
                                                        ,urlImagem4
                                                        ,urlImagem5
                                                        ,dataHoraInicio
                                                        ,dataHoraTermino
                                                        ,descricaoEvento)
                                                    VALUES
                                                        (@dataCriacao
                                                        ,@idUsuarioCriacao
                                                        ,@nomeEvento
                                                        ,@enderecoRua
                                                        ,@enderecoComplemento
                                                        ,@enderecoBairro
                                                        ,@enderecoNumero
                                                        ,@enderecoCEP
                                                        ,@enderecoCidade
                                                        ,@enderecoUF
                                                        ,@urlImagemCapa
                                                        ,@urlImagem1
                                                        ,@urlImagem2
                                                        ,@urlImagem3
                                                        ,@urlImagem4
                                                        ,@urlImagem5
                                                        ,@dataHoraInicio
                                                        ,@dataHoraTermino
                                                        ,@descricaoEvento);
                                            SET @idEvento = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);

                ValidaCampos(ref evento);

                cmdInserir.Parameters.Add("idEvento", SqlDbType.Int);
                cmdInserir.Parameters["idEvento"].Direction = ParameterDirection.Output;

                if (evento.DataCriacao == null)
                    cmdInserir.Parameters.Add("dataCriacao", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdInserir.Parameters.Add("dataCriacao", SqlDbType.DateTime).Value = evento.DataCriacao;
                
                cmdInserir.Parameters.Add("idUsuarioCriacao", SqlDbType.Int).Value = evento.idUsuarioCriacao;
                cmdInserir.Parameters.Add("nomeEvento", SqlDbType.VarChar).Value = evento.nomeEvento;
                cmdInserir.Parameters.Add("enderecoRua", SqlDbType.VarChar).Value = evento.enderecoRua;
                cmdInserir.Parameters.Add("enderecoComplemento", SqlDbType.VarChar).Value = evento.enderecoComplemento;
                cmdInserir.Parameters.Add("enderecoBairro", SqlDbType.VarChar).Value = evento.enderecoBairro;

                if (evento.enderecoNumero == 0)
                    cmdInserir.Parameters.Add("enderecoNumero", SqlDbType.Int).Value = evento.enderecoNumero;
                else
                    cmdInserir.Parameters.Add("enderecoNumero", SqlDbType.Int).Value = DBNull.Value;

                cmdInserir.Parameters.Add("enderecoCEP", SqlDbType.VarChar).Value = evento.enderecoCEP;
                cmdInserir.Parameters.Add("enderecoCidade", SqlDbType.VarChar).Value = evento.enderecoCidade;
                cmdInserir.Parameters.Add("enderecoUF", SqlDbType.VarChar).Value = evento.enderecoUF;
                cmdInserir.Parameters.Add("urlImagemCapa", SqlDbType.VarChar).Value = evento.urlImagemCapa;
                cmdInserir.Parameters.Add("urlImagem1", SqlDbType.VarChar).Value = evento.urlImagem1;
                cmdInserir.Parameters.Add("urlImagem2", SqlDbType.VarChar).Value = evento.urlImagem2;
                cmdInserir.Parameters.Add("urlImagem3", SqlDbType.VarChar).Value = evento.urlImagem3;
                cmdInserir.Parameters.Add("urlImagem4", SqlDbType.VarChar).Value = evento.urlImagem4;
                cmdInserir.Parameters.Add("urlImagem5", SqlDbType.VarChar).Value = evento.urlImagem5;

                if (evento.dataHoraInicio == null)
                    cmdInserir.Parameters.Add("dataHoraInicio", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdInserir.Parameters.Add("dataHoraInicio", SqlDbType.DateTime).Value = evento.dataHoraInicio.Value;

                if (evento.dataHoraTermino == null)
                    cmdInserir.Parameters.Add("dataHoraTermino", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdInserir.Parameters.Add("dataHoraTermino", SqlDbType.DateTime).Value = evento.dataHoraTermino;

                cmdInserir.Parameters.Add("descricaoEvento", SqlDbType.VarChar).Value = evento.descricaoEvento;

                try
                {
                    con.Open();
                    cmdInserir.ExecuteNonQuery();

                    return (int)cmdInserir.Parameters["idEvento"].Value;
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

        public void Atualizar(Evento evento)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"UPDATE Evento
                                       SET dataCriacao = @dataCriacao
                                          ,nomeEvento = @nomeEvento
                                          ,enderecoRua = @enderecoRua
                                          ,enderecoComplemento = @enderecoComplemento
                                          ,enderecoBairro = @enderecoBairro
                                          ,enderecoNumero = @enderecoNumero
                                          ,enderecoCEP = @enderecoCEP
                                          ,enderecoCidade = @enderecoCidade
                                          ,enderecoUF = @enderecoUF
                                          ,urlImagemCapa = @urlImagemCapa
                                          ,urlImagem1 = @urlImagem1
                                          ,urlImagem2 = @urlImagem2
                                          ,urlImagem3 = @urlImagem3
                                          ,urlImagem4 = @urlImagem4
                                          ,urlImagem5 = @urlImagem5
                                          ,dataHoraInicio = @dataHoraInicio
                                          ,dataHoraTermino = @dataHoraTermino
                                          ,descricaoEvento = @descricaoEvento
                                     WHERE idEvento = @idEvento";

                SqlCommand cmdAtualizar = new SqlCommand(stringSQL, con);

                ValidaCampos(ref evento);

                cmdAtualizar.Parameters.Add("idEvento", SqlDbType.Int).Value = evento.idEvento;

                if (evento.DataCriacao == null)
                    cmdAtualizar.Parameters.Add("dataCriacao", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdAtualizar.Parameters.Add("dataCriacao", SqlDbType.DateTime).Value = evento.DataCriacao;

                cmdAtualizar.Parameters.Add("nomeEvento", SqlDbType.VarChar).Value = evento.nomeEvento;

                cmdAtualizar.Parameters.Add("enderecoRua", SqlDbType.VarChar).Value = evento.enderecoRua;
                cmdAtualizar.Parameters.Add("enderecoComplemento", SqlDbType.VarChar).Value = evento.enderecoComplemento;
                cmdAtualizar.Parameters.Add("enderecoBairro", SqlDbType.VarChar).Value = evento.enderecoBairro;
                cmdAtualizar.Parameters.Add("enderecoNumero", SqlDbType.Int).Value = evento.enderecoNumero;
                cmdAtualizar.Parameters.Add("enderecoCEP", SqlDbType.VarChar).Value = evento.enderecoCEP;
                cmdAtualizar.Parameters.Add("enderecoCidade", SqlDbType.VarChar).Value = evento.enderecoCidade;
                cmdAtualizar.Parameters.Add("enderecoUF", SqlDbType.VarChar).Value = evento.enderecoUF;
                cmdAtualizar.Parameters.Add("urlImagemCapa", SqlDbType.VarChar).Value = evento.urlImagemCapa;
                cmdAtualizar.Parameters.Add("urlImagem1", SqlDbType.VarChar).Value = evento.urlImagem1;
                cmdAtualizar.Parameters.Add("urlImagem2", SqlDbType.VarChar).Value = evento.urlImagem2;
                cmdAtualizar.Parameters.Add("urlImagem3", SqlDbType.VarChar).Value = evento.urlImagem3;
                cmdAtualizar.Parameters.Add("urlImagem4", SqlDbType.VarChar).Value = evento.urlImagem4;
                cmdAtualizar.Parameters.Add("urlImagem5", SqlDbType.VarChar).Value = evento.urlImagem5;

                if (evento.dataHoraInicio == null)
                    cmdAtualizar.Parameters.Add("dataNascimento", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdAtualizar.Parameters.Add("dataHoraInicio", SqlDbType.DateTime).Value = evento.dataHoraInicio.Value;

                if (evento.dataHoraTermino == null)
                    cmdAtualizar.Parameters.Add("dataHoraTermino", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdAtualizar.Parameters.Add("dataHoraTermino", SqlDbType.DateTime).Value = evento.dataHoraTermino;

                cmdAtualizar.Parameters.Add("descricaoEvento", SqlDbType.VarChar).Value = evento.descricaoEvento;

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

        public Evento Buscar(int idEvento)
        {
            Evento evento = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                        FROM Evento
                                        WHERE idEvento = @idEvento";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idEvento", SqlDbType.Int).Value = idEvento;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.Read())
                    {
                        evento = new Evento();
                        PreencheCampos(drSelecao, ref evento);
                    }

                    return evento;
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

        public int Excluir(int idEvento)
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"DELETE FROM Evento
                                        WHERE idEvento = @idEvento";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, con);

                cmdDeletar.Parameters.Add("idEvento", SqlDbType.Int).Value = idEvento;

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

        public IEnumerable<Evento> BuscarTodos()
        {
            List<Evento> eventos = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                var stringSQL = @"SELECT * FROM Evento";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        eventos = new List<Evento>();

                    while (drSelecao.Read())
                    {
                        Evento evento = new Evento();
                        PreencheCampos(drSelecao, ref evento);
                        eventos.Add(evento);
                    }

                    return eventos;
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

        public IEnumerable<Evento> BuscarPorUsuario(int idUsuario = 0)
        {
            if (idUsuario == 0)
                return BuscarTodos();

            List<Evento> eventos = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                var stringSQL = @"SELECT * FROM Evento WHERE idUsuarioCriacao = @idUsuarioCriacao";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idUsuarioCriacao", SqlDbType.Int).Value = idUsuario;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.HasRows)
                        eventos = new List<Evento>();

                    while (drSelecao.Read())
                    {
                        Evento evento = new Evento();

                        PreencheCampos(drSelecao, ref evento);

                        eventos.Add(evento);
                    }

                    return eventos;
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

        private static void PreencheCampos(SqlDataReader drSelecao, ref Evento evento)
        {
            if (drSelecao["idEvento"] != DBNull.Value)
                evento.idEvento = Convert.ToInt32(drSelecao["idEvento"].ToString());

            if (drSelecao["dataCriacao"] != DBNull.Value)
                evento.DataCriacao = Convert.ToDateTime(drSelecao["dataCriacao"]);
            else
                evento.DataCriacao = null;

            if (drSelecao["idUsuarioCriacao"] != DBNull.Value)
                evento.idUsuarioCriacao = Convert.ToInt32(drSelecao["idUsuarioCriacao"].ToString());

            if (drSelecao["nomeEvento"] != DBNull.Value)
                evento.nomeEvento = drSelecao["nomeEvento"].ToString();

            if (drSelecao["enderecoRua"] != DBNull.Value)
                evento.enderecoRua = drSelecao["enderecoRua"].ToString();

            if (drSelecao["enderecoComplemento"] != DBNull.Value)
                evento.enderecoComplemento = drSelecao["enderecoComplemento"].ToString();

            if (drSelecao["enderecoBairro"] != DBNull.Value)
                evento.enderecoBairro = drSelecao["enderecoBairro"].ToString();

            if (drSelecao["enderecoNumero"] != DBNull.Value)
                evento.enderecoNumero = Convert.ToInt32(drSelecao["enderecoNumero"].ToString());

            if (drSelecao["enderecoCEP"] != DBNull.Value)
                evento.enderecoCEP = drSelecao["enderecoCEP"].ToString();

            if (drSelecao["enderecoCidade"] != DBNull.Value)
                evento.enderecoCidade = drSelecao["enderecoCidade"].ToString();

            if (drSelecao["enderecoUF"] != DBNull.Value)
                evento.enderecoUF = drSelecao["enderecoUF"].ToString();

            if (drSelecao["urlImagemCapa"] != DBNull.Value)
                evento.urlImagemCapa = drSelecao["urlImagemCapa"].ToString();

            if (drSelecao["urlImagem1"] != DBNull.Value)
                evento.urlImagem1 = drSelecao["urlImagem1"].ToString();

            if (drSelecao["urlImagem2"] != DBNull.Value)
                evento.urlImagem2 = drSelecao["urlImagem2"].ToString();

            if (drSelecao["urlImagem3"] != DBNull.Value)
                evento.urlImagem3 = drSelecao["urlImagem3"].ToString();

            if (drSelecao["urlImagem4"] != DBNull.Value)
                evento.urlImagem4 = drSelecao["urlImagem4"].ToString();

            if (drSelecao["urlImagem5"] != DBNull.Value)
                evento.urlImagem5 = drSelecao["urlImagem5"].ToString();

            if (drSelecao["dataHoraInicio"] != DBNull.Value)
                evento.dataHoraInicio = Convert.ToDateTime(drSelecao["dataHoraInicio"]);
            else
                evento.dataHoraInicio = null;

            if (drSelecao["dataHoraTermino"] != DBNull.Value)
                evento.dataHoraTermino = Convert.ToDateTime(drSelecao["dataHoraTermino"]);
            else
                evento.dataHoraTermino = null;

            if (drSelecao["descricaoEvento"] != DBNull.Value)
                evento.descricaoEvento = drSelecao["descricaoEvento"].ToString();
        }

        private static void ValidaCampos(ref Evento evento)
        {
            if (String.IsNullOrEmpty(evento.nomeEvento)) { evento.nomeEvento = String.Empty; }
            if (String.IsNullOrEmpty(evento.enderecoRua)) { evento.enderecoRua = String.Empty; }
            if (String.IsNullOrEmpty(evento.enderecoComplemento)) { evento.enderecoComplemento = String.Empty; }
            if (String.IsNullOrEmpty(evento.enderecoBairro)) { evento.enderecoBairro = String.Empty; }
            if (String.IsNullOrEmpty(evento.enderecoCEP)) { evento.enderecoCEP = String.Empty; }
            if (String.IsNullOrEmpty(evento.enderecoCidade)) { evento.enderecoCidade = String.Empty; }
            if (String.IsNullOrEmpty(evento.enderecoUF)) { evento.enderecoUF = String.Empty; }
            if (String.IsNullOrEmpty(evento.urlImagemCapa)) { evento.urlImagemCapa = String.Empty; }
            if (String.IsNullOrEmpty(evento.urlImagem1)) { evento.urlImagem1 = String.Empty; }
            if (String.IsNullOrEmpty(evento.urlImagem2)) { evento.urlImagem2 = String.Empty; }
            if (String.IsNullOrEmpty(evento.urlImagem3)) { evento.urlImagem3 = String.Empty; }
            if (String.IsNullOrEmpty(evento.urlImagem4)) { evento.urlImagem4 = String.Empty; }
            if (String.IsNullOrEmpty(evento.urlImagem5)) { evento.urlImagem5 = String.Empty; }
            if (String.IsNullOrEmpty(evento.descricaoEvento)) { evento.descricaoEvento = String.Empty; }
        }
    }
}