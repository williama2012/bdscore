using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bds.core {
    public interface IItemCollection {

        void AddItem(IItem item);

        IEnumerable Items { get; }
        IEnumerable References(string Id);

    }
}
