using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common.Data.Abstractions;
using Utilities.Common.Data.Extensions;

namespace Utilities.Common.Data
{
    public abstract class DataReaderAsync : IDataReaderAsync
    {
        private readonly IDataReader _reader;

        public DataReaderAsync(IDataReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            _reader = reader;
        }

        public virtual object this[string name] { get { return _reader[name]; } }

        public virtual object this[int i] { get { return _reader[i]; } }

        public virtual int Depth { get { return _reader.Depth; } }

        public virtual int FieldCount { get { return _reader.FieldCount; } }

        public virtual bool IsClosed { get { return _reader.IsClosed; } }

        public virtual int RecordsAffected { get { return _reader.RecordsAffected; } }

        public virtual void Close()
        {
            _reader.Close();
        }

        public virtual bool GetBoolean(int i)
        {
            return _reader.GetBoolean(i);
        }

        public virtual byte GetByte(int i)
        {
            return _reader.GetByte(i);
        }

        public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
        {
            return _reader.GetBytes(i, fieldOffset, buffer, bufferOffset, length);
        }

        public virtual char GetChar(int i)
        {
            return _reader.GetChar(i);
        }

        public virtual long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
        {
            return _reader.GetChars(i, fieldOffset, buffer, bufferOffset, length);
        }

        public virtual IDataReader GetData(int i)
        {
            return _reader.GetData(i);
        }

        public virtual string GetDataTypeName(int i)
        {
            return _reader.GetDataTypeName(i);
        }

        public virtual DateTime GetDateTime(int i)
        {
            return _reader.GetDateTime(i);
        }

        public virtual decimal GetDecimal(int i)
        {
            return _reader.GetDecimal(i);
        }

        public virtual double GetDouble(int i)
        {
            return _reader.GetDouble(i);
        }

        public virtual Type GetFieldType(int i)
        {
            return _reader.GetFieldType(i);
        }

        public virtual float GetFloat(int i)
        {
            return _reader.GetFloat(i);
        }

        public virtual Guid GetGuid(int i)
        {
            return _reader.GetGuid(i);
        }

        public virtual short GetInt16(int i)
        {
            return _reader.GetInt16(i);
        }

        public virtual int GetInt32(int i)
        {
            return _reader.GetInt32(i);
        }

        public virtual long GetInt64(int i)
        {
            return _reader.GetInt64(i);
        }

        public virtual string GetName(int i)
        {
            return _reader.GetName(i);
        }

        public virtual int GetOrdinal(string name)
        {
            return _reader.GetOrdinal(name);
        }

        public virtual DataTable GetSchemaTable()
        {
            return _reader.GetSchemaTable();
        }

        public virtual string GetString(int i)
        {
            return _reader.GetString(i);
        }

        public virtual object GetValue(int i)
        {
            return _reader.GetValue(i);
        }

        public virtual int GetValues(object[] values)
        {
            return _reader.GetValues(values);
        }

        public virtual bool IsDBNull(int i)
        {
            return _reader.IsDBNull(i);
        }

        public virtual bool NextResult()
        {
            return _reader.NextResult();
        }

        public virtual bool Read()
        {
            return _reader.Read();
        }

        public abstract Task<bool> IsDBNullAsync(int i);
        public abstract Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken);

        public abstract Task<bool> NextResultAsync();
        public abstract Task<bool> NextResultAsync(CancellationToken cancellationToken);

        public abstract Task<bool> ReadAsync();
        public abstract Task<bool> ReadAsync(CancellationToken cancellationToken);

        public virtual T To<T>(string columnName)
        {
            if (!ColumnExists(columnName)) return default;

            object value = _reader[columnName];

            if (value is null || ReferenceEquals(value, DBNull.Value)) return default;

            return (T)value;
        }

        public virtual T To<T>(string columnName, T defaultValue)
        {
            if (!ColumnExists(columnName)) return defaultValue;

            object value = _reader[columnName];

            if (value is null || ReferenceEquals(value, DBNull.Value)) return defaultValue;

            return (T)value;
        }

        public virtual T? ToNullable<T>(string columnName) where T : struct
        {
            return ToNullable<T>(columnName, null);
        }

        public virtual T? ToNullable<T>(string columnName, T? defaultValue)
            where T : struct
        {
            if (!ColumnExists(columnName)) return defaultValue;

            object value = _reader[columnName];

            if (value is null || ReferenceEquals(value, DBNull.Value)) return defaultValue;

            return (T)value;
        }

        public virtual bool ColumnExists(string columnName)
        {
            for (int i = 0; i < _reader.FieldCount; ++i)
            {
                if (string.Equals(_reader.GetName(i), columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        #region IDisposable Support

        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _reader.Dispose();
                }
                _isDisposed = true;
            }
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
