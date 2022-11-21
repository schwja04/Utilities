using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common.Data.Abstractions;

namespace Utilities.Common.Data
{
    public abstract class DataReaderAsync : IDataReaderAsync
    {
        private readonly IDataReader _reader;

        public DataReaderAsync(IDataReader reader) =>
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));

        #region Passthrough to IDataReader
        public virtual object this[string name] => _reader[name];

        public virtual object this[int i] => _reader[i];

        public virtual int Depth => _reader.Depth;

        public virtual int FieldCount => _reader.FieldCount;

        public virtual bool IsClosed => _reader.IsClosed;

        public virtual int RecordsAffected => _reader.RecordsAffected;

        public virtual void Close() => _reader.Close();

        public virtual bool GetBoolean(int i) => _reader.GetBoolean(i);

        public virtual byte GetByte(int i) => _reader.GetByte(i);

        public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length) =>
            _reader.GetBytes(i, fieldOffset, buffer, bufferOffset, length);

        public virtual char GetChar(int i) => _reader.GetChar(i);

        public virtual long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length) =>
            _reader.GetChars(i, fieldOffset, buffer, bufferOffset, length);

        public virtual IDataReader GetData(int i) => _reader.GetData(i);

        public virtual string GetDataTypeName(int i) => _reader.GetDataTypeName(i);

        public virtual DateTime GetDateTime(int i) => _reader.GetDateTime(i);

        public virtual decimal GetDecimal(int i) => _reader.GetDecimal(i);

        public virtual double GetDouble(int i) => _reader.GetDouble(i);

        public virtual Type GetFieldType(int i) => _reader.GetFieldType(i);

        public virtual float GetFloat(int i) => _reader.GetFloat(i);

        public virtual Guid GetGuid(int i) => _reader.GetGuid(i);

        public virtual short GetInt16(int i) => _reader.GetInt16(i);

        public virtual int GetInt32(int i) => _reader.GetInt32(i);

        public virtual long GetInt64(int i) => _reader.GetInt64(i);

        public virtual string GetName(int i) => _reader.GetName(i);

        public virtual int GetOrdinal(string name) => _reader.GetOrdinal(name);

        public virtual DataTable GetSchemaTable() => _reader.GetSchemaTable();

        public virtual string GetString(int i) => _reader.GetString(i);

        public virtual object GetValue(int i) => _reader.GetValue(i);

        public virtual int GetValues(object[] values) => _reader.GetValues(values);

        public virtual bool IsDBNull(int i) => _reader.IsDBNull(i);

        public virtual bool NextResult() => _reader.NextResult();

        public virtual bool Read() => _reader.Read();
        #endregion

        #region Field Extensions
        public virtual T To<T>(string columnName) => To<T>(columnName, default);

        public virtual T To<T>(string columnName, T defaultValue)
        {
            if (!ColumnExists(columnName)) return defaultValue;

            object value = _reader[columnName];

            if (value is null || ReferenceEquals(value, DBNull.Value)) return defaultValue;

            return (T)System.Convert.ChangeType(value, typeof(T));
        }

        public virtual T? ToNullable<T>(string columnName) where T : struct =>
            ToNullable<T>(columnName, null);

        public virtual T? ToNullable<T>(string columnName, T? defaultValue) where T : struct
        {
            if (!ColumnExists(columnName)) return defaultValue;

            object value = _reader[columnName];

            if (value is null || ReferenceEquals(value, DBNull.Value)) return defaultValue;

            return (T)System.Convert.ChangeType(value, typeof(T));
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
        #endregion

        #region abstract async extensions
        public abstract Task<bool> IsDBNullAsync(int i);
        public abstract Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken);

        public abstract Task<bool> NextResultAsync();
        public abstract Task<bool> NextResultAsync(CancellationToken cancellationToken);

        public abstract Task<bool> ReadAsync();
        public abstract Task<bool> ReadAsync(CancellationToken cancellationToken);
        #endregion

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
