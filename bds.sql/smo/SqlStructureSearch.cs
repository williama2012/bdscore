using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using bds.sql;

namespace bds.sql.smo {

    public delegate void SqlStructureSearchConnectionEvent(bool serverConnected, bool databaseConnected, string serverName, string databaseName);
    public delegate void SqlStructureSearchStatusEvent(string status, bool isBusy);
    public delegate void SqlStructureSearchCountEvent(int count);

    public delegate void SqlStructureSearchBusy(bool isBusy);

    public class SqlStructureSearchItem {
        public SqlStructureSearchItem(string type, string name, string body = "") {
            Type = type;
            Name = name;
            Body = body;
        }
        public string Type { get; private set; }
        public string Name { get; private set; }
        public string Body { get; private set; }
    }

    public class SqlStructureSearch {

        private Thread _thread;

        #region Events

        public event SqlStructureSearchConnectionEvent ConnectionChanged;
        public event SqlStructureSearchStatusEvent StatusChanged;
        public event SqlStructureSearchCountEvent ObjectCountChanged;

        private void CallConnectionChanged() {
            if (ConnectionChanged == null) return;
            CallStatusChanged("Connecting...", false);
            ConnectionChanged(_server != null, _database != null, _server != null ? _server.Name : "Not Connected", _database != null ? _database.Name : "Not Connected");
            CallStatusChanged("Idle.", false);
        }
        private void CallStatusChanged(string status, bool isBusy) {
            if (StatusChanged == null) return;
            Thread.Sleep(0);
            StatusChanged(status, isBusy);
            Thread.Sleep(0);
        }
        private void CallObjectCountChanged(int count) {
            if (ObjectCountChanged == null) return;
            ObjectCountChanged(count);
        }

        #endregion

        #region Connections

        private Server _server;
        private Database _database;
        public void ConnectServer(string server) {
            CallStatusChanged(string.Format("Connecting Server: {0}", server), true);
            _server = new Server(server);
            CallConnectionChanged();
            CallStatusChanged("Idle.", false);
        }

        public void ConnectDb(string database) {
            if (_server == null) return;
            CallStatusChanged(string.Format("Connecting Database: {0}", database), true);
            _database = _server.Databases[database];
            CallConnectionChanged();
            CallStatusChanged("Idle.", false);
        }

        #endregion

        private List<SqlStructureSearchItem> _cache = new List<SqlStructureSearchItem>();
        public List<SqlStructureSearchItem> Cache {
            get { return _cache; }
        }

        public IEnumerable<string> Databases {
            get {
                if (_server == null)
                    return new List<string>();
                return _server.Databases
                    .Cast<Database>()
                    .Where(db => !db.IsSystemObject)
                    .Select(db => db.Name);
            }
        }

        public void Stop() {
            if (_thread != null) {
                _thread.Abort();
            }
        }

        public void CacheObjects() {
            if (_thread != null && _thread.IsAlive)
                return;
            _thread = new Thread(RunCacheObjectsInDatabase);
            Thread.Sleep(0);
            _thread.Start();
        }

        private void RunCacheObjectsInDatabase() {
            _cache.Clear();
            var count = 0;

            CallStatusChanged("Caching Views, Procedure next...", true);
            foreach (var view in _database.Views.Cast<View>().Where(i => !i.IsSystemObject)) {
                _cache.Add(new SqlStructureSearchItem("View", view.Name, view.TextBody));
                CallObjectCountChanged(count);
                count++;
            }
            CallStatusChanged("Caching Procedures, Functions next...", true);
            foreach (var procedure in _database.StoredProcedures.Cast<StoredProcedure>().Where(i => !i.IsSystemObject)) {
                _cache.Add(new SqlStructureSearchItem("Procedure", procedure.Name, procedure.TextBody));
                CallObjectCountChanged(count);
                count++;
            }
            CallStatusChanged("Caching Functions...", true);
            foreach (var function in _database.UserDefinedFunctions.Cast<UserDefinedFunction>().Where(i => !i.IsSystemObject)) {
                _cache.Add(new SqlStructureSearchItem(function.FunctionType.ToString(), function.Name, function.TextBody));
                CallObjectCountChanged(count);
                count++;
            }
            CallStatusChanged("Idle.", false);
        }

        public IEnumerable<SqlStructureSearchItem> Search(string query) {
            if (_database == null) return null;
            CallStatusChanged(string.Format("Searching for {0}", query), true);
            query = query.ToLowerInvariant();
            var items = Cache.Where(c => c.Body.ToLowerInvariant().Contains(query));
            CallStatusChanged("Idle.", false);
            return items;
        }
    }
}
