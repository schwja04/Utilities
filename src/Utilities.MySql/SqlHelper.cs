using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Common.Data;
using Utilities.Common.Data.Abstractions;
using Utilities.Common.Sql;
using Utilities.Common.Sql.Abstractions;
using Utilities.MySql.Data;

namespace Utilities.MySql
{
    public sealed class SqlHelper : SqlHelper<MySqlParameter>
    {
        static SqlHelper()
        {
            DEFAULT_COMMAND_TIMEOUT = 30;
        }

        #region Both Synchronous and Asynchronous Method
        public override ISqlTransaction CreateTransaction(string connectionString)
        {
            return new SqlTransaction(connectionString);
        }
        #endregion

        #region Synchronous Methods
        #region ExecuteNonQuery
        public override int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters, int commandTimeout)
        {
            using var connection = new MySqlConnection(connectionString);

            if (connection.State is not ConnectionState.Open)
            {
                connection.Open();
            }

            using var command = new MySqlCommand(commandText)
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

        public override int ExecuteNonQuery(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters, int commandTimeout)
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new MySqlCommand(commandText)
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
        public override IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters, int commandTimeout)
        {
            using var connection = new MySqlConnection(connectionString);

            if (connection.State is not ConnectionState.Open)
            {
                connection.Open();
            }

            using var command = new MySqlCommand(commandText)
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

        public override IDataReader ExecuteReader(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters, int commandTimeout)
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new MySqlCommand(commandText)
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
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        #endregion

        #region ExecuteScalar
        public override T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters, int commandTimeout) where T : struct
        {
            using var connection = new MySqlConnection(connectionString);

            if (connection.State is not ConnectionState.Open)
            {
                connection.Open();
            }

            using var command = new MySqlCommand(commandText)
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

        public override T ExecuteScalar<T>(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters, int commandTimeout) where T : struct
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new MySqlCommand(commandText)
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
        public override async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters, int commandTimeout)
        {
            using var connection = new MySqlConnection(connectionString);

            using var command = new MySqlCommand(commandText)
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

        public override async Task<int> ExecuteNonQueryAsync(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters, int commandTimeout)
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new MySqlCommand(commandText)
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
        public override async Task<IDataReaderAsync> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters, int commandTimeout)
        {
            using var connection = new MySqlConnection(connectionString);

            using var command = new MySqlCommand(commandText)
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

            return new SqlDataReaderAsync(command.ExecuteReader(CommandBehavior.CloseConnection));
            // TODO: Figure out how to make this async. The following line returns DbRataReader NOT MySqlDataReader
            // return new SqlDataReaderAsync(await command.ExecuteReaderAsync(CommandBehavior.CloseConnection));
            //return new SqlDataReaderAsync(await command.ExecuteReaderAsync(CommandBehavior.CloseConnection));
        }

        public override async Task<IDataReaderAsync> ExecuteReaderAsync(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters, int commandTimeout)
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new MySqlCommand(commandText)
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

            return await Task.Run(() => new SqlDataReaderAsync(command.ExecuteReader(CommandBehavior.CloseConnection)));
            // TODO: Figure out how to make this async. The following line returns DbRataReader NOT MySqlDataReader
            // return new SqlDataReaderAsync(await command.ExecuteReaderAsync(CommandBehavior.CloseConnection));
        }
        #endregion

        #region ExecuteScalarAsync
        public override async Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters, int commandTimeout) where T : struct
        {
            using var connection = new MySqlConnection(connectionString);

            using var command = new MySqlCommand(commandText)
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

        public override async Task<T> ExecuteScalarAsync<T>(ISqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters, int commandTimeout) where T : struct
        {
            var tran = GetSqlClientTransaction(transaction);

            using var command = new MySqlCommand(commandText)
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

        private static MySqlTransaction GetSqlClientTransaction(ISqlTransaction sqlTransaction)
        {
            var tran = sqlTransaction as ISqlClientTransaction<MySqlTransaction>;

            if (tran is null)
            {
                throw new ArgumentException($"{nameof(sqlTransaction)} is null or does not implement {nameof(ISqlClientTransaction<MySqlTransaction>)}", nameof(sqlTransaction));
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
