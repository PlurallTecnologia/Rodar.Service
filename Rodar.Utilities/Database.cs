using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Utilities
{
    public class Database
    {
        public static string getConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["rodarDB"].ConnectionString;
        }

        public static SqlConnection GetCurrentDbConnection()
        {
            return new SqlConnection(getConnectionString());
        }
    }
}
