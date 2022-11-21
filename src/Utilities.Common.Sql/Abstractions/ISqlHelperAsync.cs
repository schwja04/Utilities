using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Utilities.Common.Data.Abstractions;

namespace Utilities.Common.Sql.Abstractions
{
    public interface ISqlHelperAsync<TParameter> 
    {
        ISqlTransaction CreateTransaction(string connectionString);

        Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText);
        Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, int commandTimeout);
        Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters);
        Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);
        Task<int> ExecuteNonQueryAsync(ISqlTransaction transaction, CommandType commandType, string commandText);
        Task<int> ExecuteNonQueryAsync(ISqlTransaction transaction, CommandType commandType, string commandText, int commandTimeout);
        Task<int> ExecuteNonQueryAsync(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters);
        Task<int> ExecuteNonQueryAsync(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);

        Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText);
        Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, int commandTimeout);
        Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters);
        Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);
        Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction transaction, CommandType commandType, string commandText);
        Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction transaction, CommandType commandType, string commandText, int commandTimeout);
        Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters);
        Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);

        Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText) where T : struct;
        Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, int commandTimeout) where T : struct;
        Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where T : struct;
        Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout) where T : struct;
        Task<T> ExecuteScalarAsync<T>(ISqlTransaction transaction, CommandType commandType, string commandText) where T : struct;
        Task<T> ExecuteScalarAsync<T>(ISqlTransaction transaction, CommandType commandType, string commandText, int commandTimeout) where T : struct;
        Task<T> ExecuteScalarAsync<T>(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where T : struct;
        Task<T> ExecuteScalarAsync<T>(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout) where T : struct;
    }
}
