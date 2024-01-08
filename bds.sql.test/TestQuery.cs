using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using bds.sql;
using NUnit.Framework;


namespace bds.sql.test {

    [TestFixture]
    class TestQuery {

        IQuery query;

        SqlConnection connection;

        [TestFixtureSetUp]
        public void Setup() { 
            connection = new SqlConnection(stringBuilder.buildConnString("(local)","TestDatabase"));
        
        }

        [Test]
        public void Test() {
             



        }

    }

}
