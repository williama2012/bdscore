using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace bds.xml {
    /// <summary>
    /// 
    /// </summary>
    public static class FindAndReplace {

        public static ArrayList FindReplace(string fileName, ArrayList elementNames, ArrayList attributeNames, string valueToFind, string valueToReplace) {
            XDocument doc = XDocument.Load(fileName);
            ArrayList results = FindReplace(doc.Root, elementNames, attributeNames, valueToFind, valueToReplace);
            doc.Save(fileName);
            return results;
        }

        public static ArrayList FindReplace(XElement element, ArrayList elementNames, ArrayList attributeNames, string valueToFind, string valueToRepalce) {
            ArrayList results = new ArrayList();
            foreach (XElement e in element.Elements()) {
                
                if (elementNames.Contains(e.Name.ToString())) {

                    foreach (XAttribute a in e.Attributes()) {

                        if (attributeNames.Contains(a.Name.ToString())) {

                            if (a.Value == valueToFind) {
                                a.Value = valueToRepalce;
                                results.Add(string.Format("{0},{1},{2},{3}", e.Name, a.Name, valueToRepalce, a.Value));
                            }
                        }
                    }
                }
                results.AddRange( FindReplace(e, elementNames, attributeNames, valueToFind, valueToRepalce));
            }
            return results;
        }



    }

    /// <summary>
    /// 
    /// </summary>
    public static class LoadSettings {
        private const string _rootNodeName = "Nodes";
        private const string _NodeName = "Node";

        private const string _rootAttributeName = "Attributes";
        private const string _AttributeName = "Attribute";

        public static ArrayList LoadNodeNames(string fileName) {
            XDocument doc = XDocument.Load(fileName);
            return LoadNodeNames(doc.Root);
        }

        public static ArrayList LoadNodeNames(XElement settingsNode) {
            ArrayList nodeNames = new ArrayList();

            XElement xNodes = settingsNode.Element(_rootNodeName);
            foreach (XElement node in xNodes.Elements(_NodeName)) {
                nodeNames.Add(node.Attribute("name").Value);
            }


            return nodeNames;
        }

        public static ArrayList LoadAttributeNames(string fileName) {
            XDocument doc = XDocument.Load(fileName);
            return LoadAttributeNames(doc.Root);
        }

        public static ArrayList LoadAttributeNames(XElement settingsNode) {
            ArrayList attributeNames = new ArrayList();

            XElement xNodes = settingsNode.Element(_rootAttributeName);
            foreach (XElement node in xNodes.Elements(_AttributeName)) {
                attributeNames.Add(node.Attribute("name").Value);
            }

            return attributeNames;
        }

        public static ArrayList Load(XElement settingsNode)
        {
            ArrayList attributeNames = new ArrayList();

            XElement xNodes = settingsNode.Element(_rootNodeName);
            foreach (XElement node in xNodes.Elements(_NodeName))
            {
                attributeNames.Add(node.Attribute("attributeName").Value);
            }

            return attributeNames;
        }


    
    }


}
