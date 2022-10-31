using System.Collections.Generic;
using System.Data;

namespace Utilities.Common.Sql.Abstractions
{
    public interface ISqlHelper<TParameter>
    {
        ISqlTransaction CreateTransaction(string connectionString);

        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText);
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, int commandTimeout);
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters);
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);
        int ExecuteNonQuery(ISqlTransaction transaction, CommandType commandType, string commandText);
        int ExecuteNonQuery(ISqlTransaction transaction, CommandType commandType, string commandText, int commandTimeout);
        int ExecuteNonQuery(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters);
        int ExecuteNonQuery(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);

        IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText);
        IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, int commandTimeout);
        IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters);
        IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);
        IDataReader ExecuteReader(ISqlTransaction transaction, CommandType commandType, string commandText);
        IDataReader ExecuteReader(ISqlTransaction transaction, CommandType commandType, string commandText, int commandTimeout);
        IDataReader ExecuteReader(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters);
        IDataReader ExecuteReader(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout);

        T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText) where T : struct;
        T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, int commandTimeout) where T : struct;
        T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where T : struct;
        T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout) where T : struct;
        T ExecuteScalar<T>(ISqlTransaction transaction, CommandType commandType, string commandText) where T : struct;
        T ExecuteScalar<T>(ISqlTransaction transaction, CommandType commandType, string commandText, int commandTimeout) where T : struct;
        T ExecuteScalar<T>(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where T : struct;
        T ExecuteScalar<T>(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, int commandTimeout) where T : struct;
    }
}
