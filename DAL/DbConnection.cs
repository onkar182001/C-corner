using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MahindraCrud.DAL
{
    public class DbConnection
    {
        public static SqlConnection GetConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            return new SqlConnection(connString);
        }
    }
}
