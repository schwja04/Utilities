using Npgsql;
using System;
using System.Data;
using Utilities.Common.Sql.Abstractions;
using Utilities.PSql.Abstractions;

namespace Utilities.PSql
{
    public sealed class SqlTransaction : ISqlClientTransaction, ISqlTransaction<NpgsqlTransaction>
    {
        private readonly string _connectionString;
        private NpgsqlConnection _sqlConnection;

        private NpgsqlTransaction _sqlTransaction;

        public SqlTransaction(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "ConnectionString is required.");
            }

            _connectionString = connectionString;
        }

        public NpgsqlTransaction SqlClientTransaction => _sqlTransaction;

        public void BeginTransaction()
        {
            DisposalCheck();
            if (_sqlTransaction is not null || _sqlConnection is not null)
            {
                throw new InvalidOperationException("Cannot begin transaction more than once.");
            }

            _sqlConnection = new NpgsqlConnection(_connectionString);
            _sqlConnection.Open();
            _sqlTransaction = _sqlConnection.BeginTransaction();
        }

        public void Commit()
        {
            DisposalCheck();

            if (_sqlTransaction is null && _sqlConnection is null)
            {
                throw new InvalidOperationException("Commit failed, no transaction found.");
            }

            try
            {
                _sqlTransaction?.Commit();
            }
            finally
            {
                HandleSqlConnection();
            }
        }

        public void Rollback()
        {
            DisposalCheck();

            if (_sqlTransaction is null && _sqlConnection is null)
            {
                throw new InvalidOperationException("Rollback failed, no transaction found.");
            }

            try
            {
                _sqlTransaction?.Rollback();
            }
            finally
            {
                HandleSqlConnection();
            }
        }

        #region IDisposable Support
        private bool _isDisposed = false;

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void DisposalCheck()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(_sqlTransaction), $"{nameof(_sqlTransaction)} has been disposed.");
            }
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing)
            {
                if (_sqlTransaction is not null)
                {
                    Rollback();
                }
                else
                {
                    HandleSqlConnection();
                }
            }
            _isDisposed = true;
        }

        private void HandleSqlConnection()
        {
            //Handle Transaction
            try
            {
                if (_sqlTransaction is not null)
                {
                    _sqlTransaction.Dispose();
                }
            }
            finally
            {
                _sqlTransaction = null;
            }

            //Handle Connection
            try
            {
                if (_sqlConnection is not null)
                {
                    if (_sqlConnection.State == ConnectionState.Open)
                    {
                        _sqlConnection.Close();
                    }
                    _sqlConnection.Dispose();
                }
            }
            finally
            {
                _sqlConnection = null;
            }
        }

        #endregion
    }
}
