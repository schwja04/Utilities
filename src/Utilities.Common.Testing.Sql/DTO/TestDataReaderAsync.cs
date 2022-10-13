using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common.Data;
using Utilities.Common.Data.Abstractions;

namespace Utilities.Common.Testing.Sql.DTO
{
    internal sealed class TestDataReaderAsync : DataReaderAsync
    {
        public TestDataReaderAsync(IDataReader reader)
            : base(reader)
        {
        }

        public override Task<bool> IsDBNullAsync(int i)
        {
            return Task<bool>.FromResult(IsDBNull(i));
        }

        public override Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken)
        {

            if (cancellationToken.IsCancellationRequested)
            {
                return CreatedTaskWithCancellation<bool>();
            }
            else
            {
                return Task<bool>.FromResult(IsDBNull(i));
            }
        }

        public override Task<bool> NextResultAsync()
        {
            return Task<bool>.FromResult(NextResult());
        }

        public override Task<bool> NextResultAsync(CancellationToken cancellationToken)
        {

            if (cancellationToken.IsCancellationRequested)
            {
                return CreatedTaskWithCancellation<bool>();
            }
            else
            {
                return Task<bool>.FromResult(NextResult());
            }
        }

        public override Task<bool> ReadAsync()
        {
            return Task<bool>.FromResult(Read());
        }

        public override Task<bool> ReadAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return CreatedTaskWithCancellation<bool>();
            }
            else
            {
                return Task<bool>.FromResult(Read());
            }
        }

        private static Task<T> CreatedTaskWithCancellation<T>()
        {
            var source = new TaskCompletionSource<T>();
            source.SetCanceled();
            return source.Task;
        }

        #region IDisposable Support
        protected override void Dispose(bool disposing)
        {
            // Call base class implementation
            base.Dispose(disposing);
        }
        #endregion
    }
}
