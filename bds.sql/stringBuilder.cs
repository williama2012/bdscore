using System;
using System.Linq;

namespace bds.sql {
    public static class stringBuilder {
        public static string buildConnString(string server, string database) {
            return string.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;", server, database);
        }

        public static string buildConnString(string server, string database, string user, string password) {
            return string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", server, database, user, password);
        }

        public static string buildQuery(string table, string column) {
            return string.Format("SELECT {1} FROM {0}", table, column);
        }

        public static string buildQuery(string table, string keyField, string searchString) {
            return string.Format("SELECT * FROM {0} WHERE {1} = {2}", table, keyField, searchString);
        }

    }


}
