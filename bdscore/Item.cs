using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;

namespace bds.core {
    public class Item : IItem {

        private string _itemId;
        private string _itemType;
        private object _itemObject;

        private ArrayList refList = new ArrayList();

        public Item(object itemObject) {
            _itemObject = itemObject;
            
        }

        public Item(string itemId, object itemObject) {
            _itemId = itemId;
            _itemObject = itemObject;
            _itemType = _itemObject.GetType().Name;

        }

        public string itemId {  get { return _itemId; }  }
        public string itemType { get { return _itemType; } }
        public object itemObject { get { return _itemObject; } }
        public IList References { get { return refList; } }

        public void AddReference(string Id) {
            if (HasReference(Id)) {
                refList.Add(Id);
            }
        }

        public bool HasReference(string reference) {
            reference = reference.ToLower();
            switch (_itemType) {
                case "String":
                    return ((string)_itemObject).ToLower().Contains(reference);
                //case "Table":
                //    foreach (DataColumn c in ((DataTable)_itemObject).Columns)
                //    {
                //        if (c.Name.ToLower().Contains(reference)) { return true; }
                //    }
                //    return false;
                //case "View":
                //    return ((DataView)_itemObject).TextBody.ToLower().Contains(reference);
                //case "UserDefinedFunction":
                //    return ((DataUserDefinedFunction)_itemObject).TextBody.ToLower().Contains(reference);
                //case "StoredProcedure":
                //    return ((StoredProcedure)_itemObject).TextBody.ToLower().Contains(reference);
                //case "ReportDocument":
                //    return false;//return new bds.crystal.crystalHelper.Tables(_itemObject).Contains(reference);
                //case "XmlElement":
                //    //((XmlElement)item.itemObject)
                    return false;
            }
            return false;
        }




    }
}
