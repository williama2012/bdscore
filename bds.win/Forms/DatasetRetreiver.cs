using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bds.win.Forms {

    public delegate void DatasetRetreivedHandler(object sender, DatasetRetreivedArgs e);

    public class DatasetRetreivedArgs : EventArgs {
        private DataSet _dataSet;
        public DatasetRetreivedArgs(DataSet dataset) {
            _dataSet = dataset;
        }
        public DataSet dataSet {
            get { return _dataSet; }
        }
    }


    public partial class DatasetRetreiver : Form {

        public event DatasetRetreivedHandler DataSetRetreieved;
        private DataSet _dataSet;
        public DataSet dataSet { get { return _dataSet; } }

        public DatasetRetreiver() {
            InitializeComponent();
            this.Resize += new EventHandler(DatasetRetreiver_Resize);
            this.Show();
        }

        void DatasetRetreiver_Resize(object sender, EventArgs e) {
            queryTxt.Width = this.Width - 30;
            queryTxt.Height = this.Height - 120;
        }

        private void executeBtn_Click(object sender, EventArgs e) {
            try {
                var connStr = bds.sql.stringBuilder.buildConnString(serverTxt.Text, databaseTxt.Text);
                _dataSet = bds.sql.ado.sqlHelper.ExecuteQuery(connStr, queryTxt.Text);
                DataSetRetreieved(this, new DatasetRetreivedArgs(_dataSet));
                this.Close();
                this.Dispose();

            } catch (Exception err) {
                MessageBox.Show(err.Message);
            }
        }

    }
}
