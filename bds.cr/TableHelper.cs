using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;

namespace bds.cr {
    public class TableHelper : DocHandle {

        public TableHelper(string filename) {
            try {
                doc.Load(filename);
            } catch (Exception e) { }
        }

        public IEnumerable<Table> GetTables {
            get { return ReportTables(this.doc); }
        }

        private IEnumerable<Table> ReportTables(ReportDocument doc) {
            IEnumerable<Table> ret;
            ret = doc.Database.Tables.Cast<Table>();

            var subReports =
                from subReport in doc.Subreports.Cast<ReportDocument>()
                select subReport;

            foreach (var report in subReports) {
                ret.Concat(report.Database.Tables.Cast<Table>());
            }

            return ret;
        }

    }
}