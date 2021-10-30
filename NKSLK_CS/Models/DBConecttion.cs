using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NKSLK_CS.Models
{
    public class DBConecttion
    {
        string strCon;
        public DBConecttion()
        {
            strCon = ConfigurationManager.ConnectionStrings["NKSLK_Context"].ConnectionString;

        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(strCon);
        }
    }
}