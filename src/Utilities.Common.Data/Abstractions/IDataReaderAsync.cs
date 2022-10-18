using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common.Data.Extensions;

namespace Utilities.Common.Data.Abstractions
{
    public interface IDataReaderAsync : IDataReader
    {
        Task<bool> IsDBNullAsync(int i);
        Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken);

        Task<bool> NextResultAsync();
        Task<bool> NextResultAsync(CancellationToken cancellationToken);

        Task<bool> ReadAsync();
        Task<bool> ReadAsync(CancellationToken cancellationToken);

        T To<T>(string columnName);
        T To<T>(string columnName, T defaultValue);

        T? ToNullable<T>(string columnName) where T : struct;
        T? ToNullable<T>(string columnName, T? defaultValue) where T : struct;

        bool ColumnExists(string columnName);
    }
}
