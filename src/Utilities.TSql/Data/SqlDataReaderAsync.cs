using Microsoft.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System;
using Utilities.Common.Data;

namespace Utilities.TSql.Data
{
    public sealed class SqlDataReaderAsync : DataReaderAsync
    {
        private readonly SqlDataReader _reader;

        public SqlDataReaderAsync(SqlDataReader reader)
            : base(reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public override async Task<bool> IsDBNullAsync(int i) =>
            await _reader.IsDBNullAsync(i);

        public override async Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken) =>
            await _reader.IsDBNullAsync(i, cancellationToken);

        public override async Task<bool> NextResultAsync() =>
            await _reader.NextResultAsync();

        public override async Task<bool> NextResultAsync(CancellationToken cancellationToken) =>
            await _reader.NextResultAsync(cancellationToken);

        public override async Task<bool> ReadAsync() =>
            await _reader.ReadAsync();

        public override async Task<bool> ReadAsync(CancellationToken cancellationToken) =>
            await _reader.ReadAsync(cancellationToken);

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
