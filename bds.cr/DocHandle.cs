using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;

namespace bds.cr {
    public abstract class DocHandle : IDisposable {
        protected ReportDocument doc;



        public void Dispose()         {
            if (doc.IsLoaded) doc.Close();
            doc = null;
        }
    }
}
