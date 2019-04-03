using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Rodar.Service.Services
{
    public class LoginService
    {
        public static bool Login(string login, string senha)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["rodarDB"].ToString());
            SqlCommand cmd = new SqlCommand();

            int retorno = 0;

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT count(*) FROM Usuario WHERE Email ='" + login.Trim() + "' and Senha='" + senha.Trim() + "'";
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