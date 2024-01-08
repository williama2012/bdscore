using System;
using System.Data.SqlClient;

namespace bds.sql {
    public static class ConnectionHelper {

        public static SqlConnection OpenConnection(string connectionString) {
            try {
                var connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception e) {

                throw new InvalidOperationException("Connection Failed");
            }
        }
    }
}
