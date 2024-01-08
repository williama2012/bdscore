using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bds.win.Forms {

    public partial class BrowserBotForm1 : Form {
        private BrowserWindow homePage;
        public List<BrowserWindow> windows = new List<BrowserWindow>();
        public List<string> selectList = new List<string>();

        public BrowserBotForm1() {
            InitializeComponent();
            //var n = string.Format("{0}:{1}",this.Name);
            //Application.UserAppDataRegistry.SetValue();
        }

        private void homeBtn_Click(object sender, EventArgs e) {
            if(homePage == null) {
                homePage = OpenBrowserInWindow(textBox1.Text);
                if (homePage != null) {
                    homePage.Show();
                    homeBtn.Text = "Hide Home";
                }
                    
            }else {
                homePage.Visible = homePage.Visible ? false : true;
                homeBtn.Text = homePage.Visible ? "Hide Home" : "Show Home";
            }
        }

        private BrowserWindow OpenBrowserInWindow(string url) {
            try {
                var bw = new BrowserWindow ();
                bw.Browser.Url = new Uri(url);
                bw.Visible = areShowing;
                bw.Text = url;
                return bw;
            }catch(Exception e) {
                throw;
                //MessageBox.Show(e.Message);
                return null;
            }
        }

        private bool areShowing = false;
        private void showHideBtn_Click(object sender, EventArgs e) {
            foreach (var a in windows) {
                a.Visible = !areShowing;
            }
            showHideBtn.Text = areShowing ? "Show" : "Hide";
            areShowing = areShowing ? false : true;
        }


        private void Run_Click(object sender, EventArgs e) {
            var path = System.IO.Path.Combine(textBox1.Text, textBox3.Text);

            foreach (var s in selectList) {
                var p = string.Format(path, s);
                var w = OpenBrowserInWindow(p);
                windows.Add(w);
            }

        }

        private void clearBtn_Click(object sender, EventArgs e) {
            windows.ForEach(w => w.Dispose());
            windows.Clear();
            
            label3.Text = "List Empty";
        }

        private void button1_Click(object sender, EventArgs e) {
            var a = new ListRetreiver();
            a.ListSelected += new win.Controls.ListFromDatatableSelect(a_ListSelected);
        }

        void a_ListSelected(object sender, List<string> list) {
            selectList = list;
            label3.Text = string.Format("Items In List: {0}", selectList.Count);
            this.Focus();
        }
        
    }
}
