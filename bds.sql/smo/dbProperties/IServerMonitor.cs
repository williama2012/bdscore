using System;
using System.Collections;
using System.Data;
using Microsoft.SqlServer.Management.Smo;
using System.Xml;

namespace bds.sql.smo.dbProperties {
    public interface IServerMonitor : IDisposable {
        Server ConnectedServer { get; }
        ArrayList DatabaseList { get; }
        DataTable Process(string server, ArrayList databaseList);
        DataTable GetDatabaseInformation(Database database);
        DataRow GetDatabaseProperties(Database database, DataTable table);
        object GetDatabaseProperty(Database database, string property);
        //void Dispose();
        void Dispose(bool disposing);
        void ReleaseResources();
    }
}