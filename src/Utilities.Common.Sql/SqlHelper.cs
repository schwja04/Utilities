using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Utilities.Common.Data.Abstractions;
using Utilities.Common.Sql.Abstractions;

namespace Utilities.Common.Sql
{
	public abstract class SqlHelper<TParameter> : ISqlHelperAsync<TParameter>, ISqlHelper<TParameter>
        where TParameter : DbParameter
    {
        protected static int DEFAULT_COMMAND_TIMEOUT = 30;

        #region Both Synchronous and Asynchronous Method
        public abstract ISqlTransaction CreateTransaction(string connectionString);
        #endregion

        #region Synchronous Methods
        #region ExecuteNonQuery
        public virtual int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText) =>
            ExecuteNonQuery(connectionString, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);

        public virtual int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, int commandTimeout) =>
            ExecuteNonQuery(connectionString, commandType, commandText, commandParameters: null, commandTimeout);

        public virtual int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) =>
            ExecuteNonQuery(connectionString, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);

        public abstract int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);

        public virtual int ExecuteNonQuery(ISqlTransaction transaction, CommandType commandType, string commandText) =>
            ExecuteNonQuery(transaction, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);

        public virtual int ExecuteNonQuery(ISqlTransaction transaction, CommandType commandType, string commandText, int commandTimeout) =>
            ExecuteNonQuery(transaction, commandType, commandText, commandParameters: null, commandTimeout);

        public virtual int ExecuteNonQuery(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) =>
            ExecuteNonQuery(transaction, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);

        public abstract int ExecuteNonQuery(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);
        #endregion

        #region ExecuteReader
        public virtual IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText) =>
            ExecuteReader(connectionString, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);

        public virtual IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, int commandTimeout) =>
            ExecuteReader(connectionString, commandType, commandText, commandParameters: null, commandTimeout);

        public virtual IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) =>
            ExecuteReader(connectionString, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);

        public abstract IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);

        public virtual IDataReader ExecuteReader(ISqlTransaction transaction, CommandType commandType, string commandText) =>
            ExecuteReader(transaction, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);

        public virtual IDataReader ExecuteReader(ISqlTransaction transaction, CommandType commandType, string commandText, int commandTimeout) =>
            ExecuteReader(transaction, commandType, commandText, commandParameters: null, commandTimeout);

        public virtual IDataReader ExecuteReader(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) =>
            ExecuteReader(transaction, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);

        public abstract IDataReader ExecuteReader(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);
        #endregion

        #region ExecuteScalar
        public virtual T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText) where T : struct =>
            ExecuteScalar<T>(connectionString, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);

        public virtual T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, int commandTimeout) where T : struct =>
            ExecuteScalar<T>(connectionString, commandType, commandText, commandParameters: null, commandTimeout);

        public virtual T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where T : struct =>
            ExecuteScalar<T>(connectionString, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);

        public abstract T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout) where T : struct;

        public virtual T ExecuteScalar<T>(ISqlTransaction transaction, CommandType commandType, string commandText) where T : struct =>
            ExecuteScalar<T>(transaction, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);

        public virtual T ExecuteScalar<T>(ISqlTransaction transaction, CommandType commandType, string commandText, int commandTimeout) where T : struct =>
            ExecuteScalar<T>(transaction, commandType, commandText, commandParameters: null, commandTimeout);

        public virtual T ExecuteScalar<T>(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where T : struct =>
            ExecuteScalar<T>(transaction, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);

        public abstract T ExecuteScalar<T>(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout) where T : struct;
        #endregion

        #endregion

        #region Asynchronous Methods

        #region ExecuteNonQueryAsync
        public virtual async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText) =>
            await ExecuteNonQueryAsync(connectionString, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);

        public virtual async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, int commandTimeout) =>
            await ExecuteNonQueryAsync(connectionString, commandType, commandText, commandParameters: null, commandTimeout);

        public virtual async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) =>
            await ExecuteNonQueryAsync(connectionString, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);

        public abstract Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);

        public virtual async Task<int> ExecuteNonQueryAsync(ISqlTransaction transaction, CommandType commandType, string commandText) =>
            await ExecuteNonQueryAsync(transaction, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);

        public virtual async Task<int> ExecuteNonQueryAsync(ISqlTransaction transaction, CommandType commandType, string commandText, int commandTimeout) =>
            await ExecuteNonQueryAsync(transaction, commandType, commandText, commandParameters: null, commandTimeout);

        public virtual async Task<int> ExecuteNonQueryAsync(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) =>
            await ExecuteNonQueryAsync(transaction, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);

        public abstract Task<int> ExecuteNonQueryAsync(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);
        #endregion

        #region ExecuteReaderAsync
        public virtual async Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText) =>
            await ExecuteReaderAsync(connectionString, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);

        public virtual async Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, int commandTimeout) =>
            await ExecuteReaderAsync(connectionString, commandType, commandText, commandParameters: null, commandTimeout);

        public virtual async Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) =>
            await ExecuteReaderAsync(connectionString, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);

        public abstract Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);

        public virtual async Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction transaction, CommandType commandType, string commandText) =>
            await ExecuteReaderAsync(transaction, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);

        public virtual async Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction transaction, CommandType commandType, string commandText, int commandTimeout) =>
            await ExecuteReaderAsync(transaction, commandType, commandText, commandParameters: null, commandTimeout);

        public virtual async Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) =>
            await ExecuteReaderAsync(transaction, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);

        public abstract Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);
        #endregion

        #region ExecuteScalarAsync
        public virtual async Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText) where T : struct =>
            await ExecuteScalarAsync<T>(connectionString, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);

        public virtual async Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, int commandTimeout) where T : struct =>
            await ExecuteScalarAsync<T>(connectionString, commandType, commandText, commandParameters: null, commandTimeout);

        public virtual async Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where T : struct =>
            await ExecuteScalarAsync<T>(connectionString, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);

        public abstract Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout) where T : struct;

        public virtual async Task<T> ExecuteScalarAsync<T>(ISqlTransaction transaction, CommandType commandType, string commandText) where T : struct =>
            await ExecuteScalarAsync<T>(transaction, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);

        public virtual async Task<T> ExecuteScalarAsync<T>(ISqlTransaction transaction, CommandType commandType, string commandText, int commandTimeout) where T : struct =>
            await ExecuteScalarAsync<T>(transaction, commandType, commandText, commandParameters: null, commandTimeout);

        public virtual async Task<T> ExecuteScalarAsync<T>(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where T : struct =>
            await ExecuteScalarAsync<T>(transaction, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);

        public abstract Task<T> ExecuteScalarAsync<T>(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout) where T : struct;
        #endregion

        #endregion
    }
}

