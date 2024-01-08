using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using bds.IO;
using System.IO;

namespace bds.core.test
{
    class Program
    {
        [STAThread]
        static void Main(string[] args) {
            //string[] my_args = { Assembly.GetExecutingAssembly().Location };
            //int returnCode = NUnit.ConsoleRunner.Runner.Main(my_args);
            //if (returnCode != 0)
            //    Console.Beep();

            var x = FileManager.ClearWindowsTemp(24);
            Console.WriteLine(x);

            System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog("Application","LT-049-W7","Windows Temporary Cleaner");
            eventLog.WriteEntry(string.Format("Bytes Removed: {0}",x), System.Diagnostics.EventLogEntryType.Information);

            Console.ReadLine();
        }
    }
}
