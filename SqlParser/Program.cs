using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using bds.sql;
using bds.sql.ado;

namespace SqlParser {
    class Program {
        static void Main(string[] args) {

            var createTableText = "CREATE TABLE [";
            var goText = @"GO" + Environment.NewLine;
            var sql = File.ReadAllText(@"C:\Users\william\Desktop\NTS_requested\EWorkDB.sql");

            bool go = true;
            int nextCreate = 0, nextGo = 0;
            var sb = new StringWriter();

            
            while (go) {
                nextCreate = sql.IndexOf(createTableText, nextCreate + createTableText.Length);
                if (nextCreate == -1)
                    break;
                nextGo = sql.IndexOf(goText,nextCreate, StringComparison.Ordinal);
                if (nextGo == -1)
                    break;
                var str = sql.Substring(nextCreate, nextGo - nextCreate);
                sb.Write(str);
                sb.WriteLine(goText);
                sb.WriteLine();

                Console.WriteLine(nextCreate);

            }
            File.WriteAllText(@"c:\output.sql", sb.ToString());

            Console.WriteLine("Done.");
            var wait = Console.ReadLine();
        }
    }
}
