﻿using Rodar.Domain.Entity;
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
    public class UsuarioRepository: IUsuarioRepository
    {
        public void Cadastrar(Usuario Usuario)
        {
            ValidaCampos(ref Usuario);

            if (ExisteUsuario(Usuario.Email, Usuario.CPF))
                throw new Exception("Já existe um usuário cadastrado com este CPF ou Email.");               

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"INSERT INTO Usuario(Nome, Sobrenome, RG, CPF, urlImagemSelfie, Genero, Descricao, dataNascimento, Email, numeroTelefone, Senha, facebookId, googleId, urlImagemRGFrente, urlImagemRGTras, urlImagemCPF, Transportador, OrganizadorEvento, tokenNotificacao)
                                            VALUES(@Nome, @Sobrenome, @RG, @CPF, @urlImagemSelfie, @Genero, @Descricao, @dataNascimento, @Email, @numeroTelefone, @Senha, @facebookId, @googleId, @urlImagemRGFrente, @urlImagemRGTras, @urlImagemCPF, @Transportador, @OrganizadorEvento, @tokenNotificacao);
                                            SET @idUsuario = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, con);

                cmdInserir.Parameters.Add("idUsuario", SqlDbType.Int);
                cmdInserir.Parameters["idUsuario"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("Nome", SqlDbType.VarChar).Value = Usuario.Nome;
                cmdInserir.Parameters.Add("Sobrenome", SqlDbType.VarChar).Value = Usuario.Sobrenome;
                cmdInserir.Parameters.Add("RG", SqlDbType.VarChar).Value = Usuario.RG;
                cmdInserir.Parameters.Add("CPF", SqlDbType.VarChar).Value = Usuario.CPF;
                cmdInserir.Parameters.Add("urlImagemSelfie", SqlDbType.VarChar).Value = Usuario.urlImagemSelfie;
                cmdInserir.Parameters.Add("Genero", SqlDbType.VarChar).Value = Usuario.Genero;
                cmdInserir.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Usuario.Descricao;

                if (Usuario.dataNascimento == null)
                    cmdInserir.Parameters.Add("dataNascimento", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdInserir.Parameters.Add("dataNascimento", SqlDbType.DateTime).Value = Usuario.dataNascimento.Value;

                cmdInserir.Parameters.Add("Email", SqlDbType.VarChar).Value = Usuario.Email;
                cmdInserir.Parameters.Add("numeroTelefone", SqlDbType.VarChar).Value = Usuario.numeroTelefone;
                cmdInserir.Parameters.Add("Senha", SqlDbType.VarChar).Value = Usuario.Senha;
                cmdInserir.Parameters.Add("facebookId", SqlDbType.VarChar).Value = Usuario.facebookId;
                cmdInserir.Parameters.Add("googleId", SqlDbType.VarChar).Value = Usuario.googleId;
                cmdInserir.Parameters.Add("urlImagemRGTras", SqlDbType.VarChar).Value = Usuario.urlImagemRGTras;
                cmdInserir.Parameters.Add("urlImagemRGFrente", SqlDbType.VarChar).Value = Usuario.urlImagemRGFrente;
                cmdInserir.Parameters.Add("urlImagemCPF", SqlDbType.VarChar).Value = Usuario.urlImagemCPF;
                cmdInserir.Parameters.Add("Transportador", SqlDbType.Bit).Value = Usuario.Transportador;
                cmdInserir.Parameters.Add("OrganizadorEvento", SqlDbType.Bit).Value = Usuario.OrganizadorEvento;
                cmdInserir.Parameters.Add("tokenNotificacao", SqlDbType.VarChar).Value = Usuario.tokenNotificacao;

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

        public void Atualizar(Usuario Usuario)
        {
            ValidaCampos(ref Usuario);

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"UPDATE Usuario
                                   SET Nome = @Nome
                                      ,Sobrenome = @Sobrenome
                                      ,RG = @RG
                                      ,CPF = @CPF
                                      ,urlImagemSelfie = @urlImagemSelfie
                                      ,Genero = @Genero
                                      ,Descricao = @Descricao
                                      ,dataNascimento = @dataNascimento
                                      ,numeroTelefone = @numeroTelefone
                                      ,Senha = @Senha
                                      ,facebookId = @facebookId
                                      ,googleId = @googleId
                                      ,urlImagemRGFrente = @urlImagemRGFrente
                                      ,urlImagemRGTras = @urlImagemRGTras
                                      ,urlImagemCPF = @urlImagemCPF
                                      ,Transportador = @Transportador
                                      ,OrganizadorEvento = @OrganizadorEvento
                                      ,tokenNotificacao = @tokenNotificacao
                                 WHERE Email = @Email";

                SqlCommand cmdAtualizar = new SqlCommand(stringSQL, con);

                cmdAtualizar.Parameters.Add("Email", SqlDbType.VarChar).Value = Usuario.Email;

                cmdAtualizar.Parameters.Add("Nome", SqlDbType.VarChar).Value = Usuario.Nome;
                cmdAtualizar.Parameters.Add("Sobrenome", SqlDbType.VarChar).Value = Usuario.Sobrenome;
                cmdAtualizar.Parameters.Add("RG", SqlDbType.VarChar).Value = Usuario.RG;
                cmdAtualizar.Parameters.Add("CPF", SqlDbType.VarChar).Value = Usuario.CPF;
                cmdAtualizar.Parameters.Add("urlImagemSelfie", SqlDbType.VarChar).Value = Usuario.urlImagemSelfie;
                cmdAtualizar.Parameters.Add("Genero", SqlDbType.VarChar).Value = Usuario.Genero;
                cmdAtualizar.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Usuario.Descricao;

                if (Usuario.dataNascimento == null)
                    cmdAtualizar.Parameters.Add("dataNascimento", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    cmdAtualizar.Parameters.Add("dataNascimento", SqlDbType.DateTime).Value = Usuario.dataNascimento.Value;

                cmdAtualizar.Parameters.Add("Senha", SqlDbType.VarChar).Value = Usuario.Senha;

                cmdAtualizar.Parameters.Add("numeroTelefone", SqlDbType.VarChar).Value = Usuario.numeroTelefone;
                cmdAtualizar.Parameters.Add("facebookId", SqlDbType.VarChar).Value = Usuario.facebookId;
                cmdAtualizar.Parameters.Add("googleId", SqlDbType.VarChar).Value = Usuario.googleId;
                cmdAtualizar.Parameters.Add("urlImagemRGTras", SqlDbType.VarChar).Value = Usuario.urlImagemRGTras;
                cmdAtualizar.Parameters.Add("urlImagemRGFrente", SqlDbType.VarChar).Value = Usuario.urlImagemRGFrente;
                cmdAtualizar.Parameters.Add("urlImagemCPF", SqlDbType.VarChar).Value = Usuario.urlImagemCPF;
                cmdAtualizar.Parameters.Add("Transportador", SqlDbType.Bit).Value = Usuario.Transportador;
                cmdAtualizar.Parameters.Add("OrganizadorEvento", SqlDbType.Bit).Value = Usuario.OrganizadorEvento;
                cmdAtualizar.Parameters.Add("tokenNotificacao", SqlDbType.VarChar).Value = Usuario.tokenNotificacao;

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

        public Usuario BuscarPorEmail(string Email)
        {
            Usuario usuario = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                    FROM Usuario
                                    WHERE Email = @Email";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("Email", SqlDbType.VarChar).Value = Email;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.Read())
                    {
                        usuario = new Usuario();
                        PreencheCampos(drSelecao, ref usuario);
                    }

                    return usuario;
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

        public Usuario BuscarPorId(int idUsuario)
        {
            Usuario usuario = null;

            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT *
                                    FROM Usuario
                                    WHERE idUsuario = @idUsuario";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;

                try
                {
                    con.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.Read())
                    {
                        usuario = new Usuario();
                        PreencheCampos(drSelecao, ref usuario);
                    }

                    return usuario;
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

        private void PreencheCampos(SqlDataReader drSelecao, ref Usuario usuario)
        {
            if (drSelecao["idUsuario"] != DBNull.Value)
                usuario.idUsuario = Convert.ToInt32(drSelecao["idUsuario"].ToString());

            if (drSelecao["dataNascimento"] != DBNull.Value)
                usuario.dataNascimento = Convert.ToDateTime(drSelecao["dataNascimento"]);
            else
                usuario.dataNascimento = null;

            if (drSelecao["Nome"] != DBNull.Value)
                usuario.Nome = drSelecao["Nome"].ToString();

            if (drSelecao["Sobrenome"] != DBNull.Value)
                usuario.Sobrenome = drSelecao["Sobrenome"].ToString();

            if (drSelecao["RG"] != DBNull.Value)
                usuario.RG = drSelecao["RG"].ToString();

            if (drSelecao["CPF"] != DBNull.Value)
                usuario.CPF = drSelecao["CPF"].ToString();

            if (drSelecao["urlImagemSelfie"] != DBNull.Value)
                usuario.urlImagemSelfie = drSelecao["urlImagemSelfie"].ToString();

            if (drSelecao["Genero"] != DBNull.Value)
                usuario.Genero = drSelecao["Genero"].ToString();

            if (drSelecao["Descricao"] != DBNull.Value)
                usuario.Descricao = drSelecao["Descricao"].ToString();

            if (drSelecao["Email"] != DBNull.Value)
                usuario.Email = drSelecao["Email"].ToString();

            if (drSelecao["numeroTelefone"] != DBNull.Value)
                usuario.numeroTelefone = drSelecao["numeroTelefone"].ToString();

            if (drSelecao["Senha"] != DBNull.Value)
                usuario.Senha = drSelecao["Senha"].ToString();

            if (drSelecao["facebookId"] != DBNull.Value)
                usuario.facebookId = drSelecao["facebookId"].ToString();

            if (drSelecao["googleId"] != DBNull.Value)
                usuario.googleId = drSelecao["googleId"].ToString();

            if (drSelecao["urlImagemRGFrente"] != DBNull.Value)
                usuario.urlImagemRGFrente = drSelecao["urlImagemRGFrente"].ToString();

            if (drSelecao["urlImagemRGTras"] != DBNull.Value)
                usuario.urlImagemRGTras = drSelecao["urlImagemRGTras"].ToString();

            if (drSelecao["urlImagemCPF"] != DBNull.Value)
                usuario.urlImagemCPF = drSelecao["urlImagemCPF"].ToString();

            if (drSelecao["Transportador"] != DBNull.Value)
                usuario.Transportador = Convert.ToBoolean(drSelecao["Transportador"].ToString());

            if (drSelecao["OrganizadorEvento"] != DBNull.Value)
                usuario.OrganizadorEvento = Convert.ToBoolean(drSelecao["OrganizadorEvento"].ToString());

            if (drSelecao["tokenNotificacao"] != DBNull.Value)
                usuario.tokenNotificacao = drSelecao["tokenNotificacao"].ToString();
        }

        private void ValidaCampos(ref Usuario usuario)
        {
            if (String.IsNullOrEmpty(usuario.CPF)) { usuario.CPF = String.Empty; }
            if (String.IsNullOrEmpty(usuario.Descricao)) { usuario.Descricao = String.Empty; }
            if (String.IsNullOrEmpty(usuario.Email)) { usuario.Email = String.Empty; }
            if (String.IsNullOrEmpty(usuario.facebookId)) { usuario.facebookId = String.Empty; }
            if (String.IsNullOrEmpty(usuario.Genero)) { usuario.Genero = String.Empty; }
            if (String.IsNullOrEmpty(usuario.googleId)) { usuario.googleId = String.Empty; }
            if (String.IsNullOrEmpty(usuario.Nome)) { usuario.Nome = String.Empty; }
            if (String.IsNullOrEmpty(usuario.Sobrenome)) { usuario.Sobrenome = String.Empty; }
            if (String.IsNullOrEmpty(usuario.numeroTelefone)) { usuario.numeroTelefone = String.Empty; }
            if (String.IsNullOrEmpty(usuario.RG)) { usuario.RG = String.Empty; }
            if (String.IsNullOrEmpty(usuario.Senha)) { usuario.Senha = String.Empty; }
            if (String.IsNullOrEmpty(usuario.urlImagemCPF)) { usuario.urlImagemCPF = String.Empty; }
            if (String.IsNullOrEmpty(usuario.urlImagemRGFrente)) { usuario.urlImagemRGFrente = String.Empty; }
            if (String.IsNullOrEmpty(usuario.urlImagemRGTras)) { usuario.urlImagemRGTras = String.Empty; }
            if (String.IsNullOrEmpty(usuario.urlImagemSelfie)) { usuario.urlImagemSelfie = String.Empty; }
            if (String.IsNullOrEmpty(usuario.tokenNotificacao)) { usuario.tokenNotificacao = String.Empty; }
        }

        private bool ExisteUsuario(string Email = "", string CPF = "")
        {
            using (SqlConnection con = Database.GetCurrentDbConnection())
            {
                string stringSQL = @"SELECT 1
                                    FROM Usuario
                                    WHERE Email = @Email OR CPF = @CPF";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, con);

                cmdSelecionar.Parameters.Add("Email", SqlDbType.VarChar).Value = Email;
                cmdSelecionar.Parameters.Add("CPF", SqlDbType.VarChar).Value = CPF;

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
    }
}
