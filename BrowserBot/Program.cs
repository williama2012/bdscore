using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bds.win.Forms;
using System.Windows.Forms;

namespace BrowserBot {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BrowserBotForm1());
            
        }
    }
}
