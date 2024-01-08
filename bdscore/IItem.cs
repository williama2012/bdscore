using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bds.core {
    public interface IItem    {
        string itemId { get; }
        string itemType { get; }
        object itemObject { get; }
        
        void AddReference(string Id);
        bool HasReference(string Id);


        IList References { get; }

    }
}
