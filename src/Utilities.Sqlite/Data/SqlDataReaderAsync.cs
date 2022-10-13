using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common.Data;
using Utilities.Common.Sql;
using Utilities.Common.Sql.Abstractions;

namespace Utilities.Sqlite.Data
{
    public sealed class SqlDataReaderAsync : DataReaderAsync
    {
        private readonly SqliteDataReader _reader;

        public SqlDataReaderAsync(SqliteDataReader reader)
            : base(reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            _reader = reader;
        }

        public override async Task<bool> IsDBNullAsync(int i)
        {
            return await _reader.IsDBNullAsync(i);
        }

        public override async Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken)
        {
            return await _reader.IsDBNullAsync(i, cancellationToken);
        }

        public override async Task<bool> NextResultAsync()
        {
            return await _reader.NextResultAsync();
        }

        public override async Task<bool> NextResultAsync(CancellationToken cancellationToken)
        {
            return await _reader.NextResultAsync(cancellationToken);
        }

        public override async Task<bool> ReadAsync()
        {
            return await _reader.ReadAsync();
        }

        public override async Task<bool> ReadAsync(CancellationToken cancellationToken)
        {
            return await _reader.ReadAsync(cancellationToken);
        }

        #region IDisposable Support
        private bool _isDisposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _reader.Dispose();
                }
                _isDisposed = true;
            }

            // Call base class implementation
            base.Dispose(disposing);
        }
        #endregion
    }
}
