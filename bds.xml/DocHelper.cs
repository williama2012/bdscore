using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace bds.xml {
    public class DocHelper : XmlDocument {

        public DocHelper(string fileName) {
            this.Load(fileName);
        }

        /// <summary>
        /// Reading office xml documents.
        /// </summary>
        /// <param name="fileName"></param>
        static void ReadDocXml(string fileName) {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNamespaceManager mng = new XmlNamespaceManager(doc.NameTable);
            mng.AddNamespace("pkg", "http://schemas.microsoft.com/office/2006/xmlPackage");
            mng.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

            XmlNode body = doc.SelectSingleNode("/pkg:package/pkg:part/pkg:xmlData/w:document/w:body", mng);

            string prevOutText = string.Empty;
            bool prevIsHeader = false;

            foreach (XmlNode paragraph in body.ChildNodes) {
                bool isHeader = false;
                string parText = string.Empty;
                foreach (XmlNode charSet in paragraph.ChildNodes) {
                    string setText = string.Empty;
                    XmlNode bold = charSet.SelectSingleNode("w:rPr/w:b", mng);
                    if (bold != null) { isHeader = true; }

                    XmlNode tab = charSet.SelectSingleNode("w:rPr/w:tab", mng);
                    if (tab != null) { setText += (char)9; }

                    XmlNode text = charSet.SelectSingleNode("w:t", mng);
                    if (text != null) { setText += text.InnerText; }
                }


                /*  start of heading*/
                if (isHeader == false && isHeader == true) { 
                    
                }
                

            }

            Console.WriteLine(string.Format("Nodes: {0}", body.ChildNodes.Count.ToString()));
        }

    }
}
