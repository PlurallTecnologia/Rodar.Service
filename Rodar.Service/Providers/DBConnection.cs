using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Rodar.Service.Providers
{
    public class DBConnection
    {
        public static SqlConnection GetDBConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["rodarDB"].ToString());
        }
    }
}