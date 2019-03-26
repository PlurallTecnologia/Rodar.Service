using Rodar.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Rodar.Service.Controllers
{
    public class LoginController : ApiController
    {
        //// GET: api/Login
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Login/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Login
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Login/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Login/5
        //public void Delete(int id)
        //{
        //}

        //[HttpGet]
        //[ActionName("Login")]
        //public string Login()
        //{
        //    return "Loguei";

        //}

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["rodarDB"].ToString());
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adp = null;

        [HttpPost]
        [ActionName("DoLogin")]
        public int Post([System.Web.Http.FromBody] Login Login)
        {
            int retorno = 0;

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT count(*) FROM Usuario WHERE Email ='" + Login.Email.Trim() + "' and Senha='" + Login.Senha.Trim() + "'";
                cmd.Connection = con;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                retorno = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            catch
            {
            }

            return retorno;
        }
    }
}
