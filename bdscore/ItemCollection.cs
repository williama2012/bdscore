using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace bds.core {
    public class ItemCollection : IItemCollection {

        private string _itemId;
        private IQueryable<Item> _itemL;
        private ArrayList _itemList;

        public ItemCollection(string itemId) {
            _itemId = itemId;
            _itemList = new ArrayList();
        }

        public ItemCollection(string itemId, params IItem[] items) {
            _itemId = itemId;
            _itemList = new ArrayList();
            foreach (Item i in items) {
                AddItem(i);
            }
        }

        public void AddItem(IItem item) {
            _itemList.Add(item);
        }

        public void AddItem(object item) {
            AddItem((IItem)item);
        }

        public IEnumerable References(string Id) {
            ArrayList al = new ArrayList();
            foreach (Item i in _itemList) {
                if (i.References.Contains(Id)) {
                    al.Add(i);
                }
            }
            return al;
        }

        public IEnumerable Items {
            get { return _itemList; }
        }


    }
}
