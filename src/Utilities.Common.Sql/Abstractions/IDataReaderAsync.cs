using System.Data;
using System.Threading.Tasks;
using System.Threading;

namespace Utilities.Common.Sql.Abstractions
{
    public interface IDataReaderAsync : IDataReader
    {
        Task<bool> IsDBNullAsync(int i);
        Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken);

        Task<bool> NextResultAsync();
        Task<bool> NextResultAsync(CancellationToken cancellationToken);

        Task<bool> ReadAsync();
        Task<bool> ReadAsync(CancellationToken cancellationToken);
    }
}
