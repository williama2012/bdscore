using bds.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WindowsTempCleaner
{
    internal class Program
    {
        private static void Main(string[] args) {
            //EventLog eventLog = new EventLog("Application", ".", "Windows Temp Cleaner");
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(DateTime.UtcNow.ToString());
            
            double result;
            if (args.Length == 0 || !double.TryParse(args[0], out result)) {
                stringBuilder.AppendLine("USAGE:");
                stringBuilder.AppendLine("Argument 1: Minimum number of days since Modification that file will be removed.");
            } else {
                IDictionary<string, long> source = FileManager.ClearWindowsTemp(result * 24.0);
                stringBuilder.AppendLine(string.Format("Removed {0} files - {1} bytes", (object)source.Count, (object)source.Sum<KeyValuePair<string, long>>((Func<KeyValuePair<string, long>, long>)(size => size.Value))));
                foreach (KeyValuePair<string, long> keyValuePair in (IEnumerable<KeyValuePair<string, long>>)source)
                    stringBuilder.AppendLine(string.Format("{0} - {1} bytes", (object)keyValuePair.Key, (object)keyValuePair.Value));
            }
            //eventLog.WriteEntry(stringBuilder.ToString(), EventLogEntryType.Information);
            Console.Write(stringBuilder.ToString());
        }
    }
}