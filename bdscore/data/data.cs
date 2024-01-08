using System;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bds.core.data {

    public static class DatatableFactory {
        private const string defaultName = "table";
        private const string defaultColumn = "col_";

        public static DataTable buildTable(int columns) {
            return buildTable(defaultName, columns);
        }

        public static DataTable buildTable(string tableName, int columns ) {
            DataTable dt = new DataTable(tableName);
            for (int col = 0; col < columns; col++)
            {
                dt.Columns.Add(defaultColumn + col.ToString(), typeof(string));
            }
            return dt;
        }

        public static DataTable buildTable(params string[] columns) {
            return buildTable(defaultName, columns);
        }

        public static DataTable buildTable(string tableName, params string[] columns) {
            DataTable dt = new DataTable(); int i = 0;
            foreach (string col in columns) {
                try {
                    dt.Columns.Add(defaultColumn + i.ToString(), Type.GetType(col));
                    i++;
                } catch (Exception e) { 
                    dt.Columns.Add(col,typeof(string));
                    i++;
                }
            }
            return dt;
        }


    }
}
