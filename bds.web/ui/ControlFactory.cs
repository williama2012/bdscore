using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace bds.web.ui {
    class ControlFactory {

        private string[] nodeNamesToSearch = new []{ "columnName", "" };

        private const string classNameNode = "className";


        public Control CreateControl(XmlElement webControlElement) {
            Control control = CreateControlObject(webControlElement);



            return control;
        }
        
        private Control CreateControlObject(XmlElement controlElement) {
            string typeName = controlElement.HasAttribute(classNameNode)
                ? controlElement.GetAttribute(classNameNode) 
                : null;

            if (typeName == null)
                throw new ApplicationException("Attribute 'typeName' cannot be null on Control Node.");

            Type type = Type.GetType(typeName);
            object obj = Activator.CreateInstance(type);
            
            if (!typeof(System.Web.UI.Control).IsAssignableFrom(obj.GetType()))
                throw new ApplicationException("Type must be subtype of 'System.Web.UI.WebControls.WebControl'.");

            return (Control)obj;
        }

        private DataControlField CreateFieldObject(XmlElement controlElement)        {
            string typeName = controlElement.HasAttribute(classNameNode)
                ? controlElement.GetAttribute(classNameNode)
                : null;

            if (typeName == null)
                throw new ApplicationException("Attribute 'typeName' cannot be null on Control Node.");

            Type type = Type.GetType(typeName);
            object obj = Activator.CreateInstance(type);

            if (!typeof(System.Web.UI.WebControls.DataControlField).IsAssignableFrom(obj.GetType()))
                throw new ApplicationException("Type must be subtype of 'System.Web.UI.WebControls.DataControlField'.");

            return (DataControlField)obj;
        }

        private GridView BindControl(DataView dataview, XmlElement controlElement) {
            var controlLinq = controlElement.Cast<XmlElement>().AsParallel<XmlElement>();

            GridView GridView1 = new GridView() { DataSource = dataview
                , Caption = controlElement.HasAttribute("label")
                    ? controlElement.GetAttribute("label")
                    : string.Empty
                , AutoGenerateColumns = false
                , ShowHeaderWhenEmpty = true
            };
            

            foreach (DataColumn col in dataview.Table.Columns) {
                var x = (from p in controlLinq
                        where p.HasAttribute("name")
                        && p.GetAttribute("name") == col.ColumnName
                        select p).SingleOrDefault<XmlElement>();
                
                if (x == null) {
                    BoundField field = new BoundField();
                    field.DataField = col.ColumnName;
                    field.HeaderText = col.ColumnName;
                    GridView1.Columns.Add(field);
                } else {
                    var visible = x.HasAttribute("visible")
                        ? x.GetAttribute("visible").Equals("true")
                        : false;

                    if (!visible)
                        continue;

                    BoundField field = new BoundField();
                    field.DataField = x.GetAttribute("name");

                    field.HeaderText = x.HasAttribute("label")
                        ? x.GetAttribute("label")
                        : x.GetAttribute("name");

                    field.DataFormatString = x.HasAttribute("format")
                        ? x.GetAttribute("format")
                        : string.Empty;

                    GridView1.Columns.Add(field);
                }
            }

            GridView1.DataBind();
            return GridView1;
        }

        private void LoadColumns(XmlElement controlElement) {
            XmlElement columns = (XmlElement)controlElement.SelectSingleNode("columns");
            XmlNodeList cols = columns.SelectNodes("column");
            foreach (XmlElement e in cols) {
                BoundField field = new BoundField();
                

            }
        }

    }
}
