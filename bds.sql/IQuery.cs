using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace bds.sql {
    public interface IQuery {
        SqlTransaction Transaction { get; }
        queryResult QueryResult { get; }
    }
}
