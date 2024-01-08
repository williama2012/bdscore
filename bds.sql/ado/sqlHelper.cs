using System;
using System.Data;
using System.Data.SqlClient;

namespace bds.sql.ado{
    public static class sqlHelper {

        public static object ExecuteScalar(string connectionString, string queryString) {
            object returnValue = new object();
            try {
                using (SqlConnection conn = new SqlConnection(connectionString)) {
                    conn.Open();
                    returnValue = new SqlCommand(queryString, conn).ExecuteScalar();
                    conn.Close();
                }
            }
            catch (Exception e) {

            }
            return returnValue;
        }

        public static DataSet ExecuteQuery(string connectionString, string queryString) {
            DataSet ds = new DataSet();
            try {
                using (SqlConnection conn = new SqlConnection(connectionString)) {
                    new SqlDataAdapter(queryString, conn).Fill(ds);
                }
            } catch (Exception e) {

                ds.Namespace = e.Message;
            }
            return ds;
        }

        public static int ExecuteNoQuery(string connectionString, string queryString) {



            return -1;
        } 

    }
}
