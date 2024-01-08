using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace bds.xml {
    public static class xmlWriter {

        public static void Save(XDocument doc, string fileName) {
            doc.Save(fileName);
        }

        public static void Save(XElement element, string fileName) {
            XDocument doc = new XDocument(element);
            doc.Save(fileName);
        }

        public static void Save(XNode node, string fileName) {
            XDocument doc = new XDocument(node);
            doc.Save(fileName);
        }



    }
}
