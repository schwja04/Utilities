using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using Utilities.Common.Data.Abstractions;
using Utilities.Common.Testing.Sql.DTO;

namespace Utilities.Common.Testing.Sql
{
    public static class TestDataHelper
    {
        private static Dictionary<Type, PropertyDescriptorCollection> _cachedProperties = new();

        public static IDataReader ToDataReader(params IEnumerable[] tables)
        {
            return ToDataSet(tables).CreateDataReader();
        }

        public static IDataReaderAsync ToDataReaderAsync(params IEnumerable[] tables)
        {
            return new TestDataReaderAsync(ToDataReader(tables));
        }

        public static DataSet ToDataSet(params IEnumerable[] tables)
        {
            DataSet dataSet = new();

            foreach (var table in tables)
            {
                dataSet.Tables.Add(ToDataTable(table));
            }

            return dataSet;
        }

        public static DataTable ToDataTable(IEnumerable rows)
        {
            DataTable table = new();

            if (rows.Any())
            {
                Type type = GetSingleType(rows);
                PropertyDescriptorCollection props = GetProperties(type);

                foreach (PropertyDescriptor prop in props)
                {
                    if (prop.PropertyType.IsGenericType)
                    {
                        table.Columns.Add(
                            columnName: prop.Name,
                            type: prop.PropertyType.GetGenericArguments()
                                .FirstOrDefault());
                    }
                    else
                    {
                        table.Columns.Add(prop.Name, prop.PropertyType);
                    }
                }

                List<object> values = new(capacity: props.Count);
                foreach (object item in rows)
                {
                    foreach (PropertyDescriptor prop in props)
                    {
                        values.Add(prop.GetValue(item));
                    }

                    table.Rows.Add(values.ToArray());
                    values.Clear();
                }
            }

            return table;
        }

        private static bool Any(this IEnumerable list)
        {
            if (list is null) return false;

            bool response = false;

            if (list is ICollection collection)
            {
                return collection.Count > 0;
            }

            IEnumerator e = list.GetEnumerator();
            response = e.MoveNext();
            (e as IDisposable)?.Dispose();
            return response;
        }

        private static Type GetSingleType(IEnumerable rows)
        {
            Type type = null;

            foreach(object row in rows)
            {
                if (type is null)
                {
                    type = row.GetType();
                }
                else if (type != row.GetType())
                {
                    throw new InvalidOperationException("All rows within a table must be the same .NET Type.");
                }
            }

            return type;
        }

        private static PropertyDescriptorCollection GetProperties(Type dataType)
        {
            PropertyDescriptorCollection props = null;
            if (!_cachedProperties.TryGetValue(dataType, out props))
            {
                lock (_cachedProperties)
                {
                    if (!_cachedProperties.TryGetValue(dataType, out props))
                    {
                        props = TypeDescriptor.GetProperties(dataType);
                        _cachedProperties.Add(dataType, props);
                    }
                }
            }

            return props;
        }
    }
}
