using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace bds.sql.smo.dbProperties {
    public class SiteMonitor    {

        #region Public Instance Properties

        private string _databaseListFilename;
        private string _propertiesListFilename;

        public string PropertiesFile {
            get { return _propertiesListFilename; }
        }

        public string DatabaseFile {
            get { return _databaseListFilename; }
        }

        public  XmlElement PropertiesList {
            get {

                if (!File.Exists(PropertiesFile))
                    throw new ApplicationException("Property file not found.");

                XmlDocument doc = new XmlDocument();
                doc.Load(PropertiesFile);

                var propertiesList = doc.DocumentElement;

                if (propertiesList == null)
                    throw new ApplicationException("No properties found in list.");

                return propertiesList;
            }
        }

        public XmlNodeList ServersInConfig {
            get { return GetServersFromConfig(); }
        }

        #endregion

        public SiteMonitor(string databaseListFilename, string propertiesListFilename) {
            _databaseListFilename = databaseListFilename;
            _propertiesListFilename = propertiesListFilename;

        }

        public DataView ProcessServerList() {
            DataTable dt = new DataTable();

            foreach(XmlElement serverNode in ServersInConfig) {
                using (ServerMonitor serverMonitor = new ServerMonitor(this)) {
                    ArrayList dbList = GetDatabasesFromServerNode(serverNode);
                    string serverName = serverNode.GetAttribute("value");
                    dt.Merge(serverMonitor.Process(serverName,dbList));
                }
            }

            DataView dv = dt.AsDataView();
            dv.RowFilter = PropertiesList.HasAttribute("rowFilter") ? PropertiesList.GetAttribute("rowFilter") : string.Empty;

            return dv;
        }

        #region Private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverListing"></param>
        /// <returns></returns>
        private ArrayList GetDatabasesFromServerNode(XmlElement serverListing) {
            ArrayList al = new ArrayList();

            var list = serverListing.GetElementsByTagName("database");
            
            if (list == null)
                throw new ApplicationException("No databases found in server node, don't do that.");

            foreach(XmlElement node in list) {
                al.Add(node.GetAttribute("value"));
            }
            return al;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private XmlNodeList GetServersFromConfig() {

            if (string.IsNullOrEmpty(DatabaseFile))
                throw new ApplicationException("Properties File Name not entered into 'DatabaseListFile' appSettings.");

            if (!File.Exists(DatabaseFile))
                throw new ApplicationException("File Not Found.");

            XmlDocument doc = new XmlDocument();
            doc.Load(DatabaseFile);

            var propertyList = doc.SelectNodes(string.Format("//{0}", "server"));

            if (propertyList == null)
                throw new ApplicationException("No Servers found in config.");

            return propertyList;
        }

        #endregion

        #region Public

        public DataTable NewDataTable(string name) {
            var table = new DataTable(name);
            table.Columns.Add("Server", typeof(string));
            table.Columns.Add("Database", typeof(string));

            foreach (XmlElement property in PropertiesList) {
                if (!property.HasAttribute("name"))
                    throw new ApplicationException("Property node must have 'name' defined.");
                var propName = property.GetAttribute("name");
                
                var type = string.IsNullOrEmpty(property.GetAttribute("datatype"))
                    ? Type.GetType("System.String")
                    : Type.GetType(property.GetAttribute("datatype"));
                
                var expressionString = property.HasAttribute("expression")
                    ? property.GetAttribute("expression")
                    : string.Empty;
                
                try {
                    table.Columns.Add(propName, type, expressionString);
                }
                catch (Exception e) {
                    table.Columns.Add(propName, type);
                }

            }
            return table;
        }

        #endregion

    }
}
