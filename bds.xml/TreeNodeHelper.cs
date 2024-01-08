using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace bds.xml {

    public class TreeNodeEventHandler  { }

    public delegate void NewNodeEventHandler(object sender, NewNodeEventArgs args);

    public class NewNodeEventArgs : EventArgs {
        public TreeNode key;
        public string name;
    }

    public class TreeNodeHelper  {

        private Dictionary<TreeNode, XNode> table = new Dictionary<TreeNode,XNode>();
        private TreeNodeCollection collection;
        private XDocument doc = null;

        public TreeNodeHelper(TreeNodeCollection collection) { 
            this.collection = collection; 
        }

        public void NewNodeEvent(object sender, NewNodeEventArgs args) {
            NewElement(args.key, args.name);
        }

        #region Load Xml

        /// <summary>
        /// Loads Xml File into a XDocument place holder.
        /// </summary>
        /// <param name="XmlDocumentFileName">Full path File Name.</param>
        public void LoadXml(string XmlDocumentFileName) {
            try {
                this.doc = XDocument.Load(XmlDocumentFileName);
                AddRootElement(this.doc.Root);
            }
            catch (Exception e) { throw; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void LoadXml(XElement element) {
            this.doc = new XDocument(element);
            AddRootElement(element);
        }

        #endregion

        #region Controls

        /// <summary>
        /// Creates a new element on a node.
        /// </summary>
        /// <param name="FKey"> The TreeNode associated with the element. </param>
        /// <param name="name"> Name of the element. </param>
        public void NewElement(TreeNode FKey, string name) {
            XElement parent = (XElement)GetNode(FKey);
            XElement newElem = new XElement(name);
            FKey.Nodes.Add(Add(newElem));
            parent.Add(newElem);
        }

        /// <summary>
        /// Returns XNode from HashTable with TreeNode as Key
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public XNode GetNode(TreeNode key) {
            try {
                return table[key];
            }
            catch (KeyNotFoundException e) {
                return null;
            }

        }

        /// <summary>
        /// Writes Object to Diction with TreeNode as Key.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="element"></param>
        public void UpdateNode(TreeNode key, XNode element) {
            table[key] = element;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void DeleteNode(TreeNode key) {
            table[key].Remove();
            table.Remove(key);
            key.Remove();
        }

        /// <summary>
        /// Save XDocument.
        /// </summary>
        /// <param name="FileName"></param>
        public void Save(string FileName) {
            if (doc != null) {
                doc.Save(FileName);
            }
                        
        }
        
        #endregion

        #region Private Add



        /// <summary>
        /// Adds XElement to the Tree Node Collection.
        /// </summary>
        /// <param name="element"></param>
        private void AddRootElement(XElement element) {
            collection.Add(Add(element));
        }

        /// <summary>
        /// Adds XElement to TreeNode, and All Child XNodes.
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        private TreeNode Add(XElement xmlNode) {
            TreeNode treeNode = CreateElement(xmlNode);
            foreach (XNode n in xmlNode.Nodes()) {
                switch (n.NodeType) { 
                    case XmlNodeType.Element:
                        treeNode.Nodes.Add(Add((XElement)n));
                        break;
                    case XmlNodeType.Comment:
                        treeNode.Nodes.Add(Add((XComment)n));
                        break;
                }
            }

            return treeNode;
        }

        /// <summary>
        /// Adds XComment to TreeNode.
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        private TreeNode Add(XComment xmlNode) {
            TreeNode treeNode = CreateElement(xmlNode);
            return treeNode;
        }

        /// <summary>
        /// Adds XDocument to TreeNode.
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        private TreeNode Add(XDocument xmlNode) {
            TreeNode treeNode = CreateElement(xmlNode);
            return treeNode;
        }

        /// <summary>
        /// This is the primary meathod for creating tree nodes. Creates TreeNode as HashTable Key and XNode as Value.
        /// </summary>
        /// <param name="xmlNode">The XNode to be added to the Tree.</param>
        /// <returns>TreeNode that has been added to the HashTable.</returns>
        private TreeNode CreateElement(XNode xmlNode) {
            TreeNode treeNode = new TreeNode();
            NodeText(treeNode, xmlNode);
            table.Add(treeNode, xmlNode);
            return treeNode;
        }

        #endregion

        #region NodeText - TreeNode Text Formatting

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="node"></param>
        private void NodeText(TreeNode treeNode, XNode node) {
            switch (node.NodeType) { 
                case XmlNodeType.Element:
                    NodeText(treeNode, (XElement)node);
                    break;
                case XmlNodeType.Comment:
                    NodeText(treeNode, (XComment)node);
                    break;
            }

        }

        /// <summary>
        /// Sets text on TreeNode from XElement.
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="element"></param>
        private void NodeText(TreeNode treeNode, XElement element) {
            string returnString = string.Format("<{0}",element.Name.ToString());
            foreach (XAttribute a in element.Attributes()) {
                returnString += string.Format(" {0}='{1}'", a.Name.ToString(), a.Value);
            }
            returnString += ">";
            treeNode.Text = returnString;
            treeNode.Name = element.Name.ToString();
        }

        /// <summary>
        /// Sets text on TreeNode from XComment.
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="comment"></param>
        private void NodeText(TreeNode treeNode, XComment comment) {
            treeNode.Text = string.Format("<{0}>",comment.Value);
        }

        /// <summary>
        /// Sets text on TreeNode from XDocument.
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="document"></param>
        private void NodeText(TreeNode treeNode, XDocument document) {
            treeNode.Text = document.BaseUri;
            treeNode.Name = document.BaseUri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void UpdateNodeText(TreeNode key) {

            NodeText(key, GetNode(key));
        }

        #endregion

    }
}
