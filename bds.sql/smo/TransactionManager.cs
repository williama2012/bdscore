using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using bds.IO;

namespace bds.sql.smo {

    public interface ITransactionManagerLog {
        void Error(string message, Exception exception = null);
        void Info(string messageexception);
        void InfoFormat(string formatString, params object[] args);
        void Warn(string message);
    }

    public class TransactionManagerLog : ITransactionManagerLog {

        private void Log(string message) {
            Console.WriteLine("[{0}] - {1}", DateTime.Now.ToLongTimeString(), message);
        }

        ConsoleColor originalForegroundColor = Console.ForegroundColor;
        ConsoleColor originalBackgroundColor = Console.BackgroundColor;

        private void StoreLastColors() {
            originalForegroundColor = Console.ForegroundColor;
            originalBackgroundColor = Console.BackgroundColor;
        }

        private void RestoreLastColors() {
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        private void SetColors(ConsoleColor? foreground = null, ConsoleColor? background = null) {
            if(foreground.HasValue) {
                Console.ForegroundColor = foreground.Value;
            }
            if(background.HasValue) {
                Console.BackgroundColor = background.Value;
            }
        }

        public void Error(string message, Exception exception = null) {
            StoreLastColors();
            SetColors(ConsoleColor.Red, ConsoleColor.Black);

            Log(message);
            if(exception != null) {
                Log(exception.Message);
            }
            RestoreLastColors();

        }

        public void Info(string message) {
            Log(message);
        }

        public void InfoFormat(string formatString, params object[] args) {
            Log(string.Format(formatString, args));
        }

        public void Warn(string message) {
            SetColors(ConsoleColor.Yellow, ConsoleColor.Black);
            Log(message);
            RestoreLastColors();
        }

    }

    public class TransactionManager {

        #region private properties

        private static ITransactionManagerLog log;
        private const IsolationLevel defaultIsolationLevel = IsolationLevel.Serializable;
        private string _connectionString = string.Empty;
       
        #endregion

        #region public properties

        public IsolationLevel DefaultIsolationLevel { get { return defaultIsolationLevel; } }
        public string ConnectionString { get { return _connectionString; } }
        public bool NoCommit { get; set; }
        public string ExecuteBeforeCommit { get; set; }
        public string ExecuteAfterSucess { get; set; }

        public int CommandTimeout { get; set; }
        public int TransactionTimeout { get; set; }

       #endregion

        public TransactionManager(string connectionString) {
            NoCommit = false;
            log = new TransactionManagerLog();
            _connectionString = connectionString;
        }

        public static bool ExecuteFiles(IEnumerable<string> listOfFiles, string connString) {
            var mng = new TransactionManager(connString);
            return mng.ExecuteFiles(listOfFiles);
        }

        public bool ExecuteFiles(IEnumerable<string> listOfFiles) {

            var connection = ConnectionHelper.OpenConnection(ConnectionString);
            log.InfoFormat("Connection:{0}", connection.State);

            var Id = DateTime.Now.ToShortTimeString();
            var transaction = connection.BeginTransaction(DefaultIsolationLevel, Id);
            log.InfoFormat("Transaction Isolation Level:{0}", transaction.IsolationLevel);

            var cmdSet = new List<SqlCommandWrapper>();

            listOfFiles.ToList().ForEach(fileName => {

                var sb = new StringBuilder();
                sb.AppendLine(FileManager.ReadFile(fileName));
                sb.AppendLine("GO");

                var cmd = TransactionHelper.CreateWrappedCommand(fileName, sb.ToString(), transaction);
                cmdSet.Add(cmd);
            });

            var result = ExecuteCommandSet(cmdSet);

            switch (result) {
                case false:
                    log.Warn("Transaction Failed.");
                    transaction.Rollback();
                    log.Warn("Transaction Rolled Back.");
                    break;
                case true:
                    log.Info("Transaction Passed.");
                    if (!NoCommit) {
                        transaction.Commit();
                        log.Info("Transaction Commited.");
                    } else {
                        transaction.Rollback();
                        log.Info("Transaction Rolled Back.");
                    }
                    break;
            }
            connection.Close();
            return result;
        }


        public static bool Process(string commands, string connString) {
            var mng = new TransactionManager(connString);
            return mng.Process(commands);
        }

        public bool Process(string cmds) {

            var connection = ConnectionHelper.OpenConnection(ConnectionString);
            log.InfoFormat("Connection:{0}",connection.State);

            var Id = DateTime.Now.ToShortTimeString();
            var transaction = connection.BeginTransaction(DefaultIsolationLevel, Id);
            log.InfoFormat("Transaction Isolation Level:{0}",transaction.IsolationLevel);

            var cmdSet = TransactionHelper.CreateCommandSet(cmds, transaction).ToList();
            log.InfoFormat("Commands being Sent:{0}",cmdSet.Count());

            var result = ExecuteCommandSet(cmdSet);

            switch (result) { 
                case false:
                    log.Info("Transaction Failed.");
                    transaction.Rollback();
                    log.Info("Transaction Rolled Back.");
                    break;
                case true:
                    log.Info("Transaction Passed.");
                    if(!NoCommit) {
                        transaction.Commit();
                        log.Info("Transaction Commited.");
                    }else {
                        transaction.Rollback();
                        log.Info("Transaction Rolled Back.");
                    }
                    break;
            }
            connection.Close();
            return result;
        }

        /// <summary>
        /// Execute non query of a list of SqlCommand.
        /// </summary>
        /// <param name="commandSet"></param>
        /// <returns>Returns false if any operations fail.</returns>
        private bool ExecuteCommandSet(IEnumerable<SqlCommand> commandSet) {
            return commandSet.Select(ExecuteCommand).All(result => result);
        }

        private bool ExecuteCommand(SqlCommand command) {
            try {
                command.CommandTimeout = 900;
                int result = command.ExecuteNonQuery();

                log.InfoFormat("{0}",command.CommandText);
                return true;
            }
            catch (SqlException e) {

                log.Error(command.CommandText,e);
                return false;
            }
        }

        private bool ExecuteCommandSet(IEnumerable<SqlCommandWrapper> commandSet) {
            return commandSet.Select(ExecuteCommand).All(result => result);
        }

        private bool ExecuteCommand(SqlCommandWrapper wrapper) {
            log.Info(wrapper.FileName);
            var result = ExecuteCommandSet(wrapper.Commands);
            if (!result) {
                log.Error(string.Format("Error In File: {0}", wrapper.FileName));
            }
            return result;
        }

    }

}
