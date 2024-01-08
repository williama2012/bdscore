using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using bds.sql.smo;
using System.Windows.Forms;
using System.Security;
using System.Runtime.InteropServices;

namespace bds.sql.SQLExecute {

    public class sqlExecute    {

        private const string prvSrvSettingName = "PreviousServer";
        private const string prvDbSettingName = "PreviousDatabase";

        Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
        string _server = string.Empty;
        string _database = string.Empty;

        private List<string> Files { get; set; } 

        public sqlExecute(string[] args) {
            _server = config.AppSettings.Settings[prvSrvSettingName] == null
                ? string.Empty
                : config.AppSettings.Settings[prvSrvSettingName].Value;

            _database = config.AppSettings.Settings[prvDbSettingName] == null 
                ? string.Empty
                : config.AppSettings.Settings[prvDbSettingName].Value;

            Files = GetFileList(args);
            foreach (var arg in Files) {
                Console.WriteLine(arg);
            }

            Console.WriteLine();
            Console.WriteLine("v{0}", Version);
            Console.WriteLine();

            var response = UserInput();
            if (!response) {
                Console.WriteLine();
                Console.WriteLine("Quiting without running.");
                Console.WriteLine();
                Thread.Sleep(1000);
                return;
            }

            SaveSettings();

            var result = Process(_server, _database);

            Console.WriteLine("Done. Press any key to close.");
            ConsoleKeyInfo wait = Console.ReadKey();
        }

        private string Version {
            get {
                return GetType().Assembly.GetName().Version.ToString();
            }
        }

        private void EnumeratePath(string path, List<string> list) {

            var fa = File.GetAttributes(path);
            if(fa.HasFlag(FileAttributes.Directory)) {
                Directory.GetDirectories(path)
                    .SortNatural()
                    .ToList()
                    .ForEach(dir => {
                        EnumeratePath(dir, list);
                    });
                list.AddRange(Directory.GetFiles(path, "*.sql").SortNatural());
            } else {
                list.Add(path);
            }
            
        }

        private List<string> GetFileList(IEnumerable<string> paths) {
            var list = new List<string>();
            paths.OrderBy(p => File.GetAttributes(p).HasFlag(FileAttributes.Directory) ? 0 : 1)
                .ToList()
                .ForEach(p => EnumeratePath(p, list));
            return list;
        }

        public bool Process(string server, string database) {
            return Process(stringBuilder.buildConnString(server, database));
        }

        public bool Process(string connString) {
            return TransactionManager.ExecuteFiles(Files, connString);
        }

      #region User Interface
        
        private bool GetYesAnswer() {
            Console.WriteLine();
            Console.WriteLine("Type 'YES' to continue.");
            Console.WriteLine();
            var line = Console.ReadLine();
            if (line == null)
                return false;
            var answer = line.ToLower();
            return answer == "yes";
        }

        /// <summary>
        /// Print the command prompts, pretty.
        /// </summary>
        private bool UserInput() {

            // Print previously used connection.
            var cont = false;

            if (!string.IsNullOrWhiteSpace(_server) && !string.IsNullOrWhiteSpace(_database)) {
                Console.WriteLine(string.Format("Previous, {0} @ {1}", _database, _server));
                Console.Write(@"Use Last connection? (Type 'yes' to continue.) ");
                cont = GetYesAnswer();
                if (cont) {
                    return true;
                }

            }

            Console.WriteLine();
            Console.WriteLine("Enter Connection Information:");
            Console.WriteLine();

            Console.Write("Server: ");
            _server = Console.ReadLine();
            Console.Write("Database: ");
            _database = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(_server) || string.IsNullOrWhiteSpace(_database))
                return false;
            Console.WriteLine(string.Format("Use: {0} @ {1} ?", _database, _server));

            return GetYesAnswer();
        }

        /// <summary>
        /// Save the properties.
        /// </summary>
        private void SaveSettings() {
            if (config.AppSettings.Settings[prvSrvSettingName] == null) {
                config.AppSettings.Settings.Add(prvSrvSettingName, _server);
            } else {
                config.AppSettings.Settings[prvSrvSettingName].Value = _server; 
            }

            if (config.AppSettings.Settings[prvDbSettingName] == null) {
                config.AppSettings.Settings.Add(prvDbSettingName, _database);
            } else {
                config.AppSettings.Settings[prvDbSettingName].Value = _database;
            }
            config.Save();
        }

        #endregion

    }

    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);
    }

    public sealed class NaturalStringComparer : IComparer<string> {
        public int Compare(string a, string b) {
            return SafeNativeMethods.StrCmpLogicalW(a, b);
        }
    }
    
    public static class ArraySortExtensions {

        public static string[] SortNatural(this string[] array) {
            Array.Sort(array, new NaturalStringComparer());
            return array;
        }

    }
}
