using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
using System;
using Utilities.Common.Sql.Abstractions;

namespace Utilities.MySql
{
    public class SqlDataReaderAsync : IDataReaderAsync
    {
        private readonly MySqlDataReader _reader;

        public SqlDataReaderAsync(MySqlDataReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            _reader = reader;
        }

        public object this[string name] { get { return _reader[name]; } }

        public object this[int i] { get { return _reader[i]; } }

        public int Depth { get { return _reader.Depth; } }

        public int FieldCount { get { return _reader.FieldCount; } }

        public bool IsClosed { get { return _reader.IsClosed; } }

        public int RecordsAffected { get { return _reader.RecordsAffected; } }

        public void Close()
        {
            _reader.Close();
        }

        public bool GetBoolean(int i)
        {
            return _reader.GetBoolean(i);
        }

        public byte GetByte(int i)
        {
            return _reader.GetByte(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return _reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        public char GetChar(int i)
        {
            return _reader.GetChar(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return _reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        public IDataReader GetData(int i)
        {
            return _reader.GetData(i);
        }

        public string GetDataTypeName(int i)
        {
            return _reader.GetDataTypeName(i);
        }

        public DateTime GetDateTime(int i)
        {
            return _reader.GetDateTime(i);
        }

        public decimal GetDecimal(int i)
        {
            return _reader.GetDecimal(i);
        }

        public double GetDouble(int i)
        {
            return _reader.GetDouble(i);
        }

        public Type GetFieldType(int i)
        {
            return _reader.GetFieldType(i);
        }

        public float GetFloat(int i)
        {
            return _reader.GetFloat(i);
        }

        public Guid GetGuid(int i)
        {
            return _reader.GetGuid(i);
        }

        public short GetInt16(int i)
        {
            return _reader.GetInt16(i);
        }

        public int GetInt32(int i)
        {
            return _reader.GetInt32(i);
        }

        public long GetInt64(int i)
        {
            return _reader.GetInt64(i);
        }

        public string GetName(int i)
        {
            return _reader.GetName(i);
        }

        public int GetOrdinal(string name)
        {
            return _reader.GetOrdinal(name);
        }

        public DataTable GetSchemaTable()
        {
            return _reader.GetSchemaTable();
        }

        public string GetString(int i)
        {
            return _reader.GetString(i);
        }

        public object GetValue(int i)
        {
            return _reader.GetValue(i);
        }

        public int GetValues(object[] values)
        {
            return _reader.GetValues(values);
        }

        public bool IsDBNull(int i)
        {
            return _reader.IsDBNull(i);
        }

        public async Task<bool> IsDBNullAsync(int i)
        {
            return await _reader.IsDBNullAsync(i);
        }

        public async Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken)
        {
            return await _reader.IsDBNullAsync(i, cancellationToken);
        }

        public bool NextResult()
        {
            return _reader.NextResult();
        }

        public async Task<bool> NextResultAsync()
        {
            return await _reader.NextResultAsync();
        }

        public async Task<bool> NextResultAsync(CancellationToken cancellationToken)
        {
            return await _reader.NextResultAsync(cancellationToken);
        }

        public bool Read()
        {
            return _reader.Read();
        }

        public async Task<bool> ReadAsync()
        {
            return await _reader.ReadAsync();
        }

        public async Task<bool> ReadAsync(CancellationToken cancellationToken)
        {
            return await _reader.ReadAsync(cancellationToken);
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
