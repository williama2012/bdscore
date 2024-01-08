using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bds.win.Controls {

    public interface IListFromSource {
        event ListFromDatatableSelect ListSelected;
    }

    public delegate void ListFromDatatableSelect(object sender, List<string> list);

    public partial class ListFromDatatable : UserControl, IListFromSource {

        public event ListFromDatatableSelect ListSelected;

        private DataTable _dataTable;
        public DataTable dataTable {
            get {
                return _dataTable;
            }
            set {
                _dataTable = value;
                if (_dataTable != null) {
                    dataGridView1.DataSource = _dataTable;
                }

            }
        }

        public ListFromDatatable() {
            InitializeComponent();
            this.SizeChanged += new EventHandler(ListFromDatatable_SizeChanged);
            this.dataGridView1.MouseClick += new MouseEventHandler(dataGridView1_MouseClick);
            this.dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        void dataGridView1_MouseClick(object sender, MouseEventArgs e) {
            switch (e.Button) {
                case MouseButtons.Right:
                    ShowContextMenu();
                    break;
                default:
                    break;

            }
        }

        private void ShowContextMenu() {
            var menu = new ContextMenu();
            var i1 = new MenuItem("Select cells as List");

            i1.Click += new EventHandler(i1_Select);
            menu.MenuItems.Add(i1);
            menu.Show(this, new Point(0, 0));

        }

        void i1_Select(object sender, EventArgs e) {
            var cells = dataGridView1.SelectedCells;
            var list = new List<string>();

            foreach (DataGridViewTextBoxCell c in cells) {
                
                //var v = dataTable.Rows[c.RowNumber][c.ColumnNumber];
                //list.Add(v.ToString());
                list.Add(c.Value.ToString());

            }
            ListSelected(this, list);
            this.ParentForm.Close();
        }

        void ListFromDatatable_SizeChanged(object sender, EventArgs e) {
            this.dataGridView1.Width = this.Width;
            this.dataGridView1.Height = this.Height;
        }
        
    }
}
