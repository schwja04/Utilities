using System;
using System.Data;

namespace Utilities.Common.Data.Extensions
{
    public static class DataReaderExtensions
    {
        public static T To<T>(this IDataReader reader, string columnName)
        {
            if (!ColumnExists(reader, columnName)) return GenericExtensions.GetDefaultValue<T>();

            object value = reader[columnName];

            if (value is null || ReferenceEquals(value, DBNull.Value)) return GenericExtensions.GetDefaultValue<T>();

            return (T)System.Convert.ChangeType(value, typeof(T));
        }

        public static T To<T>(this IDataReader reader, string columnName, T defaultValue)
        {
            if (!ColumnExists(reader, columnName)) return defaultValue;

            object value = reader[columnName];

            if (value is null || ReferenceEquals(value, DBNull.Value)) return defaultValue;

            return (T)System.Convert.ChangeType(value, typeof(T));
        }

        public static T? ToNullable<T>(this IDataReader reader, string columnName) where T : struct
        {
            return ToNullable<T>(reader, columnName, null);
        }

        public static T? ToNullable<T>(this IDataReader reader, string columnName, T? defaultValue)
            where T : struct
        {
            if (!ColumnExists(reader, columnName)) return defaultValue;

            object value = reader[columnName];

            if (value is null || ReferenceEquals(value, DBNull.Value)) return defaultValue;

            return (T)System.Convert.ChangeType(value, typeof(T));
        }

        public static bool ColumnExists(this IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; ++i)
            {
                if (string.Equals(reader.GetName(i), columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
