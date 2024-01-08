using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace bds.sql {
    /// <summary>
    /// Responsible for maintaining the connection.
    /// </summary>
    public class TransactionHelper {

        private static readonly string[] CmdSeperators = new[] { "GO" + Environment.NewLine };

        public static IEnumerable<SqlCommand> CreateCommandSet(string cmds, SqlTransaction transaction) {
            var commands = cmds.Split(CmdSeperators, StringSplitOptions.RemoveEmptyEntries);
            return commands.Where(c => !string.IsNullOrWhiteSpace(c)).Select(c => CreateCommand(c, transaction));
        }

        public static SqlCommand CreateCommand(string cmd, SqlTransaction transaction) {
            return new SqlCommand(cmd, transaction.Connection, transaction);
        }
        
        public static SqlCommandWrapper CreateWrappedCommand(string fileName, string cmds, SqlTransaction transaction) {
            return new SqlCommandWrapper() { FileName = fileName, Commands = CreateCommandSet(cmds, transaction) };
        }
    }

    public class SqlCommandWrapper {
        public string FileName { get; set; }
        public IEnumerable<SqlCommand> Commands { get; set; }
    }

}
