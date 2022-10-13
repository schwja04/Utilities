﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Common.Data.Abstractions;
using Utilities.Common.Sql.Abstractions;
using Utilities.Sqlite.Abstractions;
using Utilities.Sqlite.Data;

namespace Utilities.Sqlite
{
    public class SqlHelper : ISqlHelperAsync<SqliteTransaction, SqliteParameter>, ISqlHelper<SqliteTransaction, SqliteParameter>
    {
        private const int DEFAULT_COMMAND_TIMEOUT = 30;

        #region Both Synchronous and Asynchronous Method
        public ISqlTransaction<SqliteTransaction> CreateTransaction(string connectionString)
        {
            return new SqlTransaction(connectionString);
        }
        #endregion

        #region Synchronous Methods
        #region ExecuteNonQuery
        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connectionString, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);
        }

        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, int commandTimeout)
        {
            return ExecuteNonQuery(connectionString, commandType, commandText, commandParameters: null, commandTimeout);
        }

        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters)
        {
            return ExecuteNonQuery(connectionString, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);
        }

        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters, int commandTimeout)
        {
            using var connection = new SqliteConnection(connectionString);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            using var command = new SqliteCommand(commandText)
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

        public int ExecuteNonQuery(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);
        }

        public int ExecuteNonQuery(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, int commandTimeout)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, commandParameters: null, commandTimeout);
        }

        public int ExecuteNonQuery(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);
        }

        public int ExecuteNonQuery(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters, int commandTimeout)
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new SqliteCommand(commandText)
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
            return command.ExecuteNonQuery();
        }
        #endregion

        #region ExecuteReader
        public IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteReader(connectionString, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);
        }

        public IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, int commandTimeout)
        {
            return ExecuteReader(connectionString, commandType, commandText, commandParameters: null, commandTimeout);
        }

        public IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters)
        {
            return ExecuteReader(connectionString, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);
        }

        public IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters, int commandTimeout)
        {
            var connection = new SqliteConnection(connectionString);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            using var command = new SqliteCommand(commandText)
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

        public IDataReader ExecuteReader(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText)
        {
            return ExecuteReader(transaction, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);
        }

        public IDataReader ExecuteReader(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, int commandTimeout)
        {
            return ExecuteReader(transaction, commandType, commandText, commandParameters: null, commandTimeout);
        }

        public IDataReader ExecuteReader(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters)
        {
            return ExecuteReader(transaction, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);
        }

        public IDataReader ExecuteReader(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters, int commandTimeout)
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new SqliteCommand(commandText)
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
        public T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText) where T : struct
        {
            return ExecuteScalar<T>(connectionString, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);
        }

        public T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, int commandTimeout) where T : struct
        {
            return ExecuteScalar<T>(connectionString, commandType, commandText, commandParameters: null, commandTimeout);
        }

        public T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters) where T : struct
        {
            return ExecuteScalar<T>(connectionString, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);
        }

        public T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters, int commandTimeout) where T : struct
        {
            using var connection = new SqliteConnection(connectionString);

            using var command = new SqliteCommand(commandText)
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

        public T ExecuteScalar<T>(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText) where T : struct
        {
            return ExecuteScalar<T>(transaction, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);
        }

        public T ExecuteScalar<T>(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, int commandTimeout) where T : struct
        {
            return ExecuteScalar<T>(transaction, commandType, commandText, commandParameters: null, commandTimeout);
        }

        public T ExecuteScalar<T>(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters) where T : struct
        {
            return ExecuteScalar<T>(transaction, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);
        }

        public T ExecuteScalar<T>(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters, int commandTimeout) where T : struct
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new SqliteCommand(commandText)
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
        public async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText)
        {
            return await ExecuteNonQueryAsync(connectionString, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);
        }

        public async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, int commandTimeout)
        {
            return await ExecuteNonQueryAsync(connectionString, commandType, commandText, commandParameters: null, commandTimeout);
        }

        public async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters)
        {
            return await ExecuteNonQueryAsync(connectionString, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);
        }

        public async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters, int commandTimeout)
        {
            using var connection = new SqliteConnection(connectionString);

            using var command = new SqliteCommand(commandText)
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

        public async Task<int> ExecuteNonQueryAsync(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText)
        {
            return await ExecuteNonQueryAsync(transaction, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);
        }

        public async Task<int> ExecuteNonQueryAsync(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, int commandTimeout)
        {
            return await ExecuteNonQueryAsync(transaction, commandType, commandText, commandParameters: null, commandTimeout);
        }

        public async Task<int> ExecuteNonQueryAsync(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters)
        {
            return await ExecuteNonQueryAsync(transaction, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);
        }

        public async Task<int> ExecuteNonQueryAsync(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters, int commandTimeout)
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new SqliteCommand(commandText)
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
        public async Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText)
        {
            return await ExecuteReaderAsync(connectionString, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);
        }

        public async Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, int commandTimeout)
        {
            return await ExecuteReaderAsync(connectionString, commandType, commandText, commandParameters: null, commandTimeout);
        }

        public async Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters)
        {
            return await ExecuteReaderAsync(connectionString, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);
        }

        public async Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters, int commandTimeout)
        {
            var connection = new SqliteConnection(connectionString);

            using var command = new SqliteCommand(commandText)
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

        public async Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText)
        {
            return await ExecuteReaderAsync(transaction, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);
        }

        public async Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, int commandTimeout)
        {
            return await ExecuteReaderAsync(transaction, commandType, commandText, commandParameters: null, commandTimeout);
        }

        public async Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters)
        {
            return await ExecuteReaderAsync(transaction, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);
        }

        public async Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters, int commandTimeout)
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new SqliteCommand(commandText)
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
        public async Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText) where T : struct
        {
            return await ExecuteScalarAsync<T>(connectionString, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);
        }

        public async Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, int commandTimeout) where T : struct
        {
            return await ExecuteScalarAsync<T>(connectionString, commandType, commandText, commandParameters: null, commandTimeout);
        }

        public async Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters) where T : struct
        {
            return await ExecuteScalarAsync<T>(connectionString, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);
        }

        public async Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters, int commandTimeout) where T : struct
        {
            using var connection = new SqliteConnection(connectionString);

            using var command = new SqliteCommand(commandText)
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

        public async Task<T> ExecuteScalarAsync<T>(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText) where T : struct
        {
            return await ExecuteScalarAsync<T>(transaction, commandType, commandText, commandParameters: null, DEFAULT_COMMAND_TIMEOUT);
        }

        public async Task<T> ExecuteScalarAsync<T>(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, int commandTimeout) where T : struct
        {
            return await ExecuteScalarAsync<T>(transaction, commandType, commandText, commandParameters: null, commandTimeout);
        }

        public async Task<T> ExecuteScalarAsync<T>(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters) where T : struct
        {
            return await ExecuteScalarAsync<T>(transaction, commandType, commandText, commandParameters, DEFAULT_COMMAND_TIMEOUT);
        }

        public async Task<T> ExecuteScalarAsync<T>(ISqlTransaction<SqliteTransaction> transaction, CommandType commandType, string commandText, IEnumerable<SqliteParameter> commandParameters, int commandTimeout) where T : struct
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new SqliteCommand(commandText)
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

        private static SqliteTransaction GetSqlClientTransaction(ISqlTransaction<SqliteTransaction> sqlTransaction)
        {
            ISqlClientTransaction tran = sqlTransaction as ISqlClientTransaction;

            if (tran is null)
            {
                throw new ArgumentException($"{nameof(sqlTransaction)} is null or does not implement {nameof(ISqlClientTransaction)}", nameof(sqlTransaction));
            }

            return tran.SqlClientTransaction;
        }

        private static T CastScalar<T>(object obj) 
            where T : struct
        {
            return (obj is null ? default : Common.Data.Convert.Cast<T>(obj));
        }
    }
}
