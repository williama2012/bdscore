using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace bds.sql {
    /// <summary>
    /// Creates a transaction and executes a NonQuery command, does not automatically commit or rollback.
    /// </summary>
    public class Query : IDisposable {

        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlTransaction _transaction;
        private queryResult _queryResult = queryResult.NotRun;
        private string _transactionName = Guid.NewGuid().ToString();

        public Query(SqlConnection connection, string commandText) {
            _connection = connection;

            _command = _connection.CreateCommand();

            _command.Connection = _connection;
            _command.Transaction = _transaction;
            
            _command.CommandText = commandText;
        }



        public queryResult Execute() {

            try {
                Command.ExecuteNonQuery();
            }
            catch (SqlException e) {
                _queryResult = queryResult.ConnectionFail;
            }

            
            return QueryResult;
        }



        public void Commit() {
            Transaction.Commit();
        }

        public void Rollback() {
            Transaction.Rollback();
        }

        private queryResult TryBeginTransaction() {
            try {
                _transaction = _connection.BeginTransaction(TransactionId);
                return queryResult.TransactionSuccess;
            }
            catch (Exception e) {
                return queryResult.TransactionFail;
            }

        }



        #region Disposable Members

        private bool _disposed;

        public bool Disposed {
            get { return _disposed; }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing) {

            if (disposing) {
                ReleaseResources();
            }

        }

        private void ReleaseResources() {
            Transaction.Dispose();
            Command.Dispose();
            _disposed = true;
        }

        #endregion

        #region Properties

        public SqlCommand Command {
            get { return _command; }
        }

        public SqlTransaction Transaction {
            get { return _transaction; }
        }

        public queryResult QueryResult {
            get { return _queryResult; }
        }

        public string TransactionId {
            get { return _transactionName; }
        }

        #endregion

    }



}
