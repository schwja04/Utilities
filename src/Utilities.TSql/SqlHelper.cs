using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Common.Data;
using Utilities.Common.Data.Abstractions;
using Utilities.Common.Sql;
using Utilities.Common.Sql.Abstractions;
using Utilities.TSql.Data;

using TSqlCommand = Microsoft.Data.SqlClient.SqlCommand;
using TSqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using TSqlParameter = Microsoft.Data.SqlClient.SqlParameter;
using TSqlTransaction = Microsoft.Data.SqlClient.SqlTransaction;

namespace Utilities.TSql
{
    public sealed class SqlHelper : SqlHelper<TSqlParameter>
    {
        private readonly IConvert _convert;

        public SqlHelper() : this(new Common.Data.Convert()) { }

        public SqlHelper(IConvert convert)
        {
            _convert = convert ?? throw new ArgumentNullException(nameof(convert));
        }

        #region Both Synchronous and Asynchronous Method
        public override ISqlTransaction CreateTransaction(string connectionString) => new SqlTransaction(connectionString);
        #endregion

        #region Synchronous Methods
        #region ExecuteNonQuery
        public override int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, IEnumerable<TSqlParameter> commandParameters, int commandTimeout)
        {
            using var connection = new TSqlConnection(connectionString);

            if (connection.State is not ConnectionState.Open)
            {
                connection.Open();
            }

            using var command = new TSqlCommand(commandText)
            {
                CommandTimeout = commandTimeout,
                CommandType = commandType,
                Connection = connection
            };

            if (commandParameters is not null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }

            return command.ExecuteNonQuery();
        }

        public override int ExecuteNonQuery(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TSqlParameter> commandParameters, int commandTimeout)
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new TSqlCommand(commandText)
            {
                CommandTimeout = commandTimeout,
                CommandType = commandType,
                Connection = tran.Connection,
                Transaction = tran
            };

            if (commandParameters is not null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }

            return command.ExecuteNonQuery();
        }
        #endregion

        #region ExecuteReader
        public override IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, IEnumerable<TSqlParameter> commandParameters, int commandTimeout)
        {
            using var connection = new TSqlConnection(connectionString);

            if (connection.State is not ConnectionState.Open)
            {
                connection.Open();
            }

            using var command = new TSqlCommand(commandText)
            {
                CommandTimeout = commandTimeout,
                CommandType = commandType,
                Connection = connection
            };

            if (commandParameters is not null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public override IDataReader ExecuteReader(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TSqlParameter> commandParameters, int commandTimeout)
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new TSqlCommand(commandText)
            {
                CommandTimeout = commandTimeout,
                CommandType = commandType,
                Connection = tran.Connection,
                Transaction = tran
            };

            if (commandParameters != null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        #endregion

        #region ExecuteScalar
        public override T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<TSqlParameter> commandParameters, int commandTimeout) where T : struct
        {
            using var connection = new TSqlConnection(connectionString);

            if (connection.State is not ConnectionState.Open)
            {
                connection.Open();
            }

            using var command = new TSqlCommand(commandText)
            {
                Connection = connection,
                CommandType = commandType,
                CommandTimeout = commandTimeout
            };

            if (commandParameters is not null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }

            return CastScalar<T>(command.ExecuteScalar());
        }

        public override T ExecuteScalar<T>(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TSqlParameter> commandParameters, int commandTimeout) where T : struct
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new TSqlCommand(commandText)
            {
                Connection = tran.Connection,
                CommandType = commandType,
                CommandTimeout = commandTimeout,
                Transaction = tran
            };

            if (commandParameters is not null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }

            return CastScalar<T>(command.ExecuteScalar());
        }
        #endregion

        #endregion

        #region Asynchronous Methods

        #region ExecuteNonQueryAsync
        public override async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<TSqlParameter> commandParameters, int commandTimeout)
        {
            using var connection = new TSqlConnection(connectionString);

            using var command = new TSqlCommand(commandText)
            {
                CommandTimeout = commandTimeout,
                CommandType = commandType,
                Connection = connection
            };

            if (commandParameters is not null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }

            await connection.OpenAsync();

            return await command.ExecuteNonQueryAsync();
        }

        public override async Task<int> ExecuteNonQueryAsync(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TSqlParameter> commandParameters, int commandTimeout)
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new TSqlCommand(commandText)
            {
                CommandTimeout = commandTimeout,
                CommandType = commandType,
                Connection = tran.Connection,
                Transaction = tran
            };

            if (commandParameters is not null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }

            return await command.ExecuteNonQueryAsync();
        }
        #endregion

        #region ExecuteReaderAsync
        public override async Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<TSqlParameter> commandParameters, int commandTimeout)
        {
            using var connection = new TSqlConnection(connectionString);

            using var command = new TSqlCommand(commandText)
            {
                Connection = connection,
                CommandType = commandType,
                CommandTimeout = commandTimeout
            };

            if (commandParameters is not null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }

            await connection.OpenAsync();

            return new SqlDataReaderAsync(await command.ExecuteReaderAsync(CommandBehavior.CloseConnection));
        }

        public override async Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TSqlParameter> commandParameters, int commandTimeout)
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new TSqlCommand(commandText)
            {
                CommandTimeout = commandTimeout,
                CommandType = commandType,
                Connection = tran.Connection,
                Transaction = tran
            };

            if (commandParameters is not null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }

            return new SqlDataReaderAsync(await command.ExecuteReaderAsync(CommandBehavior.CloseConnection));
        }
        #endregion

        #region ExecuteScalarAsync
        public override async Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<TSqlParameter> commandParameters, int commandTimeout) where T : struct
        {
            using var connection = new TSqlConnection(connectionString);

            using var command = new TSqlCommand(commandText)
            {
                Connection = connection,
                CommandType = commandType,
                CommandTimeout = commandTimeout
            };

            if (commandParameters is not null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }

            await connection.OpenAsync();

            return CastScalar<T>(await command.ExecuteScalarAsync());
        }

        public override async Task<T> ExecuteScalarAsync<T>(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<TSqlParameter> commandParameters, int commandTimeout) where T : struct
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new TSqlCommand(commandText)
            {
                Connection = tran.Connection,
                CommandType = commandType,
                CommandTimeout = commandTimeout,
                Transaction = tran
            };

            if (commandParameters is not null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }

            return CastScalar<T>(await command.ExecuteScalarAsync());
        }
        #endregion

        #endregion

        private static TSqlTransaction GetSqlClientTransaction(ISqlTransaction sqlTransaction)
        {
            var tran = sqlTransaction as SqlTransaction;

            if (tran is null)
            {
                throw new ArgumentException(
                    $"{nameof(sqlTransaction)} is null or does not implement {nameof(SqlTransaction)}",
                    nameof(sqlTransaction));
            }

            return tran.SqlClientTransaction;
        }

        private T CastScalar<T>(object obj) 
            where T : struct
        {
            return (obj is null ? default : _convert.Cast<T>(obj));
        }
    }
}
