using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace bds.xml.SnippetManager {
    class SnippetManager {
        private const string ListFile = "snippets.xml";
        
        private string configCachePath = null;
        private string snippetsPath = null;


        private XElement configFile = null;

        DirectoryInfo configCache = null;

        public SnippetManager(string baseFolderLocation) {
            this.configCachePath = baseFolderLocation;

            this.configCache = new DirectoryInfo(configCachePath);
            LoadListFile();
        }

        #region Xml List Controls

        private void LoadListFile() {
            this.configFile = new XElement("snippets");
            this.configFile.Add(ListFilesInDirectory(configCache));
        }

        private void SaveListFile() {
            XDocument doc = new XDocument(configFile);
            doc.Save(Path.Combine(configCachePath, ListFile));
        }

        

        private XElement ListFilesInDirectory(DirectoryInfo directory) {
            XElement element = new XElement("group",new XAttribute("name",directory.Name));
            foreach (FileInfo file in directory.GetFiles()) {
                element.Add(new XElement("snippet",new XAttribute("name",file.Name)));
            }
            foreach (DirectoryInfo dir in directory.GetDirectories()) {
                element.Add(ListFilesInDirectory(dir));
            }

            return element;
        }

        #endregion

    }
}
