using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Configuration;

namespace bds.sql.smo.dbProperties {
    public class ServerMonitor : IServerMonitor {

        #region Instance Properties

        private Server _server;
        private ArrayList _databaseList;
        private SiteMonitor _siteMonitor;

        public Server ConnectedServer {
            get { return _server; }
        }
        
        public ArrayList DatabaseList {
            get { return _databaseList; }
        }

        public SiteMonitor SiteServerMonitor {
            get { return _siteMonitor; }
        }

        #endregion

        public ServerMonitor(SiteMonitor siteMonitor) {
            _siteMonitor = siteMonitor;
        }

        public DataTable Process(string server, ArrayList databaseList) {
            _databaseList = databaseList;
            _server = new Server(server);

            DataTable dt = SiteServerMonitor.NewDataTable(ConnectedServer.Name);
            
            foreach(string dbName in DatabaseList) {
                Database db = ConnectedServer.Databases[dbName];
                dt.Merge(GetDatabaseInformation(db));
            }
            return dt;
        }

        public DataTable GetDatabaseInformation(Database database) {
            var dt = SiteServerMonitor.NewDataTable(ConnectedServer.Name);
            dt.Rows.Add(GetDatabaseProperties(database, dt));
            return dt;
        }

        public DataRow GetDatabaseProperties(Database database, DataTable table) {
            var row = table.NewRow();

            row["Server"] = database == null ? "Connection/Database Failure" : database.Parent.Name;
            row["Database"] = database == null ? string.Empty : database.Name;

            foreach (XmlElement property in SiteServerMonitor.PropertiesList.GetElementsByTagName("property")) {
                if (!property.HasAttribute("name"))
                    throw new ApplicationException("Property node must have attribute 'name' defined.");
                var propName = property.GetAttribute("name");
                row[propName] = database == null ? DBNull.Value : GetDatabaseProperty(database, propName);
                    
            }
            return row;
        }

        public object GetDatabaseProperty(Database database, string property) {
            if(database.Properties.Contains(property)) {
                return database.Properties[property].Value;
            }
            return DBNull.Value;
        }

        #region IDisposible Members

        private bool _disposed = false;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    ReleaseResources();
                }
            }
            _disposed = true;
        }

        public void ReleaseResources() {
            _server = null;
        }

        #endregion

    }
}
