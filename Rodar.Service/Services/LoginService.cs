using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Rodar.Service.Globals;
using Rodar.Service.Providers;

namespace Rodar.Service.Services
{
    public class LoginService
    {
        public static bool Login(string login, string senha)
        {
            SqlConnection con = DBConnection.GetDBConnection();
            SqlCommand cmdSelecionar = new SqlCommand();
            var retorno = false;

            try
            {
                cmdSelecionar.CommandType = CommandType.Text;
                cmdSelecionar.CommandText = "SELECT idUsuario FROM Usuario WHERE Email ='" + login.Trim() + "' and Senha='" + senha.Trim() + "'";
                cmdSelecionar.Connection = con;

                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();
                SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                if (drSelecao.Read())
                {
                    LoggedUserInformation.userId = Convert.ToInt32(drSelecao["idUsuario"].ToString());
                    LoggedUserInformation.userEmail = login.Trim();
                    return true;
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }

            return retorno;
        }

        public static bool LoginByFacebook(string facebookId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["rodarDB"].ToString());
            SqlCommand cmd = new SqlCommand();

            int retorno = 0;

            try
            {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT count(*) FROM Usuario WHERE facebookId ='" + facebookId + "'";
                    cmd.Connection = con;

                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();
                retorno = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            catch
            {
            }

            return retorno == 1;
        }


    }
}