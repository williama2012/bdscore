using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bds.win.Forms {
    public partial class BrowserWindow : Form {
        public BrowserWindow() {
            InitializeComponent();
            Browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(Browser_DocumentCompleted);

        }

        public WebBrowser Browser {
            get { return webBrowser1; }
        }

        void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            
        }


    }
}
