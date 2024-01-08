using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using bds.win.Controls;

namespace bds.win.Forms {

    public partial class ListRetreiver : Form, IListFromSource {

        public event bds.win.Controls.ListFromDatatableSelect ListSelected;

        public ListRetreiver() {
            InitializeComponent();
            this.listFromDatatable1.ListSelected += new win.Controls.ListFromDatatableSelect(listFromDatatable1_ListSelected);
            this.Show();
        }

        void listFromDatatable1_ListSelected(object sender, List<string> list) {
            ListSelected(sender, list);
        }

        private void getDatasetBtn_Click(object sender, EventArgs e) {
            var a = new DatasetRetreiver();
            a.DataSetRetreieved += new DatasetRetreivedHandler(a_DataSetRetreieved);
        }

        void a_DataSetRetreieved(object sender, DatasetRetreivedArgs e) {
            this.listFromDatatable1.dataTable = e.dataSet.Tables[0];
        }
    }


}
