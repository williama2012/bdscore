using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using bds.sql.smo;

namespace SqlSmoSearcher {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            Initialize();
        }

        private readonly SqlStructureSearch _searcher = new SqlStructureSearch();
        private void Initialize() {
            this.Closing += Form1_Closing;
            _searcher.ConnectionChanged += ConnectionChanged;
            _searcher.ObjectCountChanged += delegate(int count) {
                Invoke(new Action(delegate() {
                    lblObjectsInDb.Text = string.Format("Objects in Db: {0}", count);
                }));
            };

            _searcher.StatusChanged += delegate(string status, bool isBusy) {
                Invoke(new Action(delegate() {
                    lblStatus.Text = string.Format("Status: {0}", status);
                    if (isBusy) {
                        cbDbList.Enabled = false;
                        tbKeyword.Enabled = false;
                        btnConnect.Enabled = false;
                        tbKeyword.Enabled = false;
                        btnSearch.Enabled = false;
                    } else {
                        cbDbList.Enabled = true;
                        tbKeyword.Enabled = true;
                        btnConnect.Enabled = true;
                        tbKeyword.Enabled = true;
                        btnSearch.Enabled = true;
                    }

                }));
            };
        }

        void Form1_Closing(object sender, CancelEventArgs e) {
            if (_searcher != null) {
                _searcher.Stop();
            }
        }

        private void ConnectionChanged(bool serverConnected, bool databaseConnected, string serverName, string databaseName) {
            lblServer.Text = string.Format("Server: {0}", serverName);
            lblDatabase.Text = string.Format("Database: {0}", databaseName);
            if (serverConnected) {
                tbServer.Enabled = false;
                cbDbList.Enabled = true;
                if (!databaseConnected) {
                    cbDbList.Items.Clear();
                    cbDbList.Items.AddRange(_searcher.Databases.Cast<object>().ToArray());                    
                }
            }
            if (databaseConnected) {
                _searcher.CacheObjects();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(tbServer.Text)) {
                return;
            }
            _searcher.ConnectServer(tbServer.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (cbDbList.SelectedItem == null) return;
            _searcher.ConnectDb(cbDbList.SelectedItem.ToString());
        }

        private void Form1_Load(object sender, EventArgs e) { }

        void Form1_Resize(object sender, System.EventArgs e) {
            var form = (Form) sender;

            dataGridView1.Width = form.Width - 35;
            dataGridView1.Height = form.Height - dataGridView1.Top - 45;

        }

        private void btnSearch_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(tbKeyword.Text)) {

                return;
            }
            var dt = new DataTable("table");
            dt.Columns.AddRange(new []{new DataColumn("Type"), new DataColumn("Name"), new DataColumn("Body"),   });
            var results = _searcher.Search(tbKeyword.Text);
            foreach (var result in results) {
                var r = dt.NewRow();
                r["Type"] = result.Type;
                r["Name"] = result.Name;
                r["Body"] = result.Body;
                dt.Rows.Add(r);
            }
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;
            dataGridView1.AutoResizeColumns();
        }

        void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            var gv = (DataGridView) sender;
            var cell = gv[e.ColumnIndex, e.RowIndex];
            LoadContent(cell.Value.ToString());
        }

        private void LoadContent(string content) {
            var tempFile = Path.GetTempFileName();
            tempFile = Path.ChangeExtension(tempFile, "sql");
            File.AppendAllText(tempFile, content);
            System.Diagnostics.Process.Start(tempFile);
        }

    }
}
