using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace MVCEmployeeScheduler.DataAccess
{
    public class DAL
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMDBContext"].ToString());

        public static bool UserIsValid(string EmailAddress, string Password)
        {
            bool authenticated = false;

            string query = string.Format("select * FROM Employee WHERE EmailAddress LIKE '{0}' AND password= '{1}'", EmailAddress, Password);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            authenticated = sdr.HasRows;
            conn.Close();
            return (authenticated);
        }
        public static bool Position(string Position, string EmailAddress)
        {
            bool authenticated = false;

			string query = string.Format("select * FROM Employee WHERE EmailAddress LIKE '" + EmailAddress + "' AND position LIKE '" + Position + "'");
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            authenticated = sdr.HasRows;
            conn.Close();
            return (authenticated);
        }
    }

}